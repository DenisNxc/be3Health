# Sistema de Cadastro de Pacientes - Be3Health

![Angular](https://img.shields.io/badge/Angular-17+-DD0031?logo=angular)
![.NET](https://img.shields.io/badge/.NET-9-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-2019-CC2927?logo=microsoft-sql-server)

Sistema completo para gerenciamento de pacientes com frontend em Angular e backend em .NET 9.

## 📦 Estrutura do Repositório

be3Health/
├── front/ # Frontend Angular
│ ├── src/ # Código fonte do Angular
│ ├── angular.json # Configuração do Angular CLI
│ └── package.json # Dependências do frontend
│
├── back/ # Backend .NET
│ ├── Controllers/ # Endpoints da API
│ ├── Models/ # Entidades do banco
│ ├── Migrations/ # Migrações do EF Core
│ └── appsettings.json # Configurações
│
└── README.md # Este arquivo


## 🚀 Pré-requisitos

- Node.js v18+ e npm v9+ (frontend)
- .NET 9 SDK (backend)
- SQL Server 2019+ (ou Docker)
- Angular CLI (opcional)

## ⚙️ Configuração

### Backend (.NET API)
1. Configure a connection string em `back/appsettings.json`
2. Execute as migrations:
   ```powershell
   cd back
   dotnet ef database update

3.Inicie a API:

powershell
dotnet run
Acesse: http://localhost:5010/swagger

Frontend (Angular)
Instale as dependências:

powershell
cd front
npm install
Inicie o servidor de desenvolvimento:

powershell
npm start
Acesse: http://localhost:4200

🔧 Funcionalidades
Frontend:

Cadastro/edição de pacientes com validação

Listagem com filtros e paginação

Máscaras para CPF, telefone e outros campos

Interface responsiva

Backend:

API REST com CRUD completo

Entity Framework Core + SQL Server

Migrations pré-configuradas

Swagger integrado

🤝 Dependências entre Projetos
O frontend espera que o backend esteja:

Rodando em http://localhost:5010

Com o banco de dados criado (via migrations)

Com os endpoints padrão (ver Swagger)

📌 Observações Importantes
O frontend usa Angular standalone components (sem NgModules)

O backend já inclui dados iniciais de convênios

Ambos projetos devem ser executados simultaneamente

Configure o CORS no backend se necessário

📄 Documentação Adicional
Frontend Documentation

Backend API Docs (quando rodando)
