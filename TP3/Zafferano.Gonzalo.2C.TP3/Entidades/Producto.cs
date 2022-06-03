using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [XmlInclude(typeof(Disenio))]
    [XmlInclude(typeof(Impresion))]
    public abstract class Producto : IObtenerIgualdad, IArchivo
    {
        private const string rutaRelativaArchivo = "Productos\\productos";

        //IMPLEMENTACION GENERICS
        private static ListaGenerica<Producto> productos;
        private int idProducto;
        private string nombreProducto;
        private string descripcion;
        private double precioProducto;

        /// <summary>
        /// constructor estatico de la clase Producto.
        /// Inicializa la  lista Generica y lee el archivo de productos para cargarlos en el sistema.
        /// </summary>
        static Producto()
        {
            Producto.productos = new ListaGenerica<Producto>();
            Producto.LeerArchivoDeProductos();
        }

        /// <summary>
        /// Constructor publico para serializacion. NO utilizar esta sobrecarga.
        /// </summary>
        public Producto()
        {

        }

        /// <summary>
        /// Constructor de prodcuto, crea un nuevo producto asignando un ID unico.
        /// </summary>
        /// <param name="nombre">nombre producto</param>
        /// <param name="descripcion">descripcion producto</param>
        /// <param name="precio">precio producto</param>
        protected Producto(string nombre, string descripcion, double precio)
            : this(nombre, descripcion, precio, 0)
        {

        }

        /// <summary>
        /// Constructor de prodcuto, crea un producto asignando el ID recibido.
        /// Este constructor se utiliza para cargar elementos ya existentes o clonar.
        /// </summary>
        /// <param name="nombre">nombre producto</param>
        /// <param name="descripcion">descripcion producto</param>
        /// <param name="precio">precio producto</param>
        /// <param name="id">Id del producto</param>
        protected Producto(string nombre, string descripcion, double precio, int id)
        {
            this.NombreProducto = nombre;
            this.DescripcionProducto = descripcion;
            this.PrecioProducto = precio;
            this.IdProducto = id;
        }

        public abstract string Tipo { get; }

        /// <summary>
        /// Obtiene la cantidad de productos en el sistema.
        /// </summary>
        public static int Count
        {
            get
            {
                return Producto.productos.Count;
            }
        }

        /// <summary>
        /// Obtiene y setea el Id del producto, previa validacion.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalidos.</exception>
        public int IdProducto
        {
            get
            {
                return this.idProducto;
            }
            set
            {
                if (value == 0)
                {
                    this.idProducto = IdentificadorUnico.IdenfiticadorUnico.ObtenerIdUnicoParaProducto();
                }
                else if (value > 0)
                {
                    this.idProducto = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("El id del producto no puede ser negativo.");
                }
            }
        }

        /// <summary>
        /// Obtiene y setea el nombre del producto previa validacion.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalidos.</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        public string NombreProducto
        {
            get
            {
                return this.nombreProducto;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (value.EsCadenaAlfanumericaConEspacios())
                    {
                        this.nombreProducto = value.DarFormatoDeNombre();
                    }
                    else
                    {
                        throw new CargaDeDatosInvalidosException("El nombre del producto solo puede contener letras, numeros y/o espacios.");
                    }
                }
                else
                {
                    throw new NullReferenceException("El nombre del producto no puede ser NULL o Vacio o Espacios en blanco");
                }
            }
        }

        /// <summary>
        /// Obtiene y setea la descripcion del producto previa validacion.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalidos.</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        public string DescripcionProducto
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.descripcion = value.DarFormatoDeDescripcion();
                }
                else
                {
                    throw new NullReferenceException("La descripcion no puede ser NULL o Vacio o Espacios en blanco");
                }
            }
        }

        /// <summary>
        /// Obtiene y setea el precio del producto previa validacion.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalidos.</exception>
        public virtual double PrecioProducto
        {
            get
            {
                return this.precioProducto;
            }
            set
            {
                if (value > 0)
                {
                    this.precioProducto = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("El precio del producto no puede ser 0 o negativo.");
                }
            }
        }

        public abstract Producto ClonarProducto<T>(T parametro) where T : struct;

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">id del producto.</param>
        /// <returns>El producto que corresponde al ID, caso contrario NULL.</returns>
        public static Producto ObtenerProductoPorId(int id)
        {
            return Producto.productos.ObtenerElementoPorIdentificador(id);
        }

        /// <summary>
        /// Obtiene un elemento por indice.
        /// </summary>
        /// <param name="indice">indice del elemento que se busca.</param>
        /// <returns>El elemento que corresponde al indice.</returns>
        /// <exception cref="IndexOutOfRangeException">Indice fuera de rango.</exception>
        public static Producto ObtenerUnProductoDeLaListaPorIndice(int indice)
        {
            if (indice >= 0 && indice < Producto.Count)
            {
                return Producto.productos[indice];
            }
            throw new IndexOutOfRangeException("Indice fuera de rango");
        }

        /// <summary>
        /// Agrega un producto al sistema y lo guarda en un archivo.
        /// </summary>
        /// <param name="producto">producto a guardar</param>
        /// <returns>True si guardo el producto, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        internal static bool AgregarProducto(Producto producto)
        {       
            if(Producto.productos.CargarElementoAlSistema(producto))
            {
                return ((IArchivo)producto).GuardarArchivo();
            }

           return false;
        }

        /// <summary>
        /// Guarda una modificacion en el sistema.
        /// </summary>
        /// <returns>True si se cargo la modificacion al sistema</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool CargarModificacionDeProducto()
        {
            return ((IArchivo)this).GuardarArchivo();
        }

        /// <summary>
        /// Elimina un producto del sistema y lo guarda en un archivo.
        /// </summary>
        /// <param name="producto">producto a eliminar</param>
        /// <returns>True si elimino el producto, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        internal static bool EliminarProducto(Producto producto)
        {
            if(Producto.productos.EliminarElementoDelSistema(producto))
            {
                return ((IArchivo)producto).GuardarArchivo();
            }            
            return false;
        }

        /// <summary>
        /// Obtiene una cadena con todos los datos del producto.
        /// </summary>
        /// <returns>Una cadena con todos los datos del producto.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id Producto: {this.IdProducto}");
            sb.AppendLine($"Nombre Producto: {this.NombreProducto}");
            sb.AppendLine($"Descripcion: {this.DescripcionProducto}");
            sb.AppendLine($"Precio Base: ${string.Format("{0:0,0.00}", this.PrecioProducto)}");

            return sb.ToString();
        }

        /// <summary>
        /// Evalua si 2 elementos son iguales.
        /// </summary>
        /// <typeparam name="T">Clase que implemente la interfaz IObtenerIgualdad</typeparam>
        /// <param name="elemento">Elemento que se evaluara.</param>
        /// <returns>True si es el mismo elemento, caso contrario False.</returns>
        public bool EsMismoElemento<T>(T elemento) where T : class, IObtenerIgualdad
        {
            bool retorno = false;
            if(elemento is Producto productoAuxiliar)
            {
                retorno = this == productoAuxiliar;
            }
            return retorno;
        }

        /// <summary>
        /// Evalua si 2 productos son iguales segun el identificador (id)
        /// </summary>
        /// <param name="identificador">identificador</param>
        /// <returns>True si es el misma, caso contrario False.</returns>
        public bool EsMismoIdentificador(int identificador)
        {
            return this == identificador;
        }
             
        /// <summary>
        /// Guarda la lista de productos en un archivo, respaldando la informacion.
        /// </summary>
        /// <returns>True si pudo guardar, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        bool IArchivo.GuardarArchivo()
        {
            try
            {
                List<Producto> productosASerializar = new List<Producto>();

                for (int i = 0; i < Producto.Count; i++)
                {
                    productosASerializar.Add(Producto.productos[i]);
                }

                return SerializadorXml<List<Producto>>.GuardarXml(Producto.rutaRelativaArchivo, productosASerializar);
            }
            catch (ArchivoException)
            {
                throw;
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error externo al sistema al intentar serializar el producto. Clase Producto. Revisar InnerException", ex);
            }
        }

        /// <summary>
        /// Lee la lista de productos que esta respaldada en un archivo, y la carga al sistema.
        /// </summary>
        void IArchivo.LeerArchivo()
        {
            Producto.LeerArchivoDeProductos();
        }

        /// <summary>
        /// Lee la lista de productos que esta respaldada en un archivo, y la carga al sistema.
        /// Este metodo captura todas las excepciones pero no toma acciones sobre ellas, ya que es invocado unicamente por el constructor estatico.
        /// </summary>
        private static void LeerArchivoDeProductos()
        {
            try
            {
                List<Producto> productosDeserializados = SerializadorXml<List<Producto>>.Leer(Producto.rutaRelativaArchivo);

                for (int i = 0; i < productosDeserializados.Count; i++)
                {
                    Producto.productos.CargarElementoAlSistema(productosDeserializados[i]);
                }
            }
            catch (Exception)
            {

            }
        }

        public static bool operator ==(Producto producto, int id)
        {
            bool retorno = false;

            if(producto is not null)
            {
                retorno = producto.IdProducto == id;
            }
            return retorno;
        }
        
        public static bool operator !=(Producto producto, int id)
        {
            return !(producto == id);
        }
                
        public static bool operator == (Producto productoUno, Producto productoDos)
        {
            bool retorno = false;

            if(productoUno is not null && productoDos is not null)
            {
                retorno = productoUno.IdProducto == productoDos.IdProducto;
            }

            return retorno;
        }
        
        public static bool operator !=(Producto productoUno, Producto productoDos)
        {
            return !(productoUno == productoDos);
        }    
    }
}
