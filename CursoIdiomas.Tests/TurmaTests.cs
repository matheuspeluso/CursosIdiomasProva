using Bogus;
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

            var client = new WebApplicationFactory<Program>().CreateClient();//criando o client http
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

    }
}
