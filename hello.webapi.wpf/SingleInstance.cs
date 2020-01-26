//-----------------------------------------------------------------------
// <copyright file="SingleInstance.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
//     This class checks to make sure that only one instance of 
//     this application is running at a time.
// </summary>
//-----------------------------------------------------------------------
// Where does this come from? This is an adapted & cleaned up version of
// a battle-proven approach implemented by Microsoft and others for a
// single instance WPF app that can
//
// - be used with custom url protocol handlers (so integrating browser-workflows become easy)
// - activate the first instance w. marshalling the supplied command line arguments (uses deprecated remoting, though)
//
// References:
// - http://blogs.microsoft.co.il/blogs/arik/SingleInstance.cs.txt
// - https://github.com/microsoft/EyeDrive/blob/master/EyeDrive/SingleInstance.cs
// - https://github.com/joecastro/wpf-shell/blob/master/Microsoft.Windows.Shell/standard.net/Wpf/SingleInstance.cs
// - https://github.com/JohanLarsson/Gu.Wpf.SingleInstance/blob/master/Gu.Wpf.SingleInstance/SingleInstance.cs
// - https://stackoverflow.com/a/44774609/2592915
// - https://www.meziantou.net/single-instance-of-an-application-in-csharp.htm

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Threading;

// ReSharper disable StaticFieldInGenericType
// ReSharper disable UnusedMember.Global
namespace hello.webapi.wpf
{
    public interface ISingleInstanceApp
    {
        void SignalExternalCommandLineArgs(IList<string> args);
    }

    /// <summary>
    /// This class checks to make sure that only one instance of 
    /// this application is running at a time.
    /// </summary>
    /// <remarks>
    /// Note: this class should be used with some caution, because it does no
    /// security checking. For example, if one instance of an app that uses this class
    /// is running as Administrator, any other instance, even if it is not
    /// running as Administrator, can activate it with command line arguments.
    /// For most apps, this will not be much of an issue.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public static class SingleInstance
    {
        private const string Delimiter = ":";
        private const string ChannelNameSuffix = "SingeInstanceIPCChannel";
        private const string RemoteServiceName = "SingleInstanceApplicationService";
        private const string IpcProtocol = "ipc://";
        private static Mutex _singleInstanceMutex;
        private static IpcServerChannel _channel;
        private static ISingleInstanceApp _current;

        public static ISingleInstanceApp Current
        {
            get => _current;
            private set
            {
                if (value == null) return;
                if (_current != null) throw new InvalidOperationException("Single instance already initialized.");
                _current = value;
            }
        }

        #region Public Methods

        /// <summary>
        /// Checks if the instance of the application attempting to start is the first instance. 
        /// </summary>
        /// <returns>True if this is the first instance of the application.</returns>
        public static bool InitializeAsFirstInstance(string appName, ISingleInstanceApp app = null)
        {
            // Build unique application Id and the IPC channel name.
            var applicationIdentifier = appName + Environment.UserName;
            var channelName = ChannelNameFor(appName);

            // Create mutex based on unique application Id to check if this is the first instance of the application. 
            _singleInstanceMutex = new Mutex(true, applicationIdentifier, out var firstInstance);
            if (firstInstance)
                CreateRemoteService(channelName);

            Current = app;

            return firstInstance;
        }

        private static string ChannelNameFor(string appName)
        {
            return $"{appName}{Environment.UserName}{Delimiter}{ChannelNameSuffix}";
        }

        /// <summary>
        /// Cleans up single-instance code, clearing shared resources, mutexes, etc.
        /// </summary>
        public static void Cleanup()
        {
            if (_singleInstanceMutex != null)
            {
                _singleInstanceMutex.Close();
                _singleInstanceMutex = null;
            }

            if (_channel != null)
            {
                ChannelServices.UnregisterChannel(_channel);
                _channel = null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a remote service for communication.
        /// </summary>
        /// <param name="channelName">Application's IPC channel name.</param>
        private static void CreateRemoteService(string channelName)
        {
            var serverProvider = new BinaryServerFormatterSinkProvider {TypeFilterLevel = TypeFilterLevel.Full};
            IDictionary props = new Dictionary<string, string>();

            props["name"] = channelName;
            props["portName"] = channelName;
            props["exclusiveAddressUse"] = "false";

            // Create the IPC Server channel with the channel properties
            _channel = new IpcServerChannel(props, serverProvider);

            // Register the channel with the channel services
            ChannelServices.RegisterChannel(_channel, true);

            // Expose the remote service with the REMOTE_SERVICE_NAME
            var remoteService = new IpcRemoteService();
            RemotingServices.Marshal(remoteService, RemoteServiceName);
        }

        /// <summary>
        /// Creates a client channel and obtains a reference to the remoting service exposed by the server - 
        /// in this case, the remoting service exposed by the first instance. Calls a function of the remoting service 
        /// class to pass on command line arguments from the second instance to the first and cause it to activate itself.
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="args">
        /// Command line arguments for the second instance, passed to the first instance to take appropriate action.
        /// </param>
        /// <param name="timeOut">maximum Timeout</param>
        public static void SignalFirstInstance(string appName, IList<string> args,
            TimeSpan timeOut = default)
        {
            var secondInstanceChannel = new IpcClientChannel();
            ChannelServices.RegisterChannel(secondInstanceChannel, true);

            var channelName = ChannelNameFor(appName);
            var remotingServiceUrl = $"{IpcProtocol}{channelName}/{RemoteServiceName}";

            Trace.WriteLine($"Connecting to '{appName}' ...");
            // Obtain a reference to the remoting service exposed by the server i.e the first instance of the application
            var service = (IpcRemoteService)RemotingServices.Connect(typeof(IpcRemoteService), remotingServiceUrl);
            var sw = Stopwatch.StartNew();
            var delay = TimeSpan.FromSeconds(1);
            do
            {
                try
                {
                    service.InvokeFirstInstance(args);
                    Trace.WriteLine($"Connected to '{appName}' ({sw.Elapsed}).");
                    return;
                }
                catch (RemotingException)
                {
                    Trace.WriteLine($"Waiting '{sw.Elapsed}' ...");
                    Thread.Sleep(delay);
                }
            } while (sw.Elapsed < timeOut);
            sw.Stop();

            Trace.WriteLine($"Could not connect to '{appName}' ({timeOut}).");
        }

        /// <summary>
        /// Activates the first instance of the application with arguments from a second instance.
        /// </summary>
        /// <param name="args">List of arguments to supply the first instance of the application.</param>
        private static void ActivateFirstInstance(IList<string> args)
        {
            Current?.SignalExternalCommandLineArgs(args);
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// Remoting service class which is exposed by the server i.e the first instance and called by the second instance
        /// to pass on the command line arguments to the first instance and cause it to activate itself.
        /// </summary>
        private class IpcRemoteService : MarshalByRefObject
        {
            /// <summary>
            /// Activates the first instance of the application.
            /// </summary>
            /// <param name="args">List of arguments to pass to the first instance.</param>
            public void InvokeFirstInstance(IList<string> args)
            {
                ActivateFirstInstance(args);
            }

            /// <summary>
            /// Remoting Object's ease expires after every 5 minutes by default. We need to override the InitializeLifetimeService class
            /// to ensure that lease never expires.
            /// </summary>
            /// <returns>Always null.</returns>
            public override object InitializeLifetimeService()
            {
                return null;
            }
        }

        #endregion
    }
}
