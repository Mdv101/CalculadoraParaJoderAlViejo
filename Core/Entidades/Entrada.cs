using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades
{
    public abstract class Entrada
    {
        //Propiedades
        public virtual List<Type> TiposQuePermite { get; protected set; }

        //Metodos Publicos
        public abstract bool Procesar(out string StatusMessage, out Numero numero, Stack<Numero> filaNumeros = null);
        public abstract string Symbol();
        public abstract bool CheckForComposition <T>(T entrada) where T: Entrada; 

    }
}
