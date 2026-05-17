Com base nos arquivos do seu projeto, preparei um README.md completo, formal e educativo, com sumário, instruções detalhadas e explicações técnicas.

```markdown
# 🎮 SISTEMA VENDA GEEK

**Sistema desktop para gestão de vendas em loja de jogos, acessórios e produtos geek**

---

## 📑 SUMÁRIO

1. [Sobre o Projeto](#-sobre-o-projeto)
2. [Tecnologias Utilizadas](#-tecnologias-utilizadas)
3. [Arquitetura do Sistema](#-arquitetura-do-sistema)
4. [Perfis de Usuário e Permissões](#-perfis-de-usuário-e-permissões)
5. [Estrutura de Pastas](#-estrutura-de-pastas)
6. [Pré-requisitos para Execução](#-pré-requisitos-para-execução)
7. [Como Executar o Projeto](#-como-executar-o-projeto)
8. [Como Publicar o Executável](#-como-publicar-o-executável)
9. [Banco de Dados](#-banco-de-dados)
10. [Funcionalidades Detalhadas](#-funcionalidades-detalhadas)
11. [Fluxos do Sistema](#-fluxos-do-sistema)
12. [Testes Realizados](#-testes-realizados)
13. [Problemas Conhecidos e Soluções](#-problemas-conhecidos-e-soluções)
14. [Contribuidores](#-contribuidores)
15. [Considerações Finais](#-considerações-finais)

---

## 📋 SOBRE O PROJETO

O **Sistema Venda Geek** é uma aplicação desktop desenvolvida em **C# com Windows Forms** para gerenciar as operações de uma loja especializada em produtos da cultura geek. O sistema atende desde o atendimento ao cliente até o controle de estoque, passando por gestão de vendas e cadastro de clientes.

### Objetivos Educacionais

Este projeto foi desenvolvido como parte do **PIM VI (Projeto Integrado Multidisciplinar)** do curso de **Análise e Desenvolvimento de Sistemas** da **UNIP EaD**, integrando conhecimentos de:

- Análise Orientada a Objetos
- Banco de Dados Relacional
- Engenharia de Software
- Interface Homem-Computador

### Funcionalidades Principais

| Módulo | Funcionalidades |
|--------|-----------------|
| **Login** | Autenticação com 3 perfis distintos |
| **Vendas** | Registro de vendas, carrinho, controle de itens raros |
| **Produtos** | Cadastro, consulta, atualização de estoque |
| **Clientes** | Cadastro e consulta de clientes |
| **Financeiro** | Integração simulada com sistema financeiro |

---

## 🚀 TECNOLOGIAS UTILIZADAS

| Tecnologia | Versão | Finalidade |
|------------|--------|-------------|
| **C# (.NET)** | 10.0 | Linguagem principal |
| **Windows Forms** | - | Interface gráfica desktop |
| **SQLite** | 1.0.118 | Banco de dados embarcado |
| **SQLitePCLRaw** | 2.1.0 | Biblioteca nativa para SQLite |

### Por que SQLite?

O SQLite foi escolhido por ser:
- **Embarcado**: não requer instalação de servidor
- **Auto-contido**: um único arquivo `.db` armazena todos os dados
- **Portável**: funciona em qualquer sistema operacional
- **Leve**: ideal para aplicações desktop de pequeno porte

---

## 🏗️ ARQUITETURA DO SISTEMA

O projeto segue o padrão de **arquitetura em camadas**:

```
┌─────────────────────────────────────────────────────────┐
│                     CAMADA DE APRESENTAÇÃO               │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐   │
│  │FrmLogin  │ │FrmDashboard│ │FrmNovaVenda│ │FrmProduto│   │
│  └──────────┘ └──────────┘ └──────────┘ └──────────┘   │
├─────────────────────────────────────────────────────────┤
│                     CAMADA DE SERVIÇOS                   │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐   │
│  │AuthService│ │VendaService│ │ProdutoService│ │Cliente...│   │
│  └──────────┘ └──────────┘ └──────────┘ └──────────┘   │
├─────────────────────────────────────────────────────────┤
│                     CAMADA DE ACESSO A DADOS             │
│  ┌─────────────────────────────────────────────────────┐│
│  │                 DatabaseHelper.cs                    ││
│  │    (Criação do banco, conexões, tabelas, dados)     ││
│  └─────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────┘
```

### Explicação das Camadas

1. **Camada de Apresentação (Forms)**
   - Responsável pela interface gráfica
   - Captura entradas do usuário
   - Exibe resultados e mensagens

2. **Camada de Serviços (Services)**
   - Contém as regras de negócio
   - Valida permissões de usuário
   - Executa operações no banco de dados

3. **Camada de Acesso a Dados (Database)**
   - Gerencia conexões com SQLite
   - Cria tabelas automaticamente
   - Insere dados de teste

---

## 👥 PERFIS DE USUÁRIO E PERMISSÕES

O sistema possui **três perfis** com níveis diferentes de acesso:

| Perfil | Login | Senha | Permissões |
|--------|-------|-------|-------------|
| **Atendente** | `atendente` | `123` | • Consultar preços<br>• Registrar vendas<br>• Cadastrar clientes<br>• Cancelar venda (requer autorização do supervisor) |
| **Supervisor** | `supervisor` | `123` | • Todas as permissões do atendente<br>• Cancelar vendas sem autorização<br>• Gerenciar produtos |
| **Estoquista** | `estoquista` | `123` | • Cadastrar produtos<br>• Atualizar estoque<br>• Consultar produtos |

### Fluxo de Autorização para Cancelamento

```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│  Atendente  │────▶│  Supervisor │────▶│ Cancelamento │
│ solicita    │     │ autentica   │     │  da venda   │
│ cancelamento│     │             │     │             │
└─────────────┘     └─────────────┘     └─────────────┘
```

---

## 📁 ESTRUTURA DE PASTAS

```
SistemaVendaGeek/
│
├── 📁 Forms/                 # Telas do sistema (8 arquivos)
│   ├── FrmLogin.cs           # Autenticação de usuários
│   ├── FrmDashboard.cs       # Menu principal
│   ├── FrmNovaVenda.cs       # Registro de vendas com carrinho
│   ├── FrmConsultarPreco.cs  # Consulta de produtos
│   ├── FrmCadastrarProduto.cs # Cadastro e gestão de produtos
│   ├── FrmCadastrarCliente.cs # Cadastro de clientes
│   └── FrmConsultarVendas.cs  # Histórico de vendas
│
├── 📁 Services/              # Regras de negócio (6 arquivos)
│   ├── AuthService.cs        # Autenticação e controle de perfil
│   ├── ProdutoService.cs     # CRUD de produtos e estoque
│   ├── ClienteService.cs     # Consulta de clientes
│   ├── VendaService.cs       # Registro e cancelamento de vendas
│   └── SistemaFinanceiroService.cs # Integração com financeiro
│
├── 📁 Database/              # Banco de dados
│   └── DatabaseHelper.cs     # Criação do banco, tabelas e dados iniciais
│
├── 📁 Models/                # Classes de modelo (3 arquivos)
│   ├── Produto.cs            # Entidade Produto
│   ├── Cliente.cs            # Entidade Cliente
│   └── Venda.cs              # Entidade Venda e ItemVenda
│
├── Program.cs                # Ponto de entrada da aplicação
└── SistemaVendaGeek.csproj   # Configuração do projeto
```

---

## ⚙️ PRÉ-REQUISITOS PARA EXECUÇÃO

### Para Desenvolvimento (rodar com `dotnet run`)

| Requisito | Versão | Download |
|-----------|--------|----------|
| .NET SDK | 10.0 ou superior | [dotnet.microsoft.com](https://dotnet.microsoft.com/download) |
| Git (opcional) | Qualquer | [git-scm.com](https://git-scm.com/) |
| Visual Studio 2022+ | Qualquer edição | [visualstudio.microsoft.com](https://visualstudio.microsoft.com/) |

### Para Execução do Aplicativo Publicado

Caso você baixe apenas o arquivo executável (`.exe`), é necessário instalar o **.NET Desktop Runtime**:

| Requisito | Versão | Download |
|-----------|--------|----------|
| .NET Desktop Runtime | 10.0.x | [Clique aqui](https://dotnet.microsoft.com/download/dotnet/10.0) |

> ⚠️ **Importante:** O runtime não vem instalado por padrão no Windows. Sem ele, o sistema exibirá a mensagem: *"You must install .NET Desktop Runtime to run this application"*.

---

## 🛠️ COMO EXECUTAR O PROJETO

### Opção 1: A partir do código fonte (recomendado para desenvolvedores)

```bash
# Clone o repositório
git clone https://github.com/RayssaLGSV207/Sistema-Venda-Geek.git

