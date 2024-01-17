using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalPagar { get; set; }
        public DateTimeOffset Fecha { get; set; }

    }
}
