using System.Collections.Generic;
using System.Linq;
using WinCompetitionsParsing.DAL.Domain;
using WinCompetitionsParsing.DAL.Repositories.Abstract;

namespace WinCompetitionsParsing.DAL.Repositories.Implementation
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
