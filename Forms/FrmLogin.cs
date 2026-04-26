using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Forms;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmLogin : Form
    {
        //Controles do formulário
        private TextBox txtUsuario;
        private TextBox txtSenha;
        private Button btnEntrar;
        private Button btnSair;
        private Label lblUsuario;
        private Label lblSenha;
        private Label lblTitulo;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //objetos do formulário
            this.txtUsuario = new TextBox();
            this.txtSenha = new TextBox();
            this.btnEntrar = new Button();
            this.btnSair = new Button();
            this.lblTitulo = new Label();
            this.lblUsuario = new Label();
            this.lblSenha = new Label();

            // Configurações do formulário - Titulo
            this.lblTitulo.Text = "Sistema de Vendas Geek";
            this.lblTitulo.Font = new Font("Arial", 18, FontStyle.Bold);
            this.lblTitulo.Size = new Size(400, 50);
            this.lblTitulo.Location = new Point(50, 30);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // Configurações do formulário - Usuario
            this.lblUsuario.Text = "Usuário:";
            this.lblUsuario.Font = new Font("Arial", 10, FontStyle.Bold);
            this.lblUsuario.Size = new Size(80, 25);
            this.lblUsuario.Location = new Point(100,110);
            this.lblUsuario.TextAlign = ContentAlignment.MiddleRight;

            this.txtUsuario.Size = new Size(200, 25);
            this.txtUsuario.Location = new Point(190, 110);
            this.txtUsuario.Font = new Font("Arial", 10);

            // Configurações do formulário - Senha
            this.lblSenha.Text = "Senha:";
            this.lblSenha.Font = new Font("Arial", 10, FontStyle.Bold);
            this.lblSenha.Size = new Size(80, 25);
            this.lblSenha.Location = new Point(100, 160);
            this.lblSenha.TextAlign = ContentAlignment.MiddleRight;

            this.txtSenha.Size = new Size(200, 25);
            this.txtSenha.Location = new Point(190, 160);
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Font = new Font("Arial", 10);

            // Configurações do formulário - Botões
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.Size = new Size(100, 40);
            this.btnEntrar.Location = new Point(130, 230);
            this.btnEntrar.BackColor = Color.LightGreen;
            this.btnEntrar.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnEntrar.Click += new EventHandler(btnEntrar_Click);

            this.btnSair.Text = "Sair";
            this.btnSair.Size = new Size(100, 40);
            this.btnSair.Location = new Point(260, 230);
            this.btnSair.BackColor = Color.LightCoral;
            this.btnSair.Click += new EventHandler(btnSair_Click);

            // Configurações do formulário
            this.Text = "Login - Vendas loja Geek";
            this.Size = new Size(600, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


            // controles do formulário
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(btnSair);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            //senha = Vendas123
            if (usuario == "admin" && senha == "Vendas123")
            {
                FrmDashboard dashboard = new FrmDashboard(usuario, "Administrador");
                dashboard.Show();
                this.Hide();
                MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


//