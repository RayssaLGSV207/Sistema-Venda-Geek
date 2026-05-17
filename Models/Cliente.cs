using System;

namespace SistemaVendaGeek.Models
{
    // Representa um cliente da loja.
    public class Cliente
    {
        // Código único do cliente (gerado automaticamente pelo banco).
        public int Id { get; set; }

        // Nome completo do cliente.
        public string Nome { get; set; }

        // CPF do cliente (11 dígitos, único).
        public string CPF { get; set; }

        // RG do cliente.
        public string RG { get; set; }

        // Data de cadastro do cliente.
        public DateTime DataCadastro { get; set; }

        // Endereço completo do cliente.
        public string Endereco { get; set; }

        // Telefone para contato.
        public string Telefone { get; set; }

        // E-mail do cliente.
        public string Email { get; set; }

        public Cliente()
        {
            DataCadastro = DateTime.Now;
        }

        public Cliente(string nome, string cpf, string telefone)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            DataCadastro = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Nome} - CPF: {CPF}";
        }
    }
}