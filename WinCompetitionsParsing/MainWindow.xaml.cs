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
using WinCompetitionsParsing.BL.Models;
using WinCompetitionsParsing.BL.Services.Abstract;
using WinCompetitionsParsing.pages;

namespace WinCompetitionsParsing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductService _productService;
        private QueryModel queryModel;

        public MainWindow(IProductService productService)
        {
            _productService = productService;
            queryModel = QueryModel.GetInstance();
            InitializeComponent();
            queryModel.LastWorkProductCode = _productService.GetLastProductCodeIsWorking();
            frPages.NavigationService.Navigate(new MakeUpPage(_productService));
            SiteBar.Margin = new Thickness(0, btMakeUp.Margin.Top, 0, 0);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btMakeUp_Click(object sender, RoutedEventArgs e)
        {
            frPages.NavigationService.Navigate(new MakeUpPage(_productService));
            SiteBar.Margin = new Thickness(0, btMakeUp.Margin.Top, 0, 0);
        }

        private void btLoadDB_Click(object sender, RoutedEventArgs e)
        {
            frPages.NavigationService.Navigate(new LoadDBPage(_productService));
            SiteBar.Margin = new Thickness(0, btLoadDB.Margin.Top, 0, 0);
        }
    }
}
