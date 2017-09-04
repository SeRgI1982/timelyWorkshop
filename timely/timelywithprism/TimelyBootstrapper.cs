using System;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using timely.ViewModels;
using timely.Views;

namespace timely
{
    public class TimelyBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ////ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            ////{
            ////    var viewName = viewType.FullName;
            ////    var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            ////    var viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
            ////    return Type.GetType(viewModelName);
            ////});

            ////ViewModelLocationProvider.SetDefaultViewModelFactory((type) => Container.Resolve(type));
        }


        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<ShellView>();
            this.Container.RegisterType<ShellViewModel>();
            this.Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
