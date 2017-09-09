using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGCM.Util
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                bool authorized = AuthorizeCore(filterContext.HttpContext);

                if (!authorized)
                    filterContext.Result = new RedirectResult("/Erro/AcessoNegado");
                else 
                    base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}