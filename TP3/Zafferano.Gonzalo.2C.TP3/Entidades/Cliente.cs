using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Entidades
{
    public class Cliente : Persona, IArchivo
    {
        private const string rutaRelativaArchivo = "Clientes\\clientes";

        //IMPLEMENTACION GENERICS
        private static ListaGenerica<Cliente> clientes;
        private static Cliente clienteTemporal;
        private string telefono;

        /// <summary>
        /// Contructor publico de la clase Cliente para Serializacion XML. NO utilizar este constructor para crear clientes.
        /// </summary>
        public Cliente()
        {

        }

        /// <summary>
        /// Constructor estatico de la clase clientes. Inicializa la lista generica, 
        /// un cliente que representara a todos los clientes temporales, Lee y carga el archivo de clientes al sistema.
        /// </summary>
        static Cliente()
        {
            Cliente.clientes = new ListaGenerica<Cliente>();
            Cliente.clienteTemporal = new Cliente("X", "X", 1000000, "00-0000-0000");
            Cliente.LeerArchivoDeClientes();
        }

        /// <summary>
        /// Contructor publico de la clase Clientes.
        /// </summary>
        /// <param name="nombre">Nombre cliente</param>
        /// <param name="apellido">Apellido cliente</param>
        /// <param name="dni">Dni cliente</param>
        /// <param name="telefono">telefono cliente.</param>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        public Cliente(string nombre, string apellido, int dni, string telefono)
            : base(nombre, apellido, dni)
        {
            base.VerificarDniDuplicado();
            this.Telefono = telefono;
        }

        /// <summary>
        /// Retorna la cantidad de clientes en el sistema.
        /// </summary>
        public static int Count
        {
            get
            {
                return Cliente.clientes.Count;
            }
        }

        /// <summary>
        /// Retorna el telefono del cliente.
        /// Setea el telefono de un cliente, que cumpla con el formato requerido {00-0000-0000}
        /// IMPLEMENTACION METODOS DE EXTENSION.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        public string Telefono
        {
            get
            {
                return this.telefono;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if(value.EsFormatoTelefonico())
                    {
                        this.telefono = value;
                    }
                    else
                    {
                        throw new CargaDeDatosInvalidosException("Telefono invalido. Por favor, respete el formato de telefono especificado.");
                    }
                }
                else
                {
                    throw new NullReferenceException("Telefono NULL");
                }
            }
        }

        /// <summary>
        /// Retorna al cliente temporal, que representa a los clientes esporadicos no registrados.
        /// </summary>
        [Browsable(false)]
        public static Cliente ClienteTemporal
        {
            get
            {
                return Cliente.clienteTemporal;
            }
        }

        /// <summary>
        /// Retorna True si el cliente es considerado recurrente (5 o mas compras en los ultimos 30 dias, a partir del dia consultado).
        /// </summary>
        [Browsable(false)]
        public bool EsClienteRecurrente
        {
            get
            {
                return this.CantidadDeComprasEnUltimoPeriodo(30) >= 7;
            }
        }

        /// <summary>
        /// Retorna True si el cliente es considerado VIP (Una inversion mayor a los $5.000 en los ultimos 3 meses, a partir del dia consultado).
        /// </summary>
        [Browsable(false)]
        public bool EsClienteVIP
        {
            get
            {
                return this.InversionTotalHastaHoy(90) > 25000;
            }
        }

        /// <summary>
        /// Obtiene la inversion total del cliente en el ultimo año, a partir del dia consultado.
        /// </summary>
        public double InversionTotalUltimoAnio
        {
            get
            {
                return this.InversionTotalHastaHoy(365);
            }
        }

        /// <summary>
        /// Obtiene un cliente a partir del indice recibido.
        /// </summary>
        /// <param name="indice">Indice del cliente.</param>
        /// <returns>El cliente encontrado en el indice.</returns>
        /// <exception cref="IndexOutOfRangeException">Indice fuera de rango.</exception>
        public static Cliente ObtenerUnClienteDeLaListaPorIndice(int indice)
        {
            if(indice >= 0 && indice < Cliente.Count)
            {
                return Cliente.clientes[indice];
            }
            throw new IndexOutOfRangeException("Indice Fuera de Rango");
        }

        /// <summary>
        /// Evalua si un cliente ya existe en el sistema por su dni.IMPLEMENTACION GENERICS.
        /// </summary>
        /// <param name="dni">Dni cliente.</param>
        /// <returns>True si existe, caso contrario False.</returns>
        protected override bool ExistePersonaPorDni(int dni)
        {
            return Cliente.clientes.ExisteElementoPorIdentificador(dni);
        }

        /// <summary>
        /// Obtiene un cliente por su DNI.
        /// </summary>
        /// <param name="dni">Dni cliente.</param>
        /// <returns>El cliente encontrado, caso contrario NULL.</returns>
        internal static Cliente ObtenerClientePorDni(int dni)
        {
            Cliente cliente = null;

            for (int i = 0; i < Cliente.Count; i++)
            {
                if (Cliente.clientes[i] == dni)
                {
                    cliente = Cliente.clientes[i];
                    break;
                }
            }
            return cliente;
        }

        /// <summary>
        /// Carga un cliente en el sistema. IMPLEMENTACION GENERICS.
        /// </summary>
        /// <param name="cliente">Cliente a cargar</param>
        /// <returns>True si pudo cargar el cliente en el sistema y guardarlo, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="Exception">Excepcion externa.</exception>
        internal static bool CargarCliente(Cliente cliente)
        {
            if(cliente != Cliente.clienteTemporal)
            {
                if(Cliente.clientes.CargarElementoAlSistema(cliente))
                {
                    return ((IArchivo)cliente).GuardarArchivo();
                }
            }
            return false;             
        }

        /// <summary>
        /// Elimina un cliente del sistema. IMPLEMENTACION GENERICS.
        /// </summary>
        /// <param name="cliente">Cliente a eliminar</param>
        /// <returns>True si pudo eliminar el cliente del sistema y guardarlo, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="Exception">Excepcion externa.</exception>
        internal static bool EliminarCliente(Cliente cliente)
        {
            if(Cliente.clientes.EliminarElementoDelSistema(cliente))
            {
                return ((IArchivo)cliente).GuardarArchivo();
            }
            return false;
        }

        /// <summary>
        /// Calcula la cantidad de compras que realizo el cliente en el ultimo periodo.
        /// </summary>
        /// <param name="dias">Dias a partir de los cuales se quiere obtener la cantidad de compras.</param>
        /// <returns>Cantidad de compras en el periodo indicado.</returns>
        private int CantidadDeComprasEnUltimoPeriodo(int dias)
        {
            List<Compra> comprasCliente = Compra.ObtenerListaComprasDeUnCliente(this);
            DateTime mesPasado = DateTime.Now.AddDays(Math.Abs(dias) * -1);
            int contadorCompras = 0;
            
            foreach(Compra compra in comprasCliente)
            {
                if(compra.FechaCompra > mesPasado)
                {
                    contadorCompras++;
                }
            }
            return contadorCompras;
        }

        /// <summary>
        /// Calcula la inversion total que realizo el cliente en el ultimo periodo.
        /// </summary>
        /// <param name="dias">Dias a partir de los cuales se quiere obtener la inversion total.</param>
        /// <returns>Inversion total en el periodo indicado.</returns>
        private double InversionTotalHastaHoy(int dias)
        {
            List<Compra> comprasCliente = Compra.ObtenerListaComprasDeUnCliente(this);
            DateTime fechaPasada = DateTime.Now.AddDays(Math.Abs(dias) * -1);
            double totalInvertido = 0;
            
            foreach (Compra compra in comprasCliente)
            {
                if (compra.FechaCompra > fechaPasada)
                {
                    totalInvertido += compra.PrecioTotal;  
                }
            }
            return totalInvertido;
        }

        /// <summary>
        /// Obtiene una cadena con todos los datos del cliente.
        /// </summary>
        /// <returns>Una cadena con todos los datos del cliente.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{base.ToString()}");
            sb.AppendLine($"Telefono: {this.Telefono}");
            sb.AppendLine($"Inversion Anual: ${string.Format("{0:0,0.00}", this.InversionTotalUltimoAnio)}");

            return sb.ToString();    
        }

        /// <summary>
        /// Guarda una modificacion en el sistema.
        /// </summary>
        /// <returns>True si se guardo el cliente en el archivo.</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="ArgumentNullException">Datos NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool CargarModificacionDeCliente()
        {
            return ((IArchivo)this).GuardarArchivo();
        }

        /// <summary>
        /// Guarda un cliente en archivo. IMPLEMENTACION SERIALIZACION.
        /// </summary>
        /// <returns>True si se guardo el cliente en el archivo.</returns>
        /// <exception cref="ArchivoException">Error relacionado con el archivo.</exception>
        /// <exception cref="ArgumentNullException">Datos NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        bool IArchivo.GuardarArchivo()
        {
            try
            {
                List<Cliente> clientesASerializar = new List<Cliente>();

                for (int i = 0; i < Cliente.Count; i++)
                {
                    clientesASerializar.Add(Cliente.clientes[i]);
                }

                return SerializadorXml<List<Cliente>>.GuardarXml(Cliente.rutaRelativaArchivo, clientesASerializar);
            }
            catch(ArchivoException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error externo al sistema al intentar serializar el cliente. Clase Cliente. Revisar InnerException", ex);
            }
        }

        /// <summary>
        /// Lee el archivo de clientes y lo carga en la lista del sistema.
        /// </summary>
        void IArchivo.LeerArchivo()
        {
            Cliente.LeerArchivoDeClientes();
        }

        /// <summary>
        /// Lee el archivo de clientes y lo carga en la lista del sistema.
        /// IMPLEMENTACION SERIALIZACION.
        /// Este metodo captura todas las excepciones pero no toma acciones sobre ellas, ya que es invocado unicamente por el constructor estatico.
        /// </summary>
        private static void LeerArchivoDeClientes()
        {
            try
            {
                List<Cliente> clientesDeserializados = SerializadorXml<List<Cliente>>.Leer(Cliente.rutaRelativaArchivo);

                for (int i = 0; i < clientesDeserializados.Count; i++)
                {
                    Cliente.clientes.CargarElementoAlSistema(clientesDeserializados[i]);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
