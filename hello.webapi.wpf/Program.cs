using System;

namespace hello.webapi.wpf
{
    public static class Program
    {
        private static readonly string Name = typeof(Program).FullName;


        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            if (SingleInstance.InitializeAsFirstInstance(Name, app))
            {
                app.InitializeComponent();
                app.Run();
            }
            else 
                SingleInstance.SignalFirstInstance(Name, args);
        }
    }
}