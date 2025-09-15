# 📚 CursoIdiomas - Sistema de Gerenciamento de Cursos de Idiomas

Este projeto trata-se de 2 API microsserviços  desenvolvidas em **.NET 8** para gerenciar cursos de idiomas, permitindo o cadastro de alunos e turmas, além do relacionamento entre eles.
O sistema foi estruturado seguindo princípios de **Clean Architecture**, separando responsabilidades em camadas de API, Domain, Infra.Data e Tests.

já o segundo projeto é uma **API de autenticação**, permitindo a criação e login de usuários, com suporte a JWT para autenticação e segurança nos endpoints.

---

## 🚀 Tecnologias Utilizadas
- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [xUnit](https://xunit.net/) para testes
- [Docker](https://www.docker.com/) para containerização
- [Swagger](https://swagger.io/) para documentação de endpoints

---

## 📂 Estrutura do Projeto
```bash
├── CursoIdiomas.API # Camada de apresentação (Controllers, Program.cs)
│ └── Controllers # AlunoController, TurmaController
├── CursoIdiomas.Auth.API    # Nova API de autenticação (Cadastro e Autenticação com JWT)
├── CursoIdiomas.Domain # Regras de negócio
│ ├── Entities # Entidades: Aluno, Turma, AlunoTurma
│ ├── Dtos # DTOs de entrada e saída
│ ├── Services # Serviços de aplicação
│ └── Interfaces # Contratos de repositórios e serviços
│
├── CursoIdiomas.Infra.Data # Persistência e acesso a dados
│ ├── Contexts # ApplicationContext (DbContext)
│ ├── Repositories # Implementações de repositórios
│ ├── Mappings # Configurações de mapeamento EF Core
│ └── Migrations # Histórico de migrations
│
├── CursoIdiomas.Tests # Testes de Integração com Xunit
│ └── TurmaTests.cs
├── Helpers # Classe AuthHelper.cs usada para autenticar na api e retornar um token JWT para ser usado nos testes.
│
├── docker-compose.yml # arquivo para subir o banco de dados SQL Server
└── CursoIdiomas.sln # Solução do Visual Studio
```
---
## ⚙️ Configuração do Ambiente

### ✅ Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/) para rodar o container de banco de dados do sql server

---

### ▶️ Rodando a aplicação

1. **Clonar o repositório**
```bash
    git clone https://github.com/matheuspeluso/CursosIdiomasProva.git
    cd CursosIdiomasProva/CursoIdiomas
```
**Subindo container do docker**
```bash
    docker-compose up -d --build
```

**Antes de rodar a Migration é necessario trocar a connection string no arquivo ApplicationContext.cs**
```bash
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Prova2025;Encrypt=False"); //CONEXÃO PARA RODAR LOCAL
            //optionsBuilder.UseSqlServer("Server=sqlserver-db,1433;Database=master;User Id=sa;Password=Prova2025;TrustServerCertificate=True;"); //CONEXÃO PARA RODAR NO DOCKER
        }
```

**Adicione o projeto infra.Data como projeto de inicialização, abra o console do nuget e rode o comando abaixo:**
```bash
    Update-Database
```

**Troque novamente a connection string para a Docker**
```bash
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Prova2025;Encrypt=False"); //CONEXÃO PARA RODAR LOCAL
            optionsBuilder.UseSqlServer("Server=sqlserver-db,1433;Database=master;User Id=sa;Password=Prova2025;TrustServerCertificate=True;"); //CONEXÃO PARA RODAR NO DOCKER
        }
```

**Caso o container ainda não esteja online, rode novamente usando o comando:**
```bash
    docker-compose up -d --build
```

**Projeto rodando nas portas:**
```bash
    http://localhost:5001/swagger/index.html //apiAuth
    http://localhost:5000/swagger/index.html //apiCursosIdiomas
```

**Observações:**
Os end-points da API CursosIdiomas são bloqueados, e só funcionaram com usuários autenticados, então antes fazer qualquer requisição é necessario criar sua conta no CursoIdiomas.Auth.API , realizar o login,
e mandar o Bearer token nas requisições do CursosIdiomas.API.