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
    public class PacienteController : SGCMControllerBase
    {
        private PacienteRepository pacientes = new PacienteRepository();

        public ActionResult Index()
        {
            PacienteFiltroViewModel modelo = new PacienteFiltroViewModel();

            return View(modelo);
        }

        public ActionResult ListarPacientes(PacienteFiltroViewModel filtros)
        {
            var listaPacientes = pacientes.GetAll()
                                          .Where(prop => (string.IsNullOrEmpty(filtros.Nome) || prop.Nome.Contains(filtros.Nome)) &&
                                                         (string.IsNullOrEmpty(filtros.CPF) || prop.CPF.Equals(filtros.CPF)) &&
                                                         (!filtros.DataNascimento.HasValue || prop.DataNascimento == filtros.DataNascimento))
                                          .OrderBy(prop => prop.Id);

            ViewBag.RouteValues = filtros.RouteValues;

            if (listaPacientes.Count() == 0)
                return PartialView("_GridSemRegistros");

            return PartialView("_ListarPaciente", listaPacientes.ToPagedList(filtros.Pagina, ViewModelBase.NUMERO_ITENS_PAGINA));
        }

        public ActionResult Visualizar(string id)
        {
            Paciente paciente = pacientes.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        public ActionResult Incluir()
        {
            Paciente modelo = new Paciente()
            {
                Id = Guid.NewGuid().ToString(),
                DataNascimento = DateTime.Now.Date
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                pacientes.Add(paciente);
                pacientes.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Paciente incluído com sucesso!");

                return RedirectToAction("Alterar", new { id = paciente.Id });
            }

            return View(paciente);
        }

        public ActionResult Alterar(string id)
        {
            Paciente paciente = pacientes.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                pacientes.Edit(paciente);
                pacientes.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Paciente alterado com sucesso!");

                return RedirectToAction("Alterar", new { id = paciente.Id });
            }

            return View(paciente);
        }

        public ActionResult Excluir(string id)
        {
            Paciente paciente = pacientes.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (paciente == null)
                return HttpNotFound();

            return View(paciente);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(string id)
        {
            Paciente paciente = pacientes.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(paciente == null)
                return HttpNotFound();

            pacientes.Delete(paciente);
            pacientes.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Paciente excluído com sucesso!");

            return RedirectToAction("Index");
        }

        public ActionResult BuscarPacienteAutocomplete(string term)
        {
            var listaPacientes = pacientes.FindBy(prop => prop.Nome.ToUpper().StartsWith(term.ToUpper()))
                                          .Select(prop => new { id = prop.Id, label = prop.Nome} )
                                          .ToArray();

            return Json(listaPacientes, JsonRequestBehavior.AllowGet);
        }
    }
}