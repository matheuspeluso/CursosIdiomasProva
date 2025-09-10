using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Helpers;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Domain.Interfacies.Services;

namespace CursoIdiomas.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public CriarUsuarioResponse CriarUsuario(CriarUsuarioRequest request)
        {
            if(_usuarioRepository.Any(request.Email))
                throw new ApplicationException("O email informado ja esta cadastrado.");

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = CryptoHelper.SHA256Encrypt(request.Senha)
            };

            _usuarioRepository.Add(usuario);

            return new CriarUsuarioResponse
            {
                Mensagem = "Usuario criado com sucesso.",
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email

            };
        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            var usuario = _usuarioRepository?.Get(request.Email, CryptoHelper.SHA256Encrypt(request.Senha));

            if(usuario is null)
                throw new ApplicationException("Usuário não encontrado. Acesso negado!");

            return new AutenticarUsuarioResponse
            {
                Mensagem = "Usuário autenticado com sucesso.",
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraAcesso = DateTime.Now,
                DataHoraExpiracao = DateTime.Now.AddMinutes(JwtBearerHelper.ExpirationInMinutes),
                AccessToken = JwtBearerHelper.CreateToken(usuario.Email)
            };
        }
    }
}
