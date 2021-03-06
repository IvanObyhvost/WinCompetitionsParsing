﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MakeUpPage : Page
    {
        private readonly IProductService _productService;
        private readonly ISubcategoryService _subcategoryService;
        private string url = "https://makeup.com.ua/news/25343/"; //"Enter url find news here...";
        List<Grid> listGridMenu = new List<Grid>();
        private FindQueryModel findQueryModel;
        private BackgroundWorker worker;
        private bool IsOpenSubMenu = false;

        public MakeUpPage(IProductService productService, ISubcategoryService subcategoryService)
        {
            InitializeComponent();
            _productService = productService;
            _subcategoryService = subcategoryService;
            findQueryModel = FindQueryModel.GetInstance();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;

            InitGrids();
            Init();
        }

        private void InitGrids()
        {
            var uiBulder = new UIBuilder();
            var allButtons = grMenu.Children.OfType<Button>();
            foreach(var button in allButtons)
            {
                var content = button.Content.ToString();
                var categoryModel = new CategoryModel();
                categoryModel.Name = content;
                categoryModel.Subcategories = _subcategoryService.GetSubcategories(categoryModel.Name).ToList();
                var maxCol = categoryModel.Subcategories.Max(x => x.Col) +1;
                var maxRow = categoryModel.Subcategories.Max(x => x.Row) + 1;
                categoryModel.DrowGrid = new DrowGrid(button.Name.Replace("bt", "gr"), maxRow, maxCol);
                var grid = uiBulder.BuildSubMenu(categoryModel);

                listGridMenu.Add(grid);
                grSubMenu.Children.Add(grid);

                var allLabel = grid.Children.OfType<Label>();
                foreach (var label in allLabel)
                {
                    label.MouseEnter += lbSubCategory_MouseEnter;
                    label.MouseLeave += lbSubCategory_MouseLeave;
                    label.MouseLeftButtonUp += btSubCategory_Click;
                }

            }
        }

        private void Init()
        {
            grSubMenu.Visibility = Visibility.Hidden;
            grFindLink.Visibility = Visibility.Hidden;
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
            {
                IsOpenSubMenu = false;
                findQueryModel.SubCategory = null;
            }
                
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
                var selectGrid = listGridMenu.First(x => x.Name == nameGrid);
                selectGrid.Visibility = Visibility.Visible;
                grSubMenu.Height = selectGrid.Height;
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
            dgResult.Items.Clear();
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

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            CheckFindLink(sender, e);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) tbInfo.Text = "Worker cancelled";
            grFindLink.Visibility = Visibility.Hidden;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
        private void worker_ChangeTextOnTextBlock(TextBlock textBlock, string text)
        {
            textBlock.Dispatcher.Invoke((Action)(() => textBlock.Text = text));
        }
        private void CheckFindLink(object sender, DoWorkEventArgs e)
        {
            var parsingSite = new ParsingSite();
            string link = tbSearch.Text;
            var products = _productService.GetProductsByCategoryAndSubcategory(findQueryModel.Category, findQueryModel.SubCategory).ToList();
            for(int i = 0; i < products.Count(); i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                worker_ChangeTextOnTextBlock(tbInfo, string.Format("Find link {0} : {1}", i, products.Count()));
                ((BackgroundWorker)sender).ReportProgress(i);
                var IsFind = parsingSite.CheckFindLink(products[i].Uri, link);
                if (IsFind)
                {
                    dgResult.Items.Add(new ProductGridModel(products[i].Name, products[i].Uri));
                }
                    
            }
        }

        private void MakeUpPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy)
                worker.CancelAsync();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy)
                worker.CancelAsync();
            btCancel.IsEnabled = false;
        }
    }
}
