using System;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Services
{
    public class VendaService
    {
        // Registra uma nova venda no banco de dados.
        public static string RegistrarVenda(string codigoBarras, int quantidade, string? clienteCPF = null)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sqlVerificar = "SELECT IsRaro, QuantidadeEstoque, Valor, Nome FROM Produto WHERE CodigoBarras = @codigo";
                    using (var cmd = new SQLiteCommand(sqlVerificar, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoBarras);
                        using (var leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                bool isRaro = Convert.ToInt32(leitor["IsRaro"]) == 1;
                                int estoque = Convert.ToInt32(leitor["QuantidadeEstoque"]);
                                decimal valor = Convert.ToDecimal(leitor["Valor"]);
                                string nome = leitor["Nome"].ToString();

                                if (isRaro && estoque < quantidade)
                                {
                                    MessageBox.Show($"Item raro '{nome}' não pode ser recomprado. Estoque insuficiente.",
                                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return null;
                                }

                                if (estoque < quantidade)
                                {
                                    MessageBox.Show($"Estoque insuficiente para o produto '{nome}'. Disponível: {estoque}",
                                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return null;
                                }

                                string codigoVenda = Guid.NewGuid().ToString();

                                string sqlRegistrar = @"INSERT INTO Venda (CodigoVenda, DataVenda, ValorTotal, Status, ClienteCPF) 
                                                VALUES (@codigoVenda, @dataVenda, @valorTotal, 'Pendente', @clienteCPF)";

                                using (var cmdRegistrar = new SQLiteCommand(sqlRegistrar, conn))
                                {
                                    cmdRegistrar.Parameters.AddWithValue("@codigoVenda", codigoVenda);
                                    cmdRegistrar.Parameters.AddWithValue("@dataVenda", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    cmdRegistrar.Parameters.AddWithValue("@valorTotal", valor * quantidade);
                                    cmdRegistrar.Parameters.AddWithValue("@clienteCPF", clienteCPF ?? "");
                                    cmdRegistrar.ExecuteNonQuery();
                                }

                                string sqlItem = @"INSERT INTO ItemVenda (CodigoVenda, CodigoBarras, Quantidade, PrecoUnitario) 
                                           VALUES (@codigoVenda, @codigoBarras, @quantidade, @precoUnitario)";

                                using (var cmdItem = new SQLiteCommand(sqlItem, conn))
                                {
                                    cmdItem.Parameters.AddWithValue("@codigoVenda", codigoVenda);
                                    cmdItem.Parameters.AddWithValue("@codigoBarras", codigoBarras);
                                    cmdItem.Parameters.AddWithValue("@quantidade", quantidade);
                                    cmdItem.Parameters.AddWithValue("@precoUnitario", valor);
                                    cmdItem.ExecuteNonQuery();
                                }

                                string sqlEstoque = "UPDATE Produto SET QuantidadeEstoque = QuantidadeEstoque - @quantidade WHERE CodigoBarras = @codigo";
                                using (var cmdEstoque = new SQLiteCommand(sqlEstoque, conn))
                                {
                                    cmdEstoque.Parameters.AddWithValue("@quantidade", quantidade);
                                    cmdEstoque.Parameters.AddWithValue("@codigo", codigoBarras);
                                    cmdEstoque.ExecuteNonQuery();
                                }

                                MessageBox.Show($"Venda registrada com sucesso!\nCódigo: {codigoVenda.Substring(0, 8)}",
                                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return codigoVenda;
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado. Verifique o código de barras.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show($"Erro ao registrar venda: {erro.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Cancela uma venda (para supervisor já logado - não pede credenciais)
        public static bool CancelarVenda(string codigoVenda)
        {
            if (!AuthService.IsSupervisor())
            {
                MessageBox.Show("Apenas supervisor pode cancelar vendas.", "Permissão Negada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Venda SET Status = 'Cancelada' WHERE CodigoVenda = @codigo";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoVenda);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Venda cancelada com sucesso!", "Cancelamento",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cancelar venda: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Cancela uma venda (com validação de credenciais de supervisor)
        public static bool CancelarVenda(string codigoVenda, string supervisorLogin, string supervisorSenha)
        {
            // Só continua se as credenciais forem de supervisor
            if (!AuthService.ValidarCredenciaisSupervisor(supervisorLogin, supervisorSenha))
            {
                MessageBox.Show("Apenas supervisor pode cancelar vendas.", "Permissão Negada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Venda SET Status = 'Cancelada' WHERE CodigoVenda = @codigo";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoVenda);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Venda cancelada com sucesso!", "Cancelamento",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cancelar venda: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Atualiza o status da venda
        public static bool AtualizarStatusVenda(string codigoVenda, string novoStatus)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Venda SET Status = @status WHERE CodigoVenda = @codigo";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", novoStatus);
                        cmd.Parameters.AddWithValue("@codigo", codigoVenda);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao atualizar status: " + erro.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}