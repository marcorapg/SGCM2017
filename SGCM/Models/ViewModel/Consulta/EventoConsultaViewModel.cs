using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCM.Models
{
    public class EventoConsultaViewModel
    {
        public string Id { get; set; }

        public string MedicoIdEventoAgenda { get; set; }
        public string PacienteIdEventoAgenda { get; set; }

        public DateTime DataInicioEventoAgenda { get; set; }
        public DateTime DataFimEventoAgenda { get; set; }
    }
}