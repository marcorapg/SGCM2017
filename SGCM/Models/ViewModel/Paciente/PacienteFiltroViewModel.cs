﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SGCM.Models
{
    public class PacienteFiltroViewModel : ViewModelBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }

        public RouteValueDictionary RouteValues
        {
            get
            {
                RouteValueDictionary _routeValues = new RouteValueDictionary();

                if (!string.IsNullOrEmpty(Nome))
                    _routeValues.Add("Nome", Nome);

                if (!string.IsNullOrEmpty(CPF))
                    _routeValues.Add("CPF", CPF);

                if (DataNascimento.HasValue)
                    _routeValues.Add("DataNascimento", DataNascimento);

                return _routeValues;
            }
        }
    }
}