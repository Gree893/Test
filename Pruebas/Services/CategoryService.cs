using Pruebas.Dto;
using Pruebas.Modelos;
using Pruebas.Models.Dto;

namespace Pruebas.Services
{
    public class CategoryService: ICategoryService
    {
        public static List<CategoryDto> _categoryList = new List<CategoryDto>
        {
            new CategoryDto
            {
                Id = 1,
                Name = "Category 1",
                
            },
            new CategoryDto
            {
                Id = 2,
                Name = "Category 2",
               
            },
            new CategoryDto
            {
                Id = 3,
                Name = "Category 3",
               
            }
        };
        public IEnumerable<CategoryDto> Get() => _categoryList;

        public CategoryDto? Get(int id) => _categoryList.FirstOrDefault(x => x.Id == id);

        public CategoryDto Create(CategoryDto category)
        {
            // Genera un nuevo ID. Debes asegurarte de que los IDs sean únicos.
            int newId = _categoryList.Max(a => a.Id) + 1;
            category.Id = newId;
            _categoryList.Add(category);
            return category;
        }

        public void Update(CategoryDto category)
        {
            var existingCategory = _categoryList.FirstOrDefault(a => a.Id == category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
               
            }
        }

        public void Delete(int id)
        {
            var categoryToDelete = _categoryList.FirstOrDefault(a => a.Id == id);
            if (categoryToDelete != null)
            {
                _categoryList.Remove(categoryToDelete);
            }
        }
    }
}
