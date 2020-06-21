using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades.Operaciones
{
    class RaizCuadrada : Operacion
    {
        public RaizCuadrada()
        {
            Values = new Numero[1];
            Orden = 3;
            TiposQuePermite = new List<Type>()
            {
                typeof(Numero),
            };
        }
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
                double value = Math.Sqrt(Values[0].Valor) ;
                StatusMessage = "raiz cuadrada procesada " + value.ToString();
                numero = new Numero(value);
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al procesar en raiz cuadrada: {ex.Message}";
                numero = new Numero(0);
                return false;
            }
        }

        public override string Symbol()
        {
            return ("√");
        }
    }
}
