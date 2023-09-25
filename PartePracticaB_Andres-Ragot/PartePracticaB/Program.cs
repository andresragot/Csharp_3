using System;

namespace PartePracticaB
{
    class Program
    {
        static int Menu(int dinero, string[] vs) // Esta funcion es la que utilizamos para el HUD inicial
        {
            int opcion;
            bool numOpcion;
            do
            {
                Console.Clear();
                Console.Write("Dinero disponible " + dinero); //representamos el dinero
                foreach(string value in vs)
                {
                    Console.Write("\t\t\t\t" + value + "\n\t\t");// y las pocisiones
                }
                Console.WriteLine("1. Comprar poción");
                Console.WriteLine("\t\t2. Vender poción");
                Console.WriteLine("\t\t3. Salir");
                numOpcion = int.TryParse(Console.ReadLine(), out opcion);//intentamos convertir lo que nos han dado en un int
                if (!numOpcion || opcion < 1 || opcion > 3)//verificamos que este correcto
                {
                    Console.WriteLine("Opción incorrecta, inserte un valor entre 1 y 3. \n Pulse una tecla para continuar");
                    Console.ReadKey();
                }
            } while (!numOpcion || opcion < 1 || opcion > 3);//se repite mientras que no sea correcto
            return opcion;//devolvemos la opcion escogiga para poder ser utilizada en nuestro modo empezar
        }

        static void Comprar(ref int dinero, string[] vs, string[] pociones, int[] precio)// esta es la funcion de Comprar avanzado
        {
            int numOpcion;
            bool opcion;
            do
            {
                Console.Clear();
                Console.Write("Dinero disponible " + dinero);//representamos en todo momento el dinero
                foreach (string value in vs)
                {
                    Console.Write("\t\t\t\t" + value + "\n\t\t");// y las pociones
                }
                Console.WriteLine("1. "+ pociones[0]+ "("+precio[0] +" monedas)");//damos las opciones de las pociones
                Console.WriteLine("\t\t2. " + pociones[1] + " (" + precio[1] + " monedas)");
                Console.WriteLine("\t\t3. " + pociones[2] + " (" + precio[2] + " monedas)");
                Console.WriteLine("\t\t4. " + pociones[3] + " (" + precio[3] + " monedas)");
                Console.WriteLine("\t\t5. " + pociones[4] + " (" + precio[4] + " monedas)");
                opcion = int.TryParse(Console.ReadLine(), out numOpcion);
                if (!opcion || numOpcion< 1 || numOpcion > 5)//verificamos
                {
                    Console.WriteLine("Opción incorrecta, inserte un valor entre 1 y 5. \n Pulse una tecla para continuar");
                    Console.ReadKey();
                }
            } while (!opcion || numOpcion< 1 || numOpcion> 5);//se repite mientras que el valor indicado no cumple con los requisitos

            int numPosicion;
            bool posicion;
            do//aca pedimos ahora la posicion en la que queremos guardar la pocion
            {
                Console.WriteLine("Escoge una posicion en donde guardar tu poción");
                Console.WriteLine("Un numero del 1 al 5");
                posicion = int.TryParse(Console.ReadLine(), out numPosicion);
                if (!posicion || numPosicion < 1 || numPosicion > 5)//verificamos
                {
                    Console.WriteLine("Opción incorrecta, inserte un valor entre 1 y 5. \n Pulse una tecla para continuar");
                    Console.ReadKey();
                }
            } while (!posicion || numPosicion< 1 || numPosicion > 5);//se repite si no cumple con los requisitos

            if (dinero >= precio[numOpcion - 1] && vs[numPosicion - 1].Equals("vacio"))// si el dinero uqe tenemos es mayor que lo que cuesta podemos comprar. y si la pocision escogida esta vacia tambien podemos comprar
            {
                dinero -= precio[numOpcion - 1];//se resta de la lista de precios
                vs[numPosicion - 1] = pociones[numOpcion - 1];//se agrega nuestra pocion nueva
                Console.Clear();
                Console.Write("Dinero disponible " + dinero);// se representa el valor del dinero ahora
                foreach (string value in vs)
                {
                    Console.Write("\t\t\t\t" + value + "\n\t\t");// y tambien se representa el de las pociones que tenemos
                }
                Console.WriteLine("La compra se ha realizado exitosamente\nPulse cualquier tecla para continuar");
                Console.ReadKey();
            }
            else if (dinero >= precio[numOpcion - 1])// si tenemos el dinero, entonces ya habia una pocion ahi
            {
                Console.WriteLine("Ya tienes una pocin en la posicion escogida\nPulse cualquier tecla para continuar");
                Console.ReadKey();
            }
            else// si no, no tenemos dinero
            {
                Console.WriteLine("Saldo insuficiente\nPulse cualquier tecla para continuar");
                Console.ReadKey();
            }
        }

