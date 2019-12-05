using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using SaleManager.Wpf.Admin.Views;
using SaleManager.Wpf.Inflastructor.ViewModels;
using SaleManager.Wpf.Inflastructor.Views;
using SaleManager.Wpf.ViewModels;
using SaleManager.Wpf.Views;
using System.Windows;

namespace SaleManager.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<SaleManager.Wpf.Admin.AdminModule>();
        }
    }
}
