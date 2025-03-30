using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced.ApplicationLayer.DTOs;

namespace Advanced.ApplicationLayer.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO GetCategoryById(int id);
        void AddCategory(CategoryDTO categoryDTO);
        void UpdateCategory(CategoryDTO categoryDto);
        void DeleteCategory(int id);
    }
}
