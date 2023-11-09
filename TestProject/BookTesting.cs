using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pruebas.Controllers;
using Pruebas.Dto;
using Pruebas;
using Pruebas.Modelos;
using Pruebas.Services;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Hosting;

namespace TestProject
{
    public class BookTesting 
    {
        private readonly BookController _controller;
        private readonly IBookService _service;
      
        public BookTesting()
        {
            _service = new BookService(); 
            _controller = new BookController(_service);
        }

        [Fact]
        public void Get_OK()
        {
            var result = _controller.Get();
            Assert.NotNull(result);

            Assert.IsAssignableFrom<ActionResult<IEnumerable<BookDto>>>(result);

            if (result is ActionResult<IEnumerable<BookDto>> actionResult)
            {
                var okResult = actionResult.Result as OkObjectResult;
                Assert.NotNull(okResult);

                // Luego, verifica el tipo del contenido.
                var content = okResult.Value;
                Assert.IsAssignableFrom<IEnumerable<BookDto>>(content);
            }
        }




        [Fact]
        public void Create_ReturnsCreatedResult()
        {

            var BookService = new BookService();
            var newBook = new BookDto
            {
                Id = 4,
                Title = "Libro 1",
                Description = "Descripción del Libro 1",
                Pages = 200,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 1),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 1)
            };


            var addedBook = BookService.Create(newBook);
            Assert.Contains(addedBook, BookService._bookList);



        }

        [Fact]
        public void Update_ReturnsNoContent()
        {
            var existingBook = new BookDto
            {
                Id = 4,
                Title = "Libro ",
                Description = "Descripción del libro",
                Pages = 200,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 1),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 1)
            };


            _service.Create(existingBook);

            // Modifica los datos del book existente aquí.
            var updatedBook = new BookDto
            {
                Id = 4,
                Title = "Libro update",
                Description = "Descripción del Libro update",
                Pages = 200,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 1),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 1)
            };

            var result = _controller.Update(existingBook.Id, updatedBook);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent()
        {
            var BookToDelete = new BookDto
            {
                Id = 4,
                Title = "Libro 1",
                Description = "Descripción del Libro 1",
                Pages = 200,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 1),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 1)
            };

            // Agrega el book que se va a eliminar a tu servicio antes de la prueba.
            _service.Create(BookToDelete);

            var result = _controller.Delete(BookToDelete.Id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
