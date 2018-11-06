using System;
using System.Collections.Generic;
using System.Text;
using WinCompetitionsParsing.BL.Models;

namespace WinCompetitionsParsing.BL.Services.Abstract
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();
        ProductModel GetProduct(int productCode);
        int GetLastProductCodeIsWorking();
        void AddProduct(ProductModel productModel);
        void UpdateProduct(ProductModel productModel);
        
    }
}