# Acesse a pasta do projeto
cd Sistema-Venda-Geek

# Restaure os pacotes NuGet
dotnet restore

# Execute o projeto (o banco de dados será criado automaticamente)
dotnet run
```

### Opção 2: Usando Visual Studio

1. Abra o arquivo `SistemaVendaGeek.csproj` no Visual Studio
2. Aguarde a restauração dos pacotes NuGet
3. Pressione `F5` ou clique em "Iniciar"

### Opção 3: A partir do executável publicado

1. Baixe o arquivo `SistemaVendaGeek.exe`
2. Coloque-o em uma pasta vazia
3. Execute duplo clique no arquivo

> ⚠️ Certifique-se de ter o **.NET Desktop Runtime 10.0** instalado.

### O que acontece na primeira execução?

```
┌────────────────────────────────────────────────────────────┐
│                    PRIMEIRA EXECUÇÃO                        │
├────────────────────────────────────────────────────────────┤
│ 1. O sistema verifica se o arquivo .db já existe           │
│ 2. Se não existir, cria o arquivo SistemaVendaGeek.db      │
│ 3. Cria todas as tabelas (Usuario, Produto, Cliente, etc)  │
│ 4. Insere dados de teste:                                  │
│    • 3 usuários (atendente, supervisor, estoquista)        │
│    • 30 produtos (jogos, acessórios, itens raros)          │
│    • 20 clientes cadastrados                               │
│ 5. Exibe mensagem: "Banco de dados criado com sucesso!"    │
│ 6. Abre a tela de login                                    │
└────────────────────────────────────────────────────────────┘
```

---

## 📦 COMO PUBLICAR O EXECUTÁVEL

### Publicação com runtime incluído (autossuficiente)

```bash
dotnet publish -c Release -r win-x64 --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -o ./publicar
```

### Publicação sem runtime incluído (mais leve)

```bash
dotnet publish -c Release -r win-x64 --self-contained false \
  -p:PublishSingleFile=true -o ./publicar
