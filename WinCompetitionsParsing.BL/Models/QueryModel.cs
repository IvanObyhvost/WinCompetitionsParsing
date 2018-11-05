using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCompetitionsParsing.BL.Models
{
    public class QueryModel
    {
        private static readonly QueryModel instance = new QueryModel();
        public string MainLink { get; set; }
        public string FindLink { get; set; }
        public int StartProduct { get; set; }
        public int EndProduct { get; set; }
        public int SelectProduct { get; set; }
        public static QueryModel GetInstance()
        {
            return instance;
        }
        public string GetUriProduct()
        {
            return String.Format("{0}{1}/", MainLink, SelectProduct);
        }
    }
}
