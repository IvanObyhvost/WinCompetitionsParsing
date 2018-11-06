using System;
using System.Collections.Generic;
using System.Text;
using WinCompetitionsParsing.BL.Models;

namespace WinCompetitionsParsing.BL.Services.Abstract
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();
        void AddProduct(ProductModel productModel);
    }
}
