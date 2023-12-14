# TrybeHotel

Este projeto é uma API para o site de reservas de hotéis TrybeHotel. O projeto foi desenvolvido como parte do curso de Desenvolvimento Full Stack da Trybe.

## Tecnologias Utilizadas

- .NET 6.0
- FluentAssertions.AspNetCore.Mvc 4.2.0
- Microsoft.AspNetCore.Authentication 2.2.0
- Microsoft.AspNetCore.Authentication.JwtBearer 6.0
- Microsoft.EntityFrameworkCore 7.0.4
- Microsoft.EntityFrameworkCore.Design 7.0.4
- Microsoft.EntityFrameworkCore.SqlServer 7.0.4
- Swashbuckle.AspNetCore 6.2.3
- System.Net.Http 4.3.4
- System.Text.Json 7.0.3

## Estrutura do Projeto

O projeto segue a estrutura padrão de um projeto ASP.NET e utiliza o padrão de arquitetura MVC. As interfaces dos repositórios foram implementadas pela Trybe eu apenas as tornei funcionais aplicado-as aos repositórios.

## Configuração

Certifique-se de ter as dependências instaladas e o ambiente configurado corretamente:

```bash

dotnet restore
```

## Requisição à API de GeoLocation

Este projeto utiliza `HttpClient` para realizar requisições à API de GeoLocation. Certifique-se de configurar corretamente as URLs e as chaves de autenticação, se necessário.

## Execução

Para executar a API, utilize o seguinte comando:

```bash
dotnet run
```

A API estará acessível em http://localhost:5000.

## Documentação da API

A documentação da API pode ser encontrada em http://localhost:5000/swagger após a execução do projeto.

## Contribuição

Se desejar contribuir, por favor, siga as diretrizes de contribuição do projeto.

## Contato

Para entrar em contato com a equipe, envie um e-mail para <a href='mailto:gabrielferdev@gmail.com'>Gabriel</a>.
