using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using timely.common.Infrastructure;
using timely.timesheets.Services;
using timely.timesheets.Views;

namespace timely.timesheets
{
    [Module(ModuleName = ModuleNames.TimesheetsModule)]
    public class TimesheetsModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public TimesheetsModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<TimesheetEntriesView>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<TimelyService>(new ContainerControlledLifetimeManager());
            this.container.RegisterInstance(DialogServiceProvider.Instance.DialogService, new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(TimesheetEntriesView));
        }
    }
}
