using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendaGeek.Models
{
    public class Venda
    {
        public string CodigoVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemVenda> Itens { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; }
        public string StatusPagamento { get; set; }
        public string StatusVenda { get; set; }

        public Venda()
        {
            CodigoVenda = Guid.NewGuid().ToString();
            DataVenda = DateTime.Now;
            Itens = new List<ItemVenda>();
            StatusVenda = "Aberta";
            StatusPagamento = "Pendente";
            ValorTotal = 0;
        }

        public Venda(Cliente cliente) : this()
        {
            Cliente = cliente;
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            var itemExistente = Itens.FirstOrDefault(i => i.Produto.CodigoBarras == produto.CodigoBarras);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
                itemExistente.Subtotal = itemExistente.Quantidade * itemExistente.PrecoUnitario;
            }
            else
            {
                Itens.Add(new ItemVenda
                {
                    Produto = produto,
                    Quantidade = quantidade,
                    PrecoUnitario = produto.Valor,
                    Subtotal = produto.Valor * quantidade
                });
            }

            CalcularTotal();
        }

        public void RemoverItem(string codigoBarras)
        {
            var item = Itens.FirstOrDefault(i => i.Produto.CodigoBarras == codigoBarras);
            if (item != null)
            {
                Itens.Remove(item);
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            ValorTotal = Itens.Sum(i => i.Subtotal);
        }

        public void Finalizar(string formaPagamento)
        {
            FormaPagamento = formaPagamento;
            StatusPagamento = "Pago";
            StatusVenda = "Finalizada";
        }

        public void Cancelar()
        {
            StatusPagamento = "Cancelado";
            StatusVenda = "Cancelada";
        }

        public string ObterResumo()
        {
            return $"Venda: {CodigoVenda.Substring(0, 8)} | Data: {DataVenda:dd/MM/yyyy HH:mm} | Total: R$ {ValorTotal:F2} | Status: {StatusVenda}";
        }

        public string ObterDetalhesItens()
        {
            if (Itens.Count == 0)
                return "Nenhum item na venda.";

            string detalhes = "";
            foreach (var item in Itens)
            {
                detalhes += $"{item.Quantidade}x {item.Produto.Nome} - R$ {item.Subtotal:F2}\n";
            }
            detalhes += $"Total: R$ {ValorTotal:F2}";
            return detalhes;
        }
    }

    public class ItemVenda
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}