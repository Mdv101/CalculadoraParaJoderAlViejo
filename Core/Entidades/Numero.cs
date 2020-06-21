using System;
using System.Collections.Generic;


namespace CalculadoraParaJoderAlViejo.Core.Entidades
{
    public class Numero : Entrada
    {
        //Campos
        bool isZero;
        bool hasDecimals;
        bool positive;
        double lastValueAdded;

        //Propiedades
        public double Valor { get; private set; }

        public bool IsZero { get => isZero; 
            set
            {
                if (value == false)
                {
                    isZero = false;
                    return;
                }
                else
                {
                    isZero = true;
                    Valor = 0;
                }
            } 
        }

        //Constructor 
        public  Numero (double valor)
        {
            Valor = valor;
            lastValueAdded = valor;
            EvaluateIfDecimal();
            EvaluateIfPositivo();
            EvaluateIfZero();

            TiposQuePermite = new List<Type>
            {
                typeof(Punto),
                typeof(Numero),
                typeof(Operacion),
                typeof(Delimitador)
            };
        }

        //Heradado
        public override bool Procesar(out string StatusMessage,out Numero numero, Stack<Numero> numeros)
        {
            try
            {
                StatusMessage = "Numero valido";
                numero = this;
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                numero = this;
                return false;
            }

        }

        public override string Symbol()
        {
            string symbol = "";

            if (!positive)
            {
                symbol = "-";
            }
            if (hasDecimals)
            {
                return symbol += Valor.ToString("0.0####");
            }
            return symbol += Valor.ToString();
        }

        public override bool CheckForComposition<T>(T entrada)
        {
            if (entrada is Punto)
            {
                AddPunto(entrada as Punto);
                return true;
            }

            if (entrada is Numero)
            {
                AddNumero(entrada as Numero);
                return true;
            }

            return false;
        }

        //Metodos Publicos
        public void UndoLastValueAdded()
        {
            Valor -= lastValueAdded;
            EvaluateIfDecimal();
            EvaluateIfPositivo();
            EvaluateIfZero();
        }
        
        ///Metodos Privados
        void AddPunto(Punto punto)
        {
            hasDecimals = true;
        }
        void AddNumero(Numero numero)
        {
            //Aqui ajustaba el valor haciendo la operacion, pero no estaba funcionado y decidi cortar por lo facil
            //concatenando string, pero esto se puede optimizar para performance 
            string valorInString = Valor.ToString();
            if (hasDecimals)
            {
                valorInString += ".";
            }
            string addValorInString = numero.Valor.ToString();

            string combine = valorInString + addValorInString;
            Valor = double.Parse(combine);

        }
        void EvaluateIfDecimal()
        {
            if (Math.Abs(Valor % 1) >= (Double.Epsilon * 100))
            {
                hasDecimals = true;
                return;
            }
            hasDecimals = false;
        }
        void EvaluateIfPositivo()
        {
            if (Valor < 0)
            {
                positive = false;
                return;
            }
            positive = true;
        }
        void EvaluateIfZero()
        {
            if (Valor == 0)
            {
                isZero = true;
                return;
            }
            isZero = false;
            return;
        }
     
    }
    
}