```

### Comparação entre os métodos

| Método | Tamanho do arquivo | Precisa instalar runtime? | Recomendado para |
|--------|-------------------|---------------------------|------------------|
| Com runtime (`--self-contained true`) | ~60-80 MB | ❌ Não | Usuários finais |
| Sem runtime (`--self-contained false`) | ~2-5 MB | ✅ Sim | Desenvolvedores |

---

## 🗄️ BANCO DE DADOS

### Esquema do Banco (5 tabelas)

```sql
-- Tabela de Usuários
Usuario (Login PK, Senha, Perfil)

-- Tabela de Produtos
Produto (CodigoBarras PK, Nome, Categoria, Fabricante, 
         Valor, QuantidadeEstoque, IsRaro, Plataforma, PrazoGarantia)

-- Tabela de Clientes
Cliente (Id PK, Nome, CPF UNIQUE, RG, DataCadastro, Endereco, Telefone, Email)

-- Tabela de Vendas
Venda (CodigoVenda PK, DataVenda, ValorTotal, Status, FormaPagamento, ClienteCPF)

-- Tabela de Itens da Venda
ItemVenda (Id PK, CodigoVenda FK, CodigoBarras FK, Quantidade, PrecoUnitario)
```

### Relacionamentos

```
┌─────────┐     ┌─────────────┐     ┌─────────┐
│ Cliente │────▶│    Venda    │◀────│ Produto │
└─────────┘     └─────────────┘     └─────────┘
                      │
                      ▼
                ┌──────────┐
                │ItemVenda │ (tabela de ligação N:N)
                └──────────┘
```

### Dados de Teste Inseridos Automaticamente

| Tipo | Quantidade | Exemplos |
|------|------------|----------|
| Usuários | 3 | atendente, supervisor, estoquista |
| Produtos | 30 | Zelda, God of War, Controle DualSense |
| Produtos Raros | 5 | Mario 64 Lacrado, Pokemon 1ª Edição |
| Clientes | 20 | Distribuídos em 17 estados brasileiros |

---

## 🔧 FUNCIONALIDADES DETALHADAS

### 1. Módulo de Login (`AuthService.cs`)

```csharp
// Método principal de autenticação
public static bool Autenticar(string login, string senha, out string perfil)

// Valida credenciais de supervisor (usado para cancelamentos)
public static bool ValidarCredenciaisSupervisor(string login, string senha)

// Verificações de perfil
public static bool IsSupervisor()
public static bool IsEstoquista()
public static bool IsAtendente()
```

**Fluxo de autenticação:**
1. Usuário digita login e senha
2. Sistema consulta tabela `Usuario`
3. Se encontrado, armazena dados na sessão
4. Redireciona para Dashboard com permissões adequadas

### 2. Módulo de Produtos (`ProdutoService.cs`)

```csharp
// Cadastrar novo produto (requer perfil Estoquista ou Supervisor)
public static bool CadastrarProduto(...)

