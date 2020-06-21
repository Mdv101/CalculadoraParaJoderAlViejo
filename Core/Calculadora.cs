using CalculadoraParaJoderAlViejo.Core;
using CalculadoraParaJoderAlViejo.Core.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalculadoraParaJoderAlViejo
{
    public class Calculadora
    {

        //Propiedades
        public Queue<Numero> NumerosInMemoria { get; internal set; } // Esta varible servira para guardar resultados anteriores 

        public Queue<Entrada> Entradas { get; set; } = new Queue<Entrada>();

        Entrada lastEntrada = null;


        public List<Type> TiposPermitidos { get; private set; }
        public List<Entrada[]> OperacionesPasadas { get; set; }
        public Numero CurrentValor { get; private set; }

        public event Action UpdatedCalculator = delegate { }; //Evento para notificar a la UI o a quien le interese que la calculadora hizo algo, 
                                                              //El delegado vacio es por que si llamas un evento sin ningun delgado que escuche pues da error

        //Singletoon implementacion
        public static Calculadora Instancia { get; private set; }

        public Calculadora()
        {
            CurrentValor = new Numero(0);
            TiposPermitidos = new List<Type>();
            NumerosInMemoria = new Queue<Numero>();

            TiposPermitidos = new List<Type>()
            {
                typeof(Numero),
                typeof(Delimitador),
                typeof(Operacion),
            };
            if (Instancia == null)
            {
                Instancia = this;
                return;
            }
        }


        //Metodos publicos

        public bool AddEntrada<T>(T entrada) where T : Entrada
        {
            if (TipoEstaPermitido<T>())
            {
                Console.WriteLine("Add Entrada Fail " + entrada.GetType().Name);
                return false;
            }

            if (Entradas.Count <= 0)
            {
                Entradas.Enqueue(entrada);
                lastEntrada = entrada;
                UpdateTypesPermitidos(entrada);
                Console.WriteLine("Add Entrada Succes " + entrada.GetType().Name);
                UpdatedCalculator();
                return true;
            }

            if (lastEntrada.CheckForComposition(entrada))
            {
                UpdatedCalculator();
                Console.WriteLine("Add Entrada Succes Compuesta" + entrada.GetType().Name);
                return true;
            }

            Entradas.Enqueue(entrada);
            UpdateTypesPermitidos(entrada);
            lastEntrada = entrada;
            Console.WriteLine("Add Entrada Succes " + entrada.GetType().Name);
            UpdatedCalculator();
            return true;
        }

        public void ErasedLastEntrada()
        {
            if (Entradas.Count <= 0)
            {
                return;
            }

            if (Entradas.Peek() is Numero)
            {
                Numero num = Entradas.Peek() as Numero;

                if (num.IsZero)
                {
                    Entradas.Dequeue();
                }
                else
                {
                    num.UndoLastValueAdded();
                }

            }
            else
            {
                Entradas.Dequeue();
            }

            UpdateTypesPermitidos(Entradas.Peek());
            UpdatedCalculator();
        }

        public void ErasedAllEntradas()
        {
            Entradas.Clear();
            UpdatedCalculator();
        }

        public void Calcular()
        {
            Queue<Entrada> finalExpresion = new Queue<Entrada>();
            Stack<Operacion> stack = new Stack<Operacion>();

            List<Queue<Entrada>> tempExpresion = new List<Queue<Entrada>>();
            List <Stack<Operacion>> tempOperacionesStack = new List<Stack<Operacion>>();

            tempExpresion.Add(new Queue<Entrada>());
            tempOperacionesStack.Add(new Stack<Operacion>());

            int isTemp = 0;

             while (Entradas.Count > 0)
             {
                if (isTemp <= 0)
                {
                    if (Entradas.Peek() is Delimitador)
                    {
                        Delimitador del = Entradas.Dequeue() as Delimitador;
                        if (del.Apertura)
                        {
                            isTemp++;
                            Console.WriteLine($"tem++ {isTemp}");
                            //Esto fue un arreglo rapido por que no lograba procesar bien las expresiones con muchos (()) , habria que implementar un 
                            //Object pool para estas filas y stacks temporales 
                            tempOperacionesStack.Add(new Stack<Operacion>());
                            tempExpresion.Add(new Queue<Entrada>());

                            tempExpresion[isTemp] = new Queue<Entrada>();
                            tempOperacionesStack[isTemp] = new Stack<Operacion>();
                            continue;
                        }
                    }
                    else if (Entradas.Peek() is Numero)
                    {
                        finalExpresion.Enqueue(Entradas.Dequeue());
                        continue;
                    }

                    else if (Entradas.Peek() is Operacion)
                    {
                        Operacion operacion = Entradas.Dequeue() as Operacion;
                        if (stack.Count <= 0)
                        {
                            stack.Push(operacion);
                            continue;
                        }

                        bool operacionInsertada = false;

                        while (!operacionInsertada)
                        {

                            if (stack.Count <= 0)
                            {
                                stack.Push(operacion);
                                operacionInsertada = true;
                                continue;
                            }

                            if (stack.Peek().Orden < operacion.Orden)
                            {
                                stack.Push(operacion);
                                operacionInsertada = true;
                                continue;
                            }

                            if (stack.Peek().Orden >= operacion.Orden)
                            {
                                finalExpresion.Enqueue(stack.Pop());
                            }

                        }

                    }
                }
                else
                {
                    if (Entradas.Peek() is Delimitador)
                    {
                        Delimitador del = Entradas.Dequeue() as Delimitador;
                        if (del.Apertura)
                        {
                            isTemp++;

                            tempOperacionesStack.Add(new Stack<Operacion>());
                            tempExpresion.Add(new Queue<Entrada>());

                            tempExpresion[isTemp] = new Queue<Entrada>();
                            tempOperacionesStack[isTemp] = new Stack<Operacion>();
                            Console.WriteLine($"tem++ {isTemp}");
                            var y = tempOperacionesStack[isTemp].Count;
                            for (int i = 0; i < y; i++)
                            {
                                tempExpresion[isTemp].Enqueue(tempOperacionesStack[isTemp].Pop());
                            }
                            Numero num = CalcularExpresionPostFix(tempExpresion[isTemp]);
                            tempExpresion[isTemp].Enqueue(num);
                            continue;
                        }
                        else
                        {
                            
                            Console.WriteLine($"isTemp-- {isTemp}");
                            var x = tempOperacionesStack[isTemp].Count;
                            for (int i = 0; i < x; i++)
                            {
                                tempExpresion[isTemp].Enqueue(tempOperacionesStack[isTemp].Pop());
                            }
                            tempExpresion[isTemp].Enqueue(CalcularExpresionPostFix(tempExpresion[isTemp]));

                            isTemp--;
                            if (isTemp <= 0)
                            {
                                while(tempExpresion[isTemp +1].Count > 0)
                                {
                                    finalExpresion.Enqueue(tempExpresion[isTemp + 1].Dequeue());
                                }
                            }
                            else
                            {
                                while (tempExpresion[isTemp + 1].Count > 0)
                                {
                                    tempExpresion[isTemp].Enqueue(tempExpresion[isTemp + 1].Dequeue());
                                }
                            }
                            //aqui temp va al definitivo?
                            continue;

                        }

                    }
                    else if (Entradas.Peek() is Numero)
                    {
                        tempExpresion[isTemp].Enqueue(Entradas.Dequeue());
                        continue;
                    }

                    else if (Entradas.Peek() is Operacion)
                    {
                        Operacion operacion = Entradas.Dequeue() as Operacion;
                        if (tempOperacionesStack[isTemp].Count <= 0)
                        {
                            tempOperacionesStack[isTemp].Push(operacion);
                            continue;
                        }

                        bool operacionInsertada = false;

                        while (!operacionInsertada)
                        {
                            if (tempOperacionesStack[isTemp].Count <= 0)
                            {
                                tempOperacionesStack[isTemp].Push(operacion);
                                operacionInsertada = true;
                                continue;
                            }

                            if (tempOperacionesStack[isTemp].Peek().Orden < operacion.Orden)
                            {
                                tempOperacionesStack[isTemp].Push(operacion);
                                operacionInsertada = true;
                                continue;
                            }

                            if (tempOperacionesStack[isTemp].Peek().Orden >= operacion.Orden)
                            {
                                tempExpresion[isTemp].Enqueue(stack.Pop());
                            }
                        }
                    }
                }
             }
            
            int m = stack.Count;
            for (int i = 0; i < m; i++)
            {
                finalExpresion.Enqueue(stack.Pop());
            }

            #region testExpresion
            Queue<Entrada> textFila = new Queue<Entrada>();
            Entrada entrada;
            string messegeText = "";
            while (finalExpresion.Count > 0)
            {
                entrada = finalExpresion.Dequeue();
                messegeText += entrada.Symbol();
                textFila.Enqueue(entrada);
            };

            Console.WriteLine(messegeText);
            while (textFila.Count > 0)
            {
                entrada = textFila.Dequeue();
                finalExpresion.Enqueue(entrada);
            };
            #endregion

            CurrentValor = CalcularExpresionPostFix(finalExpresion);
            Console.WriteLine("Valor calculado: " + CurrentValor.Valor.ToString());
            UpdatedCalculator();
        }

        public Numero Calcular(Entrada[] entradas)
        {
            return null;
        }

        //Metodos privados
        private void UpdateTypesPermitidos(Entrada entrada)
        {
            TiposPermitidos = entrada.TiposQuePermite;

        }

        private Numero CalcularExpresionPostFix(Queue<Entrada> expresionPostFix)
        {
            Stack<Numero> stackNumeros = new Stack<Numero>();

            string debugText = "";

            int n = expresionPostFix.Count;

            for (int i = 0; i < n; i++)
            {
                if (expresionPostFix.Peek() is Numero)
                {
                    Numero num = expresionPostFix.Dequeue() as Numero;
                    stackNumeros.Push(num);
                    debugText = num.Symbol();
                    Console.WriteLine(debugText);
                }
                else if (expresionPostFix.Peek() is Operacion)
                {
                    var operacion = expresionPostFix.Dequeue();
                    debugText = operacion.Symbol();
                    operacion.Procesar(out string status, out Numero result, stackNumeros);
                    Console.WriteLine( "//" + status);
                    stackNumeros.Push(result);
                }
            }

            if (stackNumeros.Count == 1)
            {
                return stackNumeros.Pop();
            }
            Console.WriteLine($"Se evaluo una expresion y al final tenia mas de un numero {stackNumeros.Count} {debugText}");
            return new Numero(0);

        }

        private bool TipoEstaPermitido<T>() where T : Entrada
        {
            for (int i = 0; i < TiposPermitidos.Count; i++)
            {
                if (TiposPermitidos[i] is T)
                {
                    return true;
                }
            }
            return false;
        }

        }
    }


