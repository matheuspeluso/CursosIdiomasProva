using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public bool Any(string email)
        {
            return _context.Set<Usuario>().Any(u => u.Email.Equals(email));
        }

        public Usuario? Get(string email, string senha)
        {
            return _context.Set<Usuario>()
                .Where(u=> u.Email.Equals(email) 
                && u.Senha.Equals(senha)).FirstOrDefault();
        }
    }
}
