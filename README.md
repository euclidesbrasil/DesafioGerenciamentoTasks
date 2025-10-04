# API Geral - Gestão de Tarefas e Projetos

## 📌 Descrição

Esta API fornece recursos para gerenciar autenticação, tarefas, projetos, historico e comentário associados ao gerenciamento de tarefas/projetos.

## Tecnologias Utilizadas

- .NET 8
- PostgreSQL

## 🗉 Pré-requisitos

Clone este projeto usando a URL: [https://github.com/euclidesbrasil/DesafioGerenciamentoTasks](https://github.com/euclidesbrasil/DesafioGerenciamentoTasks)

Antes de baixar o projeto, certifique-se de ter instalado:

- **Visual Studio** (Versão utilizada: Microsoft Visual Studio Community 2022 - Versão 17.10.1, preferencialmente após a versão 17.8)
- **PostgreSQL** (Versão utilizada: 17.2-3) [Baixar aqui](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

##  Configuração Antes da Execução

### 1. Configuração do PostgreSQL

No projeto **ArquiteturaDesafio.General.Api**, abra o arquivo `appsettings.json` e ajuste a seção `DefaultConnection` com as credenciais do seu banco de dados local:

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=ARQDESAFIODOTNETTASK;Username=postgres;Password=admin"
```

### 2. Executando o Projeto

Basta executar o projeto para iniciar a API. Na primeira execução, o banco de dados será criado automaticamente e os dados iniciais serão carregados. Poderá ser usado via Swagger;

Caso queira rodar via docker,inicio o docker destop, e no visual studio abra o "PowerShell do Desenvolvedor" referente a raiz da solução e execute o comando:
```json
docker-compose up --build -d
```
Isso fará que o docker build a aplicação e suba as imagens necessárias. Acesso no navegador a aplicação pelo link: 
 http://localhost:5000/swagger/index.html
 
 Para rodar a cobertura de testes execute no prompt o comando:
 ```json
 dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
 ```
##  Autenticação

Para utilizar os endpoints, é necessário obter um token de autenticação. Utilize as credenciais iniciais:

- **Usuário:** admin ou manager , para simular perfis diferentes
- **Senha:** s3nh@

##  ( Fase 2: Refinamento )
Perguntas ao PO:
- Posso criar uma Task inicialmente sem o status padrão que defini? Criar como Doing ou Done?
- Está correto mudar uma task de projeto?
- Um usuário terá acesso a qualquer task ou projeto?
- A criação de tarefas/projetos, deveria ser atrelado ao usuário logado, ou mantenho a questão de identificar qual o usuário?
