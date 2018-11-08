using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCompetitionsParsing.DAL.Domain;
using WinCompetitionsParsing.DAL.Repositories.Abstract;

namespace WinCompetitionsParsing.DAL.Repositories.Implementation
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly MakeUpContext db;
        public SubcategoryRepository()
        {
            db = new MakeUpContext();
        }
        public IEnumerable<Subcategory> GetAllSubcategories()
        {
            return db.Subcategories;
        }

        public IEnumerable<Subcategory> GetSubcategories(string nameCategory)
        {
            return db.Subcategories.Where(x => x.Category == nameCategory).ToList();
        }
    }
}
