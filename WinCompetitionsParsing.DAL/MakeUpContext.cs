using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WinCompetitionsParsing.DAL.Domain;

namespace WinCompetitionsParsing.DAL
{
    public class MakeUpContext: DbContext
    {
        public MakeUpContext() : base("DefaultConnection") { }
        public DbSet<Product> Products { get; set; }
    }

}
