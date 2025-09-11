using Bogus;
using Bogus.Extensions.Brazil;
using CursoIdiomas.Domain.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;


namespace CursoIdiomas.Tests
{
    public class TurmaTests
    {
        [Fact]
        public void DeveCriarTurmaComSucesso()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();//criando o client http
                 

            #region Criar os dados do test

            var faker = new Faker("pt_BR");
            

            var request = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            //serializando os dados para JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json");

            #endregion

            #region Realizar a requisição de teste

            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;

            #endregion

            #region Realizar as assertions de teste

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            #endregion

        }

        [Fact]
        public void NaoDeveCriarTurmaComNUmeroRepetido()
        {
            var faker = new Faker("pt_BR");
            var numero = faker.Random.String2(3, 8, "0123456789");


            var request = new TurmaRequest
            {
                Numero = numero,
                AnoLetivo = "2025"
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var client = new WebApplicationFactory<Program>().CreateClient();//criando o client http
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //refazendo a mesma requisição
            response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            var content = response.Content.ReadAsStringAsync().Result;
            content.Should().Be("Não é possivel cadastrar duas turmas com o mesmo número.");

        }

        [Fact]
        public void NaoDeveCriarTurmaComNumeroEAnoLetivoInvalidos()
        {
            var request = new TurmaRequest
            {
                Numero = "1",
                AnoLetivo = "1"
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeveListarTodasTurmasComSucesso()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.GetAsync("/api/Turma/buscarTodasTurmas")?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void DeveBuscarTurmoPorIdComSucesso()
        {
            var faker = new Faker("pt_BR");


            var request = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;
            
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var dadosResposta = response.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);

            var response2 = client.GetAsync("/api/Turma/buscarTurmaPorId/" + turma.Id)?.Result;

            response2?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void NaoDeveBuscarTurmaPorIdInexistente()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.GetAsync("/api/Turma/buscarTurmaPorId/"+Guid.NewGuid())?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeveExcluirTurmaComSucesso()
        {
            var faker = new Faker("pt_BR");


            var request = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var dadosResposta = response.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            var response2 = client.DeleteAsync("/api/Turma/excluirTurma/" + idTurma)?.Result;
            response2?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var dadpsRespostaExclusao = response2.Content.ReadAsStringAsync().Result;
            var turmaExcluida = JsonConvert.DeserializeObject<TurmaResponse>(dadpsRespostaExclusao);

            turmaExcluida?.DataExclusao.Should().NotBeNull();
        }

        [Fact]
        public void NaoDeveExcluirTurmaQueTenhaAlunosMatriculados()
        {
            #region Criar requisição de turma

            var faker = new Faker("pt_BR");
            var request = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequest)?.Result;

            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //capturando o id da turma criada para usar na requisição de aluno
            var dadosResposta = response.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            #endregion

            #region Criar requisição de aluno

            var requestAluno = new AlunoRequest
            {
                Nome = faker.Person.FullName,
                Cpf = faker.Random.Replace("###########").ToString(),
                Email = faker.Person.Email,
                TurmasIds = new List<Guid> {idTurma},
            };

            var jsonRequestAluno = new StringContent(JsonConvert.SerializeObject(requestAluno), Encoding.UTF8, "application/json");

            var responseAluno = client.PostAsync("/api/Aluno/cadastrarAluno", jsonRequestAluno)?.Result;

            responseAluno?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            #endregion

            #region Realizar requisição de exclusão de turma
            
            var response2 = client.DeleteAsync("/api/Turma/excluirTurma/" + idTurma)?.Result;
            response2?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            #endregion

        }

        [Fact]
        public void DeveEditarTurmaComSucesso()
        {
            #region Cadastrar turma
            
            var faker = new Faker("pt_BR");
            var requestCriacao = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestCriacao = new StringContent(JsonConvert.SerializeObject(requestCriacao), Encoding.UTF8, "application/json");
            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequestCriacao)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //capturando o id da turma criada para usar na requisição de edição
            var dadosResposta = response.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            #endregion

            #region Editar turma

            var requestEdicao = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestEdicao = new StringContent(JsonConvert.SerializeObject(requestEdicao), Encoding.UTF8, "application/json");
            var responseEdicao = client.PutAsync("/api/Turma/atualizarTurma/" + idTurma, jsonRequestEdicao)?.Result;
            responseEdicao?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public void DeveEditarTurmaComMesmoNumero()
        {
            #region Cadastrar turma

            var faker = new Faker("pt_BR");
            var numero = faker.Random.String2(3, 8, "0123456789");
            var requestCriacao = new TurmaRequest
            {
                Numero = numero,
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestCriacao = new StringContent(JsonConvert.SerializeObject(requestCriacao), Encoding.UTF8, "application/json");
            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequestCriacao)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //capturando o id da turma criada para usar na requisição de edição
            var dadosResposta = response.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            #endregion

            #region Editar turma

            var requestEdicao = new TurmaRequest
            {
                Numero = numero,
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestEdicao = new StringContent(JsonConvert.SerializeObject(requestEdicao), Encoding.UTF8, "application/json");
            var responseEdicao = client.PutAsync("/api/Turma/atualizarTurma/" + idTurma, jsonRequestEdicao)?.Result;
            responseEdicao?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public void NaoDeveEditarTurmaComNumeroJaExistente()
        {
            var faker = new Faker("pt_BR");
            var numeroTurma1 = faker.Random.String2(3, 8, "0123456789");
            var numeroTurma2 = faker.Random.String2(3, 8, "0123456789");

            #region Cadastrar turma 1

            var requestCriacao = new TurmaRequest
            {
                Numero = numeroTurma1,
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestCriacao = new StringContent(JsonConvert.SerializeObject(requestCriacao), Encoding.UTF8, "application/json");
            var client = new WebApplicationFactory<Program>().CreateClient();
            var response = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequestCriacao)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //pegar id da turma criada
            var dadosResposta = response.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            #endregion

            #region cadastrar turma 2

            var requestCriacao2 = new TurmaRequest
            {
                Numero = numeroTurma2,
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestCriacao2 = new StringContent(JsonConvert.SerializeObject(requestCriacao2), Encoding.UTF8, "application/json");
            var response2 = client.PostAsync("/api/Turma/cadastrarTurma", jsonRequestCriacao2)?.Result;
            response2?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            #endregion

            #region Editar Turma 1 com numero da turma 2

            var requestEdicao = new TurmaRequest
            {
                Numero = numeroTurma2,
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonRequestEdicao = new StringContent(JsonConvert.SerializeObject(requestEdicao), Encoding.UTF8, "application/json");
            var responseEdicao = client.PutAsync("/api/Turma/atualizarTurma/" + idTurma, jsonRequestEdicao)?.Result;
            responseEdicao?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            #endregion
        }

    }
}
