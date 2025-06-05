# SGCV - Sistema de Gestão de Concessionárias de Veículos

Este projeto visa construir uma aplicação web para a gestão de concessionárias de veículos. O sistema é composto por duas partes:

- **Backend/API:** Desenvolvido com ASP.NET Core Web API, este projeto expõe endpoints REST para operações de CRUD, vendas, geração de relatórios e integração com APIs externas.
- **Frontend (MVC):** Desenvolvido com ASP.NET Core MVC, este projeto consome os serviços da API por meio de HttpClient e renderiza as views utilizando Razor, além de incorporar recursos como gráficos e notificações.

> **Ambiente de Desenvolvimento Local:**  
> Você pode1 roda-lo em ambiente local utilizando a URL:  
> **https://localhost:7270**

---

## Índice

- [Visão Geral](#visão-geral)
- [Arquitetura do Sistema](#arquitetura-do-sistema)
- [Decisões de Design](#decisões-de-design)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Requisitos Funcionais](#requisitos-funcionais)
- [Instalação e Configuração](#instalação-e-configuração)
- [Execução do Projeto](#execução-do-projeto)
- [Testes](#testes)
- [Documentação da API](#documenta%C3%A7%C3%A3o-da-api)
- [Considerações Finais](#considera%C3%A7%C3%B5es-finais)

---

## Visão Geral

O **SGCV** é um sistema completo para a gestão de concessionárias de veículos que oferece:

- **Cadastro e Gerenciamento:** Permite o cadastro, edição, visualização e deleção lógica de fabricantes, veículos, concessionárias, clientes e vendas.
- **Autenticação e Autorização:** Gerencia o acesso dos usuários (Administrador, Vendedor, Gerente) utilizando ASP.NET Identity e JWT.
- **Relatórios e Dashboards:** Gera dashboards e relatórios mensais com gráficos interativos, auxiliando na análise do desempenho das vendas.
- **Integração Externa:** Integração com APIs externas, como ViaCEP, para validação de CEP e obtenção de dados automotivos.
- **Otimização de Desempenho:** Uso de caching de memória distribuída e otimizações nas consultas ao banco de dados para melhorar o desempenho da aplicação.

---

## Arquitetura do Sistema

O sistema foi estruturado seguindo os princípios da **Clean Architecture** e **Domain-Driven Design (DDD)**, distribuído em cinco projetos principais:

- **Domain:**  
  Contém as entidades principais (Fabricante, Veículo, Concessionária, Cliente, Venda, Usuário), enums, value objects e interfaces que definem as regras de negócio.

- **Application:**  
  Implementa os serviços de aplicação e lógica de negócio (ex.: `ClienteService`, `VendaService`), bem como os DTOs para transferência de dados entre as camadas.

- **Infrastructure:**  
  Responsável pela persistência de dados utilizando Entity Framework Core, integração com caching, ASP.NET Identity e outras integrações (como APIs externas).

- **API (Backend):**  
  Projeto ASP.NET Core Web API que expõe os endpoints REST para o sistema. Este projeto também inclui a documentação da API via Swagger e implementa autenticação JWT.

- **Frontend (MVC):**  
  Projeto ASP.NET Core MVC que consome os serviços da API para renderizar as views usando Razor. Também utiliza Bootstrap, Chart.js e AJAX para uma experiência de usuário moderna.

---

## Decisões de Design

- **Clean Architecture & DDD:**  
  A separação do sistema em camadas bem definidas permite maior organização, facilidade de manutenção e escalabilidade. Cada camada possui responsabilidades específicas, garantindo desacoplamento e alta coesão.

- **Princípios SOLID e Clean Code:**  
  Foram adotados os princípios SOLID para que o código seja modular, testável e de fácil manutenção. O uso de factories, mapeadores e injeção de dependências ajuda a manter o código limpo e intuitivo.

- **Segurança e Autenticação:**
  A aplicação utiliza ASP.NET Identity e JWT para gerenciar a autenticação e autorização dos usuários, protegendo os endpoints e garantindo a segurança dos dados. As configurações de token agora são carregadas em um objeto `JwtSettings` registrado via `IOptions`, o que facilita o gerenciamento seguro das chaves.

- **Integração com APIs Externas e Caching:**  
  A integração com APIs externas, como a ViaCEP para validação de CEP, e a utilização de caching de memória distribuída são essenciais para otimizar o desempenho e proporcionar uma boa experiência ao usuário.

---

## Tecnologias Utilizadas

- **Frontend:**  
  - ASP.NET Core MVC  
  - Bootstrap  
  - JavaScript, Jquery  
  - Chart.js  
  - Select2 e NToastNotify (para notificações)

- **Backend:**  
  - ASP.NET Core Web API  
  - Entity Framework Core  
  - ASP.NET Identity  
  - JWT Authentication

- **Banco de Dados:**  
  - SQL Server

- **Documentação da API:**  
  - Swagger

- **Testes:**  
  - xUnit (para testes unitários e de integração)

---

## Requisitos Funcionais

O sistema contempla os seguintes casos de uso:

1. **Gestão de Fabricantes:**  
   Cadastro, edição, visualização e deleção lógica de fabricantes, com validações como nome único, comprimento máximo e URL válida.

2. **Gestão de Veículos:**  
   Cadastro e gerenciamento de veículos, com validações para modelo, ano de fabricação, preço, tipo (enum) e associação obrigatória com fabricante.

3. **Gestão de Concessionárias:**  
   Registro e administração de concessionárias, incluindo dados de endereço, capacidade máxima de veículos e informações de contato.

4. **Gestão de Clientes:**  
   Cadastro de clientes com validação de CPF, nome e telefone, garantindo que não haja duplicidade de CPF.

5. **Realização de Vendas:**  
   Processo de vendas que inclui seleção de veículo, concessionária, cliente, data de venda e preço, com geração automática de protocolo único.

6. **Relatórios e Dashboards:**  
   Geração de relatórios mensais e dashboards interativos para análise de vendas, desempenho de concessionárias, faturamento, etc.

7. **Autenticação e Autorização:**  
   Implementação de login e controle de acesso baseado em papéis (Administrador, Vendedor, Gerente).

---

## Instalação e Configuração

### Pré-requisitos

- [.NET 6 ou superior](https://dotnet.microsoft.com/)
- [SQL Server ou LocalDB](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Configuração do Projeto

1. **Clone o Repositório:**

   ```bash
   git clone https://github.com/thalesaugustodias/gestaodeconcessionaria.git
   cd sgcv
   ```
