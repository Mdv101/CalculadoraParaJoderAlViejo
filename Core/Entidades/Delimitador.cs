using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraParaJoderAlViejo.Core.Entidades
{
    class Delimitador : Entrada
    {
        //Campos
        string symbol = "";

        //Propiedades

        public bool Apertura { get; private set; }

        public Delimitador(bool isApertura)
        {
            Apertura = isApertura;
            if (isApertura)
            {
                symbol = "(";
                TiposQuePermite = new List<Type>
                {
                    typeof(Numero),
                    typeof(Delimitador)
                };
            }
            else
            {
                Apertura = isApertura;
                symbol = ")";
                TiposQuePermite = new List<Type>
                {
                    typeof(Operacion)
                };
            }
        }

        public override bool CheckForComposition<T>(T entrada)
        {
            return false;
        }

        public override bool Procesar(out string StatusMessage, out Numero numero, Stack<Numero> numeros)
        {
            StatusMessage = "Delimitador - Apertura " + Apertura;
            numero = new Numero(0);
            return true;
        }

        public override string Symbol()
        {
            return symbol;
        }
    }
}
