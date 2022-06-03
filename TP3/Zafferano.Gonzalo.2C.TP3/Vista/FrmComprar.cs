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
    public partial class FrmComprar : Form
    {
        private Empleado empleado;
        private Cliente cliente;
        private CarritoDeCompra carrito;
        List<ProductoEnCarrito> productosEnCarrito;
        List<Producto> productos;

        public FrmComprar(Empleado empleado, Cliente cliente, CarritoDeCompra carrito)
        {
            this.InitializeComponent();
            this.empleado = empleado;
            this.cliente = cliente;
            this.carrito = carrito;
            this.productosEnCarrito = new List<ProductoEnCarrito>();
            this.productos = new List<Producto>();
        }

        private void FrmComprar_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Menu Atender Cliente. ");
            sb.Append($"{this.empleado.GetType().Name}: ");
            sb.Append($"{this.empleado.NombreUsuario} ");
            sb.Append($"({this.empleado.NombreCompleto})");

            this.Text = sb.ToString();

            this.rBtnChico.Checked = true;
            this.rBtnSinColor.Checked = true;
            this.nUpDownCantidad.Minimum = 1;
            this.nUpDownCantidad.Maximum = 100;

            this.CargarDataGridListaProductos();
            this.dgvListaCarrito.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgvListaCarrito.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }

        /// <summary>
        /// Carga el datagrid Lista de productos con datos, invocando distintos metodos.
        /// </summary>
        private void CargarDataGridListaProductos()
        {
            this.CrearColumnasDataGridProductos();
            this.AsociarPropiedadesAlDataGrid();
            this.CargarListaDeProductos();
            this.dgvListaProductos.DataSource = this.productos;
            this.OrdenarColumnasDataGrid();

            this.dgvListaProductos.Width = this.productos.Count > 5 ?
                this.AnchoTotalColumnasProducto(this.dgvListaProductos) + 22 : 
                this.AnchoTotalColumnasProducto(this.dgvListaProductos);
        }

        /// <summary>
        /// Crea las columnas del datagrid Lista de productos.
        /// </summary>
        private void CrearColumnasDataGridProductos()
        {
            this.dgvListaProductos.Columns.Add("id", "Id");
            this.dgvListaProductos.Columns.Add("nombre", "Nombre Producto");
            this.dgvListaProductos.Columns.Add("descripcion", "Descripcion");
            this.dgvListaProductos.Columns.Add("precio", "Precio");
            this.dgvListaProductos.Columns.Add("tipo", "Tipo");
        }

        /// <summary>
        /// Asocia propiedades a cada columna del datagrid Lista de productos.
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
        /// Carga la lista de la instancia con elementos de la clase Productos.
        /// </summary>
        private void CargarListaDeProductos()
        {
            for (int i = 0; i < Producto.Count; i++)
            {
                this.productos.Add(Producto.ObtenerUnProductoDeLaListaPorIndice(i));
            }
        }

        /// <summary>
        /// Ordena las columnas del datagrid Lista de productos y les asigna un ancho.
        /// </summary>
        private void OrdenarColumnasDataGrid()
        {
            this.dgvListaProductos.Columns["id"].DisplayIndex = 0;
            this.dgvListaProductos.Columns["nombre"].DisplayIndex = 1;
            this.dgvListaProductos.Columns["tipo"].DisplayIndex = 2;
            this.dgvListaProductos.Columns["descripcion"].DisplayIndex = 3;
            this.dgvListaProductos.Columns["precio"].DisplayIndex = 4;

            this.dgvListaProductos.Columns["id"].Width = 125;
            this.dgvListaProductos.Columns["nombre"].Width = 220;
            this.dgvListaProductos.Columns["descripcion"].Width = 350;
            this.dgvListaProductos.Columns["precio"].DefaultCellStyle.Format = "$0,0.00";
        }

        /// <summary>
        /// Obtiene el ancho total del datagrid
        /// </summary>
        /// <param name="dataGrid">datagrid a evaluar</param>
        /// <returns>Ancho total del datagrid.</returns>
        private int AnchoTotalColumnasProducto(DataGridView dataGrid)
        {
            int sumadorAncho = 0;
            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                sumadorAncho += dataGrid.Columns[i].Width;
            }
            return sumadorAncho + 3;
        }

        /// <summary>
        /// Actualiza el datagrid de Lista en carrito.
        /// </summary>
        private void RefrescarDataGridCarrito()
        {
            this.dgvListaCarrito.DataSource = null;
            this.CargarListaDeProductosEnCarrito();
            this.CargarDataGridListaCarrito();
            this.ActualizarPrecio();

            this.dgvListaCarrito.Width = this.productosEnCarrito.Count > 1 ?
                this.AnchoTotalColumnasProducto(this.dgvListaCarrito) + 22 :
                this.AnchoTotalColumnasProducto(this.dgvListaCarrito);
        }

        /// <summary>
        /// Carga la lista de elementos en el datagrid.
        /// </summary>
        private void CargarListaDeProductosEnCarrito()
        {
            this.productosEnCarrito.Clear();

            for (int i = 0; i < this.carrito.Count; i++)
            {
                this.productosEnCarrito.Add(this.carrito.ObtenerUnProductoDelCarritoPorIndice(i));
            }
        }

        /// <summary>
        /// Carga el datagrid lista en carrito.
        /// </summary>
        private void CargarDataGridListaCarrito()
        {
            this.CrearColumnasDataGridListaCarrito();
            this.AsociarPropiedadesAlDataGridCarrito();
            this.dgvListaCarrito.DataSource = this.productosEnCarrito;
            this.OrdenarColumnasDataGridCarrito();
        }

        /// <summary>
        /// Actualiza el precio en el Label del total.
        /// </summary>
        private void ActualizarPrecio()
        {
            this.lblTotal.Text = $"Total: ${string.Format("{0:0,0.00}", this.carrito.PrecioTotalAcumuladoEnCarritoSinDescuentoIncluido)}";
        }

        /// <summary>
        /// Crea las columnas del datagrid lista en carrito.
        /// </summary>
        private void CrearColumnasDataGridListaCarrito()
        {
            this.dgvListaCarrito.Columns.Add("id", "Id");
            this.dgvListaCarrito.Columns.Add("nombre", "Nombre Producto");
            this.dgvListaCarrito.Columns.Add("descripcion", "Descripcion");
            this.dgvListaCarrito.Columns.Add("precio", "Precio x Unidad");
            this.dgvListaCarrito.Columns.Add("cantidad", "Cantidad");
            this.dgvListaCarrito.Columns.Add("precioTotal", "Precio Total");
        }

        /// <summary>
        /// Asocia propiedades en las columnas del datagrid lista en carrito.
        /// </summary>
        private void AsociarPropiedadesAlDataGridCarrito()
        {
            this.dgvListaCarrito.Columns["id"].DataPropertyName = "IdProducto";
            this.dgvListaCarrito.Columns["nombre"].DataPropertyName = "NombreProducto";
            this.dgvListaCarrito.Columns["descripcion"].DataPropertyName = "DescripcionProducto";
            this.dgvListaCarrito.Columns["precio"].DataPropertyName = "PrecioUnitarioProductoEnCarrito";
            this.dgvListaCarrito.Columns["cantidad"].DataPropertyName = "Cantidad";
            this.dgvListaCarrito.Columns["precioTotal"].DataPropertyName = "PrecioTotalProductoEnCarrito";
        }

        /// <summary>
        /// Ordena las columnas del datagrid lista en carrito.
        /// </summary>
        private void OrdenarColumnasDataGridCarrito()
        {
            this.dgvListaCarrito.Columns["id"].DisplayIndex = 0;
            this.dgvListaCarrito.Columns["nombre"].DisplayIndex = 1;
            this.dgvListaCarrito.Columns["descripcion"].DisplayIndex = 2;
            this.dgvListaCarrito.Columns["precio"].DisplayIndex = 3;
            this.dgvListaCarrito.Columns["cantidad"].DisplayIndex = 4;
            this.dgvListaCarrito.Columns["precioTotal"].DisplayIndex = 5;

            this.dgvListaCarrito.Columns["id"].Width = 125;
            this.dgvListaCarrito.Columns["descripcion"].Width = 350;
            this.dgvListaCarrito.Columns["nombre"].Width = 220;
            this.dgvListaCarrito.Columns["precio"].Width = 205;
            this.dgvListaCarrito.Columns["precioTotal"].Width = 175;
            this.dgvListaCarrito.Columns["precio"].DefaultCellStyle.Format = "$0,0.00";
            this.dgvListaCarrito.Columns["precioTotal"].DefaultCellStyle.Format = "$0,0.00"; 
        }

        private void btnCargarALaLista_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvListaCarrito.SelectedRows != null && this.dgvListaProductos.SelectedRows.Count > 0)
                {
                    Producto producto = (this.dgvListaProductos.SelectedRows[0].DataBoundItem as Producto);

                    if (producto != null)
                    {
                        if (producto is Disenio)
                        {
                            foreach (Control control in this.gBoxTamanio.Controls)
                            {
                                if (control is RadioButton && ((RadioButton)control).Checked &&
                                    Enum.TryParse(((RadioButton)control).Text, out Disenio.Tamanio tamanio))
                                {
                                    producto = producto.ClonarProducto(tamanio);
                                }
                            }
                        }
                        else if (producto is Impresion)
                        {
                            producto = producto.ClonarProducto(this.rBtnColor.Checked);
                        }

                        ProductoEnCarrito productoEnCarrito = new ProductoEnCarrito(producto, (int)this.nUpDownCantidad.Value, this.txtDetallesProducto.Text);

                        if(this.carrito.CargarProductoEnCarrito(productoEnCarrito))
                        {
                            this.txtDetallesProducto.Clear();
                            this.RefrescarDataGridCarrito();
                        }                  
                    }
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("No se ha podido cargar el producto en el carrito. Reintente mas tarde", "Aviso: Error al cargar producto.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(CargaDeDatosInvalidosException ex)
            {
                MessageBox.Show(ex.Message, "Aviso: Error al cargar producto.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show($"Debe seleccionar un producto.", "Aviso: Debe seleccionar un producto.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBorrarDelCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.dgvListaCarrito.SelectedRows != null && this.dgvListaCarrito.SelectedRows.Count > 0)
                {
                    ProductoEnCarrito productoEnCarrito = (this.dgvListaCarrito.SelectedRows[0].DataBoundItem as ProductoEnCarrito);

                    if(productoEnCarrito != null && this.carrito.EliminarProductoDelCarrito(productoEnCarrito))
                    {
                        this.RefrescarDataGridCarrito();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Debe seleccionar un producto.", "Aviso: Debe seleccionar un producto.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvListaProductos_SelectionChanged(object sender, EventArgs e)
        {
            if(this.dgvListaProductos.SelectedRows.Count > 0)
            {
                if(this.dgvListaProductos.SelectedRows[0].DataBoundItem is Disenio)
                {
                    this.gBoxTamanio.Visible = true;
                    this.gBoxColor.Visible = false;
                    this.nUpDownCantidad.Value = 1;
                    this.nUpDownCantidad.Enabled = false;
                }
                else if (this.dgvListaProductos.SelectedRows[0].DataBoundItem is Impresion)
                {
                    this.gBoxTamanio.Visible = false;
                    this.gBoxColor.Visible = true;
                    this.nUpDownCantidad.Enabled = true;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(this.carrito.Count > 0)
            {
                FrmConfirmarCompra confirmarCompra = new FrmConfirmarCompra(this.empleado, this.cliente, this.carrito);
                if(confirmarCompra.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("El carrito esta vacio", "Aviso: Carrito vacio.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
