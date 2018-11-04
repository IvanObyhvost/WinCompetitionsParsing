using System;
using System.Collections.Generic;
using System.Text;

namespace WinCompetitionsParsing.BL.Models
{
    public class ProductModel
    {
        public int ProductCode { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsWorking { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
