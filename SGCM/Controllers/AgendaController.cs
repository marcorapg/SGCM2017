using Microsoft.AspNet.Identity;
using SGCM.Models;
using SGCM.Repository;
using SGCM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGCM.Controllers
{
    public class AgendaController : Controller
    {
        protected AgendaRepository agendaRepository = new AgendaRepository();
        protected MedicoRepository medicoRepository = new MedicoRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var medico = medicoRepository.FindBy(prop => prop.UserId == userId).FirstOrDefault();

            if (medico == null)
                return HttpNotFound();

            AgendaViewModel agenda = new AgendaViewModel()
            {
                MedicoId = medico.Id,
                NomeMedico = medico.Nome
            };

            return View("Index", agenda);
        }

        public ActionResult BuscarItensAgenda(DateTime start, DateTime end, string medicoId)
        {
            List<Agenda> itensAgenda = agendaRepository.GetAll()
                                                       .Where(prop => prop.DataInicio >= start &&
                                                                      prop.DataFim <= end &&
                                                                      prop.MedicoId == medicoId)
                                                       .ToList();

            var itensEventoAgenda = new List<EventoModel>();

            foreach (var itemAgenda in itensAgenda)
            {
                itensEventoAgenda.Add( new EventoModel()
                {
                    id = itemAgenda.Id,
                    start = new DataHora(itemAgenda.DataInicio.Value).ToString(),
                    end = new DataHora(itemAgenda.DataFim.Value).ToString()
                });
            }

            return Json(itensEventoAgenda.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarEvento(EventoAgendaViewModel eventoAgenda)
        {
            Agenda agenda = new Agenda()
            {
                Id = Guid.NewGuid().ToString(),
                DataInicio = eventoAgenda.DataInicioEventoAgenda,
                DataFim = eventoAgenda.DataFimEventoAgenda,
                MedicoId = eventoAgenda.MedicoIdEventoAgenda
            };

            agendaRepository.Add(agenda);
            agendaRepository.Save();

            DataHora dataHoraInicio = new DataHora(eventoAgenda.DataInicioEventoAgenda);
            DataHora dataHoraFim = new DataHora(eventoAgenda.DataFimEventoAgenda);

            return Json(new { dataInicio = dataHoraInicio.ToString(), dataFim = dataHoraFim.ToString() });
        }

        [HttpPost]
        public ActionResult ExcluirItemAgenda(string id)
        {
            var agenda = agendaRepository.FindBy(prop => prop.Id == id).FirstOrDefault();

            agendaRepository.Delete(agenda);
            agendaRepository.Save();

            return Json(id);
        }
    }
}