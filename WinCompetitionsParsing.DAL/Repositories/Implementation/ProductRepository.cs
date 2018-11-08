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
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product GetProduct(int productCode)
        {
            return db.Products.FirstOrDefault(x => x.ProductCode == productCode);
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var entity = db.Products.Find(product.Id);
            if (entity == null) return;
            db.Entry(entity).CurrentValues.SetValues(product);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
