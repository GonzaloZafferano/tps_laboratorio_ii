using System;

namespace Entidades
{
    public static class Calculadora
    {     
        /// <summary>
        /// Valida que el parametro recibido se corresponda con un operador aritmetico.
        /// </summary>
        /// <param name="operador">Operador que se evaluara</param>
        /// <returns>El operador + , - , * , / ya validado, o + (por defecto) en caso de que el parametro recibido sea invalido.</returns>
        private static char ValidarOperador(char operador)
        {
            char retorno = '+';

            if (operador == '-' || operador == '*' || operador == '/')
            {
                retorno = operador;
            }

            return retorno;
        }

        /// <summary>
        /// Realiza la operacion entre dos elementos de tipo 'Operando', previa validacion del parametro 'operador'
        /// </summary>
        /// <param name="num1">Operando A</param>
        /// <param name="num2">Operando B</param>
        /// <param name="operador">Tipo de operacion a realizar</param>
        /// <returns>Un double, resultado de operar el Operando A con el Operando B.</returns>
        public static double Operar(Operando num1, Operando num2, char operador)
        {
            char tipoOperacion = Calculadora.ValidarOperador(operador);

            double retorno = 0;

            switch (tipoOperacion)
            {
                case '-':
                    retorno = num1 - num2;
                    break;
                case '+':
                    retorno = num1 + num2;
                    break;
                case '*':
                    retorno = num1 * num2;
                    break;
                case '/':
                    retorno = num1 / num2;
                    break;
            }
            return retorno;
        }      
    }
}
