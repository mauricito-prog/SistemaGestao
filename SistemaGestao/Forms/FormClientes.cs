using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaGestao.DAL;
using SistemaGestao.Models;

namespace SistemaGestao.Forms
{
    public class FormClientes : Form
    {
        private DataGridView dgv;
        private TextBox txtNome;
        private TextBox txtTelefone;
        private TextBox txtEmail;
        private TextBox txtEndereco;
        private Button btnNovo;
        private Button btnSalvar;
        private Button btnExcluir;

        private ClienteDAL _dal = new ClienteDAL();
        private Cliente _clienteSelecionado = null;

        public FormClientes()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void InitializeComponent()
        {
            this.Text = "Cadastro de Clientes";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblNome = new Label { Text = "Nome:", Left = 20, Top = 20, Width = 80 };
            txtNome = new TextBox { Left = 110, Top = 18, Width = 300 };

            var lblTelefone = new Label { Text = "Telefone:", Left = 20, Top = 55, Width = 80 };
            txtTelefone = new TextBox { Left = 110, Top = 53, Width = 200 };

            var lblEmail = new Label { Text = "Email:", Left = 20, Top = 90, Width = 80 };
            txtEmail = new TextBox { Left = 110, Top = 88, Width = 300 };

            var lblEndereco = new Label { Text = "Endereço:", Left = 20, Top = 125, Width = 80 };
            txtEndereco = new TextBox { Left = 110, Top = 123, Width = 400 };

            btnNovo = new Button { Text = "Novo", Left = 20, Top = 170, Width = 100 };
            btnSalvar = new Button { Text = "Salvar", Left = 130, Top = 170, Width = 100 };
            btnExcluir = new Button { Text = "Excluir", Left = 240, Top = 170, Width = 100 };

            btnNovo.Click += BtnNovo_Click;
            btnSalvar.Click += BtnSalvar_Click;
            btnExcluir.Click += BtnExcluir_Click;

            dgv = new DataGridView
            {
                Left = 20,
                Top = 210,
                Width = 740,
                Height = 330,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgv.CellClick += Dgv_CellClick;

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(lblTelefone);
            this.Controls.Add(txtTelefone);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblEndereco);
            this.Controls.Add(txtEndereco);
            this.Controls.Add(btnNovo);
            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnExcluir);
            this.Controls.Add(dgv);
        }

        private void CarregarDados()
        {
            dgv.DataSource = _dal.ListarTodos();
            dgv.ClearSelection();
            _clienteSelecionado = null;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            _clienteSelecionado = null;
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Nome e Telefone são obrigatórios.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_clienteSelecionado == null) // Inserir
            {
                var cliente = new Cliente
                {
                    Nome = txtNome.Text.Trim(),
                    Telefone = txtTelefone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Endereco = txtEndereco.Text.Trim()
                };

                _dal.Inserir(cliente);
            }
            else // Atualizar
            {
                _clienteSelecionado.Nome = txtNome.Text.Trim();
                _clienteSelecionado.Telefone = txtTelefone.Text.Trim();
                _clienteSelecionado.Email = txtEmail.Text.Trim();
                _clienteSelecionado.Endereco = txtEndereco.Text.Trim();

                _dal.Atualizar(_clienteSelecionado);
            }

            CarregarDados();
            LimparCampos();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (_clienteSelecionado == null)
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var r = MessageBox.Show($"Deseja excluir o cliente '{_clienteSelecionado.Nome}'?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                _dal.Excluir(_clienteSelecionado.Id);
                CarregarDados();
                LimparCampos();
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv.Rows.Count > 0)
            {
                var row = dgv.Rows[e.RowIndex];
                _clienteSelecionado = new Cliente
                {
                    Id = Convert.ToInt32(row.Cells["Id"].Value),
                    Nome = row.Cells["Nome"].Value?.ToString(),
                    Telefone = row.Cells["Telefone"].Value?.ToString(),
                    Email = row.Cells["Email"].Value?.ToString(),
                    Endereco = row.Cells["Endereco"].Value?.ToString()
                };

                txtNome.Text = _clienteSelecionado.Nome;
                txtTelefone.Text = _clienteSelecionado.Telefone;
                txtEmail.Text = _clienteSelecionado.Email;
                txtEndereco.Text = _clienteSelecionado.Endereco;
            }
        }
    }
}
