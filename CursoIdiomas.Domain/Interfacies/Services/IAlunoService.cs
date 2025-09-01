using CursoIdiomas.Domain.Dtos;

namespace CursoIdiomas.Domain.Interfacies.Services
{
    public interface IAlunoService
    {
        AlunoResponse CadastrarAluno(AlunoRequest request);
        AlunoResponse AtualizarAluno(Guid id, AlunoRequest request);
        AlunoResponse ExcluirAluno(Guid id);
        AlunoResponse BuscarAlunoPorId(Guid id);
        List<AlunoResponse> BuscarAlunos();
    }
}
