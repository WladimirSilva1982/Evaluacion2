using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViajesEtechEval.Web.Models.Entities
{
    public class ViajeDisponible
    {
        public string CodViaje { get; set; }

        public int NroPlazas { get; set; }

        public string LugarDestino { get; set; }

        public string LugarOrigen { get; set; }

        public decimal Precio { get; set; }
    }
}
