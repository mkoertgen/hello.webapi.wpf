using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace hello.webapi.wpf
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        public ISalesViewModel Sales { get; private set; }

        public ShellViewModel(ISalesViewModel salesViewModel)
        {
            Sales = salesViewModel;
            if (salesViewModel == null) throw new ArgumentNullException(nameof(salesViewModel));

            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            DisplayName = "Hello.WebAPI.WPF";
        }
    }
}