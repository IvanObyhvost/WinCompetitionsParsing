using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class LoadDBPage : Page
    {
        private readonly IProductService _productService;
        private ParsingSite parsingSite;
        private BackgroundWorker worker;
        private QueryModel queryModel;
        private List<ProductModel> productModels;
        public LoadDBPage(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            parsingSite = new ParsingSite();
            worker = new BackgroundWorker();
            queryModel = QueryModel.GetInstance();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
            Init();
        }

        private void Init()
        {
            productModels = new List<ProductModel>();//_productService.GetAll().ToList();
            queryModel.MainLink = tbLinkSite.Text;
            btCancel.Visibility = Visibility.Hidden;
            tbStart.Text = queryModel.StartProduct != 0 ? queryModel.StartProduct.ToString() : tbStart.Text;
            tbFinish.Text = queryModel.EndProduct != 0 ? queryModel.EndProduct.ToString() : tbFinish.Text;
            tbTotalLoadedProducts.Text = _productService.GetTotalProducts().ToString();
            tbTotalWorkProducts.Text = _productService.GetWorkProducts().ToString();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            LoadAllProducts(sender, e);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) tbInfo.Text = "Worker cancelled";
            btCancel.IsEnabled = true;
            btCancel.Visibility = Visibility.Hidden;
            btUpdateDB.Visibility = Visibility.Visible;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
        private void worker_ChangeTextOnTextBlock(TextBlock textBlock, string text)
        {
            textBlock.Dispatcher.Invoke((Action)(() => textBlock.Text = text));
        }
        

        private void btUpdateDB_Click(object sender, RoutedEventArgs e)
        {
            queryModel.StartProduct = Convert.ToInt32(tbStart.Text);
            queryModel.EndProduct = Convert.ToInt32(tbFinish.Text);
            pbStatus.Value = 0;
            pbStatus.Maximum = Math.Abs(queryModel.EndProduct);
            btUpdateDB.Visibility = Visibility.Hidden;
            btCancel.Visibility = Visibility.Visible;
            worker.RunWorkerAsync();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadAllProducts(object sender, DoWorkEventArgs e)
        {
            worker_ChangeTextOnTextBlock(tbInfo, "Load all products");
            for (int i = queryModel.StartProduct; i <= queryModel.EndProduct; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                worker_ChangeTextOnTextBlock(tbInfo, 
                    string.Format("Load all products {0} : {1}", i, queryModel.EndProduct));
                ((BackgroundWorker)sender).ReportProgress(i);
                queryModel.SelectProduct = i;
                var productModel = _productService.GetProduct(i);
                if(productModel == null)
                {
                    productModel = new ProductModel(i);
                    productModel.Uri = queryModel.GetUriProduct();
                    var html = parsingSite.GetHtml(queryModel.GetUriProduct());
                    if (html != string.Empty)
                    {
                        parsingSite.GetDefaultInformationAboutProduct(html, productModel);
                    }
                    else
                    {
                        productModel.IsDelete = true;
                    }
                    _productService.AddProduct(productModel);
                }
                else
                {
                    //if (!productModel.IsDelete)
                    //{
                    //    var html = parsingSite.GetHtml(queryModel.GetUriProduct());
                    //    if (html == string.Empty)
                    //    {
                    //        productModel.IsDelete = true;
                    //        _productService.UpdateProduct(productModel);
                    //    }
                    //}
                }
                
            }
        }

        private void LoadDBPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if(worker.IsBusy)
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
