﻿using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestAdministrador
    {
        [TestMethod]
        public void CambiarPuestoDeEmpleado_CuandoElEmpleadoEsEmpleado_DeberiaRetornarUnAdministrador()
        {
            //ARRANGE
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
            Empleado actual = Administrador.CambiarPuestoDeEmpleado(empleadoA);

            //ASSERT
            Assert.IsInstanceOfType(actual, typeof(Administrador));
        }

        [TestMethod]
        public void CambiarPuestoDeEmpleado_CuandoElEmpleadoEsEmpleado_DeberiaRetornarUnAdministradorConLosMismosDatos()
        {
            //ARRANGE
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
            Empleado empleadoB = Administrador.CambiarPuestoDeEmpleado(empleadoA);

            actual = empleadoA.NombreCompleto == empleadoB.NombreCompleto && empleadoA.Dni == empleadoB.Dni &&
                        empleadoA.Id == empleadoB.Id && empleadoB is Administrador;

            //ASSERT
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(CargaDeDatosInvalidosException))]
        public void ConstructorAdministrador_CuandoSePasaUnNombreDeUsuarioConCaracterInvalido_DeberiaRetornarUnaCargaDeDatosInvalidaException()
        {
            //ARRANGE
            string nombreUsuarioInvalido = "Gon.za";

            //ACT
            new Administrador("Gonzalo", "Zafferano", 12345678, 1, nombreUsuarioInvalido);
        }
    }
}
