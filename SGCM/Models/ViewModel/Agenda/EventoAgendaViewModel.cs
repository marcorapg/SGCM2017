using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCM.Models
{
    public class EventoAgendaViewModel
    {
        public string Id { get; set; }

        public string MedicoIdEventoAgenda { get; set; }

        public DateTime DataInicioEventoAgenda { get; set; }
        public DateTime DataFimEventoAgenda { get; set; }
    }
}