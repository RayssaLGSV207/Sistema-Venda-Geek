using System.Windows.Forms;
using SistemaVendaGeek.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // cria o banco de dados na inicialização
        DatabaseHelper.CriarBanco();

        Application.Run(new FrmLogin());
    }
}