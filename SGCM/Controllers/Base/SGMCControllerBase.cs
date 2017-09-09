using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGCM.Models;

namespace SGCM.Controllers
{
    public class SGCMControllerBase : Controller
    {
        public void ConfiguraMensagem(TipoMensagem tipoMensagem, string textoMensagem)
        {
            TempData["Mensagem"] = new Mensagem()
            {
                TipoMensagem = tipoMensagem,
                TextoMensagem = textoMensagem
            };
        }
    }
}