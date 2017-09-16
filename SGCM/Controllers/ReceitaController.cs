using Microsoft.AspNet.Identity;
using PagedList;
using Rotativa;
using SGCM.Models;
using SGCM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGCM.Controllers
{
    public class ReceitaController : SGCMControllerBase
    {
        protected ReceitaRepository receitas = new ReceitaRepository();
        protected ItemReceitaRepository itensReceita = new ItemReceitaRepository();

        protected PacienteRepository pacientes = new PacienteRepository();
        protected MedicoRepository medicos = new MedicoRepository();

        public ActionResult Index()
        {
            ReceitaFiltroViewModel modelo = new ReceitaFiltroViewModel() { Data = DateTime.Now.Date };

            return View(modelo);
        }

        public ActionResult ListarReceitas(ReceitaFiltroViewModel filtros)
        {
            var listaReceitas = receitas.GetAll()
                                        .Where(prop => (!prop.Data.HasValue || prop.Data == filtros.Data) &&
                                                       (string.IsNullOrEmpty(filtros.MedicoId) || prop.MedicoId.Contains(filtros.MedicoId)) &&
                                                       (string.IsNullOrEmpty(filtros.PacienteId) || prop.PacienteId.Contains(filtros.PacienteId)))
                                        .OrderBy(prop => prop.Id);

            ViewBag.RouteValues = filtros.RouteValues;

            if (listaReceitas.Count() == 0)
                return PartialView("_GridSemRegistros");

            return PartialView("_ListarReceita", listaReceitas.ToPagedList(filtros.Pagina, ViewModelBase.NUMERO_ITENS_PAGINA));
        }

        public ActionResult Visualizar(string id)
        {
            Receita receita = receitas.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (receita == null)
                return HttpNotFound();

            return View(receita);
        }

        public ActionResult Incluir()
        {
            Receita modelo = new Receita()
            {
                Id = Guid.NewGuid().ToString()
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(Receita receita)
        {
            ModelState.Remove("Paciente");
            ModelState.Remove("Medico");

            if (ModelState.IsValid)
            {
                receitas.Add(receita);
                receitas.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Receita incluída com sucesso!");

                return RedirectToAction("Alterar", new { id = receita.Id });
            }

            receita.Paciente = pacientes.FindBy(prop => prop.Id == receita.PacienteId).FirstOrDefault();
            receita.Medico = medicos.FindBy(prop => prop.Id == receita.MedicoId).FirstOrDefault();

            return View(receita);
        }

        public ActionResult Alterar(string id)
        {
            Receita receita = receitas.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (receita == null)
                return HttpNotFound();

            return View(receita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Receita receita)
        {
            if (ModelState.IsValid)
            {
                receitas.Edit(receita);
                receitas.Save();

                ConfiguraMensagem(TipoMensagem.Sucesso, "Receita alterada com sucesso!");

                return RedirectToAction("Alterar", new { id = receita.Id });
            }

            return View(receita);
        }

        public ActionResult Excluir(string id)
        {
            Receita receita = receitas.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (receita == null)
                return HttpNotFound();

            return View(receita);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(string id)
        {
            Receita receita = receitas.FindBy(prop => prop.Id == id).FirstOrDefault();

            if (receita == null)
                return HttpNotFound();

            foreach (var itemReceita in receita.ItemReceita)
            {
                itensReceita.Delete(itensReceita.FindBy(prop => prop.Id == itemReceita.Id).FirstOrDefault());
            }

            receitas.Delete(receita);
            receitas.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Receita excluída com sucesso!");

            return RedirectToAction("Index");
        }

        public ActionResult Imprimir(string id)
        {
            Receita receita = receitas.FindBy(prop => prop.Id == id).FirstOrDefault();
            return new ViewAsPdf("Imprimir", receita);
            //return View("Imprimir", receita);
        }

        public ActionResult SalvarItemReceita(Receita receita, string medicamentoId, string observacao)
        {
            if (string.IsNullOrEmpty(receita.Id))
            {
                receita.Id = Guid.NewGuid().ToString();
                receitas.Add(receita);
                receitas.Save();
            }

            ItemReceita itemReceita = new ItemReceita()
            {
                Id = Guid.NewGuid().ToString(),
                ReceitaId = receita.Id,
                MedicamentoId = medicamentoId,
                Observacao = observacao
            };

            itensReceita.Add(itemReceita);
            itensReceita.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Item da receita incluído com sucesso!");

            return RedirectToAction("Alterar", "Receita", new { id = receita.Id });
        }

        public ActionResult ExcluirItemReceita(string id)
        {
            ItemReceita itemReceita = itensReceita.FindBy(prop => prop.Id == id).FirstOrDefault();

            string idReceita = itemReceita.Receita.Id;

            itensReceita.Delete(itemReceita);
            itensReceita.Save();

            ConfiguraMensagem(TipoMensagem.Sucesso, "Item da receita excluído com sucesso!");

            return RedirectToAction("Alterar", "Receita", new { id = idReceita });
        }
    }
}