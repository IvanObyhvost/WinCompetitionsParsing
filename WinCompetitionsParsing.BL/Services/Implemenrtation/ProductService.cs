using System;
using System.Collections.Generic;
using System.Text;
using MakeUpСompetition.BLL.Models;
using MakeUpСompetition.BLL.Services.Abstract;
using MakeUpСompetition.DAL.Domain;
using MakeUpСompetition.DAL.Repositories.Abstract;

namespace MakeUpСompetition.BLL.Services.Implemenrtation
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var products = _productRepository.GetAll();
            return mapper(products);
        }

        private IEnumerable<ProductModel> mapper(IEnumerable<Product> products)
        {
            var productModels = new List<ProductModel>();
            foreach (var product in products)
            {
                productModels.Add(new ProductModel()
                {
                    Name = product.Name,
                    Uri = product.Uri,
                    IsWorking = product.IsWorking
                });
            }
            return productModels;
            
        }
    }
}
