using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public class IniciativaEntidad
    {
        public int Cod_Iniciativa { get; set; }
        public bool Rentable { get; set; }
        public decimal CostoTotalEquipo { get; set; }
        public decimal CostoTotalServicio { get; set; }
        public decimal CostoTotalCliente { get; set; }
        public decimal GananciaBruta { get; set; }
        public decimal MedidadServicio { get; set; }
        public string TamañoServicio { get; set; }


    }
}
