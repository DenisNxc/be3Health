# Sistema de Cadastro de Pacientes

Este projeto é um sistema web para cadastro, edição e listagem de pacientes, desenvolvido em Angular. O sistema permite gerenciar informações de pacientes, incluindo dados pessoais, documentos, contatos e convênios médicos, com validação forte de dados e interface amigável.

## Arquitetura Utilizada

- **Frontend:** Angular 17+ (standalone components)
- **Componentização:**
  - `PatientFormComponent`: Formulário de cadastro/edição de pacientes, com validação de campos obrigatórios e máscaras para CPF, telefone, etc.
  - `PatientListComponent`: Listagem de pacientes com filtros, paginação e ações de edição.
  - `HeaderComponent`: Cabeçalho do sistema.
- **Serviços:**
  - `PatientService`: Comunicação com backend REST para operações de CRUD de pacientes e convênios.
- **Diretivas customizadas:**
  - `OnlyNumbersDirective`: Restringe campos para aceitar apenas números.
- **Validações:**
  - Utiliza funções utilitárias para validação de CPF e formatação de campos.
- **Estilo:** SCSS modularizado, responsivo e com feedback visual de erro nos formulários.

## Pré-requisitos

- Node.js (v18 ou superior recomendado)
- npm (v9 ou superior)
- Angular CLI (global, recomendado)
- Backend REST rodando em `http://localhost:5010` (não incluso neste repositório)

## Instalação

1. Clone o repositório ou copie os arquivos para sua máquina:

```powershell
cd "c:\Users\denis\OneDrive\Área de Trabalho\Nova pasta\client-system"
```

2. Instale as dependências:

```powershell
npm install
```

## Execução

1. Inicie o servidor de desenvolvimento Angular:

```powershell
npm start
```

2. Acesse o sistema em [http://localhost:4200](http://localhost:4200) no seu navegador.

## Observações

- O sistema depende de um backend REST para funcionar corretamente. Certifique-se de que o backend está rodando e acessível em `http://localhost:5010`.
- Para customizar validações ou máscaras, edite os arquivos em `src/app/@components/patient-form/` e `src/app/utils-cpf.ts`.
- O sistema utiliza Angular standalone components, não há necessidade de módulos tradicionais.

## Estrutura de Pastas

- `src/app/@components/` — Componentes reutilizáveis (formulário, lista, header)
- `src/app/services/` — Serviços de acesso à API
- `src/app/interfaces/` — Interfaces TypeScript para tipagem
- `src/app/utils-cpf.ts` — Função utilitária para validação de CPF
- `src/app/only-numbers.directive.ts` — Diretiva para inputs numéricos
- `src/assets/` — Imagens e recursos estáticos


# ClientSystem

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.2.12.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.
