using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinCompetitionsParsing.BL.Models;

namespace WinCompetitionsParsing.BL.Services.Abstract
{
    public interface ISubcategoryService
    {
        IEnumerable<SubcategoryModel> GetAllSubcategories();
        IEnumerable<SubcategoryModel> GetSubcategories(string nameCategory);
    }
}
