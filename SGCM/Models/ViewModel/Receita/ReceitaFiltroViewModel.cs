using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SGCM.Models
{
    public class ReceitaFiltroViewModel : ViewModelBase
    {
        public DateTime? Data { get; set; }
        public string PacienteId { get; set; }
        public string MedicoId { get; set; }

        public RouteValueDictionary RouteValues
        {
            get
            {
                RouteValueDictionary _routeValues = new RouteValueDictionary();

                if (Data.HasValue)
                    _routeValues.Add("Data", Data);

                if (!string.IsNullOrEmpty(PacienteId))
                    _routeValues.Add("PacienteId", PacienteId);

                if (!string.IsNullOrEmpty(MedicoId))
                    _routeValues.Add("MedicoId", MedicoId);

                return _routeValues;
            }
        }
    }
}