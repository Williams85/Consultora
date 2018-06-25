using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public partial class IniciativaEntidad
    {
        public int Cod_Iniciativa { get; set; }
        public string Nom_Iniciativa { get; set; }
        public string Des_Iniciativa { get; set; }
        public string Cod_Oportunidad { get; set; }
        public NegocioEntidad Negocio { get; set; }
        public ServicioEntidad  Servicio { get; set; }
        public ClienteEntidad   Cliente{ get; set; }
        public string RFP { get; set; }
        public string Propuesta_Tecnica { get; set; }
        public string Correo_Propuesta_Tecnica { get; set; }
        public string Aceptacion_Propuesta_Tecnica { get; set; }
        public UsuarioEntidad ResponsableServicio { get; set; }
        public UsuarioEntidad ConsultorLider { get; set; }
        public byte Estado_Iniciativa { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public bool Rentable { get; set; }
        public decimal CostoTotalEquipo { get; set; }
        public decimal CostoTotalServicio { get; set; }
        public decimal CostoTotalCliente { get; set; }
        public decimal GananciaBruta { get; set; }
        public decimal MedidadServicio { get; set; }
        public string TamañoServicio { get; set; }
        public string Cod_Servicio_Generado { get; set; }
        public DateTime Fecha_Inicio_Servicio { get; set; }
        public DateTime Fecha_Fin_Servicio { get; set; }
        public string Motivo_Cancelacion { get; set; }
        public string Comentarios { get; set; }

    }
}
