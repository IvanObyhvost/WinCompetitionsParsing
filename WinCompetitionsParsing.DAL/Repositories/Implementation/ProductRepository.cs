using System;
using System.Collections.Generic;
using System.Linq;
using WinCompetitionsParsing.DAL.Domain;
using WinCompetitionsParsing.DAL.Repositories.Abstract;

namespace WinCompetitionsParsing.DAL.Repositories.Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly MakeUpContext db;
        public ProductRepository(MakeUpContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
        }
    }
}
