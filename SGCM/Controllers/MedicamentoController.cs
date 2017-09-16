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
    public class MedicamentoController : SGCMControllerBase
    {
        private MedicamentoRepository medicamentos = new MedicamentoRepository();

        public ActionResult Index()
        {
            MedicamentoFiltroViewModel modelo = new MedicamentoFiltroViewModel();

            return View(modelo);
        }

        public ActionResult ListarMedicamentos(MedicamentoFiltroViewModel filtros)
        {
            var listaMedicamentos = medicamentos.GetAll()
                                          .Where(prop => (string.IsNullOrEmpty(filtros.NomeGenerico) || prop.NomeGenerico.Contains(filtros.NomeGenerico)) &&
                                                         (string.IsNullOrEmpty(filtros.NomeFabrica) || prop.NomeFabrica.Contains(filtros.NomeFabrica)) &&
                                                         (string.IsNullOrEmpty(filtros.NomeFabricante) || prop.NomeFabricante.Contains(filtros.NomeFabricante)))
                                          .OrderBy(prop => prop.Id);

            ViewBag.RouteValues = filtros.RouteValues;

            if (listaMedicamentos.Count() == 0)
                return PartialView("_GridSemRegistros");

            return PartialView("_ListarMedicamento", listaMedicamentos.ToPagedList(filtros.Pagina, ViewModelBase.NUMERO_ITENS_PAGINA));
        }

        public ActionResult Visualizar(string id)
        {
            Medicamento medicamento = medicamentos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(medicamento == null)
                return HttpNotFound();

            return View(medicamento);
        }

        public ActionResult Incluir()
        {
            Medicamento modelo = new Medicamento()
            {
                Id = Guid.NewGuid().ToString()
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                medicamentos.Add(medicamento);
                medicamentos.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Medicamento incluído com sucesso!");

                return RedirectToAction("Alterar", new { id = medicamento.Id });
            }

            return View(medicamento);
        }

        public ActionResult Alterar(string id)
        {
            Medicamento medicamento = medicamentos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (medicamento == null)
                return HttpNotFound();

            return View(medicamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                medicamentos.Edit(medicamento);
                medicamentos.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Medicamento alterado com sucesso!");

                return RedirectToAction("Alterar", new { id = medicamento.Id });
            }

            return View(medicamento);
        }

        public ActionResult Excluir(string id)
        {
            Medicamento medicamento = medicamentos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (medicamento == null)
                return HttpNotFound();

            return View(medicamento);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(string id)
        {
            Medicamento medicamento = medicamentos.FindBy(prop => prop.Id == id).FirstOrDefault();

            if(medicamento == null)
                return HttpNotFound();

            medicamentos.Delete(medicamento);
            medicamentos.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Medicamento excluído com sucesso!");

            return RedirectToAction("Index");
        }

        public ActionResult BuscarMedicamentoAutocomplete(string term)
        {
            var listaMedicamentos = medicamentos.FindBy(prop => prop.NomeGenerico.ToUpper().StartsWith(term.ToUpper()))
                                                .AsEnumerable()
                                                .Select(prop => new { id = prop.Id, label = string.Format("{0} - {1} - {2}", prop.NomeGenerico, prop.NomeFabrica, prop.NomeFabricante) })
                                                .ToArray();

            return Json(listaMedicamentos, JsonRequestBehavior.AllowGet);
        }
    }
}