using System;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaVendaGeek.Services;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Services
{
    public class AuthService
    {
        private static UsuarioAutenticado? _usuarioLogado;

        /// <summary>
        /// Autentica um usuario no sistema verificando login e senha no banco de dados
        /// </summary>
        /// <param name="login">Nome de usuario para autenticacao</param>
        /// <param name="senha">Senha do usuario</param>
        /// <param name="perfil">Retorna o perfil do usuario autenticado</param>
        /// <returns>True se autenticacao for bem sucedida, False caso contrario</returns>
        public static bool Autenticar(string login, string senha, out string perfil)
        {
            perfil = "";
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Perfil FROM Usuario WHERE Login = @login AND Senha = @senha";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@senha", senha);

                        var resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            perfil = resultado.ToString() ?? "";
                            _usuarioLogado = new UsuarioAutenticado
                            {
                                Login = login,
                                Perfil = perfil
                            };
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (SQLiteException erro)
            {
                MessageBox.Show("Erro ao acessar o banco de dados: " + erro.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro inesperado: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Verifica se o usuario logado atualmente possui perfil de Supervisor
        /// </summary>
        /// <returns>True se for Supervisor, False caso contrario</returns>
        public static bool IsSupervisor()
        {
            return _usuarioLogado?.Perfil == "Supervisor";
        }

        /// <summary>
        /// Verifica se o usuario logado atualmente possui perfil de Estoquista
        /// </summary>
        /// <returns>True se for Estoquista, False caso contrario</returns>
        public static bool IsEstoquista()
        {
            return _usuarioLogado?.Perfil == "Estoquista";
        }

        /// <summary>
        /// Verifica se o usuario logado atualmente possui perfil de Atendente
        /// </summary>
        /// <returns>True se for Atendente, False caso contrario</returns>
        public static bool IsAtendente()
        {
            return _usuarioLogado?.Perfil == "Atendente";
        }

        /// <summary>
        /// Valida se as credenciais fornecidas pertencem a um usuario com perfil Supervisor
        /// </summary>
        /// <param name="login">Login do supervisor</param>
        /// <param name="senha">Senha do supervisor</param>
        /// <returns>True se as credenciais forem validas e pertencerem a um supervisor</returns>
        public static bool ValidarCredenciaisSupervisor(string login, string senha)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Perfil FROM Usuario WHERE Login = @login AND Senha = @senha AND Perfil = 'Supervisor'";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@senha", senha);
                        return cmd.ExecuteScalar() != null;
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao validar credenciais: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Realiza logout do usuario atual, limpando os dados da sessao
        /// </summary>
        public static void Logout()
        {
            _usuarioLogado = null;
        }
    }

    public class UsuarioAutenticado
    {
        public string Login { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
    }
}