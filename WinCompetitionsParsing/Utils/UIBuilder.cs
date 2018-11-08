using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinCompetitionsParsing.BL.Models;

namespace WinCompetitionsParsing.Utils
{
    public class UIBuilder
    {
        private int HeightСell = 40; //40 размер ячейки
        public Grid BuildSubMenu(CategoryModel categoryModel)
        {
            var drowGrid = categoryModel.DrowGrid;
            Grid grid = new Grid();
            grid.Name = drowGrid.GridName;
            grid.Width = 810;
            grid.Height = HeightСell * drowGrid.MaxRow;
            //grid.ShowGridLines = true;
            grid.Visibility = Visibility.Hidden;

            //создать колонки
            for (int x = 0; x < drowGrid.MaxCol; x++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            //создать строчки
            for (int y = 0; y < drowGrid.MaxRow; y++)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(HeightСell);
                grid.RowDefinitions.Add(row);
            }

            foreach(var subCategory in categoryModel.Subcategories)
            {
                Label label = new Label();
                label.Content = subCategory.Name;
                label.FontSize = 12;
                label.FontFamily = new System.Windows.Media.FontFamily("Century Gothic");
                label.Cursor = Cursors.Hand;
                label.Height = 25;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Center;
                if (subCategory.Col == 0)
                    label.Margin = new Thickness(40, 0, 0, 0);

                Grid.SetRow(label, subCategory.Row);
                Grid.SetColumn(label, subCategory.Col);
                grid.Children.Add(label);
            }



            return grid;
        }
    }
}
