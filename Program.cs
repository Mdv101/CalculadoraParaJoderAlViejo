using CalculadoraParaJoderAlViejo.Core;
using CalculadoraParaJoderAlViejo.Core.Entidades;
using CalculadoraParaJoderAlViejo.Core.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalculadoraParaJoderAlViejo
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Calculadora calculadora = new Calculadora();

            //Tests de calculdora

            //Test sin () pero con hasta 3 ordenes de operacion 

            //calculadora.AddEntrada<RaizCuadrada>(new RaizCuadrada());
            //calculadora.AddEntrada<Numero>(new Numero(2));
            //calculadora.AddEntrada<Division>(new Division());
            //calculadora.AddEntrada<Numero>(new Numero(3));
            //calculadora.AddEntrada<Multiplicacion>(new Multiplicacion());
            //calculadora.AddEntrada<Numero>(new Numero(2));
            //calculadora.AddEntrada<Suma>(new Suma());
            //calculadora.AddEntrada<Numero>(new Numero(5));
            //calculadora.AddEntrada<Resta>(new Resta());
            //calculadora.AddEntrada<Numero>(new Numero(4));
            //calculadora.AddEntrada<Multiplicacion>(new Multiplicacion());
            //calculadora.AddEntrada<Numero>(new Numero(3));
            //Calculadora.Instancia.Calcular();

            //Test de () 

            //calculadora.AddEntrada<Delimitador>(new Delimitador(true));
            //calculadora.AddEntrada<Numero>(new Numero(2));
            //calculadora.AddEntrada<Suma>(new Suma());
            //calculadora.AddEntrada<Numero>(new Numero(2));
            //calculadora.AddEntrada<Delimitador>(new Delimitador(false));
            //calculadora.AddEntrada<Multiplicacion>(new Multiplicacion());
            //calculadora.AddEntrada<Delimitador>(new Delimitador(true));
            //calculadora.AddEntrada<Numero>(new Numero(2));
            //calculadora.AddEntrada<Suma>(new Suma());
            //calculadora.AddEntrada<Numero>(new Numero(2));
            //calculadora.AddEntrada<Delimitador>(new Delimitador(false));

            //Calculadora.Instancia.Calcular();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculadoraForm());
        }
    }
}
