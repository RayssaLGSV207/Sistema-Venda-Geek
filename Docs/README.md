# SISTEMA VENDA GEEK

Sistema de controle de vendas para loja de jogos, acessorios e produtos geek.  
Projeto desenvolvido como parte do PIM VI do curso de Analise e Desenvolvimento de Sistemas da UNIP EaD.

---

## SUMARIO

1. Sobre o Projeto  
2. Tecnologias Utilizadas  
3. Perfis de Usuario e Acesso  
4. Estrutura de Diretorios  
5. Pre-requisitos para Execucao  
6. Como Executar  
7. Banco de Dados  
8. Sistema de Backups  
9. Como Publicar um Executavel Autossuficiente  
10. Problemas Conhecidos e Solucoes  
11. Desenvolvedores  

---

## 1. SOBRE O PROJETO

O Sistema Venda Geek e uma aplicacao desktop desenvolvida em C# com Windows Forms.  
O sistema oferece:

- Controle de acesso com tres perfis de usuario (Atendente, Supervisor, Estoquista)
- Cadastro e consulta de produtos
- Registro de vendas com controle de estoque
- Gestao de clientes
- Integracao com sistema financeiro via arquivo de log
- Backup automatico do banco de dados

---

## 2. TECNOLOGIAS UTILIZADAS

| Tecnologia | Versao | Finalidade |
|------------|--------|-------------|
| C# (.NET) | 10.0 | Linguagem principal |
| Windows Forms | - | Interface grafica desktop |
| SQLite | 1.0.118 | Banco de dados embarcado |
| SQLitePCLRaw | 2.1.0 | Biblioteca nativa para SQLite |

---

## 3. PERFIS DE USUARIO E ACESSO

Os seguintes usuarios estao previamente cadastrados no banco de dados:

| Perfil | Login | Senha | Permissoes |
|--------|-------|-------|-------------|
| Atendente | `atendente` | `123` | Consultar precos, registrar vendas, cadastrar clientes |
| Supervisor | `supervisor` | `123` | Todas as permissoes do atendente + cancelar vendas, gerenciar produtos |
| Estoquista | `estoquista` | `123` | Cadastrar produtos, atualizar estoque |

---

## 4. ESTRUTURA DE DIRETORIOS

```
SistemaVendaGeek/
│
├── 📁 Backups/                              # Backups do banco de dados
│   └── SistemaVendaGeek_Backup_20260528_015612.db
│
├── 📁 Docs/                                 # Documentacao do projeto
│   ├── RequisitosNaoFuncionais.md
│   ├── LICENSE
│   ├── README.md
│   ├── clientes.txt
│   └── produtos.txt
│
├── 📁 Forms/                                # Telas do sistema
│   ├── FrmLogin.cs
│   ├── FrmDashboard.cs
│   ├── FrmNovaVenda.cs
│   ├── FrmConsultarPreco.cs
│   ├── FrmCadastrarProduto.cs
│   ├── FrmCadastrarCliente.cs
│   └── FrmConsultarVendas.cs
│
├── 📁 Services/                             # Regras de negocio
│   ├── AuthService.cs
│   ├── ClienteService.cs
│   ├── ProdutoService.cs
│   ├── VendaService.cs
│   └── SistemaFinanceiroService.cs
│
├── 📁 Models/                               # Classes de modelo
│   ├── Produto.cs
│   ├── Cliente.cs
│   └── Venda.cs
│
├── 📁 Database/                             # Conexao e estrutura do banco
│   ├── DatabaseHelper.cs
│   └── scripts.sql
│
├── Program.cs                               # Ponto de entrada
├── SistemaVendaGeek.csproj                  # Configuracao do projeto
├── SistemaVendaGeek.db                      # Banco de dados principal
├── config_backup.txt                        # Controle do ultimo backup
└── sistema_financeiro.log                   # Log de vendas finalizadas e canceladas
```

---

## 5. PRE-REQUISITOS PARA EXECUCAO

### Para desenvolvimento (rodar com `dotnet run`)

- .NET SDK 8.0 ou superior
- Git (opcional)

### Para execucao do executavel publicado

Caso voce tenha apenas o arquivo `.exe`, e necessario instalar o **.NET Desktop Runtime 8.0**.

| Requisito | Versao | Download |
|-----------|--------|----------|
| .NET Desktop Runtime | 8.0.x | [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0) |

> O runtime nao vem instalado por padrao no Windows. Sem ele, o sistema exibira a mensagem: "You must install .NET Desktop Runtime to run this application".

