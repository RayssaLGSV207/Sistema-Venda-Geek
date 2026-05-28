using System;

namespace SistemaVendaGeek.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Cliente()
        {
            DataCadastro = DateTime.Now;
        }

        public Cliente(string nome, string cpf, string telefone)
        {
            Nome = nome ?? string.Empty;
            CPF = cpf ?? string.Empty;
            Telefone = telefone ?? string.Empty;
            DataCadastro = DateTime.Now;
            RG = string.Empty;
            Endereco = string.Empty;
            Email = string.Empty;
        }

        public override string ToString()
        {
            return $"{Nome} - CPF: {CPF}";
        }
    }
}