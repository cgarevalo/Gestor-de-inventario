using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dominio
{
    public class Inventario
    {
        public List<Venta> ventas;

        public List<Producto> Productos { get; set; }

        public Inventario() 
        {
            ventas = new List<Venta>();
            Productos = new List<Producto>();
        }

        public void AgregarProductoAlInventario(Producto nuevoProducto)
        {
            Productos.Add(nuevoProducto);
        }

        public void ModificarProducto(Producto modProducto, string nombre, int cantidad, decimal precio)
        {
            modProducto.Nombre = nombre;
            modProducto.Cantidad = cantidad;
            modProducto.Precio = precio;
        }

        public void CompilarProductos()
        {
            if (Productos.Any())
            {
                foreach (Producto producto in Productos)
                {
                    Console.WriteLine($"ID: {producto.ID}");
                    Console.WriteLine($"Nombre: {producto.Nombre}");
                    Console.WriteLine($"Cantidad: {producto.Cantidad}");
                    Console.WriteLine($"Precio: {producto.Precio}");
                    Console.WriteLine($"Fecha de registro: {producto.Fecha}");
                    Console.WriteLine("------------------------------------------------");
                }

                Console.Write("Precione cualquier tecla para continuar.");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("No hay productos.");
                Thread.Sleep(2000);
            }
        }

        public void EliminarProducto(Producto producto)
        {
            Productos.Remove(producto);
        }

        public void AgregarVentaALaLista(Venta venta)
        {
            ventas.Add(venta);
        }

        public void CompilarVentas()
        {
            if (ventas.Any())
            {
                foreach (Venta venta in ventas)
                {
                    Console.WriteLine($"Nombre: {venta.Nombre}");
                    Console.WriteLine($"Cantidad comprada: {venta.Cantidad}");
                    Console.WriteLine($"Monto total: {venta.TotalPagar}");
                    Console.WriteLine($"Fecha de la venta: {venta.Fecha}");
                    Console.WriteLine("------------------------------------------------"); 
                }

                Console.Write("Precione cualquier tecla para continuar.");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("No hay ventas registradas.");
                Thread.Sleep(2000);
            }
        }

        public void BuscarPorId(Producto producto)
        {
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Cantidad: {producto.Cantidad}");
            Console.WriteLine($"Precio: {producto.Precio}");
            Console.WriteLine($"Fecha de registro: {producto.Fecha}");

            Console.Write("Precione cualquier tecla para continuar.");
            Console.ReadKey(true);
        }
    }
}
