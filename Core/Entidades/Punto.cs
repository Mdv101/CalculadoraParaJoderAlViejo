using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades
{
    class Punto : Entrada
    {
        public Punto()
        {
            TiposQuePermite = new List<Type>
            {
                typeof(Operacion),
                typeof(Numero)
            };
        }

        //Herencia
        public override bool CheckForComposition<T>(T entrada)
        {
            return false;
        }

        public override bool Procesar(out string StatusMessage, out Numero numero, Stack<Numero> numeros = null)
        {
            StatusMessage = "Se intento procesar un punto";
            numero = new Numero(0);
            return true;
        }

        public override string Symbol()
        {
            return ("");
        }
    }
}
