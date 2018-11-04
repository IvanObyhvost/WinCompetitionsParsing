using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCompetitionsParsing.DAL.Domain.Base;

namespace WinCompetitionsParsing.DAL.Domain
{
    [Serializable]
    public class Product
    {
        public int ProductCode { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsWorking { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
