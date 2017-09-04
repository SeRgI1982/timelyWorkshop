using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Prism.Mvvm;
using timely;

namespace timelywithprism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            

            var bs = new TimelyBootstrapper();
            bs.Run();
        }
    }
}
