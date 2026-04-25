using SistemaVendaGeek.Forms;

namespace SistemaVendaGeek;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmLogin());
    }
}

//