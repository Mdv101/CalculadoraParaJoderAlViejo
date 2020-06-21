using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades
{

    internal abstract class Operacion : Entrada
    {

        //Estas propiedades deben ser establecidas en el constructor 
        public Numero[] Values { get; protected set; }

        public int Orden { get; protected set; }

    }
}
