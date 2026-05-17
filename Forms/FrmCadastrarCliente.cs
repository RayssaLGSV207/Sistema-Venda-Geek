using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmCadastrarCliente : Form
    {
        private Label lblTitulo;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblCPF;
        private TextBox txtCPF;
        private Label lblRG;
        private TextBox txtRG;
        private Label lblEndereco;
        private TextBox txtEndereco;
        private Label lblTelefone;
        private TextBox txtTelefone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnVoltar;
        private Panel pnlCentral;

        private string _perfilUsuario;

        public FrmCadastrarCliente(string perfilUsuario)
        {
            _perfilUsuario = perfilUsuario;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.lblNome = new Label();
            this.txtNome = new TextBox();
            this.lblCPF = new Label();
            this.txtCPF = new TextBox();
            this.lblRG = new Label();
            this.txtRG = new TextBox();
            this.lblEndereco = new Label();
            this.txtEndereco = new TextBox();
            this.lblTelefone = new Label();
            this.txtTelefone = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.btnSalvar = new Button();
            this.btnCancelar = new Button();
            this.btnVoltar = new Button();

            // FORMULARIO PRINCIPAL
            this.Text = "Cadastrar Cliente - Vendas Geek";
            this.Size = new Size(850, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // PANEL CENTRAL
            this.pnlCentral.Size = new Size(800, 460);
            this.pnlCentral.Location = new Point(25, 20);
            this.pnlCentral.BackColor = Color.Transparent;

            // TITULO
            this.lblTitulo.Text = "CADASTRAR CLIENTE";
            this.lblTitulo.Font = new Font("Arial", 22, FontStyle.Bold);
            this.lblTitulo.Size = new Size(800, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // Nome
            this.lblNome.Text = "Nome Completo:";
            this.lblNome.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblNome.Size = new Size(140, 30);
            this.lblNome.Location = new Point(40, 80);
            this.lblNome.TextAlign = ContentAlignment.MiddleLeft;

            this.txtNome.Size = new Size(450, 35);
            this.txtNome.Location = new Point(190, 80);
            this.txtNome.Font = new Font("Arial", 11);

            // CPF
            this.lblCPF.Text = "CPF:";
            this.lblCPF.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblCPF.Size = new Size(140, 30);
            this.lblCPF.Location = new Point(40, 130);
            this.lblCPF.TextAlign = ContentAlignment.MiddleLeft;

            this.txtCPF.Size = new Size(220, 35);
            this.txtCPF.Location = new Point(190, 130);
            this.txtCPF.Font = new Font("Arial", 11);
            this.txtCPF.MaxLength = 14;

            // RG
            this.lblRG.Text = "RG:";
            this.lblRG.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblRG.Size = new Size(140, 30);
            this.lblRG.Location = new Point(460, 130);
            this.lblRG.TextAlign = ContentAlignment.MiddleLeft;

            this.txtRG.Size = new Size(200, 35);
            this.txtRG.Location = new Point(560, 130);
            this.txtRG.Font = new Font("Arial", 11);

            // Endereco
            this.lblEndereco.Text = "Endereco:";
            this.lblEndereco.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblEndereco.Size = new Size(140, 30);
            this.lblEndereco.Location = new Point(40, 180);
            this.lblEndereco.TextAlign = ContentAlignment.MiddleLeft;

            this.txtEndereco.Size = new Size(550, 35);
            this.txtEndereco.Location = new Point(190, 180);
            this.txtEndereco.Font = new Font("Arial", 11);

            // Telefone
            this.lblTelefone.Text = "Telefone:";
            this.lblTelefone.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblTelefone.Size = new Size(140, 30);
            this.lblTelefone.Location = new Point(40, 230);
            this.lblTelefone.TextAlign = ContentAlignment.MiddleLeft;

            this.txtTelefone.Size = new Size(220, 35);
            this.txtTelefone.Location = new Point(190, 230);
            this.txtTelefone.Font = new Font("Arial", 11);

            // Email
            this.lblEmail.Text = "E-mail:";
            this.lblEmail.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblEmail.Size = new Size(140, 30);
            this.lblEmail.Location = new Point(450, 230);
            this.lblEmail.TextAlign = ContentAlignment.MiddleLeft;

            this.txtEmail.Size = new Size(250, 35);
            this.txtEmail.Location = new Point(560, 230);
            this.txtEmail.Font = new Font("Arial", 11);

            // BOTOES
            this.btnSalvar.Text = "SALVAR";
            this.btnSalvar.Size = new Size(160, 50);
            this.btnSalvar.Location = new Point(130, 330);
            this.btnSalvar.BackColor = Color.DodgerBlue;
            this.btnSalvar.ForeColor = Color.White;
            this.btnSalvar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnSalvar.FlatStyle = FlatStyle.Flat;
            this.btnSalvar.FlatAppearance.BorderSize = 2;
            this.btnSalvar.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnSalvar.Cursor = Cursors.Hand;
            this.btnSalvar.Click += BtnSalvar_Click;

            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.Size = new Size(140, 50);
            this.btnCancelar.Location = new Point(310, 330);
            this.btnCancelar.BackColor = Color.Crimson;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatAppearance.BorderColor = Color.DarkRed;
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += BtnCancelar_Click;

            this.btnVoltar.Text = "VOLTAR";
            this.btnVoltar.Size = new Size(140, 50);
            this.btnVoltar.Location = new Point(470, 330);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.FlatAppearance.BorderColor = Color.Gray;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += BtnVoltar_Click;

            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.lblNome);
            this.pnlCentral.Controls.Add(this.txtNome);
            this.pnlCentral.Controls.Add(this.lblCPF);
            this.pnlCentral.Controls.Add(this.txtCPF);
            this.pnlCentral.Controls.Add(this.lblRG);
            this.pnlCentral.Controls.Add(this.txtRG);
            this.pnlCentral.Controls.Add(this.lblEndereco);
            this.pnlCentral.Controls.Add(this.txtEndereco);
            this.pnlCentral.Controls.Add(this.lblTelefone);
            this.pnlCentral.Controls.Add(this.txtTelefone);
            this.pnlCentral.Controls.Add(this.lblEmail);
            this.pnlCentral.Controls.Add(this.txtEmail);
            this.pnlCentral.Controls.Add(this.btnSalvar);
            this.pnlCentral.Controls.Add(this.btnCancelar);
            this.pnlCentral.Controls.Add(this.btnVoltar);

            this.Controls.Add(this.pnlCentral);
        }

        private void MostrarMensagemGrande(string titulo, string mensagem, MessageBoxIcon icone)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(500, 220);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 12);
            lblMsg.Size = new Size(450, 100);
            lblMsg.Location = new Point(25, 30);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Size = new Size(100, 40);
            btnOk.Location = new Point(200, 140);
            btnOk.BackColor = Color.LimeGreen;
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Click += (s, ev) => msgForm.Close();

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnOk);
            msgForm.ShowDialog();
        }

        private DialogResult MostrarMensagemComEscolha(string titulo, string mensagem)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(500, 220);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 12);
            lblMsg.Size = new Size(450, 80);
            lblMsg.Location = new Point(25, 30);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnSim = new Button();
            btnSim.Text = "SIM";
            btnSim.Size = new Size(100, 40);
            btnSim.Location = new Point(150, 130);
            btnSim.BackColor = Color.LimeGreen;
            btnSim.ForeColor = Color.White;
            btnSim.FlatStyle = FlatStyle.Flat;
            btnSim.DialogResult = DialogResult.Yes;

            Button btnNao = new Button();
            btnNao.Text = "NAO";
            btnNao.Size = new Size(100, 40);
            btnNao.Location = new Point(280, 130);
            btnNao.BackColor = Color.Crimson;
            btnNao.ForeColor = Color.White;
            btnNao.FlatStyle = FlatStyle.Flat;
            btnNao.DialogResult = DialogResult.No;

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnSim);
            msgForm.Controls.Add(btnNao);

            return msgForm.ShowDialog();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MostrarMensagemGrande("Atencao", "O campo Nome e obrigatorio.", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                MostrarMensagemGrande("Atencao", "O campo CPF e obrigatorio.", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO Cliente (Nome, CPF, RG, DataCadastro, Endereco, Telefone, Email) 
                                   VALUES (@nome, @cpf, @rg, @data, @endereco, @telefone, @email)";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
                        cmd.Parameters.AddWithValue("@cpf", txtCPF.Text.Trim());
                        cmd.Parameters.AddWithValue("@rg", txtRG.Text.Trim());
                        cmd.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.Trim());
                        cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MostrarMensagemGrande("Sucesso", $"Cliente '{txtNome.Text}' cadastrado com sucesso!", MessageBoxIcon.Information);

                txtNome.Text = "";
                txtCPF.Text = "";
                txtRG.Text = "";
                txtEndereco.Text = "";
                txtTelefone.Text = "";
                txtEmail.Text = "";
                txtNome.Focus();
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                    MostrarMensagemGrande("Erro", "CPF ja cadastrado no sistema.", MessageBoxIcon.Error);
                else
                    MostrarMensagemGrande("Erro", "Erro ao salvar cliente: " + ex.Message, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MostrarMensagemGrande("Erro", "Erro ao salvar cliente: " + ex.Message, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (MostrarMensagemComEscolha("Cancelar", "Tem certeza que deseja cancelar? Os dados serao perdidos.") == DialogResult.Yes)
            {
                txtNome.Text = "";
                txtCPF.Text = "";
                txtRG.Text = "";
                txtEndereco.Text = "";
                txtTelefone.Text = "";
                txtEmail.Text = "";
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}