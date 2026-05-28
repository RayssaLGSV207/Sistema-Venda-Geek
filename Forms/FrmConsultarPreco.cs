using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Services;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmConsultarPreco : Form
    {
        private Label lblTitulo;
        private TextBox txtBusca;
        private DataGridView dgvProdutos;
        private Button btnVoltar;
        private Panel pnlCentral;
        private string _perfilUsuario;

        public FrmConsultarPreco(string perfilUsuario)
        {
            _perfilUsuario = perfilUsuario;
            InitializeComponent();
            CarregarProdutos("");
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.txtBusca = new TextBox();
            this.dgvProdutos = new DataGridView();
            this.btnVoltar = new Button();

            // FORMULARIO PRINCIPAL
            this.Text = "Consultar Preco - Vendas Geek";
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
            this.lblTitulo.Text = "CONSULTAR PRECO";
            this.lblTitulo.Font = new Font("Arial", 22, FontStyle.Bold);
            this.lblTitulo.Size = new Size(850, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // CAMPO DE BUSCA
            this.txtBusca.Size = new Size(400, 35);
            this.txtBusca.Location = new Point(225, 70);
            this.txtBusca.Font = new Font("Arial", 12);
            this.txtBusca.Text = "Digite o codigo, nome ou categoria para buscar...";
            this.txtBusca.ForeColor = Color.Gray;
            this.txtBusca.Enter += TxtBusca_Enter;
            this.txtBusca.Leave += TxtBusca_Leave;
            this.txtBusca.TextChanged += TxtBusca_TextChanged;

            // DATAGRIDVIEW
            this.dgvProdutos.Size = new Size(820, 380);
            this.dgvProdutos.Location = new Point(15, 120);
            this.dgvProdutos.BackgroundColor = Color.White;
            this.dgvProdutos.AllowUserToAddRows = false;
            this.dgvProdutos.ReadOnly = true;
            this.dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdutos.RowHeadersVisible = false;
            this.dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdutos.Font = new Font("Arial", 11);
            this.dgvProdutos.RowTemplate.Height = 35;

            // COLUNAS
            this.dgvProdutos.ColumnCount = 6;
            this.dgvProdutos.Columns[0].Name = "CodigoBarras";
            this.dgvProdutos.Columns[0].HeaderText = "CODIGO";
            this.dgvProdutos.Columns[1].Name = "Nome";
            this.dgvProdutos.Columns[1].HeaderText = "PRODUTO";
            this.dgvProdutos.Columns[2].Name = "Categoria";
            this.dgvProdutos.Columns[2].HeaderText = "CATEGORIA";
            this.dgvProdutos.Columns[3].Name = "Valor";
            this.dgvProdutos.Columns[3].HeaderText = "PRECO";
            this.dgvProdutos.Columns[4].Name = "Estoque";
            this.dgvProdutos.Columns[4].HeaderText = "ESTOQUE";
            this.dgvProdutos.Columns[5].Name = "Raro";
            this.dgvProdutos.Columns[5].HeaderText = "RARO";

            // BOTAO VOLTAR
            this.btnVoltar.Text = "&VOLTAR";
            this.btnVoltar.Size = new Size(160, 50);
            this.btnVoltar.Location = new Point(345, 520);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.FlatAppearance.BorderColor = Color.Gray;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += BtnVoltar_Click;

            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.txtBusca);
            this.pnlCentral.Controls.Add(this.dgvProdutos);
            this.pnlCentral.Controls.Add(this.btnVoltar);

            this.Controls.Add(this.pnlCentral);
        }

        private void TxtBusca_Enter(object sender, EventArgs e)
        {
            if (this.txtBusca.Text == "Digite o codigo, nome ou categoria para buscar...")
            {
                this.txtBusca.Text = "";
                this.txtBusca.ForeColor = Color.Black;
            }
        }

        private void TxtBusca_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtBusca.Text))
            {
                this.txtBusca.Text = "Digite o codigo, nome ou categoria para buscar...";
                this.txtBusca.ForeColor = Color.Gray;
            }
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            string termoBusca = this.txtBusca.Text;
            if (termoBusca == "Digite o codigo, nome ou categoria para buscar...")
            {
                termoBusca = "";
            }
            CarregarProdutos(termoBusca);
        }

        private void CarregarProdutos(string termoBusca)
        {
            try
            {
                var produtos = ProdutoService.ListarTodosProdutos();
                dgvProdutos.Rows.Clear();

                foreach (var produto in produtos)
                {
                    if (!string.IsNullOrEmpty(termoBusca))
                    {
                        if (!produto.CodigoBarras.ToLower().Contains(termoBusca.ToLower()) &&
                            !produto.Nome.ToLower().Contains(termoBusca.ToLower()) &&
                            !produto.Categoria.ToLower().Contains(termoBusca.ToLower()))
                        {
                            continue;
                        }
                    }

                    dgvProdutos.Rows.Add(
                        produto.CodigoBarras,
                        produto.Nome,
                        produto.Categoria,
                        $"R$ {produto.Valor:F2}",
                        produto.QuantidadeEstoque,
                        produto.IsRaro ? "Sim" : "Nao"
                    );
                }
            }
            catch (Exception ex)
            {
                MostrarMensagemGrande("Erro", "Erro ao carregar produtos: " + ex.Message, MessageBoxIcon.Error);
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

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}