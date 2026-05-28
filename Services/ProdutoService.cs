using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Services
{
    public class ProdutoService
    {
        /// <summary>
        /// Consulta o preco de um produto pelo codigo de barras
        /// </summary>
        /// <param name="codigoBarras">Codigo de barras do produto</param>
        /// <returns>Preco do produto ou zero se nao encontrado</returns>
        public static decimal ConsultarPreco(string codigoBarras)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Valor, Nome FROM Produto WHERE CodigoBarras = @codigo";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                        using (var leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                return Convert.ToDecimal(leitor["Valor"]);
                            }
                            else
                            {
                                MessageBox.Show("Produto nao encontrado.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao consultar preco: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        /// <summary>
        /// Cadastra um novo produto no sistema
        /// </summary>
        /// <param name="codigoBarras">Codigo de barras do produto</param>
        /// <param name="nome">Nome do produto</param>
        /// <param name="categoria">Categoria do produto (Jogo, Acessorio, Produto Geek)</param>
        /// <param name="fabricante">Fabricante do produto</param>
        /// <param name="quantidade">Quantidade inicial em estoque</param>
        /// <param name="valor">Valor do produto</param>
        /// <param name="isRaro">Indica se o produto e raro</param>
        /// <param name="plataforma">Plataforma do produto</param>
        /// <param name="garantiaMeses">Prazo de garantia em meses</param>
        /// <param name="perfilUsuario">Perfil do usuario que esta cadastrando</param>
        /// <returns>True se o cadastro foi bem sucedido, False caso contrario</returns>
        public static bool CadastrarProduto(
            string codigoBarras, string nome, string categoria,
            string fabricante, int quantidade, decimal valor,
            bool isRaro, string plataforma, int garantiaMeses,
            string perfilUsuario)
        {
            if (perfilUsuario != "Estoquista" && perfilUsuario != "Supervisor")
            {
                MessageBox.Show("Apenas o estoquista ou supervisor pode cadastrar produtos.",
                    "Permissao Negada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Produto (CodigoBarras, Nome, Categoria, Fabricante, 
                                            QuantidadeEstoque, Valor, IsRaro, Plataforma, PrazoGarantia)
                        VALUES (@codigo, @nome, @categoria, @fabricante, @estoque, @valor, @isRaro, @plataforma, @garantia)";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.Parameters.AddWithValue("@fabricante", fabricante ?? "");
                        cmd.Parameters.AddWithValue("@estoque", quantidade);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.Parameters.AddWithValue("@isRaro", isRaro ? 1 : 0);
                        cmd.Parameters.AddWithValue("@plataforma", plataforma ?? "");
                        cmd.Parameters.AddWithValue("@garantia", garantiaMeses);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Produto '{nome}' cadastrado com sucesso!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (SQLiteException erro)
            {
                if (erro.Message.Contains("UNIQUE"))
                {
                    MessageBox.Show("Codigo de barras ja cadastrado.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Erro no banco de dados: " + erro.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar produto: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Busca todas as informacoes de um produto pelo codigo de barras
        /// </summary>
        /// <param name="codigoBarras">Codigo de barras do produto</param>
        /// <returns>Objeto ProdutoInfo com dados do produto ou null se nao encontrado</returns>
        public static ProdutoInfo BuscarProduto(string codigoBarras)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM Produto WHERE CodigoBarras = @codigo";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                        using (var leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                return new ProdutoInfo
                                {
                                    CodigoBarras = leitor["CodigoBarras"].ToString(),
                                    Nome = leitor["Nome"].ToString(),
                                    Categoria = leitor["Categoria"].ToString(),
                                    Fabricante = leitor["Fabricante"].ToString(),
                                    QuantidadeEstoque = Convert.ToInt32(leitor["QuantidadeEstoque"]),
                                    Valor = Convert.ToDecimal(leitor["Valor"]),
                                    IsRaro = Convert.ToInt32(leitor["IsRaro"]) == 1,
                                    Plataforma = leitor["Plataforma"].ToString(),
                                    PrazoGarantia = Convert.ToInt32(leitor["PrazoGarantia"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao buscar produto: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Atualiza a quantidade em estoque de um produto
        /// </summary>
        /// <param name="codigoBarras">Codigo de barras do produto</param>
        /// <param name="quantidade">Quantidade a ser adicionada ou removida</param>
        /// <param name="isAdicionar">True para adicionar ao estoque, False para remover</param>
        /// <returns>True se a atualizacao foi bem sucedida, False caso contrario</returns>
        public static bool AtualizarEstoque(string codigoBarras, int quantidade, bool isAdicionar)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql;
                    if (isAdicionar)
                        sql = "UPDATE Produto SET QuantidadeEstoque = QuantidadeEstoque + @qtd WHERE CodigoBarras = @codigo";
                    else
                        sql = "UPDATE Produto SET QuantidadeEstoque = QuantidadeEstoque - @qtd WHERE CodigoBarras = @codigo";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@qtd", quantidade);
                        cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao atualizar estoque: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Lista todos os produtos cadastrados no banco de dados
        /// </summary>
        /// <returns>Lista de objetos ProdutoInfo com todos os produtos</returns>
        public static List<ProdutoInfo> ListarTodosProdutos()
        {
            var produtos = new List<ProdutoInfo>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM Produto ORDER BY Nome";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var leitor = cmd.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            produtos.Add(new ProdutoInfo
                            {
                                CodigoBarras = leitor["CodigoBarras"].ToString(),
                                Nome = leitor["Nome"].ToString(),
                                Categoria = leitor["Categoria"].ToString(),
                                Fabricante = leitor["Fabricante"]?.ToString() ?? "",
                                QuantidadeEstoque = Convert.ToInt32(leitor["QuantidadeEstoque"]),
                                Valor = Convert.ToDecimal(leitor["Valor"]),
                                IsRaro = Convert.ToInt32(leitor["IsRaro"]) == 1,
                                Plataforma = leitor["Plataforma"]?.ToString() ?? "",
                                PrazoGarantia = Convert.ToInt32(leitor["PrazoGarantia"])
                            });
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao listar produtos: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return produtos;
        }
    }

    public class ProdutoInfo
    {
        public string CodigoBarras { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
        public decimal Valor { get; set; }
        public bool IsRaro { get; set; }
        public string Plataforma { get; set; } = string.Empty;
        public int PrazoGarantia { get; set; }
    }
}