using CalculadoraParaJoderAlViejo.Core;
using CalculadoraParaJoderAlViejo.Core.Entidades;
using CalculadoraParaJoderAlViejo.Core.Entidades.Operaciones;
using CalculadoraParaJoderAlViejo.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CalculadoraParaJoderAlViejo
{
    public partial class CalculadoraForm : Form
    {
        ControlFormCalculadora control;
        public CalculadoraForm()
        {
            InitializeComponent();
            control = new ControlFormCalculadora(textOperacion, labelResult);
        }
        //Click Numeros 
        private void button1_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(1));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(2));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(3));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(4));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(5));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(6));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(7));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(8));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(9));
        }

        private void button0_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Numero>(new Numero(0));
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Punto>(new Punto());
        }

        private void buttonSuma_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Suma>(new Suma());
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.Calcular();
        }

        private void buttonResta_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Resta>(new Resta());
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Division>(new Division());
        }

        private void buttonRaizCuadrado_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<RaizCuadrada>(new RaizCuadrada());
        }

        private void buttonMultiplicacion_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Multiplicacion>(new Multiplicacion());
        }

        private void buttonParentesisApertura_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Delimitador>(new Delimitador(true));

        }

        private void buttonParentesisCierre_Click(object sender, EventArgs e)
        {
            Calculadora.Instancia.AddEntrada<Delimitador>(new Delimitador(false));
        }

        private void CalculadoraForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(0));
                    break;
                case '1':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(1));
                    break;
                case '2':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(2));
                    break;
                case '3':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(3));
                    break;
                case '4':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(4));
                    break;
                case '5':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(5));
                    break;
                case '6':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(6));
                    break;
                case '7':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(7));
                    break;
                case '8':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(8));
                    break;
                case '9':
                    Calculadora.Instancia.AddEntrada<Numero>(new Numero(9));
                    break;
                case '+':
                    Calculadora.Instancia.AddEntrada<Suma>(new Suma());
                    break;
                case '-':
                    Calculadora.Instancia.AddEntrada<Resta>(new Resta());
                    break;
                case '/':
                    Calculadora.Instancia.AddEntrada<Division>(new Division());
                    break;
                case '*':
                    Calculadora.Instancia.AddEntrada<Multiplicacion>(new Multiplicacion());
                    break;
                case '(':
                    Calculadora.Instancia.AddEntrada<Delimitador>(new Delimitador(true));
                    break;
                case ')':
                    Calculadora.Instancia.AddEntrada<Delimitador>(new Delimitador(false));
                    break;
                case '.':
                    Calculadora.Instancia.AddEntrada<Punto>(new Punto());
                    break;
            }
        }
    }
}
