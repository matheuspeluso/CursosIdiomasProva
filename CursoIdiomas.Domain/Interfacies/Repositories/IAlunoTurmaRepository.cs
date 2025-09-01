using CursoIdiomas.Domain.Entities;
using System.Security.Cryptography;

namespace CursoIdiomas.Domain.Interfacies.Repositories
{
    public interface IAlunoTurmaRepository
    {
        AlunoTurma Add(AlunoTurma alunoTurma);
        bool VerificarAlunoJaMatriculado(Guid alunoId, Guid turmaId);
        int GetQuantidadeAlunosMatriculados(Guid turmaId);
        void CancelarTodasMatriculasAluno(Guid alunoId);
        bool VerificarTurmaComAluno(Guid turmaId);
    }
}
