using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Domain.Interfacies.Services;

namespace CursoIdiomas.Domain.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public TurmaService(ITurmaRepository turmaRepository, IAlunoTurmaRepository alunoTurmaRepository)
        {
            _turmaRepository = turmaRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
        }

        public TurmaResponse CadastrarTurma(TurmaRequest request)
        {
            var existTurmaComMesmoNumero = _turmaRepository.ExistTurmaMesmoNumero(request.Numero);

            if(existTurmaComMesmoNumero)
                throw new ApplicationException("Não é possivel cadastrar duas turmas com o mesmo número.");

            var turma = new Turma
            {
                Numero = request.Numero,
                AnoLetivo = request.AnoLetivo
            };

            _turmaRepository.Add(turma);

            return new TurmaResponse
            {
                Id = turma.Id,
                Numero = turma.Numero,
                AnoLetivo = turma.AnoLetivo
            };
        }

        public TurmaResponse AtualizarTurma(Guid id, TurmaRequest request)
        {
            var turma = _turmaRepository.GetById(id);
            if(turma is null)
                throw new ApplicationException("Turma não encontrada.");

            var ExistTurmaMesmoNumeroComIdDiferente = _turmaRepository.ExistTurmaMesmoNumeroComIdDiferente(request.Numero, id);
            if (ExistTurmaMesmoNumeroComIdDiferente)
                throw new ApplicationException("Numero da turma informado já está cadastrado em outra turma.");

            turma.Numero = request.Numero ?? turma.Numero;
            turma.AnoLetivo = request.AnoLetivo ?? turma.AnoLetivo;

            _turmaRepository.Update(turma);

            return new TurmaResponse
            {
                Id = turma.Id,
                Numero = turma.Numero,
                AnoLetivo = turma.AnoLetivo

            };
          
        }

        public List<TurmaResponse> BuscarTodasTurmas(int pageNumber, int pageSize)
        {
            var turmas = _turmaRepository.GetAll(pageNumber, pageSize);
            return turmas.Select(turma => new TurmaResponse
            {
                Id = turma.Id,
                Numero = turma.Numero,
                AnoLetivo = turma.AnoLetivo
            }).ToList();
             
        }

        public TurmaResponse BuscarTurmaPorId(Guid id)
        {
            var turma = _turmaRepository.GetById(id);
            if (turma is null)
                throw new ApplicationException("Turma não encontrada.");

            return new TurmaResponse
            {
                Id = turma.Id,
                Numero = turma.Numero,
                AnoLetivo = turma.AnoLetivo,
                DataExclusao = turma.DataExclusao
            };
        }


        public TurmaResponse ExcluirTurma(Guid id)
        {
            var turma = _turmaRepository.GetById(id);

            if (turma is null)
                throw new ApplicationException("Turma não encontrada.");

            var hasAlunoInTurma = _alunoTurmaRepository.VerificarTurmaComAluno(turma.Id);

            if (hasAlunoInTurma)
                throw new ApplicationException("Não é possivel excluir uma turma que possui alunos.");

            turma.DataExclusao = DateTime.Now;
            _turmaRepository.Delete(turma);

            return new TurmaResponse
            {
                Id = turma.Id,
                Numero = turma.Numero,
                AnoLetivo = turma.AnoLetivo,
                DataExclusao = turma.DataExclusao
            };
        }
    }
}
