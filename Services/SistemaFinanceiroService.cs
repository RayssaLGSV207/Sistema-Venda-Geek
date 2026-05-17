using System;
using System.IO;
using System.Windows.Forms;

namespace SistemaVendaGeek.Services
{
    // Responsável por se comunicar com o sistema financeiro da empresa.
    public static class SistemaFinanceiroService
    {
        // Arquivo de log que simula o registro no sistema financeiro
        private static readonly string ARQUIVO_LOG = "sistema_financeiro.log";

        // Notifica o sistema financeiro sobre o cancelamento de uma venda.
        public static bool EnviarCancelamento(string codigoVenda)
        {
            try
            {
                string mensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Venda CANCELADA: {codigoVenda} - Enviada ao financeiro";

                // Salva no arquivo de log (simula a integração)
                File.AppendAllText(ARQUIVO_LOG, mensagem + Environment.NewLine);

                MessageBox.Show($"Venda {codigoVenda.Substring(0, 8)} cancelada.\n" +
                    "Código enviado ao sistema financeiro com sucesso!",
                    "Sistema Financeiro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao comunicar sistema financeiro: " + erro.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Verifica no log se o cancelamento já foi registrado.
        public static string ConsultarStatus(string codigoVenda)
        {
            if (File.Exists(ARQUIVO_LOG))
            {
                string log = File.ReadAllText(ARQUIVO_LOG);
                if (log.Contains(codigoVenda))
                    return "Cancelamento registrado no sistema financeiro";
            }
            return "Cancelamento não encontrado";
        }

        // Registra uma venda finalizada no sistema financeiro.
        public static bool RegistrarVendaFinanceiro(string codigoVenda, decimal valorTotal, string formaPagamento)
        {
            try
            {
                string mensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Venda FINALIZADA: {codigoVenda} | Valor: R$ {valorTotal:F2} | Pagamento: {formaPagamento}";
                File.AppendAllText(ARQUIVO_LOG, mensagem + Environment.NewLine);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}