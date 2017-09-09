using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SGCM.Models;
using PagedList;
using SGCM.Repository;

namespace SGCM.Controllers
{
    public class EspecialidadesController : SGCMControllerBase
    {
        private EspecialidadeRepository especialidades = new EspecialidadeRepository();

        public ActionResult Index()
        {
            EspecialidadeFiltroViewModel modelo = new EspecialidadeFiltroViewModel();

            return View(modelo);
        }

        public ActionResult ListarEspecialidades(EspecialidadeFiltroViewModel filtros)
        {
            var listaEspecialidades = especialidades.GetAll()
                                                    .Where(prop => (string.IsNullOrEmpty(filtros.Descricao) || prop.Descricao.Contains(filtros.Descricao)))
                                                    .OrderBy(prop => prop.Id);

            ViewBag.RouteValues = filtros.RouteValues;

            if (listaEspecialidades.Count() == 0)
                return PartialView("_GridSemRegistros");

            return PartialView("_ListarEspecialidade", listaEspecialidades.ToPagedList(filtros.Pagina, ViewModelBase.NUMERO_ITENS_PAGINA));
        }

        public ActionResult Visualizar(string id)
        {
            Especialidade especialidade = especialidades.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(especialidade == null)
                return HttpNotFound();

            return View(especialidade);
        }

        public ActionResult Incluir()
        {
            Especialidade modelo = new Especialidade()
            {
                Id = Guid.NewGuid().ToString()
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                especialidades.Add(especialidade);
                especialidades.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Especialidade incluída com sucesso!");

                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        public ActionResult Alterar(string id)
        {
            Especialidade especialidade = especialidades.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (especialidade == null)
                return HttpNotFound();

            return View(especialidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                especialidades.Edit(especialidade);
                especialidades.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Especialidade alterada com sucesso!");

                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        public ActionResult Excluir(string id)
        {
            Especialidade especialidade = especialidades.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (especialidade == null)
                return HttpNotFound();

            return View(especialidade);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(string id)
        {
            var especialidade = especialidades.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(especialidade == null)
                return HttpNotFound();

            especialidades.Delete(especialidade);
            especialidades.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Especialidade excluída com sucesso!");

            return RedirectToAction("Index");
        }
    }
}