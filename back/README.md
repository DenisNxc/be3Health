# PatientRegistration.API

API para cadastro e gerenciamento de pacientes e convênios.

## Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (local ou em nuvem)

## Configuração do Banco de Dados
1. Configure a string de conexão no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=PatientRegistrationDb;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
   }
   ```
2. Para criar e popular o banco de dados com os convênios iniciais, execute no terminal do projeto:
   ```powershell
   dotnet ef database update
   ```
   Isso irá aplicar as migrations e criar as tabelas e dados iniciais.

## Executando a API
1. No terminal, execute:
   ```powershell
   dotnet run
   ```
2. Acesse a documentação Swagger em:
   [http://localhost:5010/swagger](http://localhost:5010/swagger)

## Endpoints principais
- `GET /api/Paciente` — Lista todos os pacientes
- `GET /api/Paciente/{id}` — Detalhe de um paciente
- `POST /api/Paciente` — Cadastro de paciente
- `PUT /api/Paciente/{id}` — Atualização de paciente
- `GET /api/Convenio` — Lista de convênios

## Observações
- Apenas os campos Nome, Sobrenome, DataNascimento, CPF e Celular são obrigatórios para cadastro/edição de paciente.
- Os dados de convênios são populados automaticamente na criação do banco.

---

Se precisar de mais instruções ou exemplos de requisições, consulte o Swagger ou entre em contato com o desenvolvedor.
