using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Datos;
using Dominio;

namespace Negocio
{
    public class GestorNegocio
    {
        private Inventario inventario;
        AccesoDatosEscritura escribir = new AccesoDatosEscritura();
        AccesoDatosLectura leer = new AccesoDatosLectura();

        public GestorNegocio()
        {
            inventario = new Inventario();
        }

        private int AsignadorID()
        {
            if (inventario.Productos.Count > 0)
            {
                // Obtiene el máximo ID existente y suma 1
                return inventario.Productos.Max(i => i.ID) + 1;
            }
            else
            {
                // Si la lista está vacía, retorna 1 como el primer ID
                return 1;
            }
        }

        public void AgregarProducto(string nombre, string cantidad, string precio)
        {
            int cantidadFinal;
            decimal precioFinal;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("No puede dejar campo vacío.");
                Thread.Sleep(2000);
                return;
                
            }
            else
            {
                if (int.TryParse(cantidad, out cantidadFinal) && cantidadFinal >= 0)
                {
                    if (decimal.TryParse(precio, out precioFinal) && precioFinal >= 0)
                    {
                        Producto nuevoProd = new Producto { ID = AsignadorID(), Nombre = nombre, Cantidad = cantidadFinal, Precio = precioFinal, Fecha = DateTimeOffset.Now };

                        inventario.AgregarProductoAlInventario(nuevoProd);

                        escribir.SerializarListaProductos(inventario.Productos);

                        Console.WriteLine("¡Producto agregado¡");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("Ingrese un precio válido.");
                        Thread.Sleep(2000);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese una cantidad válida.");
                    Thread.Sleep(2000);
                    return;
                }
            }
        }

        public void Modificar(string strId, string nombre, string strCantidad, string strPrecio)
        {
            int id;
            int cantidad;
            decimal precio;

            if (!int.TryParse(strId, out id) && string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Ingrese un ID válido.");
                Thread.Sleep(2000);
                return;
            }


            Producto modificarProducto = inventario.Productos.Find(p => p.ID == id);

            if (modificarProducto != null)
            {
                int cantidadFinal;
                decimal precioFinal;

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.WriteLine("No puede dejar campo vacío.");
                    Thread.Sleep(2000);
                    return;
                }
                else
                {
                    if (int.TryParse(strCantidad, out cantidadFinal) && cantidadFinal >= 0)
                    {
                        if (decimal.TryParse(strPrecio, out precioFinal) && precioFinal >= 0)
                        {
                            cantidad = cantidadFinal;
                            precio = precioFinal;

                            inventario.ModificarProducto(modificarProducto, nombre, cantidad, precio);

                            escribir.SerializarListaProductos(inventario.Productos);

                            Console.WriteLine("¡Producto modificado!");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine("Ingrese un precio válido.");
                            Thread.Sleep(2000);
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una cantidad válida.");
                        Thread.Sleep(2000);
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("No se encontró el ID.");
                Console.WriteLine("Ingrese cualquier tecla para continuar.");
                Console.ReadKey(true);
            }
        }

        public void MostrarProductos()
        {
            inventario.CompilarProductos();
        }

        public void CargarLista()
        {
            inventario.Productos = leer.DeserializarListaProductos();
            inventario.ventas = leer.DeserializarListaVentas();
        }

        public void Eliminar(string strId)
        {
            int id;
            if (int.TryParse(strId, out id))
            {
                Producto prodEliminar = inventario.Productos.Find(p => p.ID == id);

                if (prodEliminar != null)
                {
                    inventario.EliminarProducto(prodEliminar);
                    escribir.SerializarListaProductos(inventario.Productos);
                }
                else
                {
                    Console.WriteLine("No se encontró el ID.");
                    Thread.Sleep(2000);
                    return;
                }
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
                Thread.Sleep(2000);
            }
        }

        public void AgregarVenta(string strId, string strCantidad)
        {
            int id, cantidad = 0;
            decimal totalPagar;

            if (!int.TryParse(strId, out id) || !int.TryParse(strCantidad, out cantidad))
            {
                Console.WriteLine("Ingrese datos válidos.");
                Thread.Sleep(2000);
                return;
            }

            Producto productoSeleccionado = inventario.Productos.Find(p => p.ID == id);

            if (productoSeleccionado != null)
            {
                if (cantidad > 0 && cantidad <= productoSeleccionado.Cantidad)
                {
                    // Suma los precios
                    totalPagar = productoSeleccionado.Precio * cantidad;

                    // Actualizar la cantidad del producto
                    productoSeleccionado.Cantidad -= cantidad;

                    // Crear la venta
                    Venta venta = new Venta
                    {
                        ID = id,
                        Nombre = productoSeleccionado.Nombre,
                        Cantidad = cantidad,
                        TotalPagar = totalPagar,
                        Fecha = DateTimeOffset.Now
                    };

                    inventario.AgregarVentaALaLista(venta);
                    escribir.SerializarListaVentas(inventario.ventas);
                    escribir.SerializarListaProductos(inventario.Productos);

                    Console.WriteLine("¡Venta registrada!");
                    Console.WriteLine($"Total a pagar {totalPagar.ToString()}$");
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine("La cantidad ingresada no es válida o supera el stock disponible.");
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.WriteLine("No se encontró el ID.");
                Thread.Sleep(2000);
                return;
            }
        }

        public void MostrarVentas()
        {
            inventario.CompilarVentas();
        }

        public void MostrarProductoPorId(string strId)
        {
            int id;

            if (!int.TryParse(strId, out id))
            {
                Console.WriteLine("Ingrese un ID válido.");
                Thread.Sleep(2000);
                return;
            }

            Producto productoPorId = inventario.Productos.Find(p => p.ID == id);

            if (productoPorId != null)
            {
                inventario.BuscarPorId(productoPorId);
            }
        }
    }
}
