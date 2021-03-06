# Prática do curso: Configuração de arquitetura back-end com .NET 5

Este projeto implementa uma Web API em .NET 5 contendo operações em entidades de usuários e cursos.

**Usuario**

Efetuar login
> POST ​/api​/v1​/usuario​/logar

Registrar um usuário 
>   POST ​/api​/v1​/usuario​/registrar


**Curso**

Retorna a lista de cursos do usuário autenticado
>   GET ​ /api​/v1​/curso

Registra um curso associado ao usuáro autenticado 
>   POST ​/api​/v1​/curso​/registrar   (Registrar um curso)

### Conceitos, bibliotecas, ferramentas e padrões utilizados no projeto

 - Visual Studio 2019
 - SQL Server 2019 executando em container Docker (imagem:
   mcr.microsoft.com/mssql/server:2019-CU9-ubuntu-16.04)
 - AspNet MVC
 - Action Filter
 - Entity Framework Core
 - EF Migration com abordagem Code First
 - Mapping de entidades customizado
 - Data Annotations
 - Autenticação com JWT
 - Repository Pattern
 - IOC e DI
 - Criptografia da senha do usuário utilizando o padrão Rijndael
 - Documentação da API com Swagger

### Pacotes NuGet instalados

 - Microsoft.EntityFrameworkCore
 - Microsoft.EntityFrameworkCore.Relational
 - Microsoft.EntityFrameworkCore.SqlServer
 - Microsoft.EntityFrameworkCore.Tools
 - Microsoft.AspNetCore.Authentication
 - Microsoft.AspNetCore.Authentication.JwtBearer
 - Microsoft.AspNetCore.Mvc.Abstractions
 - Swashbuckle.AspNetCore
 - Swashbuckle.AspNetCore.Annotations
