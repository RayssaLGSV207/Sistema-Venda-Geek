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
        
        private ComboBox cmbFiltroStatus;
        private Button btnResumo;
        private Button btnAtualizar;

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
            this.cmbFiltroStatus = new ComboBox();
            this.btnResumo = new Button();
            this.btnAtualizar = new Button();

            this.Text = "Consultar Vendas - Vendas Geek";
            this.Size = new Size(950, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.pnlCentral.Size = new Size(880, 660);
            this.pnlCentral.Location = new Point(35, 20);
            this.pnlCentral.BackColor = Color.Transparent;

            this.lblTitulo.Text = "CONSULTAR VENDAS";
            this.lblTitulo.Font = new Font("Arial", 22, FontStyle.Bold);
            this.lblTitulo.Size = new Size(880, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            Label lblFiltro = new Label();
            lblFiltro.Text = "Filtrar por Status:";
            lblFiltro.Font = new Font("Arial", 11, FontStyle.Bold);
            lblFiltro.Size = new Size(150, 30);
            lblFiltro.Location = new Point(15, 70);

            this.cmbFiltroStatus = new ComboBox();
            this.cmbFiltroStatus.Size = new Size(150, 30);
            this.cmbFiltroStatus.Location = new Point(170, 70);
            this.cmbFiltroStatus.Font = new Font("Arial", 11);
            this.cmbFiltroStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFiltroStatus.Items.AddRange(new string[] { "Todas", "Pendente", "Finalizada", "Cancelada" });
            this.cmbFiltroStatus.SelectedIndex = 0;
            this.cmbFiltroStatus.SelectedIndexChanged += (s, e) => CarregarVendas();

            this.btnResumo = new Button();
            this.btnResumo.Text = "RESUMO";
            this.btnResumo.Size = new Size(120, 35);
            this.btnResumo.Location = new Point(340, 68);
            this.btnResumo.BackColor = Color.DodgerBlue;
            this.btnResumo.ForeColor = Color.White;
            this.btnResumo.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnResumo.FlatStyle = FlatStyle.Flat;
            this.btnResumo.Cursor = Cursors.Hand;
            this.btnResumo.Click += BtnResumo_Click;

            this.btnAtualizar = new Button();
            this.btnAtualizar.Text = "ATUALIZAR";
            this.btnAtualizar.Size = new Size(130, 35);
            this.btnAtualizar.Location = new Point(480, 68);
            this.btnAtualizar.BackColor = Color.LightGreen;
            this.btnAtualizar.ForeColor = Color.Black;
            this.btnAtualizar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnAtualizar.FlatStyle = FlatStyle.Flat;
            this.btnAtualizar.Cursor = Cursors.Hand;
            this.btnAtualizar.Click += (s, e) => CarregarVendas();

            this.dgvVendas.Size = new Size(850, 440);
            this.dgvVendas.Location = new Point(15, 115);
            this.dgvVendas.BackgroundColor = Color.White;
            this.dgvVendas.AllowUserToAddRows = false;
            this.dgvVendas.ReadOnly = true;
            this.dgvVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVendas.RowHeadersVisible = false;
            this.dgvVendas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendas.Font = new Font("Arial", 11);
            this.dgvVendas.RowTemplate.Height = 40;

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

            this.btnVoltar.Text = "VOLTAR";
            this.btnVoltar.Size = new Size(160, 50);
            this.btnVoltar.Location = new Point(360, 575);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += btnVoltar_Click;

            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(lblFiltro);
            this.pnlCentral.Controls.Add(this.cmbFiltroStatus);
            this.pnlCentral.Controls.Add(this.btnResumo);
            this.pnlCentral.Controls.Add(this.btnAtualizar);
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
                    string sql = "SELECT CodigoVenda, DataVenda, ValorTotal, Status, ClienteCPF FROM Venda";
                    
                    string filtro = cmbFiltroStatus?.SelectedItem?.ToString();
                    if (filtro != null && filtro != "Todas")
                    {
                        sql += " WHERE Status = @status";
                    }
                    sql += " ORDER BY DataVenda DESC";
                    
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        if (filtro != null && filtro != "Todas")
                            cmd.Parameters.AddWithValue("@status", filtro);
                        
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
            }
            catch (Exception ex)
            {
                MostrarMensagemGrande("Erro", $"Erro ao carregar vendas: {ex.Message}", MessageBoxIcon.Error);
            }
        }

        private void BtnResumo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    
                    string nl = Environment.NewLine;
                    
                    string sqlStatus = @"
                        SELECT Status, COUNT(*) as Quantidade, SUM(ValorTotal) as ValorTotal 
                        FROM Venda 
                        GROUP BY Status";
                    
                    string resumo = "RESUMO DE VENDAS" + nl + nl;
                    
                    using (var cmd = new SQLiteCommand(sqlStatus, conn))
                    using (var leitor = cmd.ExecuteReader())
                    {
                        decimal totalGeral = 0;
                        int totalVendas = 0;
                        
                        while (leitor.Read())
                        {
                            string status = leitor["Status"].ToString();
                            int qtd = Convert.ToInt32(leitor["Quantidade"]);
                            decimal valor = Convert.ToDecimal(leitor["ValorTotal"]);
                            
                            resumo += "  " + status + ": " + qtd + " venda(s) - R$ " + valor.ToString("F2") + nl;
                            totalVendas += qtd;
                            totalGeral += valor;
                        }
                        
                        resumo += nl + "  TOTAL GERAL: " + totalVendas + " venda(s) - R$ " + totalGeral.ToString("F2") + nl;
                    }
                    
                    string sqlEstoqueBaixo = @"
                        SELECT CodigoBarras, Nome, QuantidadeEstoque 
                        FROM Produto 
                        WHERE QuantidadeEstoque < 5 
                        ORDER BY QuantidadeEstoque ASC";
                    
                    resumo += nl + nl + "PRODUTOS COM ESTOQUE BAIXO (menos de 5 unidades)" + nl + nl;
                    
                    using (var cmd = new SQLiteCommand(sqlEstoqueBaixo, conn))
                    using (var leitor = cmd.ExecuteReader())
                    {
                        bool hasLowStock = false;
                        while (leitor.Read())
                        {
                            hasLowStock = true;
                            string codigo = leitor["CodigoBarras"].ToString();
                            string nome = leitor["Nome"].ToString();
                            int estoque = Convert.ToInt32(leitor["QuantidadeEstoque"]);
                            resumo += "  " + codigo + " - " + nome + " | Estoque: " + estoque + nl;
                        }
                        if (!hasLowStock)
                            resumo += "  Nenhum produto com estoque baixo." + nl;
                    }
                    
                    string sqlCategoria = @"
                        SELECT Categoria, 
                               COUNT(*) as TotalProdutos,
                               SUM(QuantidadeEstoque) as TotalUnidades,
                               SUM(Valor * QuantidadeEstoque) as ValorTotalEstoque
                        FROM Produto 
                        GROUP BY Categoria
                        ORDER BY Categoria";
                    
                    resumo += nl + nl + "ESTOQUE POR CATEGORIA" + nl + nl;
                    
                    using (var cmd = new SQLiteCommand(sqlCategoria, conn))
                    using (var leitor = cmd.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            string categoria = leitor["Categoria"].ToString();
                            int totalProdutos = Convert.ToInt32(leitor["TotalProdutos"]);
                            int totalUnidades = Convert.ToInt32(leitor["TotalUnidades"]);
                            decimal valorTotal = Convert.ToDecimal(leitor["ValorTotalEstoque"]);
                            
                            resumo += "  " + categoria + nl;
                            resumo += "    Produtos: " + totalProdutos + nl;
                            resumo += "    Unidades: " + totalUnidades + nl;
                            resumo += "    Valor: R$ " + valorTotal.ToString("F2") + nl + nl;
                        }
                    }
                    
                    MostrarResumoGrande("RELATORIO COMPLETO", resumo);
                }
            }
            catch (Exception ex)
            {
                MostrarMensagemGrande("Erro", "Erro ao gerar resumo: " + ex.Message, MessageBoxIcon.Error);
            }
        }

        private void MostrarResumoGrande(string titulo, string mensagem)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(650, 600);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.BackColor = Color.WhiteSmoke;

            RichTextBox txtResumo = new RichTextBox();
            txtResumo.Text = mensagem;
            txtResumo.Font = new Font("Arial", 11);
            txtResumo.Size = new Size(600, 480);
            txtResumo.Location = new Point(20, 20);
            txtResumo.Multiline = true;
            txtResumo.ReadOnly = true;

            Button btnOk = new Button();
            btnOk.Text = "FECHAR";
            btnOk.Size = new Size(100, 40);
            btnOk.Location = new Point(265, 515);
            btnOk.BackColor = Color.DodgerBlue;
            btnOk.ForeColor = Color.White;
            btnOk.Font = new Font("Arial", 11, FontStyle.Bold);
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Click += (s, ev) => msgForm.Close();

            msgForm.Controls.Add(txtResumo);
            msgForm.Controls.Add(btnOk);
            msgForm.ShowDialog();
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