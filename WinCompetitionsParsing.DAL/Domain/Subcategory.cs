using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinCompetitionsParsing.DAL.Domain
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
    }
    //public class Subcategory: INotifyPropertyChanged
    //{
    //    private string name;
    //    private string category;
    //    private int row;
    //    private int col;

    //    public int Id { get; set; }

    //    public string Name
    //    {
    //        get { return name; }
    //        set
    //        {
    //            name = value;
    //            OnPropertyChanged("Name");
    //        }
    //    }

    //    public string Category
    //    {
    //        get { return category; }
    //        set
    //        {
    //            category = value;
    //            OnPropertyChanged("Category");
    //        }
    //    }

    //    public int Row
    //    {
    //        get { return row; }
    //        set
    //        {
    //            row = value;
    //            OnPropertyChanged("Row");
    //        }
    //    }

    //    public int Col
    //    {
    //        get { return col; }
    //        set
    //        {
    //            col = value;
    //            OnPropertyChanged("Col");
    //        }
    //    }

    //    //public string
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        var handler = PropertyChanged;
    //        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}
}
