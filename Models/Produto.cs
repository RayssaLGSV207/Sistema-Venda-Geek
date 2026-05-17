using System;

namespace SistemaVendaGeek.Models
{
    // Representa um produto vendido na loja.
    public class Produto
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Fabricante { get; set; }
        public int QuantidadeEstoque { get; set; }
        public decimal Valor { get; set; }
        public bool IsRaro { get; set; }
        public string Plataforma { get; set; }
        public int PrazoGarantia { get; set; }

        public Produto() { }

        public Produto(string codigoBarras, string nome, string categoria, decimal valor, int quantidadeEstoque)
        {
            CodigoBarras = codigoBarras;
            Nome = nome;
            Categoria = categoria;
            Valor = valor;
            QuantidadeEstoque = quantidadeEstoque;
            IsRaro = false;
            PrazoGarantia = 12;
        }

        public string ValorFormatado => $"R$ {Valor:F2}";

        public override string ToString()
        {
            return $"{Nome} - {ValorFormatado} (Estoque: {QuantidadeEstoque})";
        }
    }
}
