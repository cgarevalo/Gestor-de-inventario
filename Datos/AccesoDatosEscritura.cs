using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Newtonsoft.Json;
using System.IO;

namespace Datos
{
    public class AccesoDatosEscritura
    {
        string CARPETA_APP_DATA_LOCAL = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string NOMBRE_PRODUCTOS = "inventario.json";
        string NOMBRE_VENTAS = "ventas.json";
        string RUTA_ARCHIVO;

        public void SerializarListaProductos(List<Producto> listaProductos)
        {
            RUTA_ARCHIVO = Path.Combine(CARPETA_APP_DATA_LOCAL, NOMBRE_PRODUCTOS);

            try
            {
                VerificarDirectorio();
                VerificarArchivo();

                if (File.Exists(RUTA_ARCHIVO))
                {
                    string archivoJson = JsonConvert.SerializeObject(listaProductos, Formatting.Indented);

                    using (StreamWriter escritor = new StreamWriter(RUTA_ARCHIVO))
                    {
                        escritor.Write(archivoJson);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Error de E/S: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Error al serializar JSON: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error desconocido: {ex.Message}", ex);
            }
        }

        public void SerializarListaVentas(List<Venta> listaVentas)
        {
            RUTA_ARCHIVO = Path.Combine(CARPETA_APP_DATA_LOCAL, NOMBRE_VENTAS);

            try
            {
                VerificarDirectorio();
                VerificarArchivo();

                if (File.Exists(RUTA_ARCHIVO))
                {
                    string archivoJson = JsonConvert.SerializeObject(listaVentas, Formatting.Indented);

                    using (StreamWriter escritor = new StreamWriter(RUTA_ARCHIVO))
                    {
                        escritor.Write(archivoJson);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Error de E/S: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Error al serializar JSON: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error desconocido: {ex.Message}", ex);
            }
        }

        private void VerificarDirectorio()
        {
            if (!Directory.Exists(CARPETA_APP_DATA_LOCAL))
            {
                Directory.CreateDirectory(CARPETA_APP_DATA_LOCAL);
            }
        }

        private void VerificarArchivo()
        {
            if (!File.Exists(RUTA_ARCHIVO))
            {
                try
                {
                    // Crea el archivo y lo cierra inmediatamente
                    using (StreamWriter escritor = File.CreateText(RUTA_ARCHIVO)) { }
                }
                catch (IOException ex)
                {
                    throw new IOException($"Error al crear el archivo: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error desconocido: {ex.Message}", ex);
                }
            }
        }
    }
}
