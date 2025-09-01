using CursoIdiomas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Interfacies.Repositories
{
    public interface IAlunoRepository
    {
        void Add(Aluno aluno);
        void Update(Aluno aluno);
        void Remove(Aluno aluno);
        Aluno GetById(Guid id);
        IEnumerable<Aluno> GetAll();
        Aluno? GetByCpf(string cpf);

    }
}
