using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaleManager.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //IContainerExtension _container;
        //IRegionManager _regionManager;
        //IRegion _region;

        //CategoryView _category;
        //CustomerView _customer;

        //public MainWindow(IContainerExtension container, IRegionManager regionManager)
        //{
        //    InitializeComponent();
        //    _container = container;
        //    _regionManager = regionManager;

        //    //this.Loaded += MainWindow_Loaded;
        //}
        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _category = _container.Resolve<CategoryView>();
        //    _customer = _container.Resolve<CustomerView>();

        //    _region = _regionManager.Regions["ContentRegion"];

        //    _region.Add(_category);
        //    _region.Add(_customer);
        //}

        ////Click event
        ////_region.Activate(_viewA);
        ////_region.Deactivate(_viewA);
    }
}
