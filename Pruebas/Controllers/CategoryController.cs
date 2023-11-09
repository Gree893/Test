using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Dto;
using Pruebas.Models.Dto;
using Pruebas.Services;

namespace Pruebas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService Service)
        {
            _categoryService = Service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            var categorys = CategoryService._categoryList;
            if (categorys == null)
            {
                return NotFound(); // Si no se encuentran autores, devuelve un resultado NotFound.
            }

            return Ok(categorys);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetById(int Id)
        {
            if (Id <= 0)
            {

                return BadRequest();
            }
            var author = AuthorService._authors.FirstOrDefault(a => a.Id == Id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDto> Create([FromBody] CategoryDto newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newCategory == null)
            {
                return BadRequest(newCategory);
            }

            newCategory.Id = GenerateNewCategoryId();
            CategoryService._categoryList.Add(newCategory);
            return CreatedAtRoute("GetAuthor", new { id = newCategory.Id }, newCategory);
        }
        private int GenerateNewCategoryId()
        {
            if (CategoryService._categoryList.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxId = CategoryService._categoryList.Max(a => a.Id);
                return maxId + 1;
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest("La categoria proporcionado es nula.");
            }
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }
            var existingCategory = CategoryService._categoryList.FirstOrDefault(a => a.Id == id);

            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.Name = updatedCategory.Name;
            return NoContent();
        }

            [HttpDelete("{id:int}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public IActionResult Delete(int id)
            {
                var categoryToDelete = CategoryService._categoryList.FirstOrDefault(a => a.Id == id);
                if (categoryToDelete == null)
                {
                    return NotFound();
                }
                CategoryService._categoryList.Remove(categoryToDelete);
                return NoContent();
            }
       }
 }

