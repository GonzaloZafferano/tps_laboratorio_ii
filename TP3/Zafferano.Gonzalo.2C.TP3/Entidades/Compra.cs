using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades
{
    public class Compra : IObtenerIgualdad, IArchivo
    {
        public enum MedioDePago { Tarjeta_Debito, Tarjeta_Credito, Efectivo }
        
        private const string rutaRelativaArchivo = "Compras\\compras";
        private const string rutaRelativaCarpetaTickets = "Tickets\\";

        //IMPLEMENTACION GENERICS
        private static ListaGenerica<Compra> compras;
        private MedioDePago medioDePagoUtilizado;
        private DateTime fechaCompra;
        private int idCompra;
        private double descuento;
        private int dniCliente;
        private string detallesCompra;
        private double precioTotal;
        private int idUsuario;
        
        /// <summary>
        /// Constructor estatico de clase. Inicializa una lista generica y lee el archivo de compras pra guardar en el sistema.
        /// </summary>
        static Compra()
        {
            Compra.compras = new ListaGenerica<Compra>();
            Compra.LeerArchivoDeCompras();
        }
        
        /// <summary>
        /// Constructor para serializacion. NO utilizar esta sobrecarga.
        /// Se ofrece este constructor para la Serializacion JSON para que no incremente el ID accidentalmente.
        /// </summary>
        [JsonConstructor]
        public Compra()
        {

        }

        /// <summary>
        /// Constructor de clase. Instancia una nueva compra, con un nuevo ID.
        /// </summary>
        /// <param name="dniCliente">dni cliente</param>
        /// <param name="descuento">descuento aplicado</param>
        /// <param name="precioFinalAcumuladoConDescuento">precio final acumulado con descuento aplicado</param>
        /// <param name="idUsuario">id usuario que atendio.</param>
        /// <param name="medioDePago">medio de pago utilizado</param>
        /// <param name="fechaCompra">fecha de la compra.</param>
        /// <param name="detallesProductos">detalles de compra</param>
        /// <exception cref="NullReferenceException">Datos NULL</exception>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        public Compra(int dniCliente, int descuento, double precioFinalAcumuladoConDescuento, int idUsuario, MedioDePago medioDePago, DateTime fechaCompra, string detallesProductos)
            : this(dniCliente, idUsuario, medioDePago, fechaCompra, descuento, precioFinalAcumuladoConDescuento, "-", 0)
        {
            this.DetallesCompra = this.DetallesDeCompra() + detallesProductos;
        }

        /// <summary>
        /// Constructor de clase. Instancia una compra, con el ID de compra recibido.
        /// Este constructor se utiliza para cargar elementos ya existentes o clonar.
        /// </summary>
        /// <param name="dniCliente">dni cliente</param>
        /// <param name="descuento">descuento aplicado</param>
        /// <param name="precioFinalAcumuladoConDescuento">precio final acumulado con descuento aplicado</param>
        /// <param name="idUsuario">id usuario que atendio.</param>
        /// <param name="medioDePago">medio de pago utilizado</param>
        /// <param name="fechaCompra">fecha de la compra.</param>
        /// <param name="detallesProductos">detalles de compra</param>
        /// <param name="idCompra">id de compra</param>
        /// <exception cref="NullReferenceException">Datos NULL</exception>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        private Compra(int dniCliente, int idUsuario, MedioDePago medioDePagoUtilizado, DateTime fechaCompra, double descuento, double precioTotal, string detallesCompra, int idCompra)
        {
            this.DniCliente = dniCliente;
            this.MedioDePagoUtilizado = medioDePagoUtilizado;
            this.FechaCompra = fechaCompra;
            this.Descuento = descuento;
            this.PrecioTotal = precioTotal;
            this.DetallesCompra = detallesCompra;
            this.IdUsuario = idUsuario;
            this.IdCompra = idCompra;
        }

        /// <summary>
        /// Obtiene y setea el id del usuario que atendio la compra, previa validacion.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        public int IdUsuario
        {
            get
            {
                return this.idUsuario;
            }
            set
            {
                if(value > 0)
                {
                    this.idUsuario = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("El Id no puede ser 0 o negativo.");
                }
            }
        }

        /// <summary>
        /// Obtiene la cantidad de compras en el sistema.
        /// </summary>
        public static int Count
        {
            get
            {
                return Compra.compras.Count;
            }
        }

        /// <summary>
        /// Obtiene el nombre del cliente que realizo la compra.
        /// </summary>
        private string NombreCliente
        {
            get
            {
                string retorno = "Cliente Eventual";

                Cliente cliente = Cliente.ObtenerClientePorDni(this.dniCliente);

                if(cliente is not null)
                {
                    retorno = cliente.NombreCompleto;
                }
                
                return retorno;
            }
        }

        /// <summary>
        /// Obtiene y setea el descuento aplicado.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        public double Descuento
        {
            get
            {
                return this.descuento;
            }
            set
            {
                if(value >= 0)
                {
                    this.descuento = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("El descuento no puede ser negativo.");
                }
            }
        }

        /// <summary>
        /// Obtiene y setea el precio total de la compra.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        public double PrecioTotal
        {
            get
            {
                return this.precioTotal;
            }
            set
            {
                if (value > 0)
                {
                    this.precioTotal = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("El precio total no puede ser 0 o negativo.");
                }
            }
        }         

        /// <summary>
        /// Obtiene y setea la fecha de compra.
        /// </summary>
        public DateTime FechaCompra
        {
            get
            {
                return this.fechaCompra;
            }
            set
            {
                this.fechaCompra = value;
            }
        }

        /// <summary>
        /// Obtiene y setea los detalles de la compra.
        /// </summary>
        /// <exception cref="NullReferenceException">Datos NULL</exception>
        public string DetallesCompra
        {
            get
            {
                return this.detallesCompra;
            }
            set
            {                
                if(!string.IsNullOrWhiteSpace(value))
                {
                    this.detallesCompra = value;
                }
                else
                {
                    throw new NullReferenceException("Detalles de compra fue NULL");
                }
            }
        }

        /// <summary>
        /// Obtiene y setea el DNI del cliente, previa validacion.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        public int DniCliente
        {
            get
            {
                return this.dniCliente;
            }
            set
            {
                if (value >= 1000000 && value <= 99999999)
                {
                    this.dniCliente = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("Dni del Cliente es Invalido.");
                }
            }
        }

        /// <summary>
        /// Obtiene y setea el medio de pago.
        /// </summary>
        public MedioDePago MedioDePagoUtilizado
        {
            get
            {
                return this.medioDePagoUtilizado;
            }
            set
            {
                this.medioDePagoUtilizado = value;
            }
        }

        /// <summary>
        /// Obtiene y setea (serializacion) el ID de compra.
        /// </summary>
        /// <exception cref="CargaDeDatosInvalidosException">Datos invalidos</exception>
        public int IdCompra
        {
            get
            {
                return this.idCompra;
            }
            set
            {
                if (value == 0)
                {
                    this.idCompra = IdentificadorUnico.IdenfiticadorUnico.ObtenerIdUnicoParaCompra();
                }
                else if (value > 0)
                {
                    this.idCompra = value;
                }
                else
                {
                    throw new CargaDeDatosInvalidosException("El id del empleado no puede ser 0 o negativo.");
                }
            }
        }

        /// <summary>
        /// Obtiene una compra por su indice
        /// </summary>
        /// <param name="indice">indice de la compra</param>
        /// <returns>Compra correspondiente al indice, si no la encuenta, retorna NULL</returns>
        internal static Compra ObtenerCompraPorIndice(int indice)
        {
            Compra compra = null;

            if(indice >= 0 && indice < Compra.Count)
            {
                compra = Compra.compras[indice];           
            }

            return compra;
        }

        /// <summary>
        /// Obtiene una cadena con los detalles de la compra.
        /// </summary>
        /// <returns>Una cadena con los detalles de la compra.</returns>
        private string DetallesDeCompra()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"ID Compra: {this.IdCompra}");
            sb.AppendLine($"ID Usuario que brindo Atencion: {this.IdUsuario}");
            sb.AppendLine($"Cliente: {this.NombreCliente}");
            sb.AppendLine($"Dni Cliente: {this.DniCliente}");
            sb.AppendLine($"Fecha de compra: {this.FechaCompra.ToShortDateString()}");
            sb.AppendLine($"Medio de pago utilizado: {this.MedioDePagoUtilizado}");            
            sb.AppendLine($"Descuento aplicado: {this.Descuento}%");
            sb.AppendLine($"Precio Total de la Compra: ${string.Format("{0:0,0.00}", this.PrecioTotal)}");        

            return sb.ToString();
        }

        /// <summary>
        /// Agrega una compra al sistema y la guarda en un archivo.
        /// </summary>
        /// <param name="compra">compra a guardar.</param>
        /// <returns>True si se guardo la compra.</returns>
        public static bool AgregarCompra(Compra compra)
        {
            if(Compra.compras.CargarElementoAlSistema(compra))
            {
                return ((IArchivo)compra).GuardarArchivo();
            }
            return false;
        }

        /// <summary>
        /// Obtiene una lista de las compras realizadas por un cliente.
        /// </summary>
        /// <param name="cliente">Cliente del cual se obtendra una lista de compras.</param>
        /// <returns>Una lista con las compras del cliente.</returns>
        internal static List<Compra> ObtenerListaComprasDeUnCliente(Cliente cliente)
        {
            List<Compra> comprasAuxiliar = new List<Compra>();

            if(cliente is not null)
            {
                for(int i = 0; i < Compra.Count; i++)
                {
                    if(cliente == Compra.compras[i].DniCliente)
                    {
                        comprasAuxiliar.Add(Compra.compras[i]);
                    }
                }              
            }
            return comprasAuxiliar;
        }

        /// <summary>
        /// Evalua si 2 elementos son iguales. IMPLEMENTA INTERFACES.
        /// </summary>
        /// <typeparam name="T">Clase que implemente la interfaz IObtenerIgualdad</typeparam>
        /// <param name="elemento">Elemento que se evaluara.</param>
        /// <returns>True si es el mismo elemento, caso contrario False.</returns>
        bool IObtenerIgualdad.EsMismoElemento<T>(T elemento)
        {
            bool retorno = false;

            if(elemento is Compra compraAuxiliar)
            {
                retorno = this.IdCompra == compraAuxiliar.IdCompra;
            }
            return retorno;
        }

        /// <summary>
        /// Evalua si 2 elementos son iguales segun el identificador (dni). IMPLEMENTA INTERFACES.
        /// </summary>
        /// <param name="identificador">identificador</param>
        /// <returns>True si es el misma, caso contrario False.</returns>
        bool IObtenerIgualdad.EsMismoIdentificador(int identificador)
        {
            return this.IdCompra == identificador;
        }

        /// <summary>
        /// Lee la lista de compras que esta respaldada en un archivo, y la carga al sistema.
        /// </summary>
        void IArchivo.LeerArchivo()
        {
            Compra.LeerArchivoDeCompras();
        }

        /// <summary>
        /// Lee la lista de compras que esta respaldada en un archivo, y la carga al sistema.
        /// IMPLEMENTA SERIALIZACION JSON.
        /// Este metodo captura todas las excepciones pero no toma acciones sobre ellas, ya que es invocado unicamente por el constructor estatico.
        /// </summary>
        private static void LeerArchivoDeCompras()
        {
            try
            {
                List<Compra> comprasDeserializadas = SerializadorJSON<List<Compra>>.Leer(Compra.rutaRelativaArchivo);

                for (int i = 0; i < comprasDeserializadas.Count; i++)
                {
                    Compra.compras.CargarElementoAlSistema(comprasDeserializadas[i]);
                }
            }
            catch(Exception)
            {

            }
        }

        /// <summary>
        /// Guarda la lista de compras en el sistema. IMPLEMENTACION DE SERIALIZACION JSON.
        /// </summary>
        /// <returns>True si se guardo en el sistema</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        bool IArchivo.GuardarArchivo()
        {
            try
            {
                List<Compra> comprasASerializar = new List<Compra>();

                for (int i = 0; i < Compra.Count; i++)
                {
                    comprasASerializar.Add(Compra.compras[i]);
                }

                return SerializadorJSON<List<Compra>>.GuardarJSON(Compra.rutaRelativaArchivo, comprasASerializar);
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
                throw new Exception("Ocurrio un error externo al sistema al intentar serializar la compra. Clase Compra. Revisar InnerException", ex);
            }
        }

        /// <summary>
        /// Genera y guarda un ticket de compra en el sistema. IMPLEMENTACION ARCHIVOS.
        /// </summary>
        /// <returns>True si se guardo en el sistema</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public bool GenerarTicketDeCompra()
        {
            try
            {
                string nombreTicket = string.Format("Ticket_IdCompra-{0}_Fecha-{1}", this.IdCompra, this.FechaCompra.ToString("dd-MM-yyy"));

                return Archivo.GuardarTxt($"{Compra.rutaRelativaCarpetaTickets}{nombreTicket}", this.DetallesCompra);
            }
            catch (ArchivoException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error externo al sistema al intentar serializar la compra. Clase Compra. Revisar InnerException", ex);
            }
        }

        /// <summary>
        /// Obtiene un Dictionary con todos los tickets de compra almacenados en el directorio de Tickets.
        /// IMPLEMENTACION ARCHIVOS.
        /// </summary>
        /// <returns>Un Dictionary con todos los tickets de compra almacenados en el directorio de Tickets</returns>
        /// <exception cref="ArchivoException">Error con la ruta del archivo.</exception>
        /// <exception cref="ArgumentNullException">Datos NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public static Dictionary<string,string> LeerTicketsDeCompra()
        {
           return Archivo.LeerArchivosDeUnDirectorio(Compra.rutaRelativaCarpetaTickets, "Ticket_IdCompra");
        }
    }
}
