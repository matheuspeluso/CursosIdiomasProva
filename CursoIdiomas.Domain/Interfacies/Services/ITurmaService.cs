using CursoIdiomas.Domain.Dtos;

namespace CursoIdiomas.Domain.Interfacies.Services
{
    public interface ITurmaService
    {
        TurmaResponse CadastrarTurma(TurmaRequest request);
        TurmaResponse AtualizarTurma(Guid id, TurmaRequest request);
        TurmaResponse ExcluirTurma(Guid id);
        TurmaResponse BuscarTurmaPorId(Guid id);
        List<TurmaResponse> BuscarTodasTurmas();
    }
}
