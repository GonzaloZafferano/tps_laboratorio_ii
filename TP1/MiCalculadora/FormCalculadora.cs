using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace MiCalculadora
{
    public partial class FormCalculadora : Form
    {
        /// <summary>
        /// Constructor del 'FormCalculadora'
        /// </summary>
        public FormCalculadora()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona sobre el btnLimpiar. Realiza una limpieza de cajas de texto, label y comboBox.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        /// <summary>
        /// Realiza una limpieza de las cajas de texto, el comboBox y el label de resultado.
        /// </summary>
        private void Limpiar()
        {
            lblResultado.Text = "";
            txtNumero1.Text = "";
            txtNumero2.Text = "";
            cmbOperador.SelectedIndex = 0;

            //True: Boton habilitado para uso. False: Boton no habilitado.
            btnConvertirABinario.Tag = true;
            btnConvertirADecimal.Tag = true;
        }

        /// <summary>
        /// Evento que se ejecuta al cargar el Form. Realiza una limpieza de las cajas de texto, combobox y el label de resultado.
        /// Carga el comboBox con las operaciones aritmeticas disponibles.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            cmbOperador.Items.Add("");
            cmbOperador.Items.Add('+');
            cmbOperador.Items.Add('-');
            cmbOperador.Items.Add('/');
            cmbOperador.Items.Add('*');
            Limpiar();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer click sobre el btnCerrar. Cierra el Form, finalizando la ejecucion.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento que se ejecuta previo al cierre de la aplicacion. Consulta si desea cerrar la aplicacion.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de querer salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Realiza una operacion aritmetica basandose en los parametros (Operando A, Operando B y operador) recibidos.
        /// </summary>
        /// <param name="numero1">Operando A</param>
        /// <param name="numero2">Operando B</param>
        /// <param name="operador">Tipo de operacion a realizar.</param>
        /// <returns>Un double, resultado de haber operado con los Operandos A y B.</returns>
        private static double Operar(string numero1, string numero2, string operador)
        {
            //Metodo char.TryParse: Si la cadena esta vacia, asignara el caracter '\0'.
            bool _ = char.TryParse(operador, out char operacionAritmetica);

            return Calculadora.Operar(new Operando(numero1), new Operando(numero2), operacionAritmetica);
        }

        /// <summary>
        /// Realiza una operacion aritmetica al presionar el btnOperar.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void btnOperar_Click(object sender, EventArgs e)
        {
            double resultado = FormCalculadora.Operar(txtNumero1.Text, txtNumero2.Text, cmbOperador.Text);

            lblResultado.Text = resultado == double.MinValue ? "Error Matemático" : Math.Round(resultado,4).ToString();

            bool _ = double.TryParse(txtNumero1.Text, out double numeroA);
            _ = double.TryParse(txtNumero2.Text, out double numeroB);

            char operadorAritmetico = string.IsNullOrEmpty(cmbOperador.Text) ? '+' : cmbOperador.Text[0];
            
            string operacionRealizada = string.Format("{0} {1} {2} = {3}", numeroA, operadorAritmetico, numeroB, lblResultado.Text);
                
            lstOperaciones.Items.Add(operacionRealizada);
            btnConvertirABinario.Tag = true;  
            btnConvertirADecimal.Tag = true;
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el btnConvertirABinario. Convierte un numero en formato string a Binario.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void btnConvertirABinario_Click(object sender, EventArgs e)
        { 
            if((bool)btnConvertirABinario.Tag)
            {                
                StringBuilder sb = new StringBuilder();

                if(double.TryParse(lblResultado.Text, out double resultado))
                {
                    sb.AppendLine($"{(int)Math.Abs(resultado)}(d) = ");
                }

                lblResultado.Text = new Operando().DecimalBinario(lblResultado.Text);

                if(double.TryParse(lblResultado.Text, out double _))
                {
                    sb.Append($"{lblResultado.Text}(b)");
                    lstOperaciones.Items.Add(sb.ToString());
                }
            }
            btnConvertirADecimal.Tag = true;                   
            btnConvertirABinario.Tag = false;
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el btnConvertirADecimal. Convierte un binario en formato string a Numero decimal.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto que contiene informacion del evento.</param>
        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {         
            if((bool)btnConvertirADecimal.Tag)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"{lblResultado.Text}(b) = ");

                lblResultado.Text = new Operando().BinarioDecimal(lblResultado.Text);

                if(double.TryParse(lblResultado.Text, out double numeroDecimal))
                {
                    sb.Append($"{numeroDecimal}(d)");
                    lstOperaciones.Items.Add(sb.ToString());
                }
            }
            btnConvertirABinario.Tag = true;
            btnConvertirADecimal.Tag = false;  
        }
    }
}
