using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGCM.Util;
using SGCM.Models;

namespace SGCM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ConnectionStringEF db = new ConnectionStringEF();

        public ActionResult Index()
        {
            if(this.User.IsInRole("administrador"))
                return HomeAdministrador();

            if (this.User.IsInRole("medico"))
                return HomeMedico();

            if (this.User.IsInRole("secretario"))
                return HomeSecretario();

            return RedirectToAction("AcessoNegado", "Erro");
        }

        [CustomAuthorization(Roles = "administrador")]
        public ActionResult HomeAdministrador()
        {
            HomeAdministradorViewModel modelo = new HomeAdministradorViewModel();

            return View("HomeAdministrador", modelo);
        }

        [CustomAuthorization(Roles = "medico")]
        public ActionResult HomeMedico()
        {
            HomeMedicoViewModel modelo = new HomeMedicoViewModel()
            {
                Medicos = db.Medico.ToList()
            };

            return View("HomeMedico", modelo);
        }

        [CustomAuthorization(Roles = "secretario")]
        public ActionResult HomeSecretario()
        {
            HomeSecretarioViewModel modelo = new HomeSecretarioViewModel();

            return View("HomeSecretario", modelo);
        }
    }
}