        static void Vender(ref int dinero, string[]vs, string[] pociones, int[] precio)// esta funcion es la que nos permite vender
        {
            int numPosicion;
            bool posicion;
            do
            {
                Console.Clear();
                Console.Write("Dinero disponible " + dinero);//representamos el dinero
                foreach (string value in vs)
                {
                    Console.Write("\t\t\t\t" + value + "\n\t\t");// asi como las pociones que tenemos
                }
                Console.WriteLine("Elige posicion de la pocion que quieres vender");//preguntamos la pocion que queremos vender, pero se pregunta es la posicion en la que la tenemos
                posicion = int.TryParse(Console.ReadLine(), out numPosicion);
                if (!posicion || numPosicion < 1 || numPosicion > 5)//verificamos
                {
                    Console.WriteLine("Opción incorrecta, inserte un valor entre 1 y 5. \n Pulse una tecla para continuar");
                    Console.ReadKey();
                }
            } while (!posicion || numPosicion < 1 || numPosicion > 5);//se repite si no cumplimos

            string pocion = vs[numPosicion - 1];
            if (pocion.Equals("vacio"))// si esta vacio escogimos mal
            {
                Console.WriteLine("No se tiene ninguna pocion en esta posición\nPulse cualquier tecla para continuar");
                Console.ReadKey();
            }
            else// sino se busca la posicion que tiene en pociones para saber su precio
            {
                int i = Array.IndexOf(pociones, pocion);
                dinero += precio[i] / 2; // se vende a la mitad
                vs[numPosicion - 1] = "vacio"; // se vacia
            }            
        }

        static void Empezar()// esto lo utilizamos para empezar todo el programa
        {
            int dinero = 1500; // cantidad de dinero total
            string[] huecos = new string[5]; // cantidad maxima de pociones
            string[] pociones = { "Poción de Vida", "Poción de Mana", "Poción de Resucitar", "Poción Antiveneno", "Poción Invisibilidad" }; // pociones de la tienda
            Random rng = new Random();
            int[] precios = { rng.Next(2, 10) * 10, rng.Next(2, 10) * 10, rng.Next(2, 10) * 10, rng.Next(2, 10) * 10, rng.Next(2, 10) * 10 };// precios randoms

            for (int i = 0; i < huecos.Length; i++)
            {
                huecos[i] = "vacio"; // llenamos los huecos de vacio
            }
            int opc = Menu(dinero, huecos);//usampos el menu
            while (opc < 3)
            {
                switch (opc)
                {
                    case 1:
                        Comprar(ref dinero, huecos, pociones, precios); // usamos comprar
                        break;

                    case 2:
                        Vender(ref dinero, huecos, pociones, precios);// usamos vender
                        break;

                    case 3:
                        break;
                }
                opc = Menu(dinero, huecos);//repetimos
            }
        }

        static void Main(string[] args)
        {
            Empezar();
        }
    }
}
