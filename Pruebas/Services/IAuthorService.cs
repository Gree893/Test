using Pruebas.Dto;
using Pruebas.Modelos;

namespace Pruebas.Services
{
    public interface IAuthorService
    {
        public IEnumerable<AuthorDto> Get();
        public AuthorDto? Get(int id);

        AuthorDto Create(AuthorDto author);
        void Update(AuthorDto author);
        void Delete(int id);
    }
}
