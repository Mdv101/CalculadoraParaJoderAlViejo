using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades
{
    class Division : Operacion
    {

        public Division()
        {
            Values = new Numero[2];
            Orden = 2;
            TiposQuePermite = new List<Type>()
            {
                typeof(Numero),
                typeof(Delimitador)
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
                double value = Values[1].Valor / Values[0].Valor;
                StatusMessage = "Division procesada " + value.ToString();
                numero = new Numero(value);
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al procesar en Division: {ex.Message}";
                numero = new Numero(0);
                return false;
            }
        }

        public override string Symbol()
        {
            return "/";
        }
    }
}
