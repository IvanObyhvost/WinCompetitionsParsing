using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeUpСompetition.DAL.Domain.Base;

namespace MakeUpСompetition.DAL.Domain
{
    [Serializable]
    public class Product
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsWorking { get; set; }
    }
}
