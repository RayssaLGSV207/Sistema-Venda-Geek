using System;
using System.IO;
using System.Windows.Forms;

namespace SistemaVendaGeek.Services
{
    public static class SistemaFinanceiroService
    {
        private static readonly string ARQUIVO_LOG = "sistema_financeiro.log";

        /// <summary>
        /// Notifica o sistema financeiro sobre o cancelamento de uma venda
        /// </summary>
        /// <param name="codigoVenda">Codigo da venda cancelada</param>
        /// <returns>True se o registro foi bem sucedido, False caso contrario</returns>
        public static bool EnviarCancelamento(string codigoVenda)
        {
            try
            {
                string mensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Venda CANCELADA: {codigoVenda} - Enviada ao financeiro";

                File.AppendAllText(ARQUIVO_LOG, mensagem + Environment.NewLine);

                MessageBox.Show($"Venda {codigoVenda.Substring(0, 8)} cancelada.\n" +
                    "Codigo enviado ao sistema financeiro com sucesso!",
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

        /// <summary>
        /// Consulta no log se o cancelamento ja foi registrado no sistema financeiro
        /// </summary>
        /// <param name="codigoVenda">Codigo da venda a ser consultada</param>
        /// <returns>Mensagem com o status da consulta</returns>
        public static string ConsultarStatus(string codigoVenda)
        {
            if (File.Exists(ARQUIVO_LOG))
            {
                string log = File.ReadAllText(ARQUIVO_LOG);
                if (log.Contains(codigoVenda))
                    return "Cancelamento registrado no sistema financeiro";
            }
            return "Cancelamento nao encontrado";
        }

        /// <summary>
        /// Registra uma venda finalizada no sistema financeiro
        /// </summary>
        /// <param name="codigoVenda">Codigo da venda finalizada</param>
        /// <param name="valorTotal">Valor total da venda</param>
        /// <param name="formaPagamento">Forma de pagamento utilizada</param>
        /// <returns>True se o registro foi bem sucedido, False caso contrario</returns>
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