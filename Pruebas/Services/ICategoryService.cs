using Pruebas.Dto;
using Pruebas.Models.Dto;

namespace Pruebas.Services
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDto> Get();
        public CategoryDto? Get(int id);

        CategoryDto Create(CategoryDto category);
        void Update(CategoryDto category);
        void Delete(int id);
    }
}
