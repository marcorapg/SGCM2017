using Microsoft.AspNet.Identity;
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

        public ActionResult IncluirReceita(string consultaId, string pacienteId)
        {
            Receita receita = new Receita()
            {
                ConsultaId = consultaId,
                PacienteId = pacienteId
            };

            return View("Receita", receita);
        }

        public ActionResult EditarReceita(string receitaId)
        {
            Receita receita = receitas.FindBy(prop => prop.Id == receitaId).FirstOrDefault();

            if (receita == null)
                return HttpNotFound();

            return View("Receita", receita);
        }

        public ActionResult SalvarReceita(Receita receita, string medicamentoId, string observacao)
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

            return RedirectToAction("EditarReceita", "Receita", new { receitaId = receita.Id } );
        }
    }
}