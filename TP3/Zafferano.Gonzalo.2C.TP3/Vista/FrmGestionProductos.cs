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
    public partial class FrmGestionProductos : Form
    {
        private Administrador administrador;
        private List<Producto> productos;
        public FrmGestionProductos(Administrador administrador)
        {
            this.InitializeComponent();
            this.administrador = administrador;
            this.productos = new List<Producto>();
        }

        private void GestionProductos_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Administrador: ");
            sb.Append($"{this.administrador.NombreUsuario} ");
            sb.Append($"({this.administrador.NombreCompleto})");

            this.Text = sb.ToString();
            this.RefrescardataGrid();        
        }

        /// <summary>
        /// Refresca el datagrid
        /// </summary>
        private void RefrescardataGrid()
        {
            this.dgvListaProductos.DataSource = null;
            this.CargarListaProductos();

            this.CargarDataGridConListaProductos();

            this.dgvListaProductos.Width = Producto.Count > 8 ? this.AnchoTotalColumnasProducto() + 22 : this.AnchoTotalColumnasProducto();
        }

        /// <summary>
        /// Obtiene el ancho total del datagrid
        /// </summary>
        /// <returns></returns>
        private int AnchoTotalColumnasProducto()
        {
            int sumadorAncho = 0;
            for (int i = 0; i < this.dgvListaProductos.ColumnCount; i++)
            {
                sumadorAncho += this.dgvListaProductos.Columns[i].Width;
            }

            return sumadorAncho + 3;
        }

        /// <summary>
        /// Carga la lista de la instancia con los Productos cargados en el sistema.
        /// </summary>
        private void CargarListaProductos()
        {
            this.productos.Clear();

            for (int i = 0; i < Producto.Count; i++)
            {
                this.productos.Add(Producto.ObtenerUnProductoDeLaListaPorIndice(i));
            }
        }

        /// <summary>
        /// Carga el datagrid con datos.
        /// </summary>
        private void CargarDataGridConListaProductos()
        {
            this.CrearColumnas();
            this.AsociarPropiedadesAlDataGrid();
            this.dgvListaProductos.DataSource = this.productos;
            this.OrdenarColumnasDataGrid();
        }

        /// <summary>
        /// Crea las columnas del datagrid.
        /// </summary>
        private void CrearColumnas()
        {
            this.dgvListaProductos.Columns.Add("id", "Id");
            this.dgvListaProductos.Columns.Add("nombre", "Nombre Producto");
            this.dgvListaProductos.Columns.Add("descripcion", "Descripcion");
            this.dgvListaProductos.Columns.Add("precio", "Precio");
            this.dgvListaProductos.Columns.Add("tipo", "Tipo");
        }

        /// <summary>
        /// Asocia propiedades a las columnas del datagrid.
        /// </summary>
        private void AsociarPropiedadesAlDataGrid()
        {
            this.dgvListaProductos.Columns["id"].DataPropertyName = "IdProducto";
            this.dgvListaProductos.Columns["nombre"].DataPropertyName = "NombreProducto";
            this.dgvListaProductos.Columns["descripcion"].DataPropertyName = "DescripcionProducto";
            this.dgvListaProductos.Columns["precio"].DataPropertyName = "PrecioProducto";
            this.dgvListaProductos.Columns["tipo"].DataPropertyName = "Tipo";
        }

        /// <summary>
        /// Ordena las columnas del datagrid.
        /// </summary>
        private void OrdenarColumnasDataGrid()
        {
            this.dgvListaProductos.Columns["id"].DisplayIndex = 0;
            this.dgvListaProductos.Columns["nombre"].DisplayIndex = 1;
            this.dgvListaProductos.Columns["tipo"].DisplayIndex = 2;
            this.dgvListaProductos.Columns["descripcion"].DisplayIndex = 3;
            this.dgvListaProductos.Columns["precio"].DisplayIndex = 4;

            this.dgvListaProductos.Columns["id"].Width = 75;
            this.dgvListaProductos.Columns["nombre"].Width = 220;
            this.dgvListaProductos.Columns["descripcion"].Width = 350;
            this.dgvListaProductos.Columns["precio"].DefaultCellStyle.Format = "$0,0.00";
        }


        private void btnAltaProducto_Click(object sender, EventArgs e)
        {
            FrmAltaProducto altaProducto = new FrmAltaProducto(this.administrador);
            if(altaProducto.ShowDialog() == DialogResult.OK)
            {
                this.RefrescardataGrid();
            }
        }

        private void btnModificacionProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.dgvListaProductos.SelectedRows != null && this.dgvListaProductos.SelectedRows.Count > 0)
                {
                    Producto producto = (this.dgvListaProductos.SelectedRows[0].DataBoundItem as Producto);

                    if(producto is not null)
                    {
                        FrmModificarProducto modificarProducto = new FrmModificarProducto(this.administrador, producto);
                        if(modificarProducto.ShowDialog() == DialogResult.OK)
                        {
                            this.RefrescardataGrid();
                        }
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show($"Debe seleccionar un producto.", "Aviso: Debe seleccionar un producto.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBajaProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvListaProductos.SelectedRows != null && this.dgvListaProductos.SelectedRows.Count > 0)
                {
                    Producto producto = (this.dgvListaProductos.SelectedRows[0].DataBoundItem as Producto);

                    if(producto != null)
                    {
                        DialogResult respuesta =  MessageBox.Show($"¿Seguro desea eliminar el producto seleccionado?{Environment.NewLine}{producto}", "Aviso: Eliminar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        try
                        {
                            if (respuesta == DialogResult.Yes && this.administrador.EliminarUnProductoDelSistema(producto))
                            {
                                this.RefrescardataGrid();
                            }
                        }
                        catch (ArchivoException ex)
                        {
                            MessageBox.Show($"Ocurrio un error relacionado con los archivos de respaldo: {ex.Message}. No se guardaran los cambios realizados al cerrar sesion.", "Aviso: Error con los archivos de respaldo.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("En este momento no se pueden guardar cambios en el sistema. Por favor reintente mas tarde.", "Aviso: No se pueden guardar los cambios.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Debe seleccionar un producto.", "Aviso: Debe seleccionar un producto.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
