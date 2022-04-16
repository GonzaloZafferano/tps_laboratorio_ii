using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Operando
    {
        private double numero;

        private string Numero
        {
            set
            {
                this.numero = ValidarOperando(value);
            }
        }

        /// <summary>
        /// Constructor de clase 'Operando' sin parametros. Asigna 0 por defecto.
        /// </summary>
        public Operando():this(0)
        {
 
        }

        /// <summary>
        /// Constructor de clase 'Operando'. 
        /// </summary>
        /// <param name="numero">Valor de inicializacion para el atributo 'numero'</param>
        public Operando(double numero)
        {
            this.numero = numero;
        }

        /// <summary>
        /// Constructor de clase 'Operando'. 
        /// </summary>
        /// <param name="numero">Valor de inicializacion en formato string para el atributo 'numero'</param>
        public Operando(string numero)
        {
            this.Numero = numero;
        }

        /// <summary>
        /// Valida un operando de tipo string, devolviendo el mismo en tipo double o 0 en caso de error.
        /// </summary>
        /// <param name="strNumero">Cadena que se intentara convertir a double</param>
        /// <returns>Retorna la cadena convertida a double, o 0 en caso de error.</returns>
        private double ValidarOperando(string strNumero)
        {
            bool _ = double.TryParse(strNumero, out double numero);
           
            return numero;
        }

        /// <summary>
        /// Evalua que una cadena este compuesta por '0' y/o '1'.
        /// </summary>
        /// <param name="binario">Cadena que se evaluara.</param>
        /// <returns>True si la cadena representa un binario, caso contrario False.</returns>
        private bool EsBinario(string binario)
        {
            bool retorno = !string.IsNullOrEmpty(binario);

            for (int i = 0; i < binario.Length; i++)
            {
                if (binario[i] != '1' && binario[i] != '0')
                {
                    retorno = false;
                    break;
                }
            }
            return retorno;
        }

        /// <summary>
        /// Convierte un numero decimal (recibido en formato double) en Binario.
        /// </summary>
        /// <param name="numero">Numero que se convertira en Binario.</param>
        /// <returns>Cadena con el numero Binario, o "Valor invalido" en caso de no poder realizar la conversion.</returns>
        public string DecimalBinario(double numero)
        {
            string retorno = "Valor inválido";
            StringBuilder resto = new StringBuilder();
            int numeroEntero = (int)Math.Abs(numero);

            do
            {
                resto.Append(numeroEntero % 2);
            } while ((numeroEntero /= 2) > 1);

            resto.Append(numeroEntero);

            while (resto.Length % 4 != 0)
            {
                resto.Append(0);
            }

            if(EsBinario(resto.ToString()))
            {
                retorno = new string(resto.ToString().Reverse().ToArray());
            }
            return retorno;
        }

        /// <summary>
        /// Convierte un numero decimal (recibido en formato string) en Binario.
        /// </summary>
        /// <param name="numero">Cadena que representa un numero.</param>
        /// <returns>Cadena con el numero Binario, o "Valor invalido" en caso de no poder realizar la conversion.</returns>
        public string DecimalBinario(string numero)
        {
            string retorno = "Valor inválido";
            
            if (double.TryParse(numero, out double numeroDouble))
            {
                retorno = DecimalBinario(numeroDouble);
            }
            return retorno;
        }

        /// <summary>
        /// Convierte un numero Binario (recibido en formato string) en numero Decimal.
        /// </summary>
        /// <param name="numero">Cadena que representa un numero Binario.</param>
        /// <returns>Cadena con el numero Decimal ya convertido, o "Valor invalido" en caso de no poder realizar la conversion.</returns>
        public string BinarioDecimal(string numero)
        {
            string retorno = "Valor inválido";
            double acumulador = 0;
            string cadenaNumerica;

            if (EsBinario(numero))
            {
                cadenaNumerica = new string(numero.Reverse().ToArray());

                for (int i = 0; i < cadenaNumerica.Length; i++)
                {
                    if (cadenaNumerica[i] != '0')
                    {
                        acumulador += Math.Pow(2, i);
                    }
                }
                retorno = acumulador.ToString();
            }
            return retorno;
        }

        /// <summary>
        /// Sobrecarga de la Suma entre elementos de tipo 'Operando'
        /// </summary>
        /// <param name="n1">Operando A</param>
        /// <param name="n2">Operando B</param>
        /// <returns>Un double, resultado de sumar A y B</returns>
        public static double operator +(Operando n1, Operando n2)
        {
            double retorno = 0;

            if(n1 is not null && n2 is not null)
            {
                retorno = n1.numero + n2.numero;
            }
            return retorno;
        }

        /// <summary>
        /// Sobrecarga de la resta entre elementos de tipo 'Operando'
        /// </summary>
        /// <param name="n1">Operando A</param>
        /// <param name="n2">Operando B</param>
        /// <returns>un Double, resultado de restar A y B</returns>
        public static double operator -(Operando n1, Operando n2)
        {
            double retorno = 0;
            
            if(n1 is not null && n2 is not null)
            {
                retorno = n1.numero - n2.numero;
            }
            return retorno;
        }

        /// <summary>
        /// Sobrecarga de la multiplicacion entre elementos de tipo 'Operando'
        /// </summary>
        /// <param name="n1">Operando A</param>
        /// <param name="n2">Operando B</param>
        /// <returns>un Double, resultado de multiplicar A y B</returns>
        public static double operator *(Operando n1, Operando n2)
        {
            double retorno = 0;

            if(n1 is not null && n2 is not null)
            {
                retorno = n1.numero * n2.numero;
            }
            return retorno;
        }

        /// <summary>
        /// Sobrecarga de la division entre elementos de tipo 'Operando'
        /// </summary>
        /// <param name="n1">Operando A</param>
        /// <param name="n2">Operando B</param>
        /// <returns>un Double, resultado de dividir A y B. O el 'double.minValue' si el operando B es 0.</returns>
        public static double operator /(Operando n1, Operando n2)
        {
            double retorno = double.MinValue;

            if (n1 is not null && n2 is not null && n2.numero != 0)
            {
                retorno = n1.numero / n2.numero;
            }
            return retorno;
        }
    }
}
