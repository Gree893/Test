using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Dto;
using Pruebas.Modelos;
using Pruebas.Services;

namespace Pruebas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;


        public AuthorController(IAuthorService Service)
        {
            _authorService = Service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
        public ActionResult<IEnumerable<AuthorDto>> Get()
        {
            var authors = AuthorService._authors;
            if (authors == null)
            {
                return NotFound(); // Si no se encuentran autores, devuelve un resultado NotFound.
            }

            return Ok(authors);
        }

        [HttpGet("{id:int}", Name = "GetAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
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
        public ActionResult<AuthorDto> Create([FromBody] AuthorDto newAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newAuthor == null)
            {
                return BadRequest(newAuthor);
            }

            newAuthor.Id = GenerateNewAuthorId();
            AuthorService._authors.Add(newAuthor);
            return CreatedAtRoute("GetAuthor", new { id = newAuthor.Id }, newAuthor);
        }
        private int GenerateNewAuthorId()
        {
            if (AuthorService._authors.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxId = AuthorService._authors.Max(a => a.Id);
                return maxId + 1;
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] AuthorDto updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                return BadRequest("El autor proporcionado es nulo.");
            }
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }
            var existingAuthor = AuthorService._authors.FirstOrDefault(a => a.Id == id);

            if (existingAuthor == null)
            {
                return NotFound();
            }
            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.FirstName = updatedAuthor.FirstName;
            existingAuthor.LastName = updatedAuthor.LastName;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var authorToDelete = AuthorService._authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete == null)
            {
                return NotFound();
            }
            AuthorService._authors.Remove(authorToDelete);
            return NoContent();
        }
    }
}
