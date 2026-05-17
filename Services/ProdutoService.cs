using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Services
{
    public class ProdutoService
    {
        // Busca o preço de um produto pelo código de barras.
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
                                MessageBox.Show("Produto não encontrado.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao consultar preço: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Cadastra um novo produto no banco. Apenas Estoquista pode fazer isso.
        public static bool CadastrarProduto(
            string codigoBarras, string nome, string categoria,
            string fabricante, int quantidade, decimal valor,
            bool isRaro, string plataforma, int garantiaMeses,
            string perfilUsuario)
        {
            // Verifica se quem está tentando cadastrar é Estoquista
            if (perfilUsuario != "Estoquista" && perfilUsuario != "Supervisor")
            {
                MessageBox.Show("Apenas o estoquista ou supervisor pode cadastrar produtos.",
                    "Permissão Negada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Código de barras já cadastrado.", "Erro",
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

        // Busca todas as informações de um produto pelo código de barras.
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

        // Aumenta ou diminui o estoque de um produto.
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

        // Lista todos os produtos cadastrados no banco de dados
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

    // Classe simples para carregar os dados de um produto.
    public class ProdutoInfo
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Fabricante { get; set; }
        public int QuantidadeEstoque { get; set; }
        public decimal Valor { get; set; }
        public bool IsRaro { get; set; }
        public string Plataforma { get; set; }
        public int PrazoGarantia { get; set; }
    }
}