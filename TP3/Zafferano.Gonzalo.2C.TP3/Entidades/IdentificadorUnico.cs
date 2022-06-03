using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class IdentificadorUnico : IArchivo
    {
        private const string rutaRelativaArchivo = "Ids\\Ids";
        private static IdentificadorUnico identificadorUnico;
        private static int empleadoId;
        private static int productoId;
        private static int compraId;

        /// <summary>
        /// Constructor estatico de la clase IdentificadorUnico. Inicializa un objeto de tipo IdentificadorUnico.
        /// Lee el archivo de Ids y los carga en el sistema.
        /// </summary>
        static IdentificadorUnico()
        {
            IdentificadorUnico.identificadorUnico = new IdentificadorUnico();

            if(!IdentificadorUnico.LeerArchivoDeIds())
            {
                IdentificadorUnico.empleadoId = 0;
                IdentificadorUnico.productoId = 0;
                IdentificadorUnico.compraId = 0;
            }
        }

        /// <summary>
        /// Retorna el objeto estatico que contiene los IDs.
        /// </summary>
        public static IdentificadorUnico IdenfiticadorUnico
        {
            get
            {                
                return IdentificadorUnico.identificadorUnico;
            }
        }

        /// <summary>
        /// Propiedad para Serializacion. Obtiene y setea el id de empleados. 
        /// </summary>
        public int EmpleadoId
        {
            get
            {
                return IdentificadorUnico.empleadoId;
            }
            set
            {
                IdentificadorUnico.empleadoId = value;
            }
        }

        /// <summary>
        /// Propiedad para Serializacion. Obtiene y setea el id de productos. 
        /// </summary>
        public int ProductoId
        {
            get
            {
                return IdentificadorUnico.productoId;
            }
            set
            {
                IdentificadorUnico.productoId = value;
            }
        }

        /// <summary>
        /// Propiedad para Serializacion. Obtiene y setea el id de compras. 
        /// </summary>
        public int CompraId
        {
            get
            {
                return IdentificadorUnico.compraId;
            }
            set
            {
                IdentificadorUnico.compraId = value;
            }
        }

        /// <summary>
        /// Obtiene un Id Unico para un empleado
        /// </summary>
        /// <returns>Id Unico para un empleado</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="ArgumentNullException">Dato NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public int ObtenerIdUnicoParaEmpleado()
        {
            int retorno = ++this.EmpleadoId;

            ((IArchivo)this).GuardarArchivo();

            return retorno;
        }

        /// <summary>
        /// Obtiene un Id unico para un producto
        /// </summary>
        /// <returns>Id unico para un producto</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="ArgumentNullException">Dato NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public int ObtenerIdUnicoParaProducto()
        {
            int retorno = ++this.ProductoId;

            ((IArchivo)this).GuardarArchivo();

            return retorno;
        }

        /// <summary>
        /// Obtiene un Id unico para una compra
        /// </summary>
        /// <returns>Id unico para una compra</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="ArgumentNullException">Dato NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public int ObtenerIdUnicoParaCompra()
        {
            int retorno = ++this.CompraId;

            ((IArchivo)this).GuardarArchivo();

            return retorno;
        }

        /// <summary>
        /// Guarda los ids en un archivo.
        /// </summary>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="ArgumentNullException">Dato NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        bool IArchivo.GuardarArchivo()
        {
            try
            {
                return SerializadorJSON<IdentificadorUnico>.GuardarJSON(IdentificadorUnico.rutaRelativaArchivo, IdentificadorUnico.IdenfiticadorUnico);
            }
            catch(ArchivoException)
            {
                throw;
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new Exception("Ocurrio un error externo al sistema al intentar serializar los Ids. Clase IdentificadorUnico. Revisar InnerException", ex);
            }
        }

        /// <summary>
        /// Obtiene los datos de IDS que estan respaldados en un archivo, y lo carga al sistema.
        /// </summary>
        void IArchivo.LeerArchivo()
        {
            IdentificadorUnico.LeerArchivoDeIds();
        }

        /// <summary>
        /// Obtiene los datos de IDS que estan respaldados en un archivo, y lo carga al sistema.
        /// </summary>
        /// <returns>True si leyo el archivo sin problemas, caso contrario False.</returns>
        private static bool LeerArchivoDeIds()
        {
            try
            {
                IdentificadorUnico clasesId = SerializadorJSON<IdentificadorUnico>.Leer(IdentificadorUnico.rutaRelativaArchivo);
                
                return true;
            }
            catch(Exception)
            {
                return false;
            }            
        }
    }
}
