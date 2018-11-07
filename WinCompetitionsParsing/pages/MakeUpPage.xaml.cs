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
using WinCompetitionsParsing.Utils;

namespace WinCompetitionsParsing.pages
{
    /// <summary>
    /// Interaction logic for MakeUpPage.xaml
    /// </summary>
    public partial class MakeUpPage : Page
    {
        private readonly IProductService _productService;
        private string url = "https://makeup.com.ua/news/25343/"; //"Enter url find news here...";
        List<Grid> listGridMenu = new List<Grid>();
        private FindQueryModel findQueryModel;
        private bool IsOpenSubMenu = false;

        public MakeUpPage(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            findQueryModel = FindQueryModel.GetInstance();

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
            tbSearch.Text = url;
            tbSearch.GotFocus += (sender, e) => { tbSearch.Text = string.Empty; };
            tbSearch.LostFocus += (sender, e) => {
                if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    tbSearch.Text = url;
            };
        }

        private void btCategory_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var text = button.Content.ToString();
            if (!IsOpenSubMenu)
                IsOpenSubMenu = true;
            else if (IsOpenSubMenu && findQueryModel.Category != text)
                IsOpenSubMenu = true;
            else
                IsOpenSubMenu = false;
            findQueryModel.Category = text;
            SetBreadCrumbs();
            HideAllGridAndShowOneGrid(button.Name);
        }

        private void HideAllGridAndShowOneGrid(string buttonName = "")
        {
            if (IsOpenSubMenu)
                grSubMenu.Visibility = Visibility.Visible;
            else
                grSubMenu.Visibility = Visibility.Hidden;

            listGridMenu.ForEach(g => g.Visibility = Visibility.Hidden);
            if(buttonName != string.Empty)
            {
                var nameGrid = buttonName.Replace("bt", "gr");
                listGridMenu.First(x => x.Name == nameGrid).Visibility = Visibility.Visible;
            }
            
        }

        private void btSubCategory_Click(object sender, MouseButtonEventArgs e)
        {
            Label button = sender as Label;
            var text = button.Content.ToString();
            findQueryModel.SubCategory = text;
            SetBreadCrumbs();
            IsOpenSubMenu = false;
            HideAllGridAndShowOneGrid();
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            findQueryModel.FindLink = tbSearch.Text;
        }

        private void lbSubCategory_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.Foreground = new SolidColorBrush(Color.FromRgb(121, 44, 155));
        }

        private void lbSubCategory_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        private void SetBreadCrumbs()
        {
            var category = findQueryModel.Category != null ? " > " + findQueryModel.Category: string.Empty;
            var subCategory = findQueryModel.SubCategory != null ? " > " + findQueryModel.SubCategory : string.Empty;
            lbBreadCrumbs.Content = string.Format("MAKEUP{0}{1}", category, subCategory);
        }
    }
}
