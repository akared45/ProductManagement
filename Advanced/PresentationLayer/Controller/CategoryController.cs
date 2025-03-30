using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.ApplicationLayer.DTOs;
using Advanced.ApplicationLayer.Interfaces;

namespace Advanced.PresentationLayer.Controller
{
    public class CategoryController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void DisplayAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Products: {string.Join(", ", category.ProductNames)}");
            }
        }
        public void AddCategory(string name)
        {
            var categoryDto = new CategoryDTO { Name = name };
            _categoryService.AddCategory(categoryDto);
            Console.WriteLine($"Added category: {name}");
        }

        public void UpdateCategory(int id, string name)
        {
            var categoryDto = new CategoryDTO { Id = id, Name = name };
            _categoryService.UpdateCategory(categoryDto);
            Console.WriteLine($"Updated category with ID: {id}");
        }

        public void DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
            Console.WriteLine($"Deleted category with ID: {id}");
        }
    }
}
