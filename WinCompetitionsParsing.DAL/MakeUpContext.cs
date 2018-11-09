using System.Data.Entity;
using WinCompetitionsParsing.DAL.Domain;

namespace WinCompetitionsParsing.DAL
{
    public class MakeUpContext: DbContext
    {
        public MakeUpContext() : base("name=DBConnection") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MakeUpContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

}