// Buscar produto por código de barras
public static ProdutoInfo BuscarProduto(string codigoBarras)

// Atualizar estoque (adicionar ou remover)
public static bool AtualizarEstoque(string codigoBarras, int quantidade, bool isAdicionar)

// Listar todos os produtos
public static List<ProdutoInfo> ListarTodosProdutos()
```

**Regras de negócio implementadas:**
- Produtos raros (`IsRaro = true`) têm controle especial de estoque
- Não é possível cadastrar dois produtos com o mesmo código de barras
- Estoque não pode ficar negativo

### 3. Módulo de Vendas (`VendaService.cs` + `FrmNovaVenda.cs`)

```csharp
// Registrar nova venda
public static string RegistrarVenda(string codigoBarras, int quantidade, string clienteCPF)

// Cancelar venda (com validação de perfil)
public static bool CancelarVenda(string codigoVenda)

// Atualizar status da venda
public static bool AtualizarStatusVenda(string codigoVenda, string novoStatus)
```

**Regras de negócio para vendas:**
- Produtos raros limitados a 1 unidade por venda
- Verificação automática de estoque antes da venda
- Geração automática de código único (`Guid`)
- Atualização automática do estoque após venda

### 4. Módulo de Clientes (`ClienteService.cs`)

```csharp
// Buscar cliente por CPF
public static ClienteInfo BuscarClientePorCPF(string cpf)

// Listar todos os clientes cadastrados
public static List<ClienteInfo> ListarTodosClientes()
```

### 5. Integração Financeira (`SistemaFinanceiroService.cs`)

```csharp
// Enviar cancelamento ao sistema financeiro
public static bool EnviarCancelamento(string codigoVenda)

// Registrar venda finalizada
public static bool RegistrarVendaFinanceiro(string codigoVenda, decimal valorTotal, string formaPagamento)

// Consultar status no log financeiro
public static string ConsultarStatus(string codigoVenda)
```

**Como funciona a integração:**
- Utiliza um arquivo `sistema_financeiro.log` para simular a comunicação
- Cada venda finalizada ou cancelada gera um registro no log
- Formato do log: `2026-05-17 14:30:00 - Venda FINALIZADA: abc123 | Valor: R$ 299,90`

---

## 🔄 FLUXOS DO SISTEMA

### Fluxo de Venda Completo

```
┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐
│  Login   │───▶│ Buscar   │───▶│Adicionar │───▶│Finalizar │
│Atendente │    │ Cliente  │    │ Produtos │    │  Venda   │
└──────────┘    └──────────┘    └──────────┘    └──────────┘
                                                      │
                                                      ▼
                                              ┌──────────────┐
                                              │Atualizar     │
                                              │Estoque       │
                                              └──────────────┘
                                                      │
                                                      ▼
                                              ┌──────────────┐
                                              │Registrar no  │
                                              │Financeiro    │
                                              └──────────────┘
```

### Fluxo de Cancelamento de Venda

```
┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐
│Atendente │───▶│Solicita  │───▶│Supervisor│───▶│ Venda    │
│          │    │Cancelamento│   │Autentica │    │Cancelada │
└──────────┘    └──────────┘    └──────────┘    └──────────┘
                                                      │
                                                      ▼
                                              ┌──────────────┐
                                              │Notifica      │
                                              │Financeiro    │
                                              └──────────────┘
