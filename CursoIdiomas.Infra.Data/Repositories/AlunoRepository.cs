using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomas.Infra.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly ApplicationContext _context;

        public AlunoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Aluno aluno)
        {
            _context.Set<Aluno>().Add(aluno);
            _context.SaveChanges();
        }

        public IEnumerable<Aluno> GetAll(int pageNumber, int pageSize)
        {
            return _context.Set<Aluno>()
                .Where(al=> al.DataExclusao == null)
                .Include(a => a.AlunoTurmas.Where(at=> at.DataExclusao == null))
                .ThenInclude(at => at.Turma)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Aluno GetById(Guid id)
        {
            var aluno = _context.Set<Aluno>()
                    .Where(al=> al.DataExclusao == null)
                    .Include(a => a.AlunoTurmas.Where(at => at.DataExclusao == null)) // já filtra aqui
                    .ThenInclude(at => at.Turma)
                    .FirstOrDefault(a => a.Id == id);

            if (aluno is null)
                throw new ApplicationException("Aluno não encontrado.");

            return aluno;
        }
            

        public void Remove(Aluno aluno)
        {
           _context.Set<Aluno>().Update(aluno);
            _context.SaveChanges();
        }

        public void Update(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;

            foreach(var at in aluno.AlunoTurmas)
            {
                if(at.Id == Guid.Empty)
                {
                    _context.Entry(at).State = EntityState.Added;
                }
                else if (at.DataExclusao != null)
                {
                    _context.Entry(at).State = EntityState.Modified;
                }
            }

            _context.SaveChanges();
        }

        public Aluno? GetByCpf(string cpf)
        {
            return _context.Set<Aluno>()
                .FirstOrDefault(a => a.Cpf == cpf);
        }

        public bool ExistAlunoComMesmoEmail(string email)
        {
            var aluno = _context.Set<Aluno>()
                .Where(a => a.Email.ToLower() == email.ToLower())
                .FirstOrDefault();
            
            return aluno is not null;
        }

        public bool ExistAlunoComMesmoEmailEIdDiferente(string email, Guid id)
        {
            return _context.Set<Aluno>().Any(a=> a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && a.Id != id);
        }
    }
}
