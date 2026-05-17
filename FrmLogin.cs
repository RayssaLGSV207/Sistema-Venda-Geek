using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Services;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmLogin : Form
    {
        private Label lblTitulo;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblSenha;
        private TextBox txtSenha;
        private Button btnEntrar;
        private Button btnSair;
        private Panel pnlCentral;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.lblUsuario = new Label();
            this.txtUsuario = new TextBox();
            this.lblSenha = new Label();
            this.txtSenha = new TextBox();
            this.btnEntrar = new Button();
            this.btnSair = new Button();

            // Form
            this.Text = "Login - Vendas Geek";
            this.Size = new Size(500, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Panel central
            this.pnlCentral.Size = new Size(360, 360);
            this.pnlCentral.Location = new Point(70, 60);
            this.pnlCentral.BackColor = Color.Transparent;

            // Título
            this.lblTitulo.Text = "SISTEMA VENDAS GEEK";
            this.lblTitulo.Font = new Font("Arial", 20, FontStyle.Bold);
            this.lblTitulo.Size = new Size(360, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // Usuário
            this.lblUsuario.Text = "USUARIO";
            this.lblUsuario.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblUsuario.Size = new Size(360, 30);
            this.lblUsuario.Location = new Point(0, 80);
            this.lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            this.lblUsuario.ForeColor = Color.DarkSlateBlue;

            this.txtUsuario.Size = new Size(360, 35);
            this.txtUsuario.Location = new Point(0, 115);
            this.txtUsuario.Font = new Font("Arial", 12);
            this.txtUsuario.BackColor = Color.White;

            // Senha
            this.lblSenha.Text = "SENHA";
            this.lblSenha.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblSenha.Size = new Size(360, 30);
            this.lblSenha.Location = new Point(0, 165);
            this.lblSenha.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSenha.ForeColor = Color.DarkSlateBlue;

            this.txtSenha.Size = new Size(360, 35);
            this.txtSenha.Location = new Point(0, 200);
            this.txtSenha.Font = new Font("Arial", 12);
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.BackColor = Color.White;

            // Botões
            this.btnEntrar.Text = "ENTRAR";
            this.btnEntrar.Size = new Size(160, 50);
            this.btnEntrar.Location = new Point(15, 270);
            this.btnEntrar.BackColor = Color.DodgerBlue;
            this.btnEntrar.ForeColor = Color.White;
            this.btnEntrar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnEntrar.FlatStyle = FlatStyle.Flat;
            this.btnEntrar.FlatAppearance.BorderSize = 2;
            this.btnEntrar.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnEntrar.Cursor = Cursors.Hand;
            this.btnEntrar.Click += BtnEntrar_Click;

            this.btnSair.Text = "SAIR";
            this.btnSair.Size = new Size(160, 50);
            this.btnSair.Location = new Point(185, 270);
            this.btnSair.BackColor = Color.Crimson;
            this.btnSair.ForeColor = Color.White;
            this.btnSair.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnSair.FlatStyle = FlatStyle.Flat;
            this.btnSair.FlatAppearance.BorderSize = 2;
            this.btnSair.FlatAppearance.BorderColor = Color.DarkRed;
            this.btnSair.Cursor = Cursors.Hand;
            this.btnSair.Click += (s, e) => Application.Exit();

            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.lblUsuario);
            this.pnlCentral.Controls.Add(this.txtUsuario);
            this.pnlCentral.Controls.Add(this.lblSenha);
            this.pnlCentral.Controls.Add(this.txtSenha);
            this.pnlCentral.Controls.Add(this.btnEntrar);
            this.pnlCentral.Controls.Add(this.btnSair);

            this.Controls.Add(this.pnlCentral);
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
            {
                MostrarMensagemGrande("Campos vazios", "Por favor, preencha usuario e senha.", MessageBoxIcon.Warning);
                return;
            }

            if (AuthService.Autenticar(usuario, senha, out string perfil))
            {
                MostrarMensagemGrande("Acesso Permitido", $"Bem-vindo ao sistema, {usuario}!\nPerfil: {perfil}", MessageBoxIcon.Information);

                FrmDashboard dashboard = new FrmDashboard(usuario, perfil);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MostrarMensagemGrande("Erro de Autenticacao", "Usuario ou senha incorretos.\nTente novamente.", MessageBoxIcon.Error);
                txtSenha.Text = "";
                txtUsuario.Focus();
            }
        }

        private void MostrarMensagemGrande(string titulo, string mensagem, MessageBoxIcon icone)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(550, 280);  // Aumentado
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 12);
            lblMsg.Size = new Size(500, 130);   // Aumentado
            lblMsg.Location = new Point(25, 25);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Size = new Size(120, 45);     // Botão maior
            btnOk.Location = new Point(215, 175);
            btnOk.BackColor = Color.LimeGreen;
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Click += (s, ev) => msgForm.Close();

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnOk);
            msgForm.ShowDialog();
        }
    }
}