---

## 6. COMO EXECUTAR

### Opcao 1: A partir do codigo fonte

```bash
git clone https://github.com/RayssaLGSV207/Sistema-Venda-Geek.git
cd Sistema-Venda-Geek
dotnet restore
dotnet run
```

O banco de dados sera criado automaticamente na primeira execucao, com 3 usuarios, 20 clientes e 35 produtos.

### Opcao 2: A partir do executavel publicado

1. Baixe os arquivos `SistemaVendaGeek.exe` e `SistemaVendaGeek.db`
2. Coloque os dois arquivos na mesma pasta
3. Execute `SistemaVendaGeek.exe`

> Certifique-se de ter o .NET Desktop Runtime 8.0 instalado.

---

## 7. BANCO DE DADOS

O sistema utiliza SQLite. O arquivo `SistemaVendaGeek.db` e criado automaticamente e contem as seguintes tabelas:

| Tabela | Descricao |
|--------|-----------|
| Usuario | Logins, senhas e perfis dos usuarios |
| Produto | Produtos da loja (codigo, nome, valor, estoque, raridade, garantia, plataforma) |
| Cliente | Clientes cadastrados (nome, CPF, RG, endereco, telefone, email) |
| Venda | Registro de vendas (codigo, data, valor total, status, cliente CPF) |
| ItemVenda | Itens de cada venda (produto, quantidade, preco unitario) |

O arquivo `Database/scripts.sql` contem o esquema completo do banco de dados, incluindo:

- Comandos CREATE TABLE para todas as entidades
- Indices para otimizacao de consultas
- Dados de teste (usuarios, produtos, clientes)
- Consultas para relatorios

### Dados de teste inseridos automaticamente

- Usuarios: `atendente/123`, `supervisor/123`, `estoquista/123`
- Clientes: 20 clientes distribuidos por varias regioes do Brasil
- Produtos: 35 produtos (jogos, acessorios, produtos geek e itens raros)

---

## 8. SISTEMA DE BACKUPS

O sistema realiza backup automatico do banco de dados `SistemaVendaGeek.db` a cada 24 horas.

### Funcionamento

- Os backups sao salvos na pasta `Backups/`
- Formato do nome: `SistemaVendaGeek_Backup_AAAAMMDD_HHMMSS.db`
- Exemplo: `SistemaVendaGeek_Backup_20260528_015612.db`
- O sistema mantem apenas os 10 backups mais recentes
- O arquivo `config_backup.txt` armazena a data/hora do ultimo backup realizado

### Backup manual

O metodo `DatabaseHelper.RealizarBackup()` pode ser chamado programaticamente para forcar um backup imediato.

---

## 9. COMO PUBLICAR UM EXECUTAVEL AUTOSSUFICIENTE

Para gerar um unico arquivo `.exe` que ja inclui o .NET Runtime (nao precisa instalar nada no computador do cliente):

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ./publicar
```

O executavel estara na pasta `publicar/`.

> O arquivo gerado tera aproximadamente 50-80 MB devido a inclusao do runtime.

---

## 10. PROBLEMAS CONHECIDOS E SOLUCOES

### Erro: "Unable to load DLL 'SQLite.Interop.dll'"

**Causa:** O pacote `System.Data.SQLite.Core` pode nao incluir automaticamente a biblioteca nativa.

**Solucao:** O projeto ja utiliza o pacote complementar `SQLitePCLRaw.bundle_e_sqlite3`. Nenhuma acao adicional e necessaria.

### Erro: "You must install .NET Desktop Runtime"

**Causa:** O computador nao possui o runtime necessario.

**Solucao:** Baixe e instale o .NET Desktop Runtime 8.0 em:  
[https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

### Aviso ao compilar: "NETSDK1138"

**Causa:** Versao do framework especificada como `net10.0-windows`, que ainda e uma versao de preview.

**Solucao:** Nao afeta a execucao do sistema. Para compilar sem avisos, altere o `TargetFramework` no arquivo `.csproj` para `net8.0-windows`.

---

## 11. DESENVOLVEDORES

| Nome | Papel | RA |
|------|-------|-----|
| Rayssa Luanna Gomes da Serra Vieira | Integradora | UP25119984 |
| Rodrigo Correa Farias | Regras de Negocio | UP25108816 |
| Caroline Moraes Monteiro | Banco de Dados | UP25116559 |
| Rosicléia Silva de Oliveira | Interface | UP25106074 |

---

**UNIP EaD - Polo Nazare - 2026**
