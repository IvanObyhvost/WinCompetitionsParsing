using System.Collections.Generic;
using WinCompetitionsParsing.DAL.Domain;

namespace WinCompetitionsParsing.DAL.Repositories.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetProduct(int productCode);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}
