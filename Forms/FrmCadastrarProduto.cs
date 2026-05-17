using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaVendaGeek.Services;

namespace SistemaVendaGeek.Forms
{
    public partial class FrmCadastrarProduto : Form
    {
        // Campos do formulário
        private Label lblTitulo;
        private TabControl tabControl;
        private TabPage tabCadastrar;
        private TabPage tabAtualizar;
        private Panel pnlCentral;
        
        // Campos da aba CADASTRAR
        private Label lblCodigoBarras;
        private TextBox txtCodigoBarras;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblFabricante;
        private TextBox txtFabricante;
        private Label lblQuantidade;
        private NumericUpDown numQuantidade;
        private Label lblValor;
        private TextBox txtValor;
        private Label lblPlataforma;
        private TextBox txtPlataforma;
        private Label lblGarantia;
        private TextBox txtGarantia;
        private Label lblIsRaro;
        private CheckBox chkIsRaro;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnVoltar;
        
        // Campos da aba ATUALIZAR ESTOQUE
        private Label lblBuscarCodigo;
        private TextBox txtBuscarCodigo;
        private Button btnBuscar;
        private Label lblProdutoNome;
        private TextBox txtProdutoNome;
        private Label lblEstoqueAtual;
        private TextBox txtEstoqueAtual;
        private Label lblQtdOperacao;
        private NumericUpDown numQtdOperacao;
        private Label lblTipoOperacao;
        private ComboBox cmbTipoOperacao;
        private Button btnAtualizarEstoque;
        
        private string _perfilUsuario;

