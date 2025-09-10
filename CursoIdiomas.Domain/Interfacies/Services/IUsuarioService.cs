using CursoIdiomas.Domain.Dtos;

namespace CursoIdiomas.Domain.Interfacies.Services
{
    public interface IUsuarioService
    {
        CriarUsuarioResponse CriarUsuario(CriarUsuarioRequest request);
        AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request);
    }
}
