using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCM.Models
{
    public class HomeMedicoViewModel
    {
        public HomeMedicoViewModel()
        {
            Medicos = new List<Medico>();
        }

        public List<Medico> Medicos { get; set; }
    }
}