        public FrmCadastrarProduto(string perfilUsuario)
        {
            _perfilUsuario = perfilUsuario;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pnlCentral = new Panel();
            this.lblTitulo = new Label();
            this.tabControl = new TabControl();
            this.tabCadastrar = new TabPage();
            this.tabAtualizar = new TabPage();
            
            // CAMPOS DA ABA CADASTRAR
            this.lblCodigoBarras = new Label();
            this.txtCodigoBarras = new TextBox();
            this.lblNome = new Label();
            this.txtNome = new TextBox();
            this.lblCategoria = new Label();
            this.cmbCategoria = new ComboBox();
            this.lblFabricante = new Label();
            this.txtFabricante = new TextBox();
            this.lblQuantidade = new Label();
            this.numQuantidade = new NumericUpDown();
            this.lblValor = new Label();
            this.txtValor = new TextBox();
            this.lblPlataforma = new Label();
            this.txtPlataforma = new TextBox();
            this.lblGarantia = new Label();
            this.txtGarantia = new TextBox();
            this.lblIsRaro = new Label();
            this.chkIsRaro = new CheckBox();
            this.btnSalvar = new Button();
            this.btnCancelar = new Button();
            this.btnVoltar = new Button();
            
            // CAMPOS DA ABA ATUALIZAR
            this.lblBuscarCodigo = new Label();
            this.txtBuscarCodigo = new TextBox();
            this.btnBuscar = new Button();
            this.lblProdutoNome = new Label();
            this.txtProdutoNome = new TextBox();
            this.lblEstoqueAtual = new Label();
            this.txtEstoqueAtual = new TextBox();
            this.lblQtdOperacao = new Label();
            this.numQtdOperacao = new NumericUpDown();
            this.lblTipoOperacao = new Label();
            this.cmbTipoOperacao = new ComboBox();
            this.btnAtualizarEstoque = new Button();

            // FORMULARIO PRINCIPAL
            this.Text = "Gestao de Produtos - Vendas Geek";
            this.Size = new Size(950, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // PANEL CENTRAL
            this.pnlCentral.Size = new Size(900, 590);
            this.pnlCentral.Location = new Point(25, 20);
            this.pnlCentral.BackColor = Color.Transparent;

            // TITULO
            this.lblTitulo.Text = "GESTAO DE PRODUTOS";
            this.lblTitulo.Font = new Font("Arial", 22, FontStyle.Bold);
            this.lblTitulo.Size = new Size(850, 50);
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.ForeColor = Color.Black;

            // TAB CONTROL
            this.tabControl.Size = new Size(870, 560);
            this.tabControl.Location = new Point(15, 70);
            this.tabControl.Font = new Font("Arial", 12, FontStyle.Bold);
            
            // Aba Cadastrar
            this.tabCadastrar.Text = "CADASTRAR PRODUTO";
            this.tabCadastrar.Size = new Size(850, 530);
            this.tabCadastrar.Padding = new Padding(20);
            
            // Aba Atualizar Estoque
            this.tabAtualizar.Text = "ATUALIZAR ESTOQUE";
            this.tabAtualizar.Size = new Size(800, 460);
            this.tabAtualizar.Padding = new Padding(20);
            
            this.tabControl.TabPages.Add(this.tabCadastrar);
            this.tabControl.TabPages.Add(this.tabAtualizar);
            
            // ================================================
            // ABA CADASTRAR - LAYOUT COM ESPACAMENTO
            // ================================================
            
            // LINHA 1: Codigo de Barras
            this.lblCodigoBarras.Text = "Codigo de Barras:";
            this.lblCodigoBarras.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblCodigoBarras.Size = new Size(200, 30);
            this.lblCodigoBarras.Location = new Point(30, 25);
            this.lblCodigoBarras.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtCodigoBarras.Size = new Size(320, 35);
            this.txtCodigoBarras.Location = new Point(230, 25);
            this.txtCodigoBarras.Font = new Font("Arial", 11);
            
            // LINHA 2: Nome do Produto
            this.lblNome.Text = "Nome do Produto:";
            this.lblNome.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblNome.Size = new Size(200, 30);
            this.lblNome.Location = new Point(30, 80);
            this.lblNome.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtNome.Size = new Size(400, 35);
            this.txtNome.Location = new Point(230, 80);
            this.txtNome.Font = new Font("Arial", 11);
            
            // LINHA 3: Categoria e Fabricante (lado a lado)
            this.lblCategoria.Text = "Categoria:";
            this.lblCategoria.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblCategoria.Size = new Size(200, 30);
            this.lblCategoria.Location = new Point(30, 135);
            this.lblCategoria.TextAlign = ContentAlignment.MiddleLeft;
            
            this.cmbCategoria.Size = new Size(200, 35);
            this.cmbCategoria.Location = new Point(230, 135);
            this.cmbCategoria.Font = new Font("Arial", 11);
            this.cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategoria.Items.AddRange(new string[] { "Jogo", "Acessorio", "Produto Geek" });
            
            this.lblFabricante.Text = "Fabricante:";
            this.lblFabricante.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblFabricante.Size = new Size(200, 30);
            this.lblFabricante.Location = new Point(430, 135);
            this.lblFabricante.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtFabricante.Size = new Size(220, 35);
            this.txtFabricante.Location = new Point(620, 135);
            this.txtFabricante.Font = new Font("Arial", 11);
            
            // LINHA 4: Quantidade Estoque e Valor (lado a lado)
            this.lblQuantidade.Text = "Quantidade Estoque:";
            this.lblQuantidade.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblQuantidade.Size = new Size(150, 30);
            this.lblQuantidade.Location = new Point(30, 190);
            this.lblQuantidade.TextAlign = ContentAlignment.MiddleLeft;
            
            this.numQuantidade.Size = new Size(120, 35);
            this.numQuantidade.Location = new Point(190, 190);
            this.numQuantidade.Font = new Font("Arial", 11);
            this.numQuantidade.Minimum = 0;
            this.numQuantidade.Maximum = 9999;
            
            this.lblValor.Text = "Valor (R$):";
            this.lblValor.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblValor.Size = new Size(150, 30);
            this.lblValor.Location = new Point(420, 190);
            this.lblValor.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtValor.Size = new Size(150, 35);
            this.txtValor.Location = new Point(540, 190);
            this.txtValor.Text = "0,00";
            this.txtValor.Font = new Font("Arial", 11);
            
            // LINHA 5: Plataforma
            this.lblPlataforma.Text = "Plataforma:";
            this.lblPlataforma.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblPlataforma.Size = new Size(150, 30);
            this.lblPlataforma.Location = new Point(30, 245);
            this.lblPlataforma.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtPlataforma.Size = new Size(300, 35);
            this.txtPlataforma.Location = new Point(190, 245);
            this.txtPlataforma.Font = new Font("Arial", 11);
            this.txtPlataforma.Text = "PC, PS5, Xbox, Switch, etc.";
            
            // LINHA 6: Garantia
            this.lblGarantia.Text = "Garantia (meses):";
            this.lblGarantia.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblGarantia.Size = new Size(200, 30);
            this.lblGarantia.Location = new Point(30, 300);
            this.lblGarantia.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtGarantia.Size = new Size(100, 35);
            this.txtGarantia.Location = new Point(230, 300);
            this.txtGarantia.Text = "12";
            this.txtGarantia.Font = new Font("Arial", 11);
            
            // LINHA 7: Produto Raro
            this.lblIsRaro.Text = "Produto Raro:";
            this.lblIsRaro.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblIsRaro.Size = new Size(150, 30);
            this.lblIsRaro.Location = new Point(30, 355);
            this.lblIsRaro.TextAlign = ContentAlignment.MiddleLeft;
            
            this.chkIsRaro.Text = "Sim (item collector)";
            this.chkIsRaro.Size = new Size(250, 30);
            this.chkIsRaro.Location = new Point(190, 355);
            this.chkIsRaro.Font = new Font("Arial", 11);
            
            // BOTOES DA ABA CADASTRAR
            this.btnSalvar.Text = "SALVAR";
            this.btnSalvar.Size = new Size(160, 50);
            this.btnSalvar.Location = new Point(130, 430);
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
            this.btnCancelar.Location = new Point(310, 430);
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
            this.btnVoltar.Location = new Point(470, 430);
            this.btnVoltar.BackColor = Color.LightGray;
            this.btnVoltar.ForeColor = Color.Black;
            this.btnVoltar.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.FlatAppearance.BorderColor = Color.Gray;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Click += BtnVoltar_Click;
            
            // Adicionar controles à aba Cadastrar
            this.tabCadastrar.Controls.Add(this.lblCodigoBarras);
            this.tabCadastrar.Controls.Add(this.txtCodigoBarras);
            this.tabCadastrar.Controls.Add(this.lblNome);
            this.tabCadastrar.Controls.Add(this.txtNome);
            this.tabCadastrar.Controls.Add(this.lblCategoria);
            this.tabCadastrar.Controls.Add(this.cmbCategoria);
            this.tabCadastrar.Controls.Add(this.lblFabricante);
            this.tabCadastrar.Controls.Add(this.txtFabricante);
            this.tabCadastrar.Controls.Add(this.lblQuantidade);
            this.tabCadastrar.Controls.Add(this.numQuantidade);
            this.tabCadastrar.Controls.Add(this.lblValor);
            this.tabCadastrar.Controls.Add(this.txtValor);
            this.tabCadastrar.Controls.Add(this.lblPlataforma);
            this.tabCadastrar.Controls.Add(this.txtPlataforma);
            this.tabCadastrar.Controls.Add(this.lblGarantia);
            this.tabCadastrar.Controls.Add(this.txtGarantia);
            this.tabCadastrar.Controls.Add(this.lblIsRaro);
            this.tabCadastrar.Controls.Add(this.chkIsRaro);
            this.tabCadastrar.Controls.Add(this.btnSalvar);
            this.tabCadastrar.Controls.Add(this.btnCancelar);
            this.tabCadastrar.Controls.Add(this.btnVoltar);
            
            // ================================================
            // ABA ATUALIZAR ESTOQUE - LAYOUT COM ESPACAMENTO
            // ================================================
            
            // LINHA 1: Codigo de Barras e Botao Buscar
            this.lblBuscarCodigo.Text = "Codigo de Barras:";
            this.lblBuscarCodigo.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblBuscarCodigo.Size = new Size(200, 30);
            this.lblBuscarCodigo.Location = new Point(30, 25);
            this.lblBuscarCodigo.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtBuscarCodigo.Size = new Size(300, 35);
            this.txtBuscarCodigo.Location = new Point(230, 25);
            this.txtBuscarCodigo.Font = new Font("Arial", 11);
            
            this.btnBuscar.Text = "BUSCAR PRODUTO";
            this.btnBuscar.Size = new Size(160, 37);
            this.btnBuscar.Location = new Point(535, 23);
            this.btnBuscar.BackColor = Color.DodgerBlue;
            this.btnBuscar.ForeColor = Color.White;
            this.btnBuscar.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnBuscar.FlatStyle = FlatStyle.Flat;
            this.btnBuscar.FlatAppearance.BorderSize = 2;
            this.btnBuscar.FlatAppearance.BorderColor = Color.DarkBlue;
            this.btnBuscar.Cursor = Cursors.Hand;
            this.btnBuscar.Click += BtnBuscar_Click;
            
            // LINHA 2: Nome do Produto
            this.lblProdutoNome.Text = "Produto:";
            this.lblProdutoNome.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblProdutoNome.Size = new Size(150, 30);
            this.lblProdutoNome.Location = new Point(30, 85);
            this.lblProdutoNome.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtProdutoNome.Size = new Size(450, 35);
            this.txtProdutoNome.Location = new Point(190, 85);
            this.txtProdutoNome.Font = new Font("Arial", 11);
            this.txtProdutoNome.ReadOnly = true;
            this.txtProdutoNome.BackColor = Color.LightGray;
            
            // LINHA 3: Estoque Atual e Quantidade (lado a lado)
            this.lblEstoqueAtual.Text = "Estoque Atual:";
            this.lblEstoqueAtual.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblEstoqueAtual.Size = new Size(150, 30);
            this.lblEstoqueAtual.Location = new Point(30, 145);
            this.lblEstoqueAtual.TextAlign = ContentAlignment.MiddleLeft;
            
            this.txtEstoqueAtual.Size = new Size(120, 35);
            this.txtEstoqueAtual.Location = new Point(190, 145);
            this.txtEstoqueAtual.Font = new Font("Arial", 11);
            this.txtEstoqueAtual.ReadOnly = true;
            this.txtEstoqueAtual.BackColor = Color.LightGray;
            this.txtEstoqueAtual.TextAlign = HorizontalAlignment.Center;
            
            this.lblQtdOperacao.Text = "Quantidade:";
            this.lblQtdOperacao.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblQtdOperacao.Size = new Size(150, 30);
            this.lblQtdOperacao.Location = new Point(340, 145);
            this.lblQtdOperacao.TextAlign = ContentAlignment.MiddleLeft;
            
            this.numQtdOperacao.Size = new Size(120, 35);
            this.numQtdOperacao.Location = new Point(460, 145);
            this.numQtdOperacao.Font = new Font("Arial", 11);
            this.numQtdOperacao.Minimum = 1;
            this.numQtdOperacao.Maximum = 9999;
            this.numQtdOperacao.Value = 1;
            
            // LINHA 4: Tipo de Operacao
            this.lblTipoOperacao.Text = "Operacao:";
            this.lblTipoOperacao.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblTipoOperacao.Size = new Size(150, 30);
            this.lblTipoOperacao.Location = new Point(30, 205);
            this.lblTipoOperacao.TextAlign = ContentAlignment.MiddleLeft;
            
            this.cmbTipoOperacao.Size = new Size(250, 35);
            this.cmbTipoOperacao.Location = new Point(190, 205);
            this.cmbTipoOperacao.Font = new Font("Arial", 11);
            this.cmbTipoOperacao.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoOperacao.Items.AddRange(new string[] { "Adicionar ao Estoque", "Remover do Estoque" });
            this.cmbTipoOperacao.SelectedIndex = 0;
            
            // LINHA 5: Botao Atualizar Estoque
            this.btnAtualizarEstoque.Text = "ATUALIZAR";
            this.btnAtualizarEstoque.Size = new Size(220, 55);
            this.btnAtualizarEstoque.Location = new Point(180, 280);
            this.btnAtualizarEstoque.BackColor = Color.LimeGreen;
            this.btnAtualizarEstoque.ForeColor = Color.White;
            this.btnAtualizarEstoque.Font = new Font("Arial", 12, FontStyle.Bold);
            this.btnAtualizarEstoque.FlatStyle = FlatStyle.Flat;
            this.btnAtualizarEstoque.FlatAppearance.BorderSize = 2;
            this.btnAtualizarEstoque.FlatAppearance.BorderColor = Color.DarkGreen;
            this.btnAtualizarEstoque.Cursor = Cursors.Hand;
            this.btnAtualizarEstoque.Click += BtnAtualizarEstoque_Click;
            
            // Adicionar controles à aba Atualizar
            this.tabAtualizar.Controls.Add(this.lblBuscarCodigo);
            this.tabAtualizar.Controls.Add(this.txtBuscarCodigo);
            this.tabAtualizar.Controls.Add(this.btnBuscar);
            this.tabAtualizar.Controls.Add(this.lblProdutoNome);
            this.tabAtualizar.Controls.Add(this.txtProdutoNome);
            this.tabAtualizar.Controls.Add(this.lblEstoqueAtual);
            this.tabAtualizar.Controls.Add(this.txtEstoqueAtual);
            this.tabAtualizar.Controls.Add(this.lblQtdOperacao);
            this.tabAtualizar.Controls.Add(this.numQtdOperacao);
            this.tabAtualizar.Controls.Add(this.lblTipoOperacao);
            this.tabAtualizar.Controls.Add(this.cmbTipoOperacao);
            this.tabAtualizar.Controls.Add(this.btnAtualizarEstoque);
            
            // Adicionar controles ao panel central
            this.pnlCentral.Controls.Add(this.lblTitulo);
            this.pnlCentral.Controls.Add(this.tabControl);
            
            this.Controls.Add(this.pnlCentral);
        }
        
        // ================================================
        // METODOS DE MENSAGEM
        // ================================================
        
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
        
        // ================================================
        // EVENTOS DA ABA CADASTRAR
        // ================================================
        
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                MostrarMensagemGrande("Atencao", "O campo Codigo de Barras e obrigatorio.", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MostrarMensagemGrande("Atencao", "O campo Nome do Produto e obrigatorio.", MessageBoxIcon.Warning);
                return;
            }
            if (cmbCategoria.SelectedIndex == -1)
            {
                MostrarMensagemGrande("Atencao", "Selecione uma categoria.", MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtValor.Text.Replace(".", ","), out decimal valor))
            {
                MostrarMensagemGrande("Atencao", "Valor invalido. Use formato como 99,90 ou 99.90", MessageBoxIcon.Warning);
                return;
            }
            
            string categoria = cmbCategoria.SelectedItem?.ToString() ?? "";
            
            bool sucesso = ProdutoService.CadastrarProduto(
                txtCodigoBarras.Text.Trim(), txtNome.Text.Trim(),
                categoria, txtFabricante.Text.Trim(),
                (int)numQuantidade.Value, valor, chkIsRaro.Checked,
                txtPlataforma.Text.Trim(), int.Parse(txtGarantia.Text), _perfilUsuario);
            
            if (sucesso)
            {
                MostrarMensagemGrande("Sucesso", "Produto cadastrado com sucesso!", MessageBoxIcon.Information);
                txtCodigoBarras.Text = "";
                txtNome.Text = "";
                cmbCategoria.SelectedIndex = -1;
                txtFabricante.Text = "";
                numQuantidade.Value = 0;
                txtValor.Text = "0,00";
                txtPlataforma.Text = "";
                txtGarantia.Text = "12";
                chkIsRaro.Checked = false;
                txtCodigoBarras.Focus();
            }
        }
        
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (MostrarMensagemComEscolha("Cancelar Cadastro", "Tem certeza que deseja cancelar o cadastro? Os dados serao perdidos.") == DialogResult.Yes)
            {
                txtCodigoBarras.Text = "";
                txtNome.Text = "";
                cmbCategoria.SelectedIndex = -1;
                txtFabricante.Text = "";
                numQuantidade.Value = 0;
                txtValor.Text = "0,00";
                txtPlataforma.Text = "";
                txtGarantia.Text = "12";
                chkIsRaro.Checked = false;
            }
        }
        
