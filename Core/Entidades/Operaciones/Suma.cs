using CalculadoraParaJoderAlViejo.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core
{

    internal class Suma : Operacion
    {
        //Constructor
        public Suma()
        {
            Values = new Numero[2];
            Orden = 1;
            TiposQuePermite = new List<Type>()
            {
                typeof(Numero),
                typeof(Delimitador)
            };
        }

        //Heredados 
        public override bool CheckForComposition<T>(T entrada)
        {
            return false;
        }

        public override bool Procesar(out string StatusMessage, out Numero numero, Stack<Numero> filaNumeros = null)
        {
            try
            {
                for (int i = 0; i < Values.Length; i++)
                {
                    Values[i] = filaNumeros.Pop();
                }
                double value = Values[0].Valor + Values[1].Valor;
                StatusMessage = "Suma procesada " + value.ToString();
                numero = new Numero(value);
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al procesar en suma: {ex.Message}";
                numero = new Numero(0);
                return false;
            }
        }

        public override string Symbol()
        {
            return "+";
        }
    }
}
