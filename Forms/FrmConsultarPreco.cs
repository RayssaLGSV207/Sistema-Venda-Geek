using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Services;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmConsultarPreco : Form
    {
        private Label lblTitulo;
        private Label lblDica;
        private Label lblPesquisa;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private DataGridView dgvProdutos;
        private Label lblTotalProdutos;
        private Button btnLimpar;
        private Button btnVoltar;
        private Panel pnlCentral;
        private List<ProdutoInfo> _todosProdutos;

        public FrmConsultarPreco(string perfilUsuario)
        {
            InitializeComponent();
            CarregarTodosProdutos();
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.lblDica = new Label();
            this.lblPesquisa = new Label();
            this.txtPesquisa = new TextBox();
            this.btnPesquisar = new Button();
            this.dgvProdutos = new DataGridView();
            this.lblTotalProdutos = new Label();
            this.btnLimpar = new Button();
            this.btnVoltar = new Button();

            // FORMULARIO PRINCIPAL
            this.Text = "Consultar Produtos - Vendas Geek";
            this.Size = new Size(850, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // PANEL CENTRAL
            this.pnlCentral.Size = new Size(800, 560);
            this.pnlCentral.Location = new Point(25, 20);
            this.pnlCentral.BackColor = Color.Transparent;

            // TITULO
            this.lblTitulo.Text = "CONSULTAR PRODUTOS";
            this.lblTitulo.Font = new Font("Arial", 22, FontStyle.Bold);
            this.lblTitulo.Size = new Size(800, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // DICA
            this.lblDica.Text = "Pesquise por Codigo, Nome ou Categoria";
            this.lblDica.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblDica.Size = new Size(800, 30);
            this.lblDica.Location = new Point(0, 65);
            this.lblDica.TextAlign = ContentAlignment.MiddleCenter;
            this.lblDica.ForeColor = Color.DarkBlue;

            // PESQUISA
            this.lblPesquisa.Text = "Buscar:";
            this.lblPesquisa.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblPesquisa.Size = new Size(80, 30);
            this.lblPesquisa.Location = new Point(40, 115);
            this.lblPesquisa.TextAlign = ContentAlignment.MiddleRight;

            this.txtPesquisa.Size = new Size(450, 35);
            this.txtPesquisa.Location = new Point(130, 115);
            this.txtPesquisa.Font = new Font("Arial", 12);
            this.txtPesquisa.BackColor = Color.White;
            this.txtPesquisa.TextChanged += new EventHandler(txtPesquisa_TextChanged);

            this.btnPesquisar.Text = "PESQUISAR";
            this.btnPesquisar.Size = new Size(130, 37);
            this.btnPesquisar.Location = new Point(600, 113);
            this.btnPesquisar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnPesquisar.BackColor = Color.DodgerBlue;
            this.btnPesquisar.ForeColor = Color.White;
            this.btnPesquisar.FlatStyle = FlatStyle.Flat;
            this.btnPesquisar.FlatAppearance.BorderSize = 2;
            this.btnPesquisar.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnPesquisar.Cursor = Cursors.Hand;
            this.btnPesquisar.Click += new EventHandler(btnPesquisar_Click);

            // DATAGRIDVIEW
            this.dgvProdutos.Size = new Size(760, 280);
            this.dgvProdutos.Location = new Point(20, 170);
            this.dgvProdutos.BackgroundColor = Color.White;
            this.dgvProdutos.AllowUserToAddRows = false;
            this.dgvProdutos.ReadOnly = true;
            this.dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdutos.RowHeadersVisible = false;
            this.dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdutos.Font = new Font("Arial", 11);
            this.dgvProdutos.RowTemplate.Height = 40;

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

            // TOTAL
            this.lblTotalProdutos.Text = "Total de produtos: 0";
            this.lblTotalProdutos.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblTotalProdutos.Size = new Size(250, 30);
            this.lblTotalProdutos.Location = new Point(20, 465);
            this.lblTotalProdutos.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTotalProdutos.ForeColor = Color.DarkSlateBlue;

            // BOTOES
            this.btnLimpar.Text = "LIMPAR";
            this.btnLimpar.Size = new Size(130, 45);
            this.btnLimpar.Location = new Point(500, 460);
            this.btnLimpar.BackColor = Color.LightGray;
            this.btnLimpar.ForeColor = Color.Black;
            this.btnLimpar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnLimpar.FlatStyle = FlatStyle.Flat;
            this.btnLimpar.FlatAppearance.BorderSize = 2;
            this.btnLimpar.FlatAppearance.BorderColor = Color.Gray;
            this.btnLimpar.Cursor = Cursors.Hand;
            this.btnLimpar.Click += new EventHandler(btnLimpar_Click);

            this.btnVoltar.Text = "VOLTAR";
            this.btnVoltar.Size = new Size(130, 45);
            this.btnVoltar.Location = new Point(650, 460);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.FlatAppearance.BorderColor = Color.Gray;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += new EventHandler(btnVoltar_Click);

            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.lblDica);
            this.pnlCentral.Controls.Add(this.lblPesquisa);
            this.pnlCentral.Controls.Add(this.txtPesquisa);
            this.pnlCentral.Controls.Add(this.btnPesquisar);
            this.pnlCentral.Controls.Add(this.dgvProdutos);
            this.pnlCentral.Controls.Add(this.lblTotalProdutos);
            this.pnlCentral.Controls.Add(this.btnLimpar);
            this.pnlCentral.Controls.Add(this.btnVoltar);

            this.Controls.Add(this.pnlCentral);
        }

        private void CarregarTodosProdutos()
        {
            try
            {
                _todosProdutos = ProdutoService.ListarTodosProdutos();
                if (_todosProdutos == null || _todosProdutos.Count == 0)
                {
                    _todosProdutos = new List<ProdutoInfo>();
                    MostrarMensagemGrande("Aviso", "Nenhum produto encontrado no banco de dados.", MessageBoxIcon.Warning);
                }
                AtualizarGrid(_todosProdutos);
            }
            catch (Exception ex)
            {
                MostrarMensagemGrande("Erro", $"Erro ao carregar produtos: {ex.Message}", MessageBoxIcon.Error);
                _todosProdutos = new List<ProdutoInfo>();
                AtualizarGrid(_todosProdutos);
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

        private void AtualizarGrid(List<ProdutoInfo> produtos)
        {
            dgvProdutos.Rows.Clear();
            foreach (var produto in produtos)
            {
                dgvProdutos.Rows.Add(
                    produto.CodigoBarras,
                    produto.Nome,
                    produto.Categoria,
                    $"R$ {produto.Valor:F2}",
                    produto.QuantidadeEstoque,
                    produto.IsRaro ? "Sim" : "Nao"
                );
            }
            lblTotalProdutos.Text = $"Total de produtos: {produtos.Count}";
        }

        private void FiltrarProdutos(string termo)
        {
            if (_todosProdutos == null)
            {
                CarregarTodosProdutos();
                return;
            }
            if (string.IsNullOrWhiteSpace(termo))
            {
                AtualizarGrid(_todosProdutos);
                return;
            }
            termo = termo.ToLower();
            var filtrados = _todosProdutos.FindAll(p =>
                p.CodigoBarras.ToLower().Contains(termo) ||
                p.Nome.ToLower().Contains(termo) ||
                p.Categoria.ToLower().Contains(termo) ||
                (p.Fabricante != null && p.Fabricante.ToLower().Contains(termo))
            );
            AtualizarGrid(filtrados);
            if (filtrados.Count == 0)
            {
                MostrarMensagemGrande("Pesquisa", $"Nenhum produto encontrado com o termo: '{txtPesquisa.Text}'", MessageBoxIcon.Information);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e) => FiltrarProdutos(txtPesquisa.Text.Trim());
        private void txtPesquisa_TextChanged(object sender, EventArgs e) => FiltrarProdutos(txtPesquisa.Text.Trim());
        private void btnLimpar_Click(object sender, EventArgs e) { txtPesquisa.Text = ""; AtualizarGrid(_todosProdutos); txtPesquisa.Focus(); }
        private void btnVoltar_Click(object sender, EventArgs e) => this.Close();
    }
}