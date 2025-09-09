using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Domain.Interfacies.Repositories;
using CursoIdiomas.Domain.Interfacies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;
        private readonly ITurmaRepository _turmaRepository;

        public AlunoService(IAlunoRepository alunoRepository, IAlunoTurmaRepository alunoTurmaRepository, ITurmaRepository turmaRepository)
        {
            _alunoRepository = alunoRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
            _turmaRepository = turmaRepository;
        }

        public AlunoResponse CadastrarAluno(AlunoRequest request)
        {
            if (request.TurmasIds is null || !request.TurmasIds.Any())
                throw new ApplicationException("Aluno deve pertencer a pelo menos uma turma.");

            foreach (var turmaId in request.TurmasIds)
            {
                var turma = _turmaRepository.GetById(turmaId);
                if (turma is null)
                    throw new ApplicationException("Não foi possivel encontrar a turma informada.");
            }

            var verificarCpfCadastrado = _alunoRepository.GetByCpf(request.Cpf);
            if (verificarCpfCadastrado is not null)
                throw new ApplicationException("Cpf já cadastrado.");

            if(_alunoRepository.ExistAlunoComMesmoEmail(request.Email))
                throw new ApplicationException("Email ja cadastrado.");
            
            var aluno = new Aluno()
            {
                Nome = request.Nome,
                Cpf = request.Cpf,
                Email = request.Email
            };

            aluno.AlunoTurmas = request.TurmasIds.Select(turmaId => new AlunoTurma
            {
                Id = Guid.NewGuid(),
                AlunoId = aluno.Id,
                TurmaId = turmaId,
                DataMatricula = DateTime.Now
            }).ToList();

            _alunoRepository.Add(aluno);


            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                DataCadastro = aluno.DataCadastro,
                AlunoTurmas = aluno.AlunoTurmas.Select(at => new AlunoTurmaResponse
                {
                    TurmaId = at.TurmaId,
                    DataMatricula = at.DataMatricula
                }).ToList()
            };


        }

        public AlunoResponse AtualizarAluno(Guid id, AlunoRequest request)
        {
            // Carrega aluno com turmas (entidades rastreadas)
            var aluno = _alunoRepository.GetById(id);

            if (aluno is null)
                throw new ApplicationException("Aluno não encontrado.");

            // Verifica duplicidade de CPF
            if (!string.Equals(aluno.Cpf, request.Cpf, StringComparison.OrdinalIgnoreCase))
            {
                var verificarCpfCadastrado = _alunoRepository.GetByCpf(request.Cpf);
                if (verificarCpfCadastrado is not null && verificarCpfCadastrado.Id != id)
                    throw new ApplicationException("CPF já cadastrado para outro aluno.");
            }

            // Atualiza dados básicos
            aluno.Nome = request.Nome ?? aluno.Nome;
            aluno.Cpf = request.Cpf ?? aluno.Cpf;
            aluno.Email = request.Email ?? aluno.Email;

            if (request.TurmasIds != null)
            {
                // 1️⃣ Marca como excluídas as turmas que foram removidas
                var turmasParaRemover = aluno.AlunoTurmas
                    .Where(at => !request.TurmasIds.Contains(at.TurmaId) && at.DataExclusao == null)
                    .ToList();

                foreach (var at in turmasParaRemover)
                {
                    at.DataExclusao = DateTime.Now;
                }

                // 2️⃣ Identifica turmas já ativas
                var turmasExistentes = aluno.AlunoTurmas
                    .Where(at => at.DataExclusao == null)
                    .Select(at => at.TurmaId)
                    .ToHashSet();

                // 3️⃣ Cria novas turmas que ainda não existem
                var novasTurmas = request.TurmasIds
                    .Where(tid => !turmasExistentes.Contains(tid))
                    .Select(turmaId => new AlunoTurma
                    {
                        Id = Guid.NewGuid(),
                        AlunoId = aluno.Id,
                        TurmaId = turmaId,
                        DataMatricula = DateTime.Now,
                        DataExclusao = null
                    })
                    .ToList();

                // 4️⃣ Adiciona novas turmas rastreadas pelo contexto
                foreach (var nova in novasTurmas)
                {
                    _alunoTurmaRepository.Add(nova);
                }
            }

            // Persiste alterações (o aluno e suas turmas já estão rastreados)
            _alunoRepository.Update(aluno);

            // Retorna response
            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                DataCadastro = aluno.DataCadastro,
                AlunoTurmas = aluno.AlunoTurmas
                    .Where(at => at.DataExclusao == null)
                    .Select(at => new AlunoTurmaResponse
                    {
                        TurmaId = at.TurmaId,
                        DataMatricula = at.DataMatricula
                    })
                    .ToList()
            };
        }




        public AlunoResponse BuscarAlunoPorId(Guid id)
        {
            var aluno = _alunoRepository.GetById(id);

            if (aluno is null)
                throw new ApplicationException("Aluno não encontrado.");

            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                DataCadastro = aluno.DataCadastro,
                AlunoTurmas = aluno.AlunoTurmas.Select(at => new AlunoTurmaResponse
                {
                    TurmaId = at.TurmaId,
                    DataMatricula = at.DataMatricula
                }).ToList()
            };
        }

        public List<AlunoResponse> BuscarAlunos(int pageNumber, int pageSize)
        {
            var alunos = _alunoRepository.GetAll(pageNumber, pageSize);
            return alunos.Select(aluno => new AlunoResponse()
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                DataCadastro = aluno.DataCadastro,

                // Fazendo o mapeamento da entidade para o DTO
                AlunoTurmas = aluno.AlunoTurmas.Select(at => new AlunoTurmaResponse()
                {
                    TurmaId = at.TurmaId,
                    DataMatricula = at.DataMatricula
                }).ToList()
            }).ToList();
        }


        public AlunoResponse ExcluirAluno(Guid id)
        {
            var aluno = _alunoRepository.GetById(id);

            if (aluno is null)
                throw new ApplicationException("Aluno não encontrado.");

            try
            {
                aluno.DataExclusao = DateTime.Now;
                _alunoRepository.Remove(aluno);

                _alunoTurmaRepository.CancelarTodasMatriculasAluno(aluno.Id);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao excluir aluno.");
            }




            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                DataCadastro = aluno.DataCadastro,
                AlunoTurmas = aluno.AlunoTurmas.Select(at => new AlunoTurmaResponse
                {
                    TurmaId = at.TurmaId,
                    DataMatricula = at.DataMatricula
                }).ToList()
            };

        }
    }
}
