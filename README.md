# ArtesianWell.Core
Funcionalidades de poços artesianos

##Design Document: ArtesianWell.Core Service

### Introdução

O serviço **ArtesianWell.Core** é um componente essencial do sistema Poços Artesianos. Ele gerencia as funcionalidades principais, incluindo a listagem, contratação e agendamento de serviços de poços artesianos. Construído utilizando o .NET 8, este serviço adota uma arquitetura limpa, seguindo os princípios CQRS (Command Query Responsibility Segregation) e os princípios SOLID para garantir um código robusto, escalável e de fácil manutenção.

### Objetivo

Este documento visa fornecer uma visão detalhada da arquitetura do serviço **ArtesianWell.Core**, explicando suas camadas e componentes. Também abordaremos o processo de instalação utilizando Docker.

### Arquitetura

A arquitetura do serviço **ArtesianWell.Core** é baseada em uma abordagem de arquitetura limpa, dividida nas seguintes camadas:

- **Application:** Contém a lógica de aplicação e os casos de uso (Commands e Queries).
- **Presentation:** Camada responsável pela comunicação com o mundo externo (APIs).
- **Domain:** Define as entidades, interfaces de repositório e regras de negócio.
- **Infrastructure:** Implementações específicas de infraestrutura, como acesso a banco de dados e integração com outros serviços.

### Diagrama C4

**Context Diagram:**

```mermaid
graph TD
    A[Cliente] -->|Realiza solicitação| B[Web App]
    A -->|Realiza solicitação| H[Mobile App]
    B -->|Autenticação| I[Kong API Gateway]
    I -->|Autenticação| C[Identity Service]
    I -->|Roteamento de requisições| J[ArtesianWell.Core Service]
    J -->|Acesso a Dados| G[Database]
    J -->|Integração| K[ArtesianWell.MS.Customer]
    J -->|Integração| L[Service Bus]
    J -->|Integração| M[Notification Service]

    subgraph Serviços de Aplicação
        C
        J
    end

    subgraph Infraestrutura
        G
        L
    end

    subgraph Gerenciamento de API
        I
    end

```

**Container Diagram:**

```mermaid
graph TD
    subgraph Presentation
        APIC[API Controller]
    end

    subgraph Application
        CMD[Commands]
        QRY[Queries]
    end

    subgraph Domain
        ENT[Entities]
        REP[Repositories]
    end

    subgraph Infrastructure
        DB[Database Context]
        EXT[External Services]
    end

    APIC --> CMD
    APIC --> QRY
    CMD --> ENT
    CMD --> REP
    QRY --> ENT
    QRY --> REP
    REP --> DB
    CMD --> EXT

```

### Camadas do Projeto

**1. Presentation**

- **API Controllers:** Exponha os endpoints da API para interação com os clientes.
- **Autenticação e Autorização:** Gerencie a autenticação e autorização utilizando Keycloak via Kong API Gateway.

**2. Application**

- **Commands:** Contém a lógica de manipulação de dados (escrita).
- **Queries:** Contém a lógica de consulta de dados (leitura).
- **DTOs (Data Transfer Objects):** Defina os modelos de dados utilizados nas interfaces públicas.

**3. Domain**

- **Entities:** Define as entidades principais do sistema, como `Service`, `Schedule`, etc.
- **Value Objects:** Objetos de valor que encapsulam propriedades que formam conceitos do domínio.
- **Interfaces de Repositório:** Defina contratos para acesso a dados.

**4. Infrastructure**

- **Database Context:** Implementações específicas de acesso a banco de dados utilizando Entity Framework.
- **Repositories:** Implementações dos repositórios definidos na camada de domínio.
- **External Services:** Integrações com serviços externos, como notificações e service bus.

### Instalação com Docker

Para facilitar a instalação e execução do serviço **ArtesianWell.Core**, usaremos o Docker. Abaixo estão os passos para configurar o ambiente Docker.

**1. Dockerfile**

Crie um arquivo `Dockerfile` na raiz do projeto:

```
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos csproj e restaurar dependências
COPY *.csproj .
RUN dotnet restore

# Copiar o restante do código e compilar
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ArtesianWell.Core.dll"]
```

**2. docker-compose.yml**

Crie um arquivo `docker-compose.yml` para orquestrar os serviços:

```yaml
version: '3.8'

services:
  artesianwell.core:
    image: artesianwell.core:latest
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Host=postgres;Database=artesianwell;Username=artesianwell;Password=yourpassword
    depends_on:
      - postgres

  postgres:
    image: postgres:13
    environment:
      POSTGRES_DB: artesianwell
      POSTGRES_USER: artesianwell
      POSTGRES_PASSWORD: yourpassword
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:

```

**3. Comandos de Instalação**

Execute os seguintes comandos para iniciar os serviços:

```bash
# Construir a imagem Docker
docker-compose build

# Iniciar os serviços
docker-compose up
```