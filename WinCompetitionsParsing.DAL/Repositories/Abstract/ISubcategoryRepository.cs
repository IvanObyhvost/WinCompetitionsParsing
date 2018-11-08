using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCompetitionsParsing.DAL.Domain;

namespace WinCompetitionsParsing.DAL.Repositories.Abstract
{
    public interface ISubcategoryRepository
    {
        IEnumerable<Subcategory> GetAllSubcategories();
        IEnumerable<Subcategory> GetSubcategories(string nameCategory);
    }
}
