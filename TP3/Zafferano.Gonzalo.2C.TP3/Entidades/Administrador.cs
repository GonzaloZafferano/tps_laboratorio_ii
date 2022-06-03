using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [XmlInclude(typeof(Jefe))]
    public class Administrador : Empleado, IArchivo
    {
        private const string rutaRelativaArchivo = "Empleados\\empleados";

        //IMPLEMENTACION GENERICS
        protected static ListaGenerica<Empleado> empleados;

        /// <summary>
        /// Constructor estatico de la clase Administrador. Inicializa la lista generica de empleados y Lee el archivo de empleados para cargarlos a dicha lista.
        /// </summary>
        static Administrador()
        {
            Administrador.empleados = new ListaGenerica<Empleado>();
            if (!Administrador.LeerArchivoDeEmpleados())
            {
                Administrador.CargarJefePrimerIngreso();
            }
        }

        /// <summary>
        /// Constructor publico de Administrador para serializacion XML. No utilizar esta sobrecarga.
        /// </summary>
        public Administrador()
        {

        }

        /// <summary>
        /// Constructor publico de Administrador. Carga un empleado luego de validar todos sus datos, 
        /// y verificar que el dni y el nombre de usuario no esten cargados en el sistema de empleados
        /// </summary>
        /// <param name="nombre">nombre empleado</param>
        /// <param name="apellido">apellido emppleado</param>
        /// <param name="dni">dni empleado</param>
        /// <param name="salario">salario empleado</param>
        /// <param name="nombreUsuario">usuario de empleado.</param>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        public Administrador(string nombre, string apellido, int dni, double salario, string nombreUsuario)
            : base(nombre, apellido, dni, salario, nombreUsuario)
        {
            this.Puesto = Empleado.Rol.Administrador;
        }

        /// <summary>
        /// Constructor interno de Administrador. Carga un empleado luego de validar todos sus datos.
        /// Este constructor se utiliza para cargar elementos ya existentes o clonar.
        /// </summary>
        /// <param name="nombre">nombre empleado</param>
        /// <param name="apellido">apellido emppleado</param>
        /// <param name="dni">dni empleado</param>
        /// <param name="salario">salario empleado</param>
        /// <param name="nombreUsuario">usuario de empleado.</param>
        /// <param name="id"></param>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        internal Administrador(string nombre, string apellido, int dni, double salario, string nombreUsuario, int id)
            : base(id, dni, nombre, apellido, salario, nombreUsuario)
        {
            this.Puesto = Empleado.Rol.Administrador;
        }

        /// <summary>
        /// Cantidad de empleados cargados en sistema.
        /// </summary>
        public static int Count
        {
            get
            {
                return empleados.Count;
            }
        }

        /// <summary>
        /// Evalua si una persona ya existe en la lista de empleados, por su DNI.
        /// </summary>
        /// <param name="dni">Dni empleado</param>
        /// <returns>True si existe, caso contrario False.</returns>
        public new static bool ExistePersonaPorDni(int dni)
        {
            return Administrador.empleados.ExisteElementoPorIdentificador(dni);
        }

        /// <summary>
        /// Obtiene un empleado por su indice.
        /// </summary>
        /// <param name="indice">Indice</param>
        /// <returns>El empleado correspondiente al indice.</returns>
        /// <exception cref="IndexOutOfRangeException">Si el indice recibido esta fuera de rango.</exception>
        public static Empleado ObtenerUnEmpleadoDeLaListaPorIndice(int indice)
        {
            if (indice >= 0 && indice < Administrador.Count)
            {
                return Administrador.empleados[indice];
            }
            throw new IndexOutOfRangeException("Indice Fuera de Rango");
        }

        /// <summary>
        /// Carga un empleado en el sistema.IMPLEMENTACION GENERICS.
        /// </summary>
        /// <param name="empleado">empleado a cargar.</param>
        /// <returns>True si lo cargo, caso contrario false.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public virtual bool CargarUnEmpleadoAlSistema(Empleado empleado)
        {
            if (!empleado.EsAdministrador)
            {
                if(Administrador.empleados.CargarElementoAlSistema(empleado))
                {
                    return ((IArchivo)this).GuardarArchivo();
                }                
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
        public bool CargarModificacionDeEmpleado()
        {
            return ((IArchivo)this).GuardarArchivo();
        }

        /// <summary>
        /// Elimina un empleado del sistema. IMPLEMENTACION GENERICS.
        /// </summary>
        /// <param name="empleado">Empleado a eliminar</param>
        /// <returns>True si elimino al empleado, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public virtual bool EliminarUnEmpleadoDelSistema(Empleado empleado)
        {
            if(!empleado.EsAdministrador)
            {
               if(Administrador.empleados.EliminarElementoDelSistema(empleado))
               {
                    return ((IArchivo)this).GuardarArchivo();
               }               
            }
            return false;
        }

        /// <summary>
        /// Carga un cliente al sistema
        /// </summary>
        /// <param name="cliente">cliente a cargar.</param>
        /// <returns>True si lo cargo, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool CargarUnClienteAlSistema(Cliente cliente)
        {
            return Cliente.CargarCliente(cliente);
        }

        /// <summary>
        /// Elimina un cliente del sistema
        /// </summary>
        /// <param name="cliente">cliente a eliminar.</param>
        /// <returns>True si lo elimino, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool EliminarUnClienteDelSistema(Cliente cliente)
        {
            return Cliente.EliminarCliente(cliente);
        }

        /// <summary>
        /// Carga un producto al sistema
        /// </summary>
        /// <param name="cliente">producto a cargar.</param>
        /// <returns>True si lo cargo, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool CargarUnProductoAlSistema(Producto producto)
        {
            return Producto.AgregarProducto(producto);
        }

        /// <summary>
        /// Elimina un producto del sistema
        /// </summary>
        /// <param name="cliente">producto a eliminar.</param>
        /// <returns>True si lo cargo, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool EliminarUnProductoDelSistema(Producto producto)
        {
            return Producto.EliminarProducto(producto);
        }
                
        /// <summary>
        /// Evalua si existe el nombre de usuario en la lista de empleados.
        /// </summary>
        /// <param name="nombreUsuario">nombre de usuario del empleado</param>
        /// <returns>True si existe, caso contrario False.</returns>
        internal static bool ExisteNombreUsuario(string nombreUsuario)
        {
            bool retorno = false;

            if(!string.IsNullOrWhiteSpace(nombreUsuario))
            {
                for(int i = 0; i < Administrador.Count; i++)
                {
                    if(Administrador.empleados[i].NombreUsuario == nombreUsuario)
                    {
                        retorno = true;
                        break;
                    }
                }
            }
            return retorno;
        }

        /// <summary>
        /// Cambia el puesto de un empleado, manteniendo el ID del mismo y todos sus datos.
        /// Si recibe un Jefe o Empleado, retorna un Administrador. Si recibe un Administrador, retorna un Empleado.
        /// </summary>
        /// <param name="empleado">Empleado que se cambiara de puesto.</param>
        /// <returns>Empleado con el puesto modificado.</returns>
        /// <exception cref="NullReferenceException">Empleado NULL</exception>
        public static Empleado CambiarPuestoDeEmpleado(Empleado empleado)
        {
            return Empleado.ClonarEmpleadoIntercambiandoPuesto(empleado);
        }

        /// <summary>
        /// Guarda la lista de empleados en un archivo, respaldando la informacion.
        /// IMPLEMENTACION SERIALIZACION E INTERFACES.
        /// </summary>
        /// <returns>True si pudo guardar, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        bool IArchivo.GuardarArchivo()
        {
            try
            {
                List<Empleado> empleadosASerializar = new List<Empleado>();

                for (int i = 0; i < Administrador.Count; i++)
                {
                    empleadosASerializar.Add(Administrador.empleados[i]);
                }

                return SerializadorXml<List<Empleado>>.GuardarXml(Administrador.rutaRelativaArchivo, empleadosASerializar);
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
                throw new Exception("Ocurrio un error externo al sistema al intentar serializar el empleado. Clase Administrador. Revisar InnerException", ex);
            }
        }

        /// <summary>
        /// Lee la lista de empleados que esta respaldada en un archivo, y la carga al sistema.
        /// IMPLEMENTACION INTERFACES.
        /// </summary>
        void IArchivo.LeerArchivo()
        {
            Administrador.LeerArchivoDeEmpleados();
        }

        /// <summary>
        /// Lee la lista de empleados que esta respaldada en un archivo, y la carga al sistema.
        /// IMPLEMENTACION SERIALIZACION.
        /// </summary>
        /// <returns>True si pudo leer el archivo sin poblemas, caso contrario False.</returns>
        private static bool LeerArchivoDeEmpleados()
        {
            try
            {
                List<Empleado> empleadosDeserializados = SerializadorXml<List<Empleado>>.Leer(Administrador.rutaRelativaArchivo);

                for (int i = 0; i < empleadosDeserializados.Count; i++)
                {
                    Administrador.empleados.CargarElementoAlSistema(empleadosDeserializados[i]);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Carga un Jefe por defecto en caso de que no se haya podido leer el archivo.
        /// IMPLEMENTACION GENERICS.
        /// </summary>
        private static void CargarJefePrimerIngreso()
        {            
            Administrador.empleados.CargarElementoAlSistema(new Jefe("Jefe", "Por Defecto", 1000000, 1, "Jefe", 1000));                   
        }
    }
}
