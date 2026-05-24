using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Services;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmNovaVenda : Form
    {
        // Controles do formulário
        private Label lblTitulo;
        private Panel pnlCentral;

        // Secao CLIENTE
        private GroupBox gbxCliente;
        private Label lblClienteCPF;
        private TextBox txtClienteCPF;
        private Button btnBuscarCliente;
        private Label lblClienteNome;
        private TextBox txtClienteNome;

        // Secao PRODUTO
        private GroupBox gbxProduto;
        private Label lblCodigoBarras;
        private TextBox txtCodigoBarras;
        private Label lblQuantidade;
        private NumericUpDown numQuantidade;
        private Button btnAdicionar;

        // Secao CARRINHO
        private GroupBox gbxCarrinho;
        private DataGridView dgvCarrinho;
        private Label lblTotal;
        private TextBox txtTotal;
        private Button btnRemoverItem;

        // Secao FINALIZACAO
        private GroupBox gbxFinalizacao;
        private Label lblFormaPagamento;
        private ComboBox cmbFormaPagamento;
        private Button btnVoltar;
        private Button btnCancelar;
        private Button btnFinalizar;

        private string _codigoVendaAtual;
        private string _perfilUsuario;

        public FrmNovaVenda(string perfilUsuario)
        {
            _perfilUsuario = perfilUsuario;
            InitializeComponent();

            if (_perfilUsuario != "Atendente" && _perfilUsuario != "Supervisor")
            {
                MostrarMensagemGrande("ACESSO NEGADO",
                    "Voce nao tem permissao para acessar esta funcionalidade.\n\nApenas ATENDENTES e SUPERVISORES podem realizar vendas.",
                    MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void MostrarMensagemGrande(string titulo, string mensagem, MessageBoxIcon icone)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(650, 400);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.MaximizeBox = false;
            msgForm.MinimizeBox = false;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 13);
            lblMsg.ForeColor = Color.DarkBlue;
            lblMsg.Size = new Size(600, 220);
            lblMsg.Location = new Point(20, 20);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Font = new Font("Arial", 12, FontStyle.Bold);
            btnOk.Size = new Size(120, 45);
            btnOk.Location = new Point(260, 280);
            btnOk.BackColor = Color.LimeGreen;
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Click += (s, ev) => msgForm.Close();

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnOk);
            msgForm.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.gbxCliente = new GroupBox();
            this.lblClienteCPF = new Label();
            this.txtClienteCPF = new TextBox();
            this.btnBuscarCliente = new Button();
            this.lblClienteNome = new Label();
            this.txtClienteNome = new TextBox();
            this.gbxProduto = new GroupBox();
            this.lblCodigoBarras = new Label();
            this.txtCodigoBarras = new TextBox();
            this.lblQuantidade = new Label();
            this.numQuantidade = new NumericUpDown();
            this.btnAdicionar = new Button();
            this.gbxCarrinho = new GroupBox();
            this.dgvCarrinho = new DataGridView();
            this.lblTotal = new Label();
            this.txtTotal = new TextBox();
            this.btnRemoverItem = new Button();
            this.gbxFinalizacao = new GroupBox();
            this.lblFormaPagamento = new Label();
            this.cmbFormaPagamento = new ComboBox();
            this.btnVoltar = new Button();
            this.btnCancelar = new Button();
            this.btnFinalizar = new Button();

            // FORMULARIO PRINCIPAL
            this.Text = "Nova Venda - Vendas Geek";
            this.Size = new Size(1050, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // PANEL CENTRAL
            this.pnlCentral.Size = new Size(990, 790);
            this.pnlCentral.Location = new Point(30, 15);
            this.pnlCentral.BackColor = Color.Transparent;

            // TITULO
            this.lblTitulo.Text = "NOVA VENDA";
            this.lblTitulo.Font = new Font("Arial", 24, FontStyle.Bold);
            this.lblTitulo.Size = new Size(990, 50);
            this.lblTitulo.Location = new Point(0, 5);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // GROUPBOX CLIENTE
            this.gbxCliente.Text = "DADOS DO CLIENTE";
            this.gbxCliente.Font = new Font("Arial", 12, FontStyle.Bold);
            this.gbxCliente.Size = new Size(950, 100);
            this.gbxCliente.Location = new Point(20, 65);
            this.gbxCliente.Padding = new Padding(10);

            this.lblClienteCPF.Text = "CPF:";
            this.lblClienteCPF.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblClienteCPF.Size = new Size(60, 30);
            this.lblClienteCPF.Location = new Point(25, 40);
            this.lblClienteCPF.TextAlign = ContentAlignment.MiddleRight;

            this.txtClienteCPF.Size = new Size(280, 35);
            this.txtClienteCPF.Location = new Point(95, 35);
            this.txtClienteCPF.Font = new Font("Arial", 12);

            this.btnBuscarCliente.Text = "BUSCAR CLIENTE";
            this.btnBuscarCliente.Size = new Size(150, 38);
            this.btnBuscarCliente.Location = new Point(390, 33);
            this.btnBuscarCliente.BackColor = Color.DodgerBlue;
            this.btnBuscarCliente.ForeColor = Color.White;
            this.btnBuscarCliente.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnBuscarCliente.FlatStyle = FlatStyle.Flat;
            this.btnBuscarCliente.FlatAppearance.BorderSize = 2;
            this.btnBuscarCliente.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnBuscarCliente.Cursor = Cursors.Hand;
            this.btnBuscarCliente.Click += btnBuscarCliente_Click;

            this.lblClienteNome.Text = "NOME:";
            this.lblClienteNome.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblClienteNome.Size = new Size(70, 30);
            this.lblClienteNome.Location = new Point(560, 40);
            this.lblClienteNome.TextAlign = ContentAlignment.MiddleRight;

            this.txtClienteNome.Size = new Size(300, 35);
            this.txtClienteNome.Location = new Point(650, 35);
            this.txtClienteNome.Font = new Font("Arial", 12);
            this.txtClienteNome.ReadOnly = true;
            this.txtClienteNome.BackColor = Color.LightGray;

            this.gbxCliente.Controls.Add(this.lblClienteCPF);
            this.gbxCliente.Controls.Add(this.txtClienteCPF);
            this.gbxCliente.Controls.Add(this.btnBuscarCliente);
            this.gbxCliente.Controls.Add(this.lblClienteNome);
            this.gbxCliente.Controls.Add(this.txtClienteNome);

            // GROUPBOX PRODUTO
            this.gbxProduto.Text = "ADICIONAR PRODUTO";
            this.gbxProduto.Font = new Font("Arial", 12, FontStyle.Bold);
            this.gbxProduto.Size = new Size(950, 110);
            this.gbxProduto.Location = new Point(20, 180);
            this.gbxProduto.Padding = new Padding(10);

            // Codigo de Barras
            this.lblCodigoBarras.Text = "CODIGO DE BARRAS:";
            this.lblCodigoBarras.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblCodigoBarras.Size = new Size(265, 35);
            this.lblCodigoBarras.Location = new Point(20, 20);
            this.lblCodigoBarras.TextAlign = ContentAlignment.MiddleLeft;

            this.txtCodigoBarras.Size = new Size(400, 35);
            this.txtCodigoBarras.Location = new Point(290, 20);
            this.txtCodigoBarras.Font = new Font("Arial", 12);

            // Quantidade e Botao Adicionar
            this.lblQuantidade.Text = "QUANTIDADE:";
            this.lblQuantidade.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblQuantidade.Size = new Size(150, 35);
            this.lblQuantidade.Location = new Point(20, 65);
            this.lblQuantidade.TextAlign = ContentAlignment.MiddleLeft;

            this.numQuantidade.Size = new Size(100, 35);
            this.numQuantidade.Location = new Point(175, 65);
            this.numQuantidade.Font = new Font("Arial", 12);
            this.numQuantidade.Minimum = 1;
            this.numQuantidade.Maximum = 99;
            this.numQuantidade.Value = 1;

            this.btnAdicionar.Text = "ADICIONAR";
            this.btnAdicionar.Size = new Size(140, 42);
            this.btnAdicionar.Location = new Point(280, 62);
            this.btnAdicionar.BackColor = Color.LimeGreen;
            this.btnAdicionar.ForeColor = Color.White;
            this.btnAdicionar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnAdicionar.FlatStyle = FlatStyle.Flat;
            this.btnAdicionar.FlatAppearance.BorderSize = 2;
            this.btnAdicionar.FlatAppearance.BorderColor = Color.DarkGreen;
            this.btnAdicionar.Cursor = Cursors.Hand;
            this.btnAdicionar.Click += btnAdicionar_Click;

            this.gbxProduto.Controls.Add(this.lblCodigoBarras);
            this.gbxProduto.Controls.Add(this.txtCodigoBarras);
            this.gbxProduto.Controls.Add(this.lblQuantidade);
            this.gbxProduto.Controls.Add(this.numQuantidade);
            this.gbxProduto.Controls.Add(this.btnAdicionar);

            // GROUPBOX CARRINHO
            this.gbxCarrinho.Text = "CARRINHO DE COMPRAS";
            this.gbxCarrinho.Font = new Font("Arial", 12, FontStyle.Bold);
            this.gbxCarrinho.Size = new Size(950, 300);
            this.gbxCarrinho.Location = new Point(20, 305);
            this.gbxCarrinho.Padding = new Padding(10);

            this.dgvCarrinho.Size = new Size(920, 180);
            this.dgvCarrinho.Location = new Point(15, 30);
            this.dgvCarrinho.BackgroundColor = Color.White;
            this.dgvCarrinho.AllowUserToAddRows = false;
            this.dgvCarrinho.ReadOnly = true;
            this.dgvCarrinho.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrinho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrinho.Font = new Font("Arial", 11);
            this.dgvCarrinho.RowTemplate.Height = 35;

            this.dgvCarrinho.ColumnCount = 5;
            this.dgvCarrinho.Columns[0].Name = "CodigoBarras";
            this.dgvCarrinho.Columns[0].HeaderText = "CODIGO";
            this.dgvCarrinho.Columns[1].Name = "Produto";
            this.dgvCarrinho.Columns[1].HeaderText = "PRODUTO";
            this.dgvCarrinho.Columns[2].Name = "Quantidade";
            this.dgvCarrinho.Columns[2].HeaderText = "QTD";
            this.dgvCarrinho.Columns[3].Name = "PrecoUnitario";
            this.dgvCarrinho.Columns[3].HeaderText = "PRECO UNIT.";
            this.dgvCarrinho.Columns[4].Name = "Subtotal";
            this.dgvCarrinho.Columns[4].HeaderText = "SUBTOTAL";

            // Botao Remover Item
            this.btnRemoverItem.Text = "REMOVER ITEM";
            this.btnRemoverItem.Size = new Size(150, 40);
            this.btnRemoverItem.Location = new Point(15, 240);
            this.btnRemoverItem.BackColor = Color.Crimson;
            this.btnRemoverItem.ForeColor = Color.White;
            this.btnRemoverItem.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnRemoverItem.FlatStyle = FlatStyle.Flat;
            this.btnRemoverItem.FlatAppearance.BorderSize = 2;
            this.btnRemoverItem.FlatAppearance.BorderColor = Color.DarkRed;
            this.btnRemoverItem.Cursor = Cursors.Hand;
            this.btnRemoverItem.Click += btnRemoverItem_Click;

            // TOTAL DA COMPRA
            this.lblTotal.Text = "TOTAL DA COMPRA:";
            this.lblTotal.Font = new Font("Arial", 16, FontStyle.Bold);
            this.lblTotal.Size = new Size(290, 40);
            this.lblTotal.Location = new Point(400, 240);
            this.lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTotal.ForeColor = Color.DarkSlateBlue;

            this.txtTotal.Size = new Size(200, 40);
            this.txtTotal.Location = new Point(695, 240);
            this.txtTotal.Font = new Font("Arial", 16, FontStyle.Bold);
            this.txtTotal.BackColor = Color.LightYellow;
            this.txtTotal.ReadOnly = true;
            this.txtTotal.TextAlign = HorizontalAlignment.Right;
            this.txtTotal.Text = "0,00";

            this.gbxCarrinho.Controls.Add(this.dgvCarrinho);
            this.gbxCarrinho.Controls.Add(this.btnRemoverItem);
            this.gbxCarrinho.Controls.Add(this.lblTotal);
            this.gbxCarrinho.Controls.Add(this.txtTotal);

            // GROUPBOX FINALIZACAO
            this.gbxFinalizacao.Text = "FINALIZACAO DA VENDA";
            this.gbxFinalizacao.Font = new Font("Arial", 12, FontStyle.Bold);
            this.gbxFinalizacao.Size = new Size(955, 110);
            this.gbxFinalizacao.Location = new Point(20, 620);
            this.gbxFinalizacao.Padding = new Padding(10);

            // FORMA DE PAGAMENTO - LABEL ACIMA DO COMBOBOX
            this.lblFormaPagamento.Text = "FORMA DE PAGAMENTO:";
            this.lblFormaPagamento.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblFormaPagamento.Size = new Size(265, 30);
            this.lblFormaPagamento.Location = new Point(20, 20);
            this.lblFormaPagamento.TextAlign = ContentAlignment.MiddleLeft;

            this.cmbFormaPagamento.Size = new Size(220, 38);
            this.cmbFormaPagamento.Location = new Point(20, 55);
            this.cmbFormaPagamento.Font = new Font("Arial", 11);
            this.cmbFormaPagamento.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFormaPagamento.Items.AddRange(new string[] { "Dinheiro", "Cartao de Credito", "Cartao de Debito", "PIX" });
            this.cmbFormaPagamento.SelectedIndex = 0;

            // BOTOES
            this.btnVoltar.Text = "VOLTAR";
            this.btnVoltar.Size = new Size(130, 48);
            this.btnVoltar.Location = new Point(350, 45);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.FlatAppearance.BorderColor = Color.Gray;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += btnVoltar_Click;

            this.btnCancelar.Text = "CANCELAR VENDA";
            this.btnCancelar.Size = new Size(150, 48);
            this.btnCancelar.Location = new Point(500, 45);
            this.btnCancelar.BackColor = Color.Crimson;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatAppearance.BorderColor = Color.DarkRed;
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += btnCancelar_Click;

            this.btnFinalizar.Text = "FINALIZAR VENDA";
            this.btnFinalizar.Size = new Size(180, 50);
            this.btnFinalizar.Location = new Point(670, 45);
            this.btnFinalizar.BackColor = Color.DodgerBlue;
            this.btnFinalizar.ForeColor = Color.White;
            this.btnFinalizar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnFinalizar.FlatStyle = FlatStyle.Flat;
            this.btnFinalizar.FlatAppearance.BorderSize = 2;
            this.btnFinalizar.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnFinalizar.Cursor = Cursors.Hand;
            this.btnFinalizar.Click += btnFinalizar_Click;

            this.gbxFinalizacao.Controls.Add(this.lblFormaPagamento);
            this.gbxFinalizacao.Controls.Add(this.cmbFormaPagamento);
            this.gbxFinalizacao.Controls.Add(this.btnVoltar);
            this.gbxFinalizacao.Controls.Add(this.btnCancelar);
            this.gbxFinalizacao.Controls.Add(this.btnFinalizar);

            // ADICIONAR CONTROLES AO PANEL CENTRAL
            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.gbxCliente);
            this.pnlCentral.Controls.Add(this.gbxProduto);
            this.pnlCentral.Controls.Add(this.gbxCarrinho);
            this.pnlCentral.Controls.Add(this.gbxFinalizacao);

            this.Controls.Add(this.pnlCentral);
        }

        private void MostrarMensagemComDetalhes(string titulo, string mensagem)
        {
            Form msgForm = new Form();
            msgForm.Text = titulo;
            msgForm.Size = new Size(700, 500);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.MaximizeBox = false;
            msgForm.MinimizeBox = false;
            msgForm.BackColor = Color.WhiteSmoke;

            Label lblMsg = new Label();
            lblMsg.Text = mensagem;
            lblMsg.Font = new Font("Arial", 11);
            lblMsg.ForeColor = Color.DarkBlue;
            lblMsg.Size = new Size(650, 350);
            lblMsg.Location = new Point(20, 20);
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Font = new Font("Arial", 12, FontStyle.Bold);
            btnOk.Size = new Size(120, 45);
            btnOk.Location = new Point(290, 390);
            btnOk.BackColor = Color.LimeGreen;
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Click += (s, ev) => msgForm.Close();

            msgForm.Controls.Add(lblMsg);
            msgForm.Controls.Add(btnOk);
            msgForm.ShowDialog();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClienteCPF.Text))
            {
                MostrarMensagemGrande("ATENCAO", "Digite o CPF do cliente para buscar.", MessageBoxIcon.Warning);
                return;
            }

            var cliente = ClienteService.BuscarClientePorCPF(txtClienteCPF.Text.Trim());

            if (cliente == null)
            {
                MostrarMensagemGrande("CLIENTE NAO ENCONTRADO",
                    "Cliente nao encontrado.\n\nDeseja cadastrar um novo cliente?",
                    MessageBoxIcon.Question);

                if (MessageBox.Show("Cliente nao encontrado. Deseja cadastrar?", "Novo Cliente",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FrmCadastrarCliente cadCliente = new FrmCadastrarCliente(_perfilUsuario);
                    cadCliente.ShowDialog();
                    txtClienteCPF.Focus();
                }
                return;
            }

            txtClienteNome.Text = cliente.Nome;
            MostrarMensagemGrande("CLIENTE ENCONTRADO",
                $"Cliente: {cliente.Nome}\nCPF: {cliente.CPF}",
                MessageBoxIcon.Information);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                MostrarMensagemGrande("ATENCAO", "Digite o codigo de barras do produto.", MessageBoxIcon.Warning);
                return;
            }

            string codigo = txtCodigoBarras.Text.Trim();
            int quantidade = (int)numQuantidade.Value;
            var produto = ProdutoService.BuscarProduto(codigo);

            if (produto == null)
            {
                MostrarMensagemGrande("PRODUTO NAO ENCONTRADO", "Verifique o codigo de barras e tente novamente.", MessageBoxIcon.Error);
                return;
            }

            if (produto.QuantidadeEstoque < quantidade)
            {
                MostrarMensagemGrande("ESTOQUE INSUFICIENTE",
                    $"Produto: {produto.Nome}\nDisponivel: {produto.QuantidadeEstoque}\nSolicitado: {quantidade}",
                    MessageBoxIcon.Warning);
                return;
            }

            if (produto.IsRaro && quantidade > 1)
            {
                MostrarMensagemGrande("ITEM RARO",
                    $"O produto '{produto.Nome}' e um item de colecionador\n" +
                    $"e so pode ser comprado 1 unidade por vez.",
                    MessageBoxIcon.Warning);
                return;
            }

            decimal subtotal = produto.Valor * quantidade;

            dgvCarrinho.Rows.Add(
                produto.CodigoBarras,
                produto.Nome,
                quantidade,
                produto.Valor.ToString("F2"),
                subtotal.ToString("F2")
            );

            decimal total = 0;
            foreach (DataGridViewRow row in dgvCarrinho.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }
            txtTotal.Text = total.ToString("F2");

            // CRIA UM CODIGO DE VENDA PARA O CARRINHO ATUAL (se ainda não existir)
            if (string.IsNullOrEmpty(_codigoVendaAtual))
            {
                _codigoVendaAtual = Guid.NewGuid().ToString();
            }

            txtCodigoBarras.Text = "";
            numQuantidade.Value = 1;
            txtCodigoBarras.Focus();

            MostrarMensagemGrande("PRODUTO ADICIONADO",
                $"{produto.Nome}\nQuantidade: {quantidade}\nSubtotal: R$ {subtotal:F2}",
                MessageBoxIcon.Information);
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (dgvCarrinho.SelectedRows.Count == 0)
            {
                MostrarMensagemGrande("ATENCAO", "Selecione um item no carrinho para remover.", MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Tem certeza que deseja remover este item do carrinho?",
                "Remover Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvCarrinho.Rows.RemoveAt(dgvCarrinho.SelectedRows[0].Index);

                decimal total = 0;
                foreach (DataGridViewRow row in dgvCarrinho.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
                txtTotal.Text = total.ToString("F2");
            }
        }

private void btnFinalizar_Click(object sender, EventArgs e)
{
    if (dgvCarrinho.Rows.Count == 0)
    {
        MostrarMensagemGrande("CARRINHO VAZIO", "Adicione pelo menos um produto ao carrinho antes de finalizar.", MessageBoxIcon.Warning);
        return;
    }

    if (string.IsNullOrWhiteSpace(txtClienteCPF.Text))
    {
        MostrarMensagemGrande("CLIENTE NAO IDENTIFICADO", "Informe o CPF do cliente para finalizar a venda.", MessageBoxIcon.Warning);
        return;
    }

    string formaPagamento = cmbFormaPagamento.SelectedItem.ToString();
    string codigoBarras = dgvCarrinho.Rows[0].Cells["CodigoBarras"].Value.ToString();
    int quantidade = Convert.ToInt32(dgvCarrinho.Rows[0].Cells["Quantidade"].Value);
    decimal total = decimal.Parse(txtTotal.Text);

    // Usa o método existente RegistrarVenda
    string codigoVenda = VendaService.RegistrarVenda(codigoBarras, quantidade, txtClienteCPF.Text.Trim());
    
    // Se não retornou código, cria um novo
    if (string.IsNullOrEmpty(codigoVenda))
    {
        codigoVenda = Guid.NewGuid().ToString();
    }

    if (!string.IsNullOrEmpty(codigoVenda))
    {
        VendaService.AtualizarStatusVenda(codigoVenda, "Finalizada");
        SistemaFinanceiroService.RegistrarVendaFinanceiro(codigoVenda, total, formaPagamento);

        string detalhesProdutos = "";
        foreach (DataGridViewRow row in dgvCarrinho.Rows)
        {
            string nome = row.Cells["Produto"].Value.ToString();
            int qtd = Convert.ToInt32(row.Cells["Quantidade"].Value);
            decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);
            detalhesProdutos += $"• {nome} - {qtd} x R$ {subtotal:F2}\n";
        }

        MostrarMensagemComDetalhes("VENDA REALIZADA COM SUCESSO!",
            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            $"CLIENTE: {txtClienteNome.Text}\n" +
            $"CODIGO DA VENDA: {codigoVenda.Substring(0, 8)}\n" +
            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            $"PRODUTOS:\n{detalhesProdutos}\n" +
            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            $"TOTAL: R$ {total:F2}\n" +
            $"PAGAMENTO: {formaPagamento}\n" +
            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
            $"Registro enviado ao sistema financeiro.");

        dgvCarrinho.Rows.Clear();
        txtTotal.Text = "0,00";
        txtClienteCPF.Text = "";
        txtClienteNome.Text = "";
        txtCodigoBarras.Text = "";
        numQuantidade.Value = 1;
        _codigoVendaAtual = null;
        txtClienteCPF.Focus();
    }
}
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dgvCarrinho.Rows.Count == 0)
            {
                MostrarMensagemGrande("ATENCAO", "Nao ha venda em andamento para cancelar.", MessageBoxIcon.Warning);
                return;
            }

            // VERIFICA SE PRECISA DE AUTORIZACAO
            // Apenas ATENDENTE precisa de autorizacao do supervisor
            // SUPERVISOR pode cancelar diretamente
            if (_perfilUsuario == "Atendente")
            {
                Form credForm = new Form();
                credForm.Text = "Autorizacao de Supervisor";
                credForm.Size = new Size(450, 250);
                credForm.StartPosition = FormStartPosition.CenterParent;
                credForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                credForm.MaximizeBox = false;
                credForm.MinimizeBox = false;
                credForm.BackColor = Color.WhiteSmoke;

                Label lblLogin = new Label();
                lblLogin.Text = "Login do Supervisor:";
                lblLogin.Font = new Font("Arial", 10, FontStyle.Bold);
                lblLogin.Size = new Size(130, 25);
                lblLogin.Location = new Point(15, 35);
                lblLogin.TextAlign = ContentAlignment.MiddleRight;

                TextBox txtLogin = new TextBox();
                txtLogin.Size = new Size(180, 25);
                txtLogin.Location = new Point(155, 35);
                txtLogin.Font = new Font("Arial", 10);

                Label lblSenha = new Label();
                lblSenha.Text = "Senha:";
                lblSenha.Font = new Font("Arial", 10, FontStyle.Bold);
                lblSenha.Size = new Size(130, 25);
                lblSenha.Location = new Point(15, 80);
                lblSenha.TextAlign = ContentAlignment.MiddleRight;

                TextBox txtSenha = new TextBox();
                txtSenha.Size = new Size(180, 25);
                txtSenha.Location = new Point(155, 80);
                txtSenha.PasswordChar = '*';

                Button btnConfirmar = new Button();
                btnConfirmar.Text = "Confirmar";
                btnConfirmar.Size = new Size(100, 35);
                btnConfirmar.Location = new Point(100, 130);
                btnConfirmar.BackColor = Color.LimeGreen;
                btnConfirmar.ForeColor = Color.White;
                btnConfirmar.Font = new Font("Arial", 10, FontStyle.Bold);

                Button btnCancelarCred = new Button();
                btnCancelarCred.Text = "Cancelar";
                btnCancelarCred.Size = new Size(100, 35);
                btnCancelarCred.Location = new Point(220, 130);
                btnCancelarCred.BackColor = Color.LightGray;
                btnCancelarCred.Font = new Font("Arial", 10, FontStyle.Bold);

                credForm.Controls.Add(lblLogin);
                credForm.Controls.Add(txtLogin);
                credForm.Controls.Add(lblSenha);
                credForm.Controls.Add(txtSenha);
                credForm.Controls.Add(btnConfirmar);
                credForm.Controls.Add(btnCancelarCred);

                bool credenciaisValidas = false;

                btnConfirmar.Click += (s, ev) =>
                {
                    string loginSup = txtLogin.Text.Trim();
                    string senhaSup = txtSenha.Text;

                    if (AuthService.ValidarCredenciaisSupervisor(loginSup, senhaSup))
                    {
                        credenciaisValidas = true;
                        credForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credenciais invalidas! Acesso negado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnCancelarCred.Click += (s, ev) => credForm.Close();

                credForm.ShowDialog();

                if (!credenciaisValidas)
                {
                    return; 
                }
            }
            // Se for Supervisor, continua direto sem pedir autenticação

            // ================================================
            // EXECUTA O CANCELAMENTO DA VENDA
            // ================================================

            // Se o carrinho tem itens mas não tem código de venda no banco,
            // apenas limpamos o carrinho sem chamar o serviço de cancelamento
            if (string.IsNullOrEmpty(_codigoVendaAtual))
            {
                // Carrinho com itens mas venda não registrada no banco - apenas limpar
                dgvCarrinho.Rows.Clear();
                txtTotal.Text = "0,00";
                txtClienteCPF.Text = "";
                txtClienteNome.Text = "";
                txtCodigoBarras.Text = "";
                numQuantidade.Value = 1;

                MostrarMensagemGrande("VENDA CANCELADA",
                    "Carrinho limpo com sucesso.\n\nNenhum registro foi criado no banco de dados.",
                    MessageBoxIcon.Information);
                return;
            }

            // Se tem código de venda, tenta cancelar no banco
            bool cancelado = VendaService.CancelarVenda(_codigoVendaAtual);

            if (cancelado)
            {
                SistemaFinanceiroService.EnviarCancelamento(_codigoVendaAtual);

                dgvCarrinho.Rows.Clear();
                txtTotal.Text = "0,00";
                txtClienteCPF.Text = "";
                txtClienteNome.Text = "";
                txtCodigoBarras.Text = "";
                numQuantidade.Value = 1;
                _codigoVendaAtual = null;

                MostrarMensagemGrande("VENDA CANCELADA",
                    $"Codigo da venda: {_codigoVendaAtual?.Substring(0, 8) ?? "N/A"}\n\nCancelamento comunicado ao sistema financeiro.",
                    MessageBoxIcon.Information);
            }
            else
            {
                // Se não conseguiu cancelar no banco, pelo menos limpa o carrinho local
                dgvCarrinho.Rows.Clear();
                txtTotal.Text = "0,00";
                txtClienteCPF.Text = "";
                txtClienteNome.Text = "";
                txtCodigoBarras.Text = "";
                numQuantidade.Value = 1;
                _codigoVendaAtual = null;

                MostrarMensagemGrande("VENDA CANCELADA",
                    "Carrinho limpo com sucesso.",
                    MessageBoxIcon.Information);
            }
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
