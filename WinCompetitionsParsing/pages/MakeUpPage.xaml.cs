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
using WinCompetitionsParsing.BL.Services.Abstract;
using WinCompetitionsParsing.Utils;

namespace WinCompetitionsParsing.pages
{
    /// <summary>
    /// Interaction logic for MakeUpPage.xaml
    /// </summary>
    public partial class MakeUpPage : Page
    {
        private readonly IProductService _productService;

        List<Grid> listGridMenu = new List<Grid>();
        public MakeUpPage(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;

            Init();
        }

        private void Init()
        {
            var allGrid = grSubMenu.Children.OfType<Grid>();
            foreach (var grid in allGrid)
            {
                listGridMenu.Add(grid);
            }
            grSubMenu.Visibility = Visibility.Hidden;
            HideAllGridAndShowOneGrid();

            tbSearch.GotFocus += (sender, e) => { tbSearch.Text = string.Empty; };
            tbSearch.LostFocus += (sender, e) => {
                if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    tbSearch.Text = "Enter url find news here...";
            };
        }

        private void btMakeup_Click(object sender, RoutedEventArgs e)
        {
            HideAllGridAndShowOneGrid("grMakeup", true);
        }

        private void HideAllGridAndShowOneGrid(string nameGrid = "", bool showSubMenu = false)
        {
            if(showSubMenu)
                grSubMenu.Visibility = Visibility.Visible;

            listGridMenu.ForEach(g => g.Visibility = Visibility.Hidden);

            if (nameGrid != "")
                listGridMenu.First(x => x.Name == nameGrid).Visibility = Visibility.Visible;
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
