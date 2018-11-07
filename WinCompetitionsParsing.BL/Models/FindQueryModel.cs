using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCompetitionsParsing.BL.Models
{
    public class FindQueryModel
    {
        private static readonly FindQueryModel instance = new FindQueryModel();
        public string FindLink { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public static FindQueryModel GetInstance()
        {
            return instance;
        }
    }
}