        // ================================================
        // EVENTOS DA ABA ATUALIZAR ESTOQUE
        // ================================================
        
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarCodigo.Text))
            {
                MostrarMensagemGrande("Atencao", "Digite o codigo de barras do produto.", MessageBoxIcon.Warning);
                return;
            }
            
            var produto = ProdutoService.BuscarProduto(txtBuscarCodigo.Text.Trim());
            
            if (produto == null)
            {
                MostrarMensagemGrande("Erro", "Produto nao encontrado. Verifique o codigo de barras.", MessageBoxIcon.Error);
                txtProdutoNome.Text = "";
                txtEstoqueAtual.Text = "";
                return;
            }
            
            txtProdutoNome.Text = produto.Nome;
            txtEstoqueAtual.Text = produto.QuantidadeEstoque.ToString();
        }
        
        private void BtnAtualizarEstoque_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarCodigo.Text))
            {
                MostrarMensagemGrande("Atencao", "Digite o codigo de barras do produto.", MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtProdutoNome.Text))
            {
                MostrarMensagemGrande("Atencao", "Busque um produto primeiro.", MessageBoxIcon.Warning);
                return;
            }
            
            if (_perfilUsuario != "Estoquista" && _perfilUsuario != "Supervisor")
            {
                MostrarMensagemGrande("Permissao Negada", "Apenas estoquistas e supervisores podem atualizar o estoque.", MessageBoxIcon.Warning);
                return;
            }
            
            string codigoBarras = txtBuscarCodigo.Text.Trim();
            int quantidade = (int)numQtdOperacao.Value;
            bool isAdicionar = cmbTipoOperacao.SelectedIndex == 0;
            int estoqueAtual = int.Parse(txtEstoqueAtual.Text);
            
            if (!isAdicionar && estoqueAtual < quantidade)
            {
                MostrarMensagemGrande("Erro", $"Estoque insuficiente.\nAtual: {estoqueAtual}\nTentativa de remover: {quantidade}", MessageBoxIcon.Error);
                return;
            }
            
            bool sucesso = ProdutoService.AtualizarEstoque(codigoBarras, quantidade, isAdicionar);
            
            if (sucesso)
            {
                string operacao = isAdicionar ? "adicionadas" : "removidas";
                MostrarMensagemGrande("Sucesso", $"Estoque atualizado com sucesso!\n\n{quantidade} unidade(s) {operacao} do estoque.\nNovo estoque: {estoqueAtual + (isAdicionar ? quantidade : -quantidade)}", MessageBoxIcon.Information);
                
                var produto = ProdutoService.BuscarProduto(codigoBarras);
                if (produto != null)
                {
                    txtEstoqueAtual.Text = produto.QuantidadeEstoque.ToString();
                }
                numQtdOperacao.Value = 1;
            }
        }
        
        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}