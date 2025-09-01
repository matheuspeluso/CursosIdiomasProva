using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Infra.Data.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly ApplicationContext _context;

        public TurmaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Turma turma)
        {
            _context.Set<Turma>().Add(turma);
            _context.SaveChanges();
        }

        public void Delete(Turma turma)
        {
            _context.Set<Turma>().Update(turma);
            _context.SaveChanges();
        }


        public IEnumerable<Turma> GetAll()
        {
            return _context.Set<Turma>()
                .Include(t => t.AlunoTurmas)
                .ThenInclude(at=> at.Aluno)
                .ToList();
        }

        public Turma GetById(Guid id)
        {
           var turma = _context.Set<Turma>()
                .Include(t => t.AlunoTurmas)
                .ThenInclude(at => at.Aluno)
                .FirstOrDefault(t => t.Id == id);

            if(turma is null)
                throw new ApplicationException("Turma não encontrada.");

            return turma;
        }

        public void Update(Turma turma)
        {
           _context.Set<Turma>().Update(turma);
           _context.SaveChanges();
        }
        public bool ExistTurmaMesmoNumero(string numero)
        {
            return _context.Set<Turma>()
                    .AsEnumerable() // pega no C# ao invés de SQL
                    .Any(t => t.Numero.Equals(numero, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistTurmaMesmoNumeroComIdDiferente(string numero, Guid id)
        {
            return _context.Set<Turma>()
                .AsEnumerable()
                .Any(t=> t.Numero.Equals(numero, StringComparison.OrdinalIgnoreCase)
                &&  t.Id != id);
        }
    }
}
