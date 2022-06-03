using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Vista
{
    public partial class FrmListaClientes : Form
    {
        private Cliente cliente;
        private List<Cliente> clientes;
        Empleado empleado;
        public Cliente Cliente
        {
            get
            {
                return this.cliente;
            }
        }
        public FrmListaClientes(Empleado empleado)
        {
            this.InitializeComponent();
            this.clientes = new List<Cliente>();
            this.empleado = empleado;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.dgvListaClientes.SelectedRows != null && this.dgvListaClientes.SelectedRows.Count > 0)
                {
                    this.cliente = (this.dgvListaClientes.SelectedRows[0].DataBoundItem as Cliente);

                    if(this.cliente != null)
                    {
                        this.DialogResult = DialogResult.OK;   
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Debe seleccionar un cliente.", "Aviso: Debe seleccionar un cliente.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmListaClientes_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Menu Seleccionar Cliente. ");
            sb.Append($"{this.empleado.GetType().Name}: ");
            sb.Append($"{this.empleado.NombreUsuario} ");
            sb.Append($"({this.empleado.NombreCompleto})");

            this.Text = sb.ToString();

            this.RefrescardataGrid();
        }

        /// <summary>
        /// Refresca el datagrid
        /// </summary>
        private void RefrescardataGrid()
        {
            this.dgvListaClientes.DataSource = null;
            this.CargarListaDeClientes();
            this.CargarDataGridConListaClientes();

            this.dgvListaClientes.Width = Cliente.Count > 8 ? this.AnchoTotalColumnasCliente() + 22 : this.AnchoTotalColumnasCliente();
        }

        /// <summary>
        /// Carga la lista de la instancia con CLientes cargados en el sistema.
        /// </summary>
        private void CargarListaDeClientes()
        {
            this.clientes.Clear();

            for (int i = 0; i < Cliente.Count; i++)
            {
                this.clientes.Add(Cliente.ObtenerUnClienteDeLaListaPorIndice(i));
            }
        }        

        /// <summary>
        /// Obtiene el ancho total del datagrid
        /// </summary>
        /// <returns></returns>
        private int AnchoTotalColumnasCliente()
        {
            int sumadorAncho = 0;
            for (int i = 0; i < this.dgvListaClientes.ColumnCount; i++)
            {
                sumadorAncho += this.dgvListaClientes.Columns[i].Width;
            }

            return sumadorAncho + 3;
        }

        /// <summary>
        /// Carga el datagrid con datos.
        /// </summary>
        private void CargarDataGridConListaClientes()
        {
            this.CrearColumnas();
            this.AsociarPropiedadesAlDataGrid();
            this.dgvListaClientes.DataSource = this.clientes;
            this.OrdenarColumnasDataGrid();
        }

        /// <summary>
        /// Crea las columnas del datagrid.
        /// </summary>
        private void CrearColumnas()
        {
            this.dgvListaClientes.Columns.Add("dni", "Dni");
            this.dgvListaClientes.Columns.Add("nombreCompleto", "Nombre");
            this.dgvListaClientes.Columns.Add("telefono", "Telefono");
            this.dgvListaClientes.Columns.Add("inversionTotal", "Inversion Anual");
        }

        /// <summary>
        /// Asocia propiedades a las columnas del datagrid.
        /// </summary>
        private void AsociarPropiedadesAlDataGrid()
        {
            this.dgvListaClientes.Columns["dni"].DataPropertyName = "Dni";
            this.dgvListaClientes.Columns["nombreCompleto"].DataPropertyName = "NombreCompleto";
            this.dgvListaClientes.Columns["telefono"].DataPropertyName = "Telefono";
            this.dgvListaClientes.Columns["inversionTotal"].DataPropertyName = "InversionTotalUltimoAnio";
        }  

        /// <summary>
        /// Ordena las columnas del datagrid.
        /// </summary>
        private void OrdenarColumnasDataGrid()
        {
            this.dgvListaClientes.Columns["dni"].DisplayIndex = 0;
            this.dgvListaClientes.Columns["nombreCompleto"].DisplayIndex = 1;
            this.dgvListaClientes.Columns["telefono"].DisplayIndex = 2;
            this.dgvListaClientes.Columns["inversionTotal"].DisplayIndex = 3;

            this.dgvListaClientes.Columns["dni"].Width = 125;
            this.dgvListaClientes.Columns["nombreCompleto"].Width = 200;
            this.dgvListaClientes.Columns["telefono"].Width = 175;
            this.dgvListaClientes.Columns["inversionTotal"].Width = 200;
            this.dgvListaClientes.Columns["inversionTotal"].DefaultCellStyle.Format = "$0,0.00";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
