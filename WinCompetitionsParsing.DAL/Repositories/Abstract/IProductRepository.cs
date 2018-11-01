using System.Collections.Generic;
using MakeUpСompetition.DAL.Domain;

namespace MakeUpСompetition.DAL.Repositories.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
    }
}
