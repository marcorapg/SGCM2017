using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SGCM.Models
{
    public class ViewModelBase
    {
        public static int NUMERO_ITENS_PAGINA = 5;

        private int _pagina { get; set; }
        public int Pagina
        {
            get
            {
                if (_pagina == 0)
                    return 1;

                return _pagina;
            }

            set
            {
                _pagina = value;
            }
        }

        public static RouteValueDictionary RouteValuesPagina(RouteValueDictionary routeValues, int pagina)
        {
            if (routeValues.ContainsKey("Pagina"))
                routeValues.Remove("Pagina");

            routeValues.Add("Pagina", pagina);

            return routeValues;
        }

        public static int CalculaIndiceRegistro(int indiceRegistro, int numeroPagina)
        {
            return (indiceRegistro + 1) + ((numeroPagina - 1) * NUMERO_ITENS_PAGINA);
        }
    }
}