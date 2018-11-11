using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCompetitionsParsing.BL.Models
{
    public class ProductGridModel
    {
        public string Uri { get; set; }
        public string Name { get; set; }

        public ProductGridModel(string name, string uri)
        {
            Name = name;
            Uri = uri;
        }
    }
}
