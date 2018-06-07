using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultora.Entidad
{
    public class IniciativaFaseEntidad
    {
        public IniciativaEntidad Iniciativa { get; set; }
        public FaseProyectoEntidad Fase { get; set; }
        public decimal Horas { get; set; }

    }
}
