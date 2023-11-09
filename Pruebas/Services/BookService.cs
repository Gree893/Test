using Pruebas.Dto;
using Pruebas.Modelos;

using System.Collections.Generic;
using System.Linq;

namespace Pruebas.Services
{
    public class BookService : IBookService
    {
        public static List<BookDto> _bookList = new List<BookDto>
        {
            new BookDto
            {
                Id = 1,
                Title = "Libro 1",
                Description = "Descripción del Libro 1",
                Pages = 200,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 1),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 1)
            },
            new BookDto
            {
               Id = 2,
                Title = "Libro 2",
                Description = "Descripción del Libro 2",
                Pages = 150,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 2),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 2)
           },
            new BookDto
            {
                Id = 3,
                Title = "Libro 3",
                Description = "Descripción del Libro 3",
                Pages = 300,
                Author = AuthorService._authors.FirstOrDefault(author => author.Id == 3),
                Category = CategoryService._categoryList.FirstOrDefault(category => category.Id == 3)
            }
        };
        public IEnumerable<BookDto> Get() => _bookList;

        public BookDto? Get(int id) => _bookList.FirstOrDefault(x => x.Id == id);

        public BookDto Create(BookDto book)
        {
            // Genera un nuevo ID. Debes asegurarte de que los IDs sean únicos.
            int newId = _bookList.Max(a => a.Id) + 1;
            book.Id = newId;
            _bookList.Add(book);
            return book;
        }

        public void Update(BookDto book)
        {
            var existingbook = _bookList.FirstOrDefault(a => a.Id == book.Id);
            if (existingbook != null)
            {
                existingbook.Title = book.Title;
                existingbook.Description = book.Description;
                existingbook.Pages = book.Pages;
                existingbook.Author = book.Author;
                existingbook.Category = book.Category;
            }
        }
        public void Delete(int id)
        {
            var bookToDelete = _bookList.FirstOrDefault(a => a.Id == id);
            if (bookToDelete != null)
            {
                _bookList.Remove(bookToDelete);
            }
        }
    }
}