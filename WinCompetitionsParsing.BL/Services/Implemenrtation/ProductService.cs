using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WinCompetitionsParsing.BL.Models;
using WinCompetitionsParsing.BL.Services.Abstract;
using WinCompetitionsParsing.DAL.Domain;
using WinCompetitionsParsing.DAL.Repositories.Abstract;

namespace WinCompetitionsParsing.BL.Services.Implemenrtation
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var products = _productRepository.GetAll();
            return _mapper.Map<List<ProductModel>>(products);
        }
        public ProductModel GetProduct(int productCode)
        {
            var product = _productRepository.GetProduct(productCode);
            return _mapper.Map<Product, ProductModel>(product);
        }
        public void AddProduct(ProductModel productModel)
        {
            var product = _mapper.Map<ProductModel, Product>(productModel);
            _productRepository.AddProduct(product);
        }

        public void UpdateProduct(ProductModel productModel)
        {
            var product = _mapper.Map<ProductModel, Product>(productModel);
            _productRepository.UpdateProduct(product);
        }

        public int GetLastProductCodeIsWorking()
        {
            var productCode = _productRepository
                                .GetAll()
                                .FirstOrDefault(x => x.IsDelete == false && x.IsWorking == true);
            return productCode != null ? productCode.ProductCode : 0;
        }

        public int GetTotalProducts()
        {
            return _productRepository.GetAll().Max(x => x.ProductCode);
        }

        public int GetWorkProducts()
        {
            return _productRepository.GetAll().Where(x => x.IsWorking).Max(x => x.ProductCode);
        }
    }
}
