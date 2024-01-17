using Dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoDatosLectura
    {
        string CARPETA_APP_DATA_LOCAL = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string NOMBRE_PRODUCTOS = "inventario.json";
        string NOMBRE_VENTAS = "ventas.json";
        string RUTA_ARCHIVO_PRODUCTOS;
        string RUTA_ARCHIVO_VENTAS;

        public List<Producto> DeserializarListaProductos()
        {
            RUTA_ARCHIVO_PRODUCTOS = Path.Combine(CARPETA_APP_DATA_LOCAL, NOMBRE_PRODUCTOS);

            try
            {
                VerificarDirectorio();

                if (File.Exists(RUTA_ARCHIVO_PRODUCTOS))
                {
                    string archivoJson = File.ReadAllText(RUTA_ARCHIVO_PRODUCTOS);
                    List<Producto> listaDes = JsonConvert.DeserializeObject<List<Producto>>(archivoJson);
                    return listaDes;
                }
                else
                {
                    return new List<Producto>(); 
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Error de E/S: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Error al deserializar JSON: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error desconocido: {ex.Message}", ex);
            }
        }

        public List<Venta> DeserializarListaVentas()
        {
            RUTA_ARCHIVO_VENTAS = Path.Combine(CARPETA_APP_DATA_LOCAL, NOMBRE_VENTAS);

            try
            {
                VerificarDirectorio();

                if (File.Exists(RUTA_ARCHIVO_VENTAS))
                {
                    string archivoJson = File.ReadAllText(RUTA_ARCHIVO_VENTAS);
                    List<Venta> listaDes = JsonConvert.DeserializeObject<List<Venta>>(archivoJson);
                    return listaDes;
                }
                else
                {
                    return new List<Venta>();
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Error de E/S: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Error al deserializar JSON: {ex.Message}", ex);
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
    }
}
