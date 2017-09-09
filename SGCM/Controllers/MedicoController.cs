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
    public class MedicoController : SGCMControllerBase
    {
        private MedicoRepository medicos = new MedicoRepository();
        private EspecialidadeRepository especilidades = new EspecialidadeRepository();
        private UsuarioRepository usuarios = new UsuarioRepository();

        public ActionResult Index()
        {
            MedicoFiltroViewModel modelo = new MedicoFiltroViewModel();

            return View(modelo);
        }

        public ActionResult ListarMedicos(MedicoFiltroViewModel filtros)
        {
            var listaMedicos = medicos.GetAll()
                                      .Where(prop => (string.IsNullOrEmpty(filtros.Nome) || prop.Nome.Contains(filtros.Nome)) &&
                                                     (string.IsNullOrEmpty(filtros.CPF) || prop.CPF.Contains(filtros.CPF)) &&
                                                     (string.IsNullOrEmpty(filtros.CRM) || prop.CRM.Contains(filtros.CRM)))
                                      .OrderBy(prop => prop.Id);

            ViewBag.RouteValues = filtros.RouteValues;

            if (listaMedicos.Count() == 0)
                return PartialView("_GridSemRegistros");

            return PartialView("_ListarMedico", listaMedicos.ToPagedList(filtros.Pagina, ViewModelBase.NUMERO_ITENS_PAGINA));
        }

        public ActionResult Visualizar(string id)
        {
            Medico medico = medicos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(medico == null)
                return HttpNotFound();

            return View(medico);
        }

        public ActionResult Incluir()
        {
            Medico modelo = new Medico()
            {
                Id = Guid.NewGuid().ToString()
            };

            ViewBag.SelectListEspecialidade = especilidades.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Descricao }).ToList();
            ViewBag.SelectListUsuario = usuarios.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Email }).ToList();

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Medico medico)
        {
            if (ModelState.IsValid)
            {
                medicos.Add(medico);
                medicos.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Médico incluído com sucesso!");

                return RedirectToAction("Alterar", new { id = medico.Id });
            }

            ViewBag.SelectListEspecialidade = especilidades.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Descricao }).ToList();
            ViewBag.SelectListUsuario = usuarios.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Email }).ToList();

            return View(medico);
        }

        public ActionResult Alterar(string id)
        {
            Medico medico = medicos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (medico == null)
                return HttpNotFound();

            ViewBag.SelectListEspecialidade = especilidades.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Descricao }).ToList();
            ViewBag.SelectListUsuario = usuarios.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Email }).ToList();

            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Medico medico)
        {
            if (ModelState.IsValid)
            {
                medicos.Edit(medico);
                medicos.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Médico alterado com sucesso!");

                return RedirectToAction("Alterar", new { id = medico.Id });
            }

            ViewBag.SelectListEspecialidade = especilidades.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Descricao }).ToList();
            ViewBag.SelectListUsuario = usuarios.GetAll().Select(prop => new SelectListItem() { Value = prop.Id, Text = prop.Email }).ToList();

            return View(medico);
        }

        public ActionResult Excluir(string id)
        {
            Medico medico = medicos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (medico == null)
                return HttpNotFound();

            return View(medico);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(string id)
        {
            Medico medico = medicos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(medico == null)
                return HttpNotFound();

            medicos.Delete(medico);
            medicos.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Médico excluído com sucesso!");

            return RedirectToAction("Index");
        }
    }
}