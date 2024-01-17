using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Gestor
{
    internal class Program
    {
        static void Main(string[] args)
        {
           GestorNegocio negocio = new GestorNegocio();
            ConfiguracionConsola();

            negocio.CargarLista();

            string strId;
            string nombre = "";
            string cantidad = "";
            string precio = "";

            string opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Ver los productos");
                Console.WriteLine("2. Ingresar un nuevo producto");
                Console.WriteLine("3. Para modificar un producto");
                Console.WriteLine("4. Para registrar una venta");
                Console.WriteLine("5. Para ver las ventas");
                Console.WriteLine("6. Para eliminar un producto");
                Console.WriteLine("0. Salir");
                Console.WriteLine();

                opcion = LeerTecla();

                switch (opcion)
                {
                    case "1":
                        negocio.MostrarProductos();                       
                        break;

                    case "2":
                        PedirDatos(ref nombre, ref cantidad, ref precio);
                        negocio.AgregarProducto(nombre, cantidad, precio);
                        break;

                        case "3":
                        Console.Write("Ingrese el ID del producto a modificar: ");
                        strId = Console.ReadLine();
                        PedirDatos(ref nombre, ref cantidad, ref precio);
                        negocio.Modificar(strId, nombre, cantidad, precio);
                        break;

                    case "4":
                        Console.Write("Ingrese el ID del producto a comprar: ");
                        strId = Console.ReadLine();
                        Console.Write("Ingrese la cantidad a comprar: ");
                        cantidad = Console.ReadLine();
                        negocio.AgregarVenta(strId, cantidad);
                        break;

                    case "5":
                        negocio.MostrarVentas();
                        break;

                    case "6":
                        Console.Write("Ingrese le ID del producto que desea eliminar: ");
                        strId = Console.ReadLine();
                        negocio.Eliminar(strId);
                        break;

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("ingrese una opción correcta.");
                        Console.WriteLine("Precione cualquier tecla para continuar.");
                        Console.ReadKey(true);
                        break;
                }

            } while (opcion != "0");
        }

        static void ConfiguracionConsola()
        {
            Console.Title = "Inventario";
            Console.CursorVisible = false; // Oculta el cursor
        }

        static string LeerTecla()
        {
            ConsoleKeyInfo ingreso = Console.ReadKey(true);
            return ingreso.KeyChar.ToString();
        }

        static void PedirDatos(ref string nombre, ref string cantidad, ref string precio)
        {
            Console.Write("Ingrese el nombre del producto: ");
            nombre = Console.ReadLine();
            Console.Write("Ingrese la cantidad: ");
            cantidad = Console.ReadLine();
            Console.Write("Ingrese el precio del producto: ");
            precio = Console.ReadLine();
        }
    }
}