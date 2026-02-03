using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaGestao.DAL;
using SistemaGestao.Models;

namespace SistemaGestao.Forms
{
    public class FormVendas : Form
    {
        private ComboBox cboCliente;
        private ComboBox cboProduto;
        private NumericUpDown numQuantidade;
        private Label lblPrecoUnitario;
        private Label lblTotal;
        private Button btnRegistrar;

        private ClienteDAL _clienteDal = new ClienteDAL();
        private ProdutoDAL _produtoDal = new ProdutoDAL();
        private VendaDAL _vendaDal = new VendaDAL();

        private List<Cliente> _clientes;
        private List<Produto> _produtos;

        public FormVendas()
        {
            InitializeComponent();
            CarregarCombos();
        }

        private void InitializeComponent()
        {
            this.Text = "Registro de Vendas";
            this.Size = new Size(600, 350);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblCliente = new Label { Text = "Cliente:", Left = 20, Top = 20, Width = 80 };
            cboCliente = new ComboBox { Left = 110, Top = 18, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblProduto = new Label { Text = "Produto:", Left = 20, Top = 60, Width = 80 };
            cboProduto = new ComboBox { Left = 110, Top = 58, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cboProduto.SelectedIndexChanged += CboProduto_SelectedIndexChanged;

            var lblQuantidade = new Label { Text = "Quantidade:", Left = 20, Top = 100, Width = 80 };
            numQuantidade = new NumericUpDown { Left = 110, Top = 98, Width = 100, Minimum = 1, Maximum = 100000 };
            numQuantidade.ValueChanged += NumQuantidade_ValueChanged;

            var lblPrecoUnitTitulo = new Label { Text = "Preço unitário:", Left = 20, Top = 140, Width = 100 };
            lblPrecoUnitario = new Label { Left = 130, Top = 140, Width = 100, Text = "R$ 0,00" };

            var lblTotalTitulo = new Label { Text = "Total:", Left = 20, Top = 180, Width = 100 };
            lblTotal = new Label
            {
                Left = 130,
                Top = 180,
                Width = 150,
                Text = "R$ 0,00",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            btnRegistrar = new Button { Text = "Registrar Venda", Left = 20, Top = 230, Width = 200, Height = 40 };
            btnRegistrar.Click += BtnRegistrar_Click;

            this.Controls.Add(lblCliente);
            this.Controls.Add(cboCliente);
            this.Controls.Add(lblProduto);
            this.Controls.Add(cboProduto);
            this.Controls.Add(lblQuantidade);
            this.Controls.Add(numQuantidade);
            this.Controls.Add(lblPrecoUnitTitulo);
            this.Controls.Add(lblPrecoUnitario);
            this.Controls.Add(lblTotalTitulo);
            this.Controls.Add(lblTotal);
            this.Controls.Add(btnRegistrar);
        }

        private void CarregarCombos()
        {
            _clientes = _clienteDal.ListarTodos();
            _produtos = _produtoDal.ListarTodos();

            cboCliente.DataSource = _clientes.ToList();
            cboCliente.DisplayMember = "Nome";
            cboCliente.ValueMember = "Id";

            cboProduto.DataSource = _produtos.ToList();
            cboProduto.DisplayMember = "Nome";
            cboProduto.ValueMember = "Id";

            AtualizarValores();
        }

        private void CboProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarValores();
        }

        private void NumQuantidade_ValueChanged(object sender, EventArgs e)
        {
            AtualizarValores();
        }

        private void AtualizarValores()
        {
            if (cboProduto.SelectedItem is Produto produto)
            {
                lblPrecoUnitario.Text = $"R$ {produto.Preco:N2}";
                var total = produto.Preco * numQuantidade.Value;
                lblTotal.Text = $"R$ {total:N2}";
            }
            else
            {
                lblPrecoUnitario.Text = "R$ 0,00";
                lblTotal.Text = "R$ 0,00";
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (!(cboCliente.SelectedItem is Cliente cliente))
            {
                MessageBox.Show("Selecione um cliente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(cboProduto.SelectedItem is Produto produto))
            {
                MessageBox.Show("Selecione um produto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numQuantidade.Value <= 0)
            {
                MessageBox.Show("Informe uma quantidade válida.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numQuantidade.Value > produto.QuantidadeEstoque)
            {
                MessageBox.Show("Quantidade maior que o estoque disponível.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var venda = new Venda
            {
                ClienteId = cliente.Id,
                ProdutoId = produto.Id,
                Quantidade = (int)numQuantidade.Value,
                ValorTotal = produto.Preco * numQuantidade.Value,
                DataVenda = DateTime.Now
            };

            _vendaDal.Inserir(venda);
            _produtoDal.AtualizarEstoque(produto.Id, (int)numQuantidade.Value);

            MessageBox.Show("Venda registrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // recarrega produtos para atualizar estoque
            CarregarCombos();
        }
    }
}
