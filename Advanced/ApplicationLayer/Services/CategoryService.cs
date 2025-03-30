using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.ApplicationLayer.DTOs;
using Advanced.ApplicationLayer.Interfaces;
using Advanced.DomainLayer.Entities;
using Advanced.DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced.ApplicationLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly Advanced.InfrastructureLayer.Logging.FileLogger _fileLogger;
        private readonly Advanced.InfrastructureLayer.Data.ApplicationDbContext _applicationDbContext;
        public CategoryService(
            IRepository<Category> categoryRepository,
            Advanced.InfrastructureLayer.Logging.FileLogger fileLogger,
            Advanced.InfrastructureLayer.Data.ApplicationDbContext applicationDbContext)
        {
            _categoryRepository = categoryRepository;
            _fileLogger = fileLogger;
            _applicationDbContext = applicationDbContext;
        }

        public void AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                ProductCategories = new List<ProductCategory>()
            };
            _categoryRepository.Add(category);
            _applicationDbContext.SaveChanges();
        }
        public void UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = _categoryRepository.GetById(categoryDTO.Id);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                _categoryRepository.Update(category);
                _applicationDbContext.SaveChanges();
            }
        }
        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                _applicationDbContext.SaveChanges();
                _fileLogger.Log($"Category {category.Name} (ID: {id}) was deleted at {DateTime.Now}");
            }
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ProductNames = c.ProductCategories.Select(pc => pc.Product.Name).ToList()
            });
        }

        public CategoryDTO GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return null;
            }
            else
            {
                return new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    ProductNames = category.ProductCategories.Select(pc => pc.Product.Name).ToList()
                };
            }
        }
    }
}
