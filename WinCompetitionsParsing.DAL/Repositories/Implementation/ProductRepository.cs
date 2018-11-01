using System.Collections.Generic;
using System.Linq;
using MakeUpСompetition.DAL.Domain;
using MakeUpСompetition.DAL.Repositories.Abstract;

namespace MakeUpСompetition.DAL.Repositories.Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly MakeUpContext Context;
        public ProductRepository()
        {
            Context = MakeUpContext.GetInstance();
        }

        public IEnumerable<Product> GetAll()
        {
            return Context.Products.ToList();
        }
    }
}
