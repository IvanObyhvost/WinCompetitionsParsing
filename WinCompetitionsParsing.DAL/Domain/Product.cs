using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinCompetitionsParsing.DAL.Domain.Base;

namespace WinCompetitionsParsing.DAL.Domain
{
    public class Product : INotifyPropertyChanged
    {
        private int productCode;
        private string name;
        private string uri;
        private bool isWorking;
        private string category;
        private string subCategory;
        private bool isDelete;

        public int Id { get; set; }

        public int ProductCode
        {
            get { return productCode;  }
            set
            {
                productCode = value;
                OnPropertyChanged("ProductCode");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Uri
        {
            get { return uri; }
            set
            {
                uri = value;
                OnPropertyChanged("Uri");
            }
        }
        public bool IsWorking
        {
            get { return isWorking; }
            set
            {
                isWorking = value;
                OnPropertyChanged("IsWorking");
            }
        }
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }
        public string SubCategory
        {
            get { return subCategory; }
            set
            {
                subCategory = value;
                OnPropertyChanged("SubCategory");
            }
        }
        public bool IsDelete
        {
            get { return isDelete; }
            set
            {
                isDelete = value;
                OnPropertyChanged("IsDelete");
            }
        }
        //public string
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
