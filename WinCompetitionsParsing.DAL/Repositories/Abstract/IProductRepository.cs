using System.Collections.Generic;
using WinCompetitionsParsing.DAL.Domain;

namespace WinCompetitionsParsing.DAL.Repositories.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void AddProduct(Product product);
    }
}
