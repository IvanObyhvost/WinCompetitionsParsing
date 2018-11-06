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
            return _mapper.Map<Product, ProductModel>(products);
        }
        public void AddProduct(ProductModel productModel)
        {
            throw new NotImplementedException();
        }
        
    }
}
