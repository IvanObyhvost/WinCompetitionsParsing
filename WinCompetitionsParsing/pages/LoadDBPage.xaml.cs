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
    /// <summary>
    /// Interaction logic for LoadDBPage.xaml
    /// </summary>
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

            Init();
        }

        private void Init()
        {
            productModels = _productService.GetAll().ToList();
            queryModel.MainLink = tbLinkSite.Text;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            LoadAllProducts(sender);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Cancelled) lbMessageInformation.Text = "Worker cancelled";
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
            
            worker.RunWorkerAsync();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadAllProducts(object sender)
        {
            worker_ChangeTextOnTextBlock(tbInfo, "LoadAllProducts");
            for (int i = queryModel.StartProduct; i <= queryModel.EndProduct; i++)
            {
                ((BackgroundWorker)sender).ReportProgress(i);
                queryModel.SelectProduct = i;
                var html = parsingSite.GetHtml(queryModel.GetUriProduct());
                if (html != String.Empty)
                {
                    var product = parsingSite.GetDefaultInformationAboutProduct(html);
                    product.ProductCode = i;
                    product.Uri = queryModel.GetUriProduct();
                    productModels.Add(product);
                }
            }
            
        }
    }
}
