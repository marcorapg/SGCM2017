using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SGCM.Models
{
    public class MedicamentoFiltroViewModel : ViewModelBase
    {
        public string NomeGenerico { get; set; }
        public string NomeFabrica { get; set; }
        public string NomeFabricante { get; set; }

        public RouteValueDictionary RouteValues
        {
            get
            {
                RouteValueDictionary _routeValues = new RouteValueDictionary();

                if (!string.IsNullOrEmpty(NomeGenerico))
                    _routeValues.Add("NomeGenerico", NomeGenerico);

                if (!string.IsNullOrEmpty(NomeFabrica))
                    _routeValues.Add("NomeFabrica", NomeFabrica);

                if (!string.IsNullOrEmpty(NomeFabricante))
                    _routeValues.Add("NomeFabricante", NomeFabricante);

                return _routeValues;
            }
        }
    }
}