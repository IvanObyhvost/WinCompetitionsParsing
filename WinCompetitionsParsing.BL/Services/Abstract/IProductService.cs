using System;
using System.Collections.Generic;
using System.Text;
using MakeUpСompetition.BLL.Models;

namespace MakeUpСompetition.BLL.Services.Abstract
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();
    }
}
