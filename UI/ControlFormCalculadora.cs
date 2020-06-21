using CalculadoraParaJoderAlViejo.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraParaJoderAlViejo.UI
{
    class ControlFormCalculadora
    {
       public Label TextOperation { get; set;}
       public Label TextResult { get; set; }

        public ControlFormCalculadora(Label textOperation, Label textResult)
        {
            TextOperation = textOperation;
            TextResult = textResult;
            textOperation.Text = "";
            Calculadora.Instancia.UpdatedCalculator += HandleNewInputs;
        }

        void HandleNewInputs()
        {
            Queue<Entrada> tempEntradas = new Queue<Entrada>();
            TextOperation.Text = "";
            int n = Calculadora.Instancia.Entradas.Count;
            Entrada entrataTemp;


            for (int i = 0; i < n; i++)
            {
                entrataTemp = Calculadora.Instancia.Entradas.Dequeue();
                TextOperation.Text += entrataTemp.Symbol();
                tempEntradas.Enqueue(entrataTemp);
            }

            Calculadora.Instancia.Entradas = tempEntradas;
            if (Calculadora.Instancia.CurrentValor != null)
            {
                TextResult.Text = Calculadora.Instancia.CurrentValor.Valor.ToString();
            }
            else
            {
                TextResult.Text = "O.o";
            }

        }
    }
}
