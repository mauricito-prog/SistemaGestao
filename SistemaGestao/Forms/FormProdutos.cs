using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaGestao.DAL;
using SistemaGestao.Models;

namespace SistemaGestao.Forms
{
    public class FormProdutos : Form
    {
        private DataGridView dgv;
        private TextBox txtNome;
        private NumericUpDown numPreco;
        private NumericUpDown numEstoque;
        private Button btnNovo;
        private Button btnSalvar;
        private Button btnExcluir;

        private ProdutoDAL _dal = new ProdutoDAL();
        private Produto _produtoSelecionado = null;

        public FormProdutos()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void InitializeComponent()
        {
            this.Text = "Cadastro de Produtos";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblNome = new Label { Text = "Nome:", Left = 20, Top = 20, Width = 80 };
            txtNome = new TextBox { Left = 110, Top = 18, Width = 300 };

            var lblPreco = new Label { Text = "Preço:", Left = 20, Top = 55, Width = 80 };
            numPreco = new NumericUpDown
            {
                Left = 110,
                Top = 53,
                Width = 120,
                DecimalPlaces = 2,
                Maximum = 1000000
            };

            var lblEstoque = new Label { Text = "Estoque:", Left = 20, Top = 90, Width = 80 };
            numEstoque = new NumericUpDown
            {
                Left = 110,
                Top = 88,
                Width = 120,
                Maximum = 1000000
            };

            btnNovo = new Button { Text = "Novo", Left = 20, Top = 130, Width = 100 };
            btnSalvar = new Button { Text = "Salvar", Left = 130, Top = 130, Width = 100 };
            btnExcluir = new Button { Text = "Excluir", Left = 240, Top = 130, Width = 100 };

            btnNovo.Click += BtnNovo_Click;
            btnSalvar.Click += BtnSalvar_Click;
            btnExcluir.Click += BtnExcluir_Click;

            dgv = new DataGridView
            {
                Left = 20,
                Top = 180,
                Width = 740,
                Height = 360,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgv.CellClick += Dgv_CellClick;

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(lblPreco);
            this.Controls.Add(numPreco);
            this.Controls.Add(lblEstoque);
            this.Controls.Add(numEstoque);
            this.Controls.Add(btnNovo);
            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnExcluir);
            this.Controls.Add(dgv);
        }

        private void CarregarDados()
        {
            dgv.DataSource = _dal.ListarTodos();
            dgv.ClearSelection();
            _produtoSelecionado = null;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            numPreco.Value = 0;
            numEstoque.Value = 0;
            _produtoSelecionado = null;
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_produtoSelecionado == null) // Inserir
            {
                var produto = new Produto
                {
                    Nome = txtNome.Text.Trim(),
                    Preco = numPreco.Value,
                    QuantidadeEstoque = (int)numEstoque.Value
                };

                _dal.Inserir(produto);
            }
            else // Atualizar
            {
                _produtoSelecionado.Nome = txtNome.Text.Trim();
                _produtoSelecionado.Preco = numPreco.Value;
                _produtoSelecionado.QuantidadeEstoque = (int)numEstoque.Value;

                _dal.Atualizar(_produtoSelecionado);
            }

            CarregarDados();
            LimparCampos();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (_produtoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var r = MessageBox.Show($"Deseja excluir o produto '{_produtoSelecionado.Nome}'?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                _dal.Excluir(_produtoSelecionado.Id);
                CarregarDados();
                LimparCampos();
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv.Rows.Count > 0)
            {
                var row = dgv.Rows[e.RowIndex];
                _produtoSelecionado = new Produto
                {
                    Id = Convert.ToInt32(row.Cells["Id"].Value),
                    Nome = row.Cells["Nome"].Value?.ToString(),
                    Preco = Convert.ToDecimal(row.Cells["Preco"].Value),
                    QuantidadeEstoque = Convert.ToInt32(row.Cells["QuantidadeEstoque"].Value)
                };

                txtNome.Text = _produtoSelecionado.Nome;
                numPreco.Value = _produtoSelecionado.Preco;
                numEstoque.Value = _produtoSelecionado.QuantidadeEstoque;
            }
        }
    }
}
