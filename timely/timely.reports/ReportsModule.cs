using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using timely.common.Infrastructure;
using timely.reports.Views;

namespace timely.reports
{
    [Module(ModuleName = ModuleNames.ReportsModule)]
    public class ReportsModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public ReportsModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<ReportsView>(new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ReportsView));
        }
    }
}
