using System;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaVendaGeek.Services;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Services
{
    public class AuthService
    {
        // Guarda o usuário que está logado no momento
        private static UsuarioAutenticado _usuarioLogado;

        // Tenta fazer login com usuário e senha. Retorna true se conseguir e devolve o perfil.
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
                            perfil = resultado.ToString();
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

        //Verifica se o usuário logado é Supervisor.
        public static bool IsSupervisor()
        {
            return _usuarioLogado?.Perfil == "Supervisor";
        }

        //Verifica se o usuário logado é Estoquista.
        public static bool IsEstoquista()
        {
            return _usuarioLogado?.Perfil == "Estoquista";
        }

        // Verifica se o usuário logado é Atendente.
        public static bool IsAtendente()
        {
            return _usuarioLogado?.Perfil == "Atendente";
        }

        //Verifica se um login/senha específico pertence a um Supervisor.
        // Usado para ações que exigem confirmação do supervisor.
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

        // Faz logout do usuário atual.
        public static void Logout()
        {
            _usuarioLogado = null;
        }
    }

    // Guarda os dados do usuário que está logado.
    public class UsuarioAutenticado
    {
        public string Login { get; set; }
        public string Perfil { get; set; }
    }
}
