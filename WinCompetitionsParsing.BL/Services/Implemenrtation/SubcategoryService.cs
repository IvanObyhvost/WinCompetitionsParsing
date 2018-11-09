using System.Collections.Generic;
using AutoMapper;
using WinCompetitionsParsing.BL.Models;
using WinCompetitionsParsing.BL.Services.Abstract;
using WinCompetitionsParsing.DAL.Domain;
using WinCompetitionsParsing.DAL.Repositories.Abstract;

namespace WinCompetitionsParsing.BL.Services.Implemenrtation
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<SubcategoryModel> GetAllSubcategories()
        {
            var subcategory = _subcategoryRepository.GetAllSubcategories();
            return _mapper.Map<List<SubcategoryModel>>(subcategory);
        }

        public IEnumerable<SubcategoryModel> GetSubcategories(string nameCategory)
        {
            var subcategory = _subcategoryRepository.GetSubcategories(nameCategory);
            return _mapper.Map<List<SubcategoryModel>>(subcategory);
        }
    }
}
