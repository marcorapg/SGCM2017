using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCM.Models
{
    public class ConsultaViewModel
    {
        public string Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }

        public string NomePaciente { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public string MedicoId { get; set; }
        public string NomeMedico { get; set; }
    }
}