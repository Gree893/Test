using Pruebas.Dto;
using Pruebas.Modelos;

using System.Collections.Generic;
using System.Linq;

namespace Pruebas.Services
{
    public class AuthorService : IAuthorService
    {
        public static List<AuthorDto> _authors = new List<AuthorDto>
        {
            new AuthorDto
            {
                Id = 1,
                Name = "Autor 1",
                FirstName = "Nombre1",
                LastName = "Apellido1"
            },
            new AuthorDto
            {
                Id = 2,
                Name = "Autor 2",
                FirstName = "Nombre2",
                LastName = "Apellido2"
            },
            new AuthorDto
            {
                Id = 3,
                Name = "Autor 3",
                FirstName = "Nombre3",
                LastName = "Apellido3"
            }
        };
        public IEnumerable<AuthorDto> Get() => _authors;

        public AuthorDto? Get(int id) => _authors.FirstOrDefault(x => x.Id == id);

        public AuthorDto Create(AuthorDto author)
        {
            // Genera un nuevo ID. Debes asegurarte de que los IDs sean únicos.
            int newId = _authors.Max(a => a.Id) + 1;
            author.Id = newId;
            _authors.Add(author);
            return author;
        }

        public void Update(AuthorDto author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.FirstName = author.FirstName;
                existingAuthor.LastName = author.LastName;
            }
        }

        public void Delete(int id)
        {
            var authorToDelete = _authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete != null)
            {
                _authors.Remove(authorToDelete);
            }
        }
    }
}
