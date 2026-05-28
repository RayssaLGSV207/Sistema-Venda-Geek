using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace SistemaVendaGeek.Database
{
    public static class DatabaseHelper
    {
        private static string _connectionString = "Data Source=SistemaVendaGeek.db;Version=3;";

        public static void CriarBanco()
        {
            try
            {
                if (!File.Exists("SistemaVendaGeek.db"))
                {
                    SQLiteConnection.CreateFile("SistemaVendaGeek.db");
                    CriarTabelas();
                    InserirDadosTeste();
                    MessageBox.Show("Banco de dados criado com 35 produtos, 3 usuarios e 20 clientes!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message, "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CriarTabelas()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                string sqlUsuario = @"
                    CREATE TABLE Usuario (
                        Login TEXT PRIMARY KEY,
                        Senha TEXT NOT NULL,
                        Perfil TEXT NOT NULL
                    )";
                using (var cmd = new SQLiteCommand(sqlUsuario, conn))
                    cmd.ExecuteNonQuery();

                string sqlProduto = @"
                    CREATE TABLE Produto (
                        CodigoBarras TEXT PRIMARY KEY,
                        Nome TEXT NOT NULL,
                        Categoria TEXT,
                        Fabricante TEXT,
                        Valor DECIMAL(10,2) NOT NULL,
                        QuantidadeEstoque INTEGER DEFAULT 0,
                        IsRaro INTEGER DEFAULT 0,
                        Plataforma TEXT,
                        PrazoGarantia INTEGER DEFAULT 12
                    )";
                using (var cmd = new SQLiteCommand(sqlProduto, conn))
                    cmd.ExecuteNonQuery();

                string sqlCliente = @"
                    CREATE TABLE Cliente (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        CPF TEXT UNIQUE,
                        RG TEXT,
                        DataCadastro TEXT,
                        Endereco TEXT,
                        Telefone TEXT,
                        Email TEXT
                    )";
                using (var cmd = new SQLiteCommand(sqlCliente, conn))
                    cmd.ExecuteNonQuery();

                string sqlVenda = @"
                    CREATE TABLE Venda (
                        CodigoVenda TEXT PRIMARY KEY,
                        DataVenda DATETIME DEFAULT CURRENT_TIMESTAMP,
                        ValorTotal DECIMAL(10,2) DEFAULT 0,
                        Status TEXT DEFAULT 'Pendente',
                        FormaPagamento TEXT,
                        ClienteCPF TEXT
                    )";
                using (var cmd = new SQLiteCommand(sqlVenda, conn))
                    cmd.ExecuteNonQuery();

                string sqlItemVenda = @"
                    CREATE TABLE ItemVenda (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CodigoVenda TEXT NOT NULL,
                        CodigoBarras TEXT NOT NULL,
                        Quantidade INTEGER NOT NULL,
                        PrecoUnitario DECIMAL(10,2) NOT NULL,
                        FOREIGN KEY (CodigoVenda) REFERENCES Venda(CodigoVenda),
                        FOREIGN KEY (CodigoBarras) REFERENCES Produto(CodigoBarras)
                    )";
                using (var cmd = new SQLiteCommand(sqlItemVenda, conn))
                    cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        private static void InserirDadosTeste()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                string sqlUsuarios = @"
                    INSERT OR IGNORE INTO Usuario (Login, Senha, Perfil) VALUES
                    ('atendente', '123', 'Atendente'),
                    ('supervisor', '123', 'Supervisor'),
                    ('estoquista', '123', 'Estoquista')";
                using (var cmd = new SQLiteCommand(sqlUsuarios, conn))
                    cmd.ExecuteNonQuery();

                string sqlProdutos = @"
                    INSERT OR IGNORE INTO Produto (CodigoBarras, Nome, Categoria, Fabricante, Valor, QuantidadeEstoque, IsRaro, Plataforma, PrazoGarantia) VALUES
                    ('JOGO001', 'The Legend of Zelda: Tears of the Kingdom', 'Jogo', 'Nintendo', 349.90, 25, 0, 'Switch', 12),
                    ('JOGO002', 'God of War Ragnarok', 'Jogo', 'Sony', 299.90, 30, 0, 'PS5', 12),
                    ('JOGO003', 'Hogwarts Legacy', 'Jogo', 'Warner Bros', 249.90, 20, 0, 'Multiplataforma', 12),
                    ('JOGO004', 'Elden Ring', 'Jogo', 'Bandai Namco', 199.90, 15, 0, 'Multiplataforma', 12),
                    ('JOGO005', 'Spider-Man 2', 'Jogo', 'Sony', 349.90, 18, 0, 'PS5', 12),
                    ('JOGO006', 'Super Mario Wonder', 'Jogo', 'Nintendo', 299.90, 22, 0, 'Switch', 12),
                    ('JOGO007', 'Final Fantasy XVI', 'Jogo', 'Square Enix', 279.90, 12, 0, 'PS5', 12),
                    ('JOGO008', 'Starfield', 'Jogo', 'Bethesda', 299.90, 10, 0, 'Xbox/PC', 12),
                    ('JOGO009', 'Street Fighter 6', 'Jogo', 'Capcom', 249.90, 20, 0, 'Multiplataforma', 12),
                    ('JOGO010', 'Resident Evil 4 Remake', 'Jogo', 'Capcom', 199.90, 25, 0, 'Multiplataforma', 12),
                    ('RARE001', 'Zelda Ocarina of Time - Edicao Colecionador', 'Jogo', 'Nintendo', 899.90, 2, 1, 'N64/Switch', 0),
                    ('RARE002', 'Super Mario 64 - Lacrado', 'Jogo', 'Nintendo', 1499.90, 1, 1, 'N64', 0),
                    ('RARE003', 'Pokemon Red/Blue - Primeira Edicao', 'Jogo', 'Nintendo', 2499.90, 1, 1, 'Game Boy', 0),
                    ('RARE004', 'Funko Pop Chase - Edicao Limitada', 'Produto Geek', 'Funko', 599.90, 3, 1, 'Colecionador', 0),
                    ('RARE005', 'Action Figure Spider-Man - Autografada', 'Produto Geek', 'Hasbro', 799.90, 2, 1, 'Colecionador', 0),
                    ('ACC001', 'Controle Sem Fio DualSense', 'Acessorio', 'Sony', 449.90, 15, 0, 'PS5', 6),
                    ('ACC002', 'Controle Pro Switch', 'Acessorio', 'Nintendo', 399.90, 12, 0, 'Switch', 6),
                    ('ACC003', 'Headset Gamer HyperX', 'Acessorio', 'HyperX', 299.90, 20, 0, 'Multiplataforma', 6),
                    ('ACC004', 'Teclado Mecanico RGB', 'Acessorio', 'Logitech', 349.90, 10, 0, 'PC', 12),
                    ('ACC005', 'Mouse Gamer Logitech G502', 'Acessorio', 'Logitech', 249.90, 18, 0, 'PC', 12),
                    ('ACC006', 'Cadeira Gamer', 'Acessorio', 'DT3', 1299.90, 5, 0, 'Gamer', 12),
                    ('ACC007', 'Suporte de Parede para Controle', 'Acessorio', 'Generic', 49.90, 30, 0, 'Gamer', 3),
                    ('ACC008', 'Carregador Portatil Switch', 'Acessorio', 'Nintendo', 199.90, 8, 0, 'Switch', 6),
                    ('ACC009', 'Fone de Ouvido Gamer', 'Acessorio', 'Razer', 329.90, 15, 0, 'Multiplataforma', 6),
                    ('ACC010', 'Mousepad RGB', 'Acessorio', 'Redragon', 89.90, 25, 0, 'PC', 3),
                    ('GEEK001', 'Caneca Stormtropper', 'Produto Geek', 'Star Wars', 49.90, 50, 0, 'N/A', 3),
                    ('GEEK002', 'Camiseta Gamer - I Love Games', 'Produto Geek', 'Geek Store', 79.90, 40, 0, 'N/A', 3),
                    ('GEEK003', 'Capa para Notebook Gamer', 'Produto Geek', 'Razer', 129.90, 15, 0, 'N/A', 6),
                    ('GEEK004', 'Chaveiro LED Mario Bros', 'Produto Geek', 'Nintendo', 19.90, 100, 0, 'N/A', 3),
                    ('GEEK005', 'Pelucia Pikachu 30cm', 'Produto Geek', 'Pokemon', 89.90, 25, 0, 'N/A', 3)";

                using (var cmd = new SQLiteCommand(sqlProdutos, conn))
                    cmd.ExecuteNonQuery();

                string sqlClientes = @"
                    INSERT OR IGNORE INTO Cliente (Nome, CPF, RG, DataCadastro, Endereco, Telefone, Email) VALUES
                    ('Joao Silva', '11122233344', 'MG11122233', date('now'), 'Rua das Flores, 100 - Centro, Sao Paulo/SP', '(11) 91234-5678', 'joao.silva@email.com'),
                    ('Maria Oliveira', '22233344455', 'SP22233344', date('now'), 'Av. Paulista, 1000 - Bela Vista, Sao Paulo/SP', '(11) 92345-6789', 'maria.oliveira@email.com'),
                    ('Pedro Santos', '33344455566', 'RJ33344455', date('now'), 'Rua do Ouvidor, 50 - Centro, Rio de Janeiro/RJ', '(21) 93456-7890', 'pedro.santos@email.com'),
                    ('Ana Costa', '44455566677', 'MG44455566', date('now'), 'Rua da Bahia, 500 - Centro, Belo Horizonte/MG', '(31) 94567-8901', 'ana.costa@email.com'),
                    ('Carlos Ferreira', '55566677788', 'RS55566677', date('now'), 'Av. Protasio Alves, 200 - Porto Alegre/RS', '(51) 95678-9012', 'carlos.ferreira@email.com'),
                    ('Juliana Lima', '66677788899', 'PR66677788', date('now'), 'Rua XV de Novembro, 300 - Curitiba/PR', '(41) 96789-0123', 'juliana.lima@email.com'),
                    ('Roberto Alves', '77788899900', 'SC77788899', date('now'), 'Av. Beira Mar, 1500 - Florianopolis/SC', '(48) 97890-1234', 'roberto.alves@email.com'),
                    ('Patricia Souza', '88899900011', 'BA88899900', date('now'), 'Rua Chile, 80 - Salvador/BA', '(71) 98901-2345', 'patricia.souza@email.com'),
                    ('Fernando Rocha', '99900011122', 'PE99900011', date('now'), 'Rua Aurora, 1200 - Recife/PE', '(81) 99012-3456', 'fernando.rocha@email.com'),
                    ('Cristina Martins', '00011122233', 'CE00011122', date('now'), 'Av. Bezerra de Menezes, 800 - Fortaleza/CE', '(85) 90123-4567', 'cristina.martins@email.com'),
                    ('Ricardo Gomes', '11133355577', 'GO11133355', date('now'), 'Rua 4, 200 - Setor Marista, Goiania/GO', '(62) 91234-5678', 'ricardo.gomes@email.com'),
                    ('Amanda Ribeiro', '22244466688', 'DF22244466', date('now'), 'SQS 308, Bloco A - Brasilia/DF', '(61) 92345-6789', 'amanda.ribeiro@email.com'),
                    ('Marcelo Lima', '33355577799', 'PA33355577', date('now'), 'Rua dos Mundurucus, 150 - Belem/PA', '(91) 93456-7890', 'marcelo.lima@email.com'),
                    ('Tatiana Mendes', '44466688800', 'AM44466688', date('now'), 'Av. Eduardo Ribeiro, 500 - Manaus/AM', '(92) 94567-8901', 'tatiana.mendes@email.com'),
                    ('Andre Cardoso', '55577799911', 'ES55577799', date('now'), 'Rua da Lama, 300 - Vitoria/ES', '(27) 95678-9012', 'andre.cardoso@email.com'),
                    ('Luciana Nunes', '66688800022', 'MS66688800', date('now'), 'Rua 14 de Julho, 800 - Campo Grande/MS', '(67) 96789-0123', 'luciana.nunes@email.com'),
                    ('Gustavo Ramos', '77799911133', 'MT77799911', date('now'), 'Av. Getulio Vargas, 100 - Cuiaba/MT', '(65) 97890-1234', 'gustavo.ramos@email.com'),
                    ('Vanessa Dias', '88800022244', 'RN88800022', date('now'), 'Rua Chile, 400 - Natal/RN', '(84) 98901-2345', 'vanessa.dias@email.com'),
                    ('Thiago Araujo', '99911133355', 'PB99911133', date('now'), 'Rua Diogo Velho, 200 - Joao Pessoa/PB', '(83) 99012-3456', 'thiago.araujo@email.com'),
                    ('Renata Carvalho', '00022244466', 'SE00022244', date('now'), 'Rua Sao Cristovao, 100 - Aracaju/SE', '(79) 90123-4567', 'renata.carvalho@email.com')";
                    
                using (var cmd = new SQLiteCommand(sqlClientes, conn))
                    cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        /// <summary>
        /// Cria e retorna uma nova conexao com o banco de dados
        /// </summary>
        /// <returns>Nova instancia de SQLiteConnection</returns>
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        /// <summary>
        /// Realiza backup do banco de dados na pasta Backups
        /// </summary>
        public static void RealizarBackup()
        {
            try
            {
                string bancoOriginal = "SistemaVendaGeek.db";
                if (!File.Exists(bancoOriginal))
                {
                    return;
                }

                string dataAtual = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nomeBackup = $"SistemaVendaGeek_Backup_{dataAtual}.db";
                
                string pastaBackup = "Backups";
                if (!Directory.Exists(pastaBackup))
                {
                    Directory.CreateDirectory(pastaBackup);
                }
                
                string caminhoBackup = Path.Combine(pastaBackup, nomeBackup);
                File.Copy(bancoOriginal, caminhoBackup, true);
                
                var backupsExistentes = Directory.GetFiles(pastaBackup, "SistemaVendaGeek_Backup_*.db");
                if (backupsExistentes.Length > 10)
                {
                    for (int i = 0; i < backupsExistentes.Length - 10; i++)
                    {
                        File.Delete(backupsExistentes[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar backup: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Realiza backup automatico se passaram mais de 24 horas desde o ultimo backup
        /// </summary>
        public static void RealizarBackupAutomatico()
        {
            try
            {
                string arquivoConfig = "config_backup.txt";
                DateTime ultimoBackup = DateTime.MinValue;
                
                if (File.Exists(arquivoConfig))
                {
                    string conteudo = File.ReadAllText(arquivoConfig);
                    if (DateTime.TryParse(conteudo, out ultimoBackup))
                    {
                        if ((DateTime.Now - ultimoBackup).TotalHours < 24)
                        {
                            return;
                        }
                    }
                }
                
                RealizarBackup();
                File.WriteAllText(arquivoConfig, DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no backup automatico: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}