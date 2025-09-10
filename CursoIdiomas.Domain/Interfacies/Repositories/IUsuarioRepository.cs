using CursoIdiomas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Interfacies.Repositories
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        Usuario? Get(string email, string senha);
        bool Any(string email);
    }
}
