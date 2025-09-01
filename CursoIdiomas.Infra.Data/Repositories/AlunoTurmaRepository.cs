using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Infra.Data.Contexts;

namespace CursoIdiomas.Infra.Data.Repositories
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly ApplicationContext _context;

        public AlunoTurmaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public AlunoTurma Add(AlunoTurma alunoTurma)
        {
            if(VerificarAlunoJaMatriculado(alunoTurma.AlunoId, alunoTurma.TurmaId))
            {
                var qtdAlunosInTurma = GetQuantidadeAlunosMatriculados(alunoTurma.TurmaId);
                var qtd = 5;
                if(qtdAlunosInTurma >= qtd)
                    throw new ApplicationException($"Turma lotada, não é possivel matricular mais de {qtd} alunos na mesma turma.");

                _context.Set<AlunoTurma>().Add(alunoTurma);
                _context.SaveChanges();
                return alunoTurma;
            }
            else
            {
                throw new ApplicationException("Essa matricula ja foi cancelada anteriormente, não será possivel matricular-se nessa turma, acesse a funcionalidade de reativação de matriculas.");
            }
        }

        public bool VerificarAlunoJaMatriculado(Guid alunoId, Guid turmaId)
        {
            var alunoTurma = _context.Set<AlunoTurma>().FirstOrDefault(at => at.AlunoId == alunoId && at.TurmaId == turmaId);

            if(alunoTurma is not null)
                return false;
            else
                return true;
        }

        public int GetQuantidadeAlunosMatriculados(Guid turmaId)
        {
            return _context.Set<AlunoTurma>().Where(at => at.TurmaId == turmaId).Count();
        }

        public void CancelarTodasMatriculasAluno(Guid alunoId)
        {
            var listaAlunoTurma = _context.Set<AlunoTurma>().Where(at => at.AlunoId == alunoId).ToList();

            foreach(var alunoTurma in listaAlunoTurma)
            {
                alunoTurma.DataExclusao = DateTime.Now;
            }

            _context.SaveChanges();
        }

        public bool VerificarTurmaComAluno(Guid turmaId)
        {
            return _context.Set<AlunoTurma>().Any(at => at.TurmaId == turmaId && at.DataExclusao == null);
        }
    }
}