using Bogus;
using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Tests
{
    public class AlunoTests
    {
        [Fact]
        public async Task DeveCriarAlunoComSucesso()
        {
            var client = new WebApplicationFactory<CursoIdiomas.API.Program>().CreateClient();
            var token = await AuthHelper.ObterTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var faker = new Faker("pt_BR");

            #region Cadastrar turma para pegar o id

            var turmaRequest = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };

            var jsonTurmaRequest = new StringContent(JsonConvert.SerializeObject(turmaRequest), Encoding.UTF8, "application/json");
            var turmaResponse = client.PostAsync("/api/Turma/cadastrarTurma", jsonTurmaRequest)?.Result;

            turmaResponse?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //pegar id da turma criada
            var dadosResposta = turmaResponse.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            #endregion

            #region Cadastrar aluno

            var request = new AlunoRequest
            {
                Nome = faker.Name.FullName(),
                Cpf = faker.Random.Replace("###########").ToString(),
                Email = faker.Internet.Email(),
                TurmasIds = new List<Guid> { idTurma }
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = client.PostAsync("/api/Aluno/cadastrarAluno", jsonRequest)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async Task NaoDeveCadastrarAlunoSemTurma()
        {
            var client = new WebApplicationFactory<CursoIdiomas.API.Program>().CreateClient();
            var token = await AuthHelper.ObterTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var faker = new Faker("pt_BR");
            var request = new AlunoRequest
            {
                Nome = faker.Name.FullName(),
                Cpf = faker.Random.Replace("###########").ToString(),
                Email = faker.Internet.Email(),
                TurmasIds = new List<Guid>()
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = client.PostAsync("/api/Aluno/cadastrarAluno", jsonRequest)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task NaoDeveCadastrarAlunoComTurmaInvalida()
        {
            var client = new WebApplicationFactory<CursoIdiomas.API.Program>().CreateClient();
            var token = await AuthHelper.ObterTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var faker = new Faker("pt_BR");
            var request = new AlunoRequest
            {
                Nome = faker.Name.FullName(),
                Cpf = faker.Random.Replace("###########").ToString(),
                Email = faker.Internet.Email(),
                TurmasIds = new List<Guid> { Guid.NewGuid() }
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = client.PostAsync("/api/Aluno/cadastrarAluno", jsonRequest)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            var content = response.Content.ReadAsStringAsync().Result;
            content.Should().Be("Não foi possivel encontrar a turma informada.");
        }

        [Fact]
        public async Task NaoDeveCadastrarAlunoComCpfRepetido()
        {
            var client = new WebApplicationFactory<CursoIdiomas.API.Program>().CreateClient();
            var token = await AuthHelper.ObterTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var faker = new Faker("pt_BR");
            var cpf = faker.Random.Replace("###########").ToString();

            #region Cadastrar turma para pegar o id

            var turmaRequest = new TurmaRequest
            {
                Numero = faker.Random.String2(3, 8, "0123456789"),
                AnoLetivo = faker.Random.Number(2018, 2030).ToString(),
            };


            var jsonTurmaRequest = new StringContent(JsonConvert.SerializeObject(turmaRequest), Encoding.UTF8, "application/json");
            var turmaResponse = client.PostAsync("/api/Turma/cadastrarTurma", jsonTurmaRequest)?.Result;

            turmaResponse?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //pegar id da turma criada
            var dadosResposta = turmaResponse.Content.ReadAsStringAsync().Result;
            var turma = JsonConvert.DeserializeObject<TurmaResponse>(dadosResposta);
            var idTurma = turma.Id;

            #endregion

            var request = new AlunoRequest
            {
                Nome = faker.Name.FullName(),
                Cpf = cpf,
                Email = faker.Internet.Email(),
                TurmasIds = new List<Guid> { idTurma }
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = client.PostAsync("/api/Aluno/cadastrarAluno", jsonRequest)?.Result;
            response?.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //refazendo a mesma requisição com o mesmo cpf
            var response2 = client.PostAsync("/api/Aluno/cadastrarAluno", jsonRequest)?.Result;
            response2?.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            var content = response2.Content.ReadAsStringAsync().Result;
            content.Should().Be("Cpf já cadastrado.");
        }
    }
}
