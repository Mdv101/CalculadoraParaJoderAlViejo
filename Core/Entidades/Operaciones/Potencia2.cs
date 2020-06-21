using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades.Operaciones
{
    class Potencia2 : Entrada
    {
        public override bool CheckForComposition<T>(T entrada)
        {
            return false;
        }

        public override bool Procesar(out string StatusMessage, out Numero numero, Stack<Numero> filaNumeros = null)
        {
            throw new NotImplementedException();
        }

        public override string Symbol()
        {
            throw new NotImplementedException();
        }
    }
}
