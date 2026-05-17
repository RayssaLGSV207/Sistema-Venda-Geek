using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Database;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmConsultarVendas : Form
    {
        private Label lblTitulo;
        private DataGridView dgvVendas;
        private Button btnVoltar;
        private Panel pnlCentral;
        private string _perfilUsuario;

        public FrmConsultarVendas(string perfilUsuario)
        {
            _perfilUsuario = perfilUsuario;
            InitializeComponent();
            CarregarVendas();
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.dgvVendas = new DataGridView();
            this.btnVoltar = new Button();

            // FORMULARIO PRINCIPAL
            this.Text = "Consultar Vendas - Vendas Geek";
            this.Size = new Size(900, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // PANEL CENTRAL
            this.pnlCentral.Size = new Size(850, 560);
            this.pnlCentral.Location = new Point(25, 20);
            this.pnlCentral.BackColor = Color.Transparent;

            // TITULO
            this.lblTitulo.Text = "CONSULTAR VENDAS";
            this.lblTitulo.Font = new Font("Arial", 22, FontStyle.Bold);
            this.lblTitulo.Size = new Size(850, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // DATAGRIDVIEW
            this.dgvVendas.Size = new Size(820, 380);
            this.dgvVendas.Location = new Point(15, 70);
            this.dgvVendas.BackgroundColor = Color.White;
            this.dgvVendas.AllowUserToAddRows = false;
            this.dgvVendas.ReadOnly = true;
            this.dgvVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVendas.RowHeadersVisible = false;
            this.dgvVendas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendas.Font = new Font("Arial", 11);
            this.dgvVendas.RowTemplate.Height = 40;

            // COLUNAS
            this.dgvVendas.ColumnCount = 5;
            this.dgvVendas.Columns[0].Name = "CodigoVenda";
            this.dgvVendas.Columns[0].HeaderText = "CODIGO";
            this.dgvVendas.Columns[1].Name = "DataVenda";
            this.dgvVendas.Columns[1].HeaderText = "DATA";
            this.dgvVendas.Columns[2].Name = "ValorTotal";
            this.dgvVendas.Columns[2].HeaderText = "VALOR";
            this.dgvVendas.Columns[3].Name = "Status";
            this.dgvVendas.Columns[3].HeaderText = "STATUS";
            this.dgvVendas.Columns[4].Name = "ClienteCPF";
            this.dgvVendas.Columns[4].HeaderText = "CLIENTE CPF";

            // BOTAO VOLTAR
            this.btnVoltar.Text = "VOLTAR";
            this.btnVoltar.Size = new Size(160, 50);
            this.btnVoltar.Location = new Point(345, 470);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.FlatAppearance.BorderColor = Color.Gray;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += new EventHandler(btnVoltar_Click);

            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.dgvVendas);
            this.pnlCentral.Controls.Add(this.btnVoltar);

            this.Controls.Add(this.pnlCentral);
        }

        private void CarregarVendas()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT CodigoVenda, DataVenda, ValorTotal, Status, ClienteCPF FROM Venda ORDER BY DataVenda DESC";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var leitor = cmd.ExecuteReader())
                    {
                        dgvVendas.Rows.Clear();
                        while (leitor.Read())
                        {
                            string codigo = leitor["CodigoVenda"].ToString();
                            string codigoExibido = codigo.Length > 8 ? codigo.Substring(0, 8) : codigo;
                            
                            dgvVendas.Rows.Add(
                                codigoExibido,
                                leitor["DataVenda"].ToString(),
                                $"R$ {Convert.ToDecimal(leitor["ValorTotal"]):F2}",
                                leitor["Status"].ToString(),
                                leitor["ClienteCPF"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensagemGrande("Erro", $"Erro ao carregar vendas: {ex.Message}", MessageBoxIcon.Error);
            }
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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}