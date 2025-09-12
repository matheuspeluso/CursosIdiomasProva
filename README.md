# 📚 CursoIdiomas - Sistema de Gerenciamento de Cursos de Idiomas

Este projeto é uma API microsserviços  desenvolvida em **.NET 8** para gerenciar cursos de idiomas, permitindo o cadastro de alunos e turmas, além do relacionamento entre eles.
O sistema foi estruturado seguindo princípios de **Clean Architecture**, separando responsabilidades em camadas de API, Domain, Infra.Data e Tests.

Agora, o projeto inclui também uma segunda **API de autenticação**, permitindo a criação e login de usuários, com suporte a JWT para autenticação e segurança nos endpoints.

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
    docker-compose up -d
```
**Adicione o projeto infra.Data como projeto de inicialização, abra o console do nuget e rode o comando abaixo:**
```bash
    Update-Database
```
**Adicione novamente a API como projeto de inialização e inicialize o projeto**
