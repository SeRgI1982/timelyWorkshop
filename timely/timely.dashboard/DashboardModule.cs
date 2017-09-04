using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using timely.common.Infrastructure;
using timely.dashboard.Views;

namespace timely.dashboard
{
    [Module(ModuleName = ModuleNames.DashboardModule)]
    public class DashboardModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public DashboardModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            this.container.RegisterType<DashboardView>(new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(DashboardView));
        }
    }
}