```

---

## 🧪 TESTES REALIZADOS

### Testes de Autenticação

| Cenário | Resultado Esperado | Status |
|---------|-------------------|--------|
| Login com atendente/123 | Acesso concedido, perfil Atendente | ✅ |
| Login com supervisor/123 | Acesso concedido, perfil Supervisor | ✅ |
| Login com estoquista/123 | Acesso concedido, perfil Estoquista | ✅ |
| Login com usuário inexistente | Mensagem de erro | ✅ |
| Login com senha incorreta | Mensagem de erro | ✅ |
| Campos vazios | Mensagem de validação | ✅ |

### Testes de Produtos

| Cenário | Resultado Esperado | Status |
|---------|-------------------|--------|
| Cadastrar produto como Estoquista | Sucesso | ✅ |
| Cadastrar produto como Atendente | Mensagem de permissão negada | ✅ |
| Cadastrar produto com código repetido | Mensagem de erro | ✅ |
| Consultar produto existente | Exibe informações corretas | ✅ |
| Atualizar estoque | Contagem atualizada | ✅ |

### Testes de Vendas

| Cenário | Resultado Esperado | Status |
|---------|-------------------|--------|
| Venda com produto comum | Sucesso, estoque reduz | ✅ |
| Venda com produto raro (1 unidade) | Sucesso | ✅ |
| Venda com produto raro (2 unidades) | Bloqueado | ✅ |
| Venda sem cliente informado | Mensagem de validação | ✅ |
| Venda com estoque insuficiente | Bloqueado | ✅ |
| Cancelamento por Supervisor | Sucesso direto | ✅ |
| Cancelamento por Atendente | Requer autenticação do Supervisor | ✅ |

### Testes de Interface

| Cenário | Resultado Esperado | Status |
|---------|-------------------|--------|
| Navegação entre telas | Sem erros | ✅ |
| Botões responsivos | Cursor hand e hover | ✅ |
| Mensagens de confirmação | Exibidas corretamente | ✅ |
| Formatação de valores (R$) | Correta | ✅ |

---

## ⚠️ PROBLEMAS CONHECIDOS E SOLUÇÕES

### Erro: "Unable to load DLL 'SQLite.Interop.dll'"

**Causa:** O pacote `System.Data.SQLite.Core` requer bibliotecas nativas que podem não estar presentes.

**Solução:** Já implementada no projeto através do pacote:
```xml
<PackageReference Include="SQLitePCLRaw.bundle_e_sqlite3" Version="2.1.0" />
```

### Erro: "You must install .NET Desktop Runtime"

**Causa:** O computador não possui o runtime necessário para executar aplicações .NET.

**Solução:** Baixe e instale o **.NET Desktop Runtime 10.0**:
[https://dotnet.microsoft.com/download/dotnet/10.0](https://dotnet.microsoft.com/download/dotnet/10.0)

### Erro: "Database is locked"

**Causa:** Múltiplas conexões simultâneas ou conexão não fechada.

**Solução:** O projeto utiliza `using` statements para garantir o fechamento automático:
```csharp
using (var conn = DatabaseHelper.GetConnection())
{
    conn.Open();
    // operações
} // conn é fechado automaticamente
```

### A tela de login não aparece

**Causa:** O banco de dados pode estar corrompido.

**Solução:** Exclua o arquivo `SistemaVendaGeek.db` e execute o programa novamente.

---

## 👨‍💻 CONTRIBUIDORES

| Nome | Papel no Projeto | RA |
|------|------------------|-----|
| Rayssa Luanna Gomes da Serra Vieira | Integradora, levantamento de requisitos | UP25119984 |
| Rodrigo Correa Farias | Regras de negócio, serviços | UP25108816 |
| Caroline Moraes Monteiro | Banco de dados, consultas SQL | UP25116559 |
| Rosicléia Silva de Oliveira | Interface gráfica, experiência do usuário | UP25106074 |

---

## 📚 DISCIPLINAS INTEGRADAS (PIM VI)

| Disciplina | Contribuição ao Projeto |
|------------|------------------------|
| Análise de Sistemas Orientada a Objetos | Diagramas UML, padrão de arquitetura em camadas |
| Banco de Dados | Modelagem relacional, SQLite, normalização |
| Gestão Estratégica de Recursos Humanos | Perfis de usuário, níveis de acesso |
| Interface Homem-Computador | Usabilidade, cores, botões, feedback visual |

---

## 💡 CONSIDERAÇÕES FINAIS

### O que aprendemos com este projeto

1. **Arquitetura em camadas**: Separar responsabilidades facilita manutenção e testes
2. **Controle de permissões**: Diferentes perfis de usuário exigem validações em cada operação
3. **Banco de dados embarcado**: SQLite é excelente para aplicações desktop
4. **Experiência do usuário**: Mensagens claras e feedback visual são essenciais
5. **Tratamento de exceções**: Capturar erros específicos (como violação de UNIQUE) melhora a usabilidade

### Possíveis Melhorias Futuras

- [ ] Implementar relatórios gráficos de vendas
- [ ] Adicionar backup automático do banco de dados
- [ ] Criar tela de devolução de produtos
- [ ] Implementar busca avançada de produtos
- [ ] Adicionar cupons de desconto e promoções
- [ ] Integração com leitor de código de barras

---

## 📄 LICENÇA

Este projeto está sob a licença MIT - consulte o arquivo `LICENSE` para mais detalhes.

---

## 📧 CONTATO

Em caso de dúvidas ou sugestões, entre em contato através do ambiente virtual da UNIP ou diretamente com o tutor da disciplina.

---

**UNIP EaD - Polo Nazaré - 2026**

*"A tecnologia move o mundo, mas a paixão por games move a gente."* 🎮
```
