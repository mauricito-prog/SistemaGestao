using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaGestao.DAL;

namespace SistemaGestao.Forms
{
    public class FormConsultaVendas : Form
    {
        private DataGridView dgv;
        private VendaDAL _dal = new VendaDAL();

        public FormConsultaVendas()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void InitializeComponent()
        {
            this.Text = "Consulta de Vendas";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            dgv = new DataGridView
            {
                Left = 20,
                Top = 20,
                Width = 840,
                Height = 520,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.Add(dgv);
        }

        private void CarregarDados()
        {
            dgv.DataSource = _dal.ListarTodas();
            dgv.ClearSelection();
        }
    }
}
