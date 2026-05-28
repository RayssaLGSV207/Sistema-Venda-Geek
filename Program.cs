using System;
using System.Windows.Forms;
using SistemaVendaGeek.Database;
using SistemaVendaGeek.Forms;

namespace SistemaVendaGeek
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            DatabaseHelper.CriarBanco();
            
            DatabaseHelper.RealizarBackupAutomatico();
            
            Application.Run(new FrmLogin());
        }
    }
}