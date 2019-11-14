using Unity;
using Prism.Unity;
using System.Windows;
using SaleManager.Wpf.Views;

namespace SaleManager.Wpf
{
    [System.Obsolete]
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
