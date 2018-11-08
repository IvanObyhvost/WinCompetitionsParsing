namespace WinCompetitionsParsing.BL.Models
{
    public class SubcategoryModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public SubcategoryModel() { }
        public SubcategoryModel(string name, string category, int row, int col)
        {
            Name = name;
            Category = category;
            Row = row;
            Col = col;
        }       
    }
   
}
