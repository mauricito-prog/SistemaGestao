using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaGestao
{
    public class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            DatabaseHelper.InicializarBanco();
        }

        private void InitializeComponent()
        {
            this.Text = "Sistema de Gestão";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            var menuStrip = new MenuStrip
            {
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F)
            };

            var menuCadastros = new ToolStripMenuItem("Cadastros");
            var menuClientes = new ToolStripMenuItem("Clientes", null, MenuClientes_Click);
            var menuProdutos = new ToolStripMenuItem("Produtos", null, MenuProdutos_Click);
            menuCadastros.DropDownItems.Add(menuClientes);
            menuCadastros.DropDownItems.Add(menuProdutos);

            var menuVendas = new ToolStripMenuItem("Vendas");
            var menuNovaVenda = new ToolStripMenuItem("Nova Venda", null, MenuNovaVenda_Click);
            var menuConsultarVendas = new ToolStripMenuItem("Consultar Vendas", null, MenuConsultarVendas_Click);
            menuVendas.DropDownItems.Add(menuNovaVenda);
            menuVendas.DropDownItems.Add(menuConsultarVendas);

            var menuSair = new ToolStripMenuItem("Sair", null, MenuSair_Click);

            menuStrip.Items.Add(menuCadastros);
            menuStrip.Items.Add(menuVendas);
            menuStrip.Items.Add(menuSair);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            var painelBemVindo = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var lblBemVindo = new Label
            {
                Text = "Bem-vindo ao Sistema de Gestão!\n\nUse o menu acima para:\n\n" +
                       "• Cadastrar clientes\n" +
                       "• Cadastrar produtos\n" +
                       "• Registrar vendas\n" +
                       "• Consultar vendas",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.SteelBlue
            };

            painelBemVindo.Controls.Add(lblBemVindo);
            this.Controls.Add(painelBemVindo);
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            using (var form = new Forms.FormClientes())
            {
                form.ShowDialog();
            }
        }

        private void MenuProdutos_Click(object sender, EventArgs e)
        {
            using (var form = new Forms.FormProdutos())
            {
                form.ShowDialog();
            }
        }

        private void MenuNovaVenda_Click(object sender, EventArgs e)
        {
            using (var form = new Forms.FormVendas())
            {
                form.ShowDialog();
            }
        }

        private void MenuConsultarVendas_Click(object sender, EventArgs e)
        {
            using (var form = new Forms.FormConsultaVendas())
            {
                form.ShowDialog();
            }
        }

        private void MenuSair_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show(
                "Deseja realmente sair do sistema?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
                Application.Exit();
        }
    }
}
