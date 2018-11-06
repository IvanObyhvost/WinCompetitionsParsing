using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WinCompetitionsParsing.DAL.Domain;
using WinCompetitionsParsing.DAL.Repositories.Abstract;

namespace WinCompetitionsParsing.DAL.Repositories.Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly MakeUpContext db;
        public ProductRepository()
        {
            db = new MakeUpContext();
            db.Products.FirstOrDefaultAsync();
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
        }
    }
}
