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

        public LoadDBPage(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            parsingSite = new ParsingSite();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Cancelled) lbMessageInformation.Text = "Worker cancelled";
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pbStatus.Value = e.ProgressPercentage;
        }
        //private void worker_ChangeTextOnLabel(Label label, string text)
        //{
        //    label.Invoke((MethodInvoker)(() => label.Text = text));
        //}
        

        private void btUpdateDB_Click(object sender, RoutedEventArgs e)
        {
            //если нашел продукт - добавить
            
            //если не нашел, то пропустить
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
