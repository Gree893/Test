using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Dto;
using Pruebas.Services;

namespace Pruebas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;


        public BookController(IBookService Service)
        {
            _bookService = Service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public ActionResult<IEnumerable<BookDto>> Get()
        {
            var Books = BookService._bookList;
            if (Books == null)
            {
                return NotFound(); // Si no se encuentran autores, devuelve un resultado NotFound.
            }

            return Ok(Books);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int Id)
        {
            if (Id <= 0)
            {

                return BadRequest();
            }
            var Book = BookService._bookList.FirstOrDefault(a => a.Id == Id);
            if (Book == null)
            {
                return NotFound();
            }
            return Ok(Book);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BookDto> Create([FromBody] BookDto newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newBook == null)
            {
                return BadRequest(newBook);
            }

            newBook.Id = GenerateNewBookId();
            BookService._bookList.Add(newBook);
            return CreatedAtRoute("GetBook", new { id = newBook.Id }, newBook);
        }
        private int GenerateNewBookId()
        {
            if (BookService._bookList.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxId = BookService._bookList.Max(a => a.Id);
                return maxId + 1;
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] BookDto updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("El autor proporcionado es nulo.");
            }
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }
            var existingBook = BookService._bookList.FirstOrDefault(a => a.Id == id);

            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = updatedBook.Title;
            existingBook.Description = updatedBook.Description;
            existingBook.Pages = updatedBook.Pages;
            existingBook.Author = updatedBook.Author;
            existingBook.Category = updatedBook.Category;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var BookToDelete = BookService._bookList.FirstOrDefault(a => a.Id == id);
            if (BookToDelete == null)
            {
                return NotFound();
            }
            BookService._bookList.Remove(BookToDelete);
            return NoContent();
        }
    }
}
