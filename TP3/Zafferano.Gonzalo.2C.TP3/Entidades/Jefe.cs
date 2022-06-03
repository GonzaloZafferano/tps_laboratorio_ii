using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Jefe : Administrador
    {
        /// <summary>
        /// Constructor para Serializacion. No utilizar esta sobrecarga.
        /// </summary>
        public Jefe()
        {

        }

        /// <summary>
        /// Constructor de la clase Jefe. Obtiene un nuevo Jefe con nuevo ID.
        /// </summary>
        /// <param name="nombre">nombre jefe</param>
        /// <param name="apellido">apellido jefe</param>
        /// <param name="dni">dni jefe</param>
        /// <param name="salario">salario jefe</param>
        /// <param name="nombreUsuario">nomrbe usuario jefe</param>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        public Jefe(string nombre, string apellido, int dni, double salario, string nombreUsuario)
            : base(nombre, apellido, dni, salario, nombreUsuario)
        {
            this.Puesto = Empleado.Rol.Jefe;
        }

        /// <summary>
        /// Constructor de la clase Jefe. Obtiene jefe, asignando el ID recibido.
        /// Este constructor se utiliza para cargar elementos ya existentes o clonar.
        /// </summary>
        /// <param name="nombre">nombre jefe</param>
        /// <param name="apellido">apellido jefe</param>
        /// <param name="dni">dni jefe</param>
        /// <param name="salario">salario jefe</param>
        /// <param name="nombreUsuario">nomrbe usuario jefe</param>
        /// <param name="id">Id del jefe</param>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        /// <exception cref="NullReferenceException">Datos NULL.</exception>
        internal Jefe(string nombre, string apellido, int dni, double salario, string nombreUsuario, int id) 
            : base(nombre, apellido, dni, salario, nombreUsuario, id)
        {
            this.Puesto = Empleado.Rol.Jefe;
        }

        /// <summary>
        /// Carga un empleado al sistema y lo guarda
        /// </summary>
        /// <param name="empleado">Empleado a cargar.</param>
        /// <returns>True si pudo cargar el empleado y guardarlo, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public override bool CargarUnEmpleadoAlSistema(Empleado empleado)
        {
            if(Jefe.empleados.CargarElementoAlSistema(empleado))
            {
                return ((IArchivo)this).GuardarArchivo();
            }
            return false;
        }

        /// <summary>
        /// Elimina un empleado del sistema y lo guarda
        /// </summary>
        /// <param name="empleado">Empleado a eliminar.</param>
        /// <returns>True si pudo eliminar el empleado y guardar cambios, caso contrario False.</returns>
        /// <exception cref="ArchivoException">Error referente al archivo.</exception>
        /// <exception cref="ArgumentNullException">Argumento NULL.</exception>
        /// <exception cref="Exception">Error externo.</exception>
        public override bool EliminarUnEmpleadoDelSistema(Empleado empleado)
        {
            if(Jefe.empleados.EliminarElementoDelSistema(empleado))
            {
                return ((IArchivo)this).GuardarArchivo();
            }
            return false;
        }

        /// <summary>
        /// Asigna el puesto de jefe a un empleado existente.
        /// </summary>
        /// <param name="empleado">Empleado que sera el nuevo Jefe</param>
        /// <returns>Un nuevo Jefe.</returns>
        /// <exception cref="NullReferenceException">Empleado NULL</exception>
        /// <exception cref="CargaDeDatosInvalidosException">Carga de datos invalida</exception>
        public static Jefe AsignarPuestoDeJefeAEmpleado(Empleado empleado)
        {
            if(empleado is not null)
            {
                return new Jefe(empleado.Nombre, empleado.Apellido, empleado.Dni, empleado.Salario, empleado.NombreUsuario, empleado.Id);
            }
            throw new NullReferenceException("Empleado null");
        }
    }
}
