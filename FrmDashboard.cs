using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Services;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmDashboard : Form
    {
        private Button btnNovaVenda;
        private Button btnConsultarPreco;
        private Button btnGestaoProdutos;
        private Button btnCadastrarCliente;
        private Button btnConsultarVendas;
        private Button btnTrocarLogin;
        private Button btnSair;
        private Label lblBoasVindas;
        private Panel pnlMenu;
        private string _usuarioLogado;
        private string _perfilLogado;

        public FrmDashboard(string usuario, string perfil)
        {
            _usuarioLogado = usuario;
            _perfilLogado = perfil;
            InitializeComponent();
            lblBoasVindas.Text = $"Bem-vindo, {_usuarioLogado} - Perfil: {_perfilLogado}";
        }

        private void InitializeComponent()
        {
            this.lblBoasVindas = new Label();
            this.pnlMenu = new Panel();
            this.btnNovaVenda = new Button();
            this.btnConsultarPreco = new Button();
            this.btnGestaoProdutos = new Button();
            this.btnCadastrarCliente = new Button();
            this.btnConsultarVendas = new Button();
            this.btnTrocarLogin = new Button();
            this.btnSair = new Button();

            // FORMULARIO
            this.Text = "Dashboard - Vendas Geek";
            this.Size = new Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Boas Vindas
            this.lblBoasVindas.Text = "Bem-vindo ao Sistema de Vendas";
            this.lblBoasVindas.Font = new Font("Arial", 18, FontStyle.Bold);
            this.lblBoasVindas.Size = new Size(800, 50);
            this.lblBoasVindas.Location = new Point(25, 20);
            this.lblBoasVindas.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBoasVindas.ForeColor = Color.Black;

            // Painel de botoes
            this.pnlMenu.Size = new Size(800, 480);
            this.pnlMenu.Location = new Point(25, 80);
            this.pnlMenu.BackColor = Color.Transparent;

            // LINHA 1: Nova Venda e Consultar Preco
            this.btnNovaVenda.Text = "NOVA VENDA";
            this.btnNovaVenda.Size = new Size(240, 75);
            this.btnNovaVenda.Location = new Point(40, 20);
            this.btnNovaVenda.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnNovaVenda.BackColor = Color.LightSkyBlue;
            this.btnNovaVenda.ForeColor = Color.Black;
            this.btnNovaVenda.FlatStyle = FlatStyle.Flat;
            this.btnNovaVenda.FlatAppearance.BorderSize = 2;
            this.btnNovaVenda.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnNovaVenda.Cursor = Cursors.Hand;
            this.btnNovaVenda.Click += BtnNovaVenda_Click;

            this.btnConsultarPreco.Text = "CONSULTAR PRECO";
            this.btnConsultarPreco.Size = new Size(240, 75);
            this.btnConsultarPreco.Location = new Point(520, 20);
            this.btnConsultarPreco.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnConsultarPreco.BackColor = Color.LightSkyBlue;
            this.btnConsultarPreco.ForeColor = Color.Black;
            this.btnConsultarPreco.FlatStyle = FlatStyle.Flat;
            this.btnConsultarPreco.FlatAppearance.BorderSize = 2;
            this.btnConsultarPreco.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnConsultarPreco.Cursor = Cursors.Hand;
            this.btnConsultarPreco.Click += BtnConsultarPreco_Click;

            // LINHA 2: Gestao de Produtos e Cadastrar Clientes
            this.btnGestaoProdutos.Text = "GESTAO DE PRODUTOS";
            this.btnGestaoProdutos.Size = new Size(240, 75);
            this.btnGestaoProdutos.Location = new Point(40, 120);
            this.btnGestaoProdutos.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnGestaoProdutos.BackColor = Color.LightSkyBlue;
            this.btnGestaoProdutos.ForeColor = Color.Black;
            this.btnGestaoProdutos.FlatStyle = FlatStyle.Flat;
            this.btnGestaoProdutos.FlatAppearance.BorderSize = 2;
            this.btnGestaoProdutos.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnGestaoProdutos.Cursor = Cursors.Hand;
            this.btnGestaoProdutos.Click += BtnGestaoProdutos_Click;

            this.btnCadastrarCliente.Text = "CADASTRAR CLIENTES";
            this.btnCadastrarCliente.Size = new Size(240, 75);
            this.btnCadastrarCliente.Location = new Point(520, 120);
            this.btnCadastrarCliente.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnCadastrarCliente.BackColor = Color.LightSkyBlue;
            this.btnCadastrarCliente.ForeColor = Color.Black;
            this.btnCadastrarCliente.FlatStyle = FlatStyle.Flat;
            this.btnCadastrarCliente.FlatAppearance.BorderSize = 2;
            this.btnCadastrarCliente.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnCadastrarCliente.Cursor = Cursors.Hand;
            this.btnCadastrarCliente.Click += BtnCadastrarCliente_Click;

            // LINHA 3: Consultar Vendas (centralizado)
            this.btnConsultarVendas.Text = "CONSULTAR VENDAS";
            this.btnConsultarVendas.Size = new Size(280, 75);
            this.btnConsultarVendas.Location = new Point(260, 230);
            this.btnConsultarVendas.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnConsultarVendas.BackColor = Color.LightGreen;
            this.btnConsultarVendas.ForeColor = Color.Black;
            this.btnConsultarVendas.FlatStyle = FlatStyle.Flat;
            this.btnConsultarVendas.FlatAppearance.BorderSize = 2;
            this.btnConsultarVendas.FlatAppearance.BorderColor = Color.DarkGreen;
            this.btnConsultarVendas.Cursor = Cursors.Hand;
            this.btnConsultarVendas.Click += BtnConsultarVendas_Click;

            // LINHA 4: Trocar Login e Sair (lado a lado)
            this.btnTrocarLogin.Text = "TROCAR LOGIN";
            this.btnTrocarLogin.Size = new Size(220, 75);
            this.btnTrocarLogin.Location = new Point(130, 350);
            this.btnTrocarLogin.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnTrocarLogin.BackColor = Color.LightCyan;
            this.btnTrocarLogin.ForeColor = Color.Black;
            this.btnTrocarLogin.FlatStyle = FlatStyle.Flat;
            this.btnTrocarLogin.FlatAppearance.BorderSize = 2;
            this.btnTrocarLogin.FlatAppearance.BorderColor = Color.DarkRed;
            this.btnTrocarLogin.Cursor = Cursors.Hand;
            this.btnTrocarLogin.Click += BtnTrocarLogin_Click;

            this.btnSair.Text = "SAIR";
            this.btnSair.Size = new Size(220, 75);
            this.btnSair.Location = new Point(450, 350);
            this.btnSair.Font = new Font("Arial", 13, FontStyle.Bold);
            this.btnSair.BackColor = Color.Red;
            this.btnSair.ForeColor = Color.White;
            this.btnSair.FlatStyle = FlatStyle.Flat;
            this.btnSair.FlatAppearance.BorderSize = 2;
            this.btnSair.FlatAppearance.BorderColor = Color.DarkRed;
            this.btnSair.Cursor = Cursors.Hand;
            this.btnSair.Click += BtnSair_Click;

            this.pnlMenu.Controls.Add(this.btnNovaVenda);
            this.pnlMenu.Controls.Add(this.btnConsultarPreco);
            this.pnlMenu.Controls.Add(this.btnGestaoProdutos);
            this.pnlMenu.Controls.Add(this.btnCadastrarCliente);
            this.pnlMenu.Controls.Add(this.btnConsultarVendas);
            this.pnlMenu.Controls.Add(this.btnTrocarLogin);
            this.pnlMenu.Controls.Add(this.btnSair);

            this.Controls.Add(this.lblBoasVindas);
            this.Controls.Add(this.pnlMenu);
        }

        private void MostrarMensagemGrande(string titulo, string mensagem, MessageBoxIcon icone)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(550, 280);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 12);
            lblMsg.Size = new Size(500, 150);
            lblMsg.Location = new Point(25, 30);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Size = new Size(100, 40);
            btnOk.Location = new Point(225, 190);
            btnOk.BackColor = Color.LightGreen;
            btnOk.ForeColor = Color.Black;
            btnOk.Click += (s, ev) => msgForm.Close();

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnOk);
            msgForm.ShowDialog();
        }

        private DialogResult MostrarMensagemComEscolha(string titulo, string mensagem)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(550, 250);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 12);
            lblMsg.Size = new Size(500, 100);
            lblMsg.Location = new Point(25, 30);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnSim = new Button();
            btnSim.Text = "SIM";
            btnSim.Size = new Size(100, 40);
            btnSim.Location = new Point(150, 160);
            btnSim.BackColor = Color.LightGreen;
            btnSim.ForeColor = Color.Black;
            btnSim.DialogResult = DialogResult.Yes;

            Button btnNao = new Button();
            btnNao.Text = "NAO";
            btnNao.Size = new Size(100, 40);
            btnNao.Location = new Point(300, 160);
            btnNao.BackColor = Color.LightCoral;
            btnNao.ForeColor = Color.Black;
            btnNao.DialogResult = DialogResult.No;

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnSim);
            msgForm.Controls.Add(btnNao);

            return msgForm.ShowDialog();
        }

        private void BtnNovaVenda_Click(object sender, EventArgs e)
        {
            if (_perfilLogado != "Atendente" && _perfilLogado != "Supervisor")
            {
                MostrarMensagemGrande("Permissao Negada", "Apenas atendentes e supervisores podem realizar vendas.", MessageBoxIcon.Warning);
                return;
            }
            FrmNovaVenda novaVenda = new FrmNovaVenda(_perfilLogado);
            novaVenda.ShowDialog();
        }

        private void BtnConsultarPreco_Click(object sender, EventArgs e)
        {
            FrmConsultarPreco consultar = new FrmConsultarPreco(_perfilLogado);
            consultar.ShowDialog();
        }

        private void BtnGestaoProdutos_Click(object sender, EventArgs e)
        {
            if (_perfilLogado != "Estoquista" && _perfilLogado != "Supervisor")
            {
                MostrarMensagemGrande("Permissao Negada", "Apenas estoquistas e supervisores podem gerenciar produtos.", MessageBoxIcon.Warning);
                return;
            }
            FrmCadastrarProduto cadastrar = new FrmCadastrarProduto(_perfilLogado);
            cadastrar.ShowDialog();
        }

        private void BtnCadastrarCliente_Click(object sender, EventArgs e)
        {
            if (_perfilLogado != "Atendente" && _perfilLogado != "Supervisor")
            {
                MostrarMensagemGrande("Permissao Negada", "Apenas atendentes e supervisores podem cadastrar clientes.", MessageBoxIcon.Warning);
                return;
            }
            FrmCadastrarCliente cadastrarCliente = new FrmCadastrarCliente(_perfilLogado);
            cadastrarCliente.ShowDialog();
        }

        private void BtnConsultarVendas_Click(object sender, EventArgs e)
        {
            FrmConsultarVendas consultarVendas = new FrmConsultarVendas(_perfilLogado);
            consultarVendas.ShowDialog();
        }

        private void BtnTrocarLogin_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MostrarMensagemComEscolha("Confirmar troca de Login", "Deseja realmente trocar de Usuario? A sessao atual sera encerrada.");

            if (resultado == DialogResult.Yes)
            {
                AuthService.Logout();
                this.Close();
                FrmLogin login = new FrmLogin();
                login.Show();
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MostrarMensagemComEscolha("Confirmar saida", "Deseja realmente sair do sistema?");
            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}