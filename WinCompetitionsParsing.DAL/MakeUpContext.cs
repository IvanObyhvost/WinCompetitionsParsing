using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WinCompetitionsParsing.DAL.Domain;

namespace WinCompetitionsParsing.DAL
{
    public class MakeUpContext
    {
        private static readonly MakeUpContext instance = new MakeUpContext();
        public IList<Product> Products { get; set; }

        private MakeUpContext()
        {
            Products = new List<Product>();
            //LoadFile();
        }

        public static MakeUpContext GetInstance()
        {
            return instance;
        }


        public void SaveChanges()
        {
            Save();
        }

        private void LoadFile()
        {
            string currentPath = Directory.GetCurrentDirectory();
            var path = new DirectoryInfo(currentPath).Parent.Parent.FullName + @"\MakeUpDb.txt";
            if (!File.Exists(path)) return;

            using (Stream stream = File.Open(path, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var l = binaryFormatter.Deserialize(stream);
                Products = (IList<Product>)l;
            }
        }

        private void Save()
        {
            string currentPath = Directory.GetCurrentDirectory();
            var path = new DirectoryInfo(currentPath).Parent.Parent.FullName + @"\MakeUpDb.txt";

            var append = false;
            if (File.Exists(path))
                append = true;

            using (Stream stream = File.Open(path, append ? FileMode.Open : FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, Products);
            }
           
        }

    }

}
