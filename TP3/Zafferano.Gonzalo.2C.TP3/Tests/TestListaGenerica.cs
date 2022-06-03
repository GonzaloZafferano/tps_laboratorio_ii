using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace Tests
{
    [TestClass]
    public class TestListaGenerica
    {
        [TestMethod]
        public void CargarElementoAlSistema_CuandoAunNoExisteElElementoCargado_DeberiaRetornarTrue()
        {
            //ARRANGE
            ListaGenerica<Empleado> empleados = new ListaGenerica<Empleado>();
            bool actual;

            Empleado empleadoA = new Empleado()
            {
                Apellido = "Z",
                Nombre = "G",
                Dni = 11111111,
                Id = 987,
                NombreUsuario = "abcd",
                Puesto = Empleado.Rol.Empleado,
                Salario = 1
            };

            //ACT
            actual = empleados.CargarElementoAlSistema(empleadoA);            

            //ASSERT
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CargarElementoAlSistema_CuandoYaExisteElElementoCargado_DeberiaRetornarFalse()
        {
            //ARRANGE
            ListaGenerica<Empleado> empleados = new ListaGenerica<Empleado>();
            bool actual = true;

            Empleado empleadoA = new Empleado()
            {
                Apellido = "Z",
                Nombre = "G",
                Dni = 11111111,
                Id = 987,
                NombreUsuario = "abcd",
                Puesto = Empleado.Rol.Empleado,
                Salario = 1
            };

            Empleado empleadoB = new Empleado()
            {
                Apellido = "Z",
                Nombre = "G",
                Dni = 11111111,
                Id = 987,
                NombreUsuario = "abcd",
                Puesto = Empleado.Rol.Empleado,
                Salario = 1
            };


            //ACT
            if(empleados.CargarElementoAlSistema(empleadoA))
            {
                actual = empleados.CargarElementoAlSistema(empleadoB);
            }

            //ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ExisteElementoPorIdentificador_CuandoElElementoEstaCargadoEnElSistema_DeberiaRetornarTrue()
        {
            //ARRANGE
            ListaGenerica<Empleado> empleados = new ListaGenerica<Empleado>();
            bool actual = false;

            Empleado empleadoA = new Empleado()
            {
                Apellido = "Z",
                Nombre = "G",
                Dni = 11111111,
                Id = 987,
                NombreUsuario = "abcd",
                Puesto = Empleado.Rol.Empleado,
                Salario = 1
            };

            //ACT
            if(empleados.CargarElementoAlSistema(empleadoA))
            {
                actual = empleados.ExisteElementoPorIdentificador(empleadoA.Dni);
            }

            //ASSERT
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ExisteElementoPorIdentificador_CuandoElElementoNoEstaCargado_DeberiaRetornarFalse()
        {
            //ARRANGE
            ListaGenerica<Empleado> empleados = new ListaGenerica<Empleado>();
            bool actual;

            Empleado empleadoA = new Empleado()
            {
                Apellido = "Z",
                Nombre = "G",
                Dni = 11111111,
                Id = 987,
                NombreUsuario = "abcd",
                Puesto = Empleado.Rol.Empleado,
                Salario = 1
            };

            //ACT
            actual = empleados.ExisteElementoPorIdentificador(empleadoA.Dni);          

            //ASSERT
            Assert.IsFalse(actual);
        }
    }
}
