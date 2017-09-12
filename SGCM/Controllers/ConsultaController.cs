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
    public class ConsultaController : SGCMControllerBase
    {
        protected ConsultaRepository consultaRepository = new ConsultaRepository();
        protected AgendaRepository agendaRepository = new AgendaRepository();
        protected MedicoRepository medicoRepository = new MedicoRepository();

        public ActionResult MarcarConsulta()
        {
            var userId = User.Identity.GetUserId();
            var medico = medicoRepository.FindBy(prop => prop.UserId == userId).FirstOrDefault();

            if (medico == null)
                return HttpNotFound();

            MarcarConsultaViewModel consulta = new MarcarConsultaViewModel()
            {
                MedicoId = medico.Id,
                NomeMedico = medico.Nome
            };

            return View("MarcarConsulta", consulta);
        }

        public ActionResult BuscarItensAgenda(DateTime start, DateTime end, string medicoId)
        {
            List<Agenda> itensAgenda = agendaRepository.GetAll()
                                                       .Where(prop => prop.DataInicio >= start &&
                                                                      prop.DataFim <= end &&
                                                                      prop.MedicoId == medicoId)
                                                       .ToList();

            List<Consulta> itensConsulta = consultaRepository.GetAllConsultaIncludeMedicoPaciente()
                                                             .Where(prop => prop.DataInicio >= start &&
                                                                            prop.DataFim <= end &&
                                                                            prop.MedicoId == medicoId)
                                                             .ToList();

            var itensEvento = new List<EventoModel>();

            foreach (var itemAgenda in itensAgenda)
            {
                itensEvento.Add(new EventoModel()
                {
                    id = itemAgenda.Id,
                    start = new DataHora(itemAgenda.DataInicio.Value).ToString(),
                    end = new DataHora(itemAgenda.DataFim.Value).ToString(),
                    rendering = "background"
                });
            }

            foreach (var itemConsulta in itensConsulta)
            {
                itensEvento.Add(new EventoModel()
                {
                    id = itemConsulta.Id,
                    title = itemConsulta.Paciente.Nome,
                    start = new DataHora(itemConsulta.DataInicio.Value).ToString(),
                    end = new DataHora(itemConsulta.DataFim.Value).ToString(),
                    color = "blue",
                    pacienteId = itemConsulta.PacienteId
                });
            }

            return Json(itensEvento.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarEvento(EventoConsultaViewModel eventoConsulta)
        {
            Consulta consulta = new Consulta()
            {
                Id = Guid.NewGuid().ToString(),
                DataInicio = eventoConsulta.DataInicioEventoAgenda,
                DataFim = eventoConsulta.DataFimEventoAgenda,
                MedicoId = eventoConsulta.MedicoIdEventoAgenda,
                PacienteId = eventoConsulta.PacienteIdEventoAgenda
            };

            consultaRepository.Add(consulta);
            consultaRepository.Save();

            return Json(true);
        }

        [HttpPost]
        public ActionResult ExcluirItemAgenda(string id)
        {
            var consulta = consultaRepository.FindBy(prop => prop.Id == id).FirstOrDefault();

            consultaRepository.Delete(consulta);
            consultaRepository.Save();

            return Json(id);
        }


        public ActionResult VisualizarConsultas()
        {
            var userId = User.Identity.GetUserId();
            var medico = medicoRepository.FindBy(prop => prop.UserId == userId).FirstOrDefault();

            if (medico == null)
                return HttpNotFound();

            MarcarConsultaViewModel consulta = new MarcarConsultaViewModel()
            {
                MedicoId = medico.Id,
                NomeMedico = medico.Nome
            };

            return View("VisualizarConsulta", consulta);
        }

        public ActionResult BuscarItensVisualizarConsultas(DateTime start, DateTime end, string medicoId)
        {
            List<Agenda> itensAgenda = agendaRepository.GetAll()
                                                       .Where(prop => prop.DataInicio >= start &&
                                                                      prop.DataFim <= end &&
                                                                      prop.MedicoId == medicoId)
                                                       .ToList();

            List<Consulta> itensConsulta = consultaRepository.GetAllConsultaIncludeMedicoPaciente()
                                                             .Where(prop => prop.DataInicio >= start &&
                                                                            prop.DataFim <= end &&
                                                                            prop.MedicoId == medicoId)
                                                             .ToList();

            var itensEvento = new List<EventoModel>();

            foreach (var itemAgenda in itensAgenda)
            {
                itensEvento.Add(new EventoModel()
                {
                    id = itemAgenda.Id,
                    start = new DataHora(itemAgenda.DataInicio.Value).ToString(),
                    end = new DataHora(itemAgenda.DataFim.Value).ToString(),
                    rendering = "background"
                });
            }

            foreach (var itemConsulta in itensConsulta)
            {
                string color = string.Empty, textColor = string.Empty;

                switch (itemConsulta.Situacao)
                {
                    case SituacaoConsulta.Pendente:
                        textColor = "white";
                        color = "blue";
                        break;

                    case SituacaoConsulta.Iniciada:
                        textColor = "black";
                        color = "yellow";
                        break;

                    case SituacaoConsulta.Finalizada:
                        textColor = "black";
                        color = "#41a931";
                        break;

                    case SituacaoConsulta.Cancelada:
                        textColor = "black";
                        color = "#52ecf3";
                        break;
                };

                itensEvento.Add(new EventoModel()
                {
                    id = itemConsulta.Id,
                    title = itemConsulta.Paciente.Nome,
                    start = new DataHora(itemConsulta.DataInicio.Value).ToString(),
                    end = new DataHora(itemConsulta.DataFim.Value).ToString(),
                    color = color,
                    pacienteId = itemConsulta.PacienteId,
                    url = Url.Action("Consulta", "Consulta", new { consultaId = itemConsulta.Id }),
                    textColor = textColor
                });
            }

            return Json(itensEvento.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Consulta(string consultaId)
        {
            var consulta = consultaRepository.GetConsultaByIdIncludeAll(consultaId);

            return View(consulta);
        }

        public ActionResult AlterarSituacaoConsulta(string id, SituacaoConsulta situacao)
        {
            var consulta = consultaRepository.GetConsultaByIdIncludeAll(id);

            consulta.Situacao = (situacao == SituacaoConsulta.Pendente) ? SituacaoConsulta.Iniciada : SituacaoConsulta.Finalizada;

            consultaRepository.Edit(consulta);
            consultaRepository.Save();

            if(consulta.Situacao == SituacaoConsulta.Iniciada)
                ConfiguraMensagem(TipoMensagem.Sucesso, "Consulta iniciada");
            else
                ConfiguraMensagem(TipoMensagem.Sucesso, "Consulta finalizada");

            return RedirectToAction("Consulta", new { consultaId = id });
        }
    }
}