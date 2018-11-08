using System.Collections.Generic;

namespace WinCompetitionsParsing.BL.Models
{
    public class CategoryModel
    {
        
        public string Name { get; set; }
        public DrowGrid DrowGrid { get; set; }
        public List<SubcategoryModel> Subcategories { get; set; }

        public CategoryModel()
        {
            Subcategories = new List<SubcategoryModel>();
        }
    }

    public class DrowGrid
    {
        public string GridName { get; set; }
        public int MaxRow { get; set; }
        public int MaxCol { get; set; }
        public DrowGrid(string gridName, int maxRow, int maxCol)
        {
            GridName = gridName;
            MaxRow = maxRow;
            MaxCol = maxCol;
        }
    }
}
