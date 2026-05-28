using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Services
{
    public class ClienteService
    {
        /// <summary>
        /// Lista todos os clientes cadastrados no sistema
        /// </summary>
        /// <returns>Lista de objetos ClienteInfo com dados dos clientes</returns>
        public static List<ClienteInfo> ListarTodosClientes()
        {
            var clientes = new List<ClienteInfo>();

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Id, Nome, CPF, Telefone, Email FROM Cliente ORDER BY Nome";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var leitor = cmd.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            clientes.Add(new ClienteInfo
                            {
                                Id = Convert.ToInt32(leitor["Id"]),
                                Nome = leitor["Nome"].ToString(),
                                CPF = leitor["CPF"].ToString(),
                                Telefone = leitor["Telefone"].ToString(),
                                Email = leitor["Email"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao listar clientes: " + erro.Message, "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return clientes;
        }

        /// <summary>
        /// Busca um cliente pelo CPF
        /// </summary>
        /// <param name="cpf">CPF do cliente a ser buscado</param>
        /// <returns>Objeto ClienteInfo com dados do cliente ou null se nao encontrado</returns>
        public static ClienteInfo BuscarClientePorCPF(string cpf)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Id, Nome, CPF, Telefone, Email, Endereco FROM Cliente WHERE CPF = @cpf";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@cpf", cpf);
                        using (var leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                return new ClienteInfo
                                {
                                    Id = Convert.ToInt32(leitor["Id"]),
                                    Nome = leitor["Nome"].ToString(),
                                    CPF = leitor["CPF"].ToString(),
                                    Telefone = leitor["Telefone"].ToString(),
                                    Email = leitor["Email"].ToString(),
                                    Endereco = leitor["Endereco"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao buscar cliente: " + erro.Message, "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }

    public class ClienteInfo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public string DataCadastro { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}