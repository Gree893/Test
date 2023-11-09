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
    public class AuthorTesting 
    {
        private readonly AuthorController _controller;
        private readonly IAuthorService _service;
      
        public AuthorTesting()
        {
            _service = new AuthorService(); // Asegúrate de que AuthorService esté configurado adecuadamente.
            _controller = new AuthorController(_service);
        }

        [Fact]
        public void Get_OK()
        {
            // Realiza alguna acción que debería devolver un ActionResult que contiene una colección de AuthorDto.
            var result = _controller.Get();

            // Verifica que el resultado no sea nulo.
            Assert.NotNull(result);

            // Verifica que el tipo del resultado sea un ActionResult que contenga una colección de AuthorDto.
            Assert.IsAssignableFrom<ActionResult<IEnumerable<AuthorDto>>>(result);

            // Luego, verifica que el resultado sea un OkObjectResult.
            if (result is ActionResult<IEnumerable<AuthorDto>> actionResult)
            {
                var okResult = actionResult.Result as OkObjectResult;
                Assert.NotNull(okResult);

                // Luego, verifica el tipo del contenido.
                var content = okResult.Value;
                Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(content);
            }
        }




        [Fact]
        public void Create_ReturnsCreatedResult()
        {

            var authorService = new AuthorService();
            // Crea un nuevo autor que deseas agregar.
            var newAuthor = new AuthorDto
            {
                Id = 4, // Asigna un ID único para el nuevo autor.
                Name = "Nuevo Autor",
                FirstName = "Nombre Nuevo",
                LastName = "Apellido Nuevo"
            };


            // Llama al método Create en AuthorService para agregar el autor.
            var addedAuthor = authorService.Create(newAuthor);

            // Verifica que el autor se haya agregado correctamente a _authors en AuthorService.
            Assert.Contains(addedAuthor, AuthorService._authors);



        }
        [Fact]
        public void Update_Returns()
        {
            var existingAuthor = new AuthorDto
            {
                Id = 3, // Asigna el mismo ID que deseas actualizar.
                Name = "Autor o",
                FirstName = "Nombre o",
                LastName = "Apellido O"
            };

            // Agrega el autor existente a tu servicio antes de la prueba.
            _service.Create(existingAuthor);

            // Modifica los datos del autor existente aquí.
            var updatedAuthor = new AuthorDto
            {
                Id = 3, // Asigna el mismo ID que deseas actualizar.
                Name = "Autor Update",
                FirstName = "Nombre update",
                LastName = "Apellido update"
            };

            var result = _controller.Update(existingAuthor.Id, updatedAuthor);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_Returns()
        {
            var authorToDelete = new AuthorDto
            {
                Id = 4, // Asigna el mismo ID que deseas actualizar.
                Name = "Autor delete",
                FirstName = "Nombre delete",
                LastName = "Apellido delete"
            };

            // Agrega el autor que se va a eliminar a tu servicio antes de la prueba.
            _service.Create(authorToDelete);

            var result = _controller.Delete(authorToDelete.Id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}

