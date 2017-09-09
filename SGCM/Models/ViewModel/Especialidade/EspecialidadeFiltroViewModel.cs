using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SGCM.Models
{
    public class EspecialidadeFiltroViewModel : ViewModelBase
    {
        public string Descricao { get; set; }

        public RouteValueDictionary RouteValues
        {
            get
            {
                RouteValueDictionary _routeValues = new RouteValueDictionary();

                if (!string.IsNullOrEmpty(Descricao))
                    _routeValues.Add("Descricao", Descricao);

                return _routeValues;
            }
        }
    }
}