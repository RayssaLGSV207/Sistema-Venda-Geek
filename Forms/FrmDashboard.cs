using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmDashboard  : Form
    {
        private Button btnNovaVenda;
        private Button btnConsultarPreco;
        private Button btnCadastrarProduto;
        private Button btnSair;
        private Label lblBoasVindas;
        private string _usuarioLogado;
        private string _perfilLogado;

        public FrmDashboard(string usuario, string perfil)
        {
            _usuarioLogado = usuario;
            _perfilLogado = perfil;
            InitializeComponent();
            lblBoasVindas.Text = $"Bem-vindo, {_usuarioLogado} ({_perfilLogado})!";
        }

        private void InitializeComponent()
        {
            this.lblBoasVindas = new Label();
            this.btnNovaVenda = new Button();
            this.btnConsultarPreco = new Button();
            this.btnCadastrarProduto = new Button();
            this.btnSair = new Button();

            // Configurações do formulário - Boas Vindas
            this.lblBoasVindas.Text = "Bem-vindo ao Sistema de Vendas!";
            this.lblBoasVindas.Font = new Font("Arial", 16, FontStyle.Bold);
            this.lblBoasVindas.Size = new Size(500, 40);
            this.lblBoasVindas.Location = new Point(50, 30);
            this.lblBoasVindas.TextAlign = ContentAlignment.MiddleCenter;

            // Configurações do formulário - Botões
            //Nova Venda
            this.btnNovaVenda.Text = "Nova Venda";
            this.btnNovaVenda.Size = new Size(250, 55);
            this.btnNovaVenda.Location = new Point(175, 100);
            this.btnNovaVenda.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnNovaVenda.BackColor = Color.LightSkyBlue;
            this.btnNovaVenda.Click += new EventHandler(btnNovaVenda_Click);

            //Consultar Preço
            this.btnConsultarPreco.Text = "Consultar Preço";
            this.btnConsultarPreco.Size = new Size(250, 55);
            this.btnConsultarPreco.Location = new Point(175, 170);
            this.btnConsultarPreco.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnConsultarPreco.BackColor = Color.LightSkyBlue;
            this.btnConsultarPreco.Click += new EventHandler(btnConsultarPreco_Click);

            //Cadastrar Produto
            this.btnCadastrarProduto.Text = "Cadastrar Produto";
            this.btnCadastrarProduto.Size = new Size(250, 55);
            this.btnCadastrarProduto.Location = new Point(175, 240);
            this.btnCadastrarProduto.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnCadastrarProduto.BackColor = Color.LightSkyBlue;
            this.btnCadastrarProduto.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnCadastrarProduto.Click += new EventHandler(btnCadastrarProduto_Click);  

            // Botão Sair
            this.btnSair.Text = "Sair";
            this.btnSair.Size = new Size(250, 55);
            this.btnSair.Location = new Point(175, 310);
            this.btnSair.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnSair.BackColor = Color.LightSkyBlue;
            this.btnSair.Click += new EventHandler(btnSair_Click);

            // configurar ao formulário
            this.Text = "Dashboard - Vendas Geek";
            this.Size = new Size(600, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Adicionar controles ao formulário
            this.Controls.Add(this.lblBoasVindas);
            this.Controls.Add(this.btnNovaVenda);
            this.Controls.Add(this.btnConsultarPreco);
            this.Controls.Add(this.btnCadastrarProduto);
            this.Controls.Add(this.btnSair);
        }

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de Nova Venda ainda não implementada.", "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnConsultarPreco_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de Consultar Preço ainda não implementada.", "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCadastrarProduto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de Cadastrar Produto ainda não implementada.", "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


//