using SGCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCM.Repository
{
    public class ConsultaRepository : GenericRepository<ConnectionStringEF, Consulta>
    {
        public IQueryable<Consulta> GetAllConsultaIncludeMedicoPaciente()
        {
            IQueryable<Consulta> query = Context.Set<Consulta>()
                                                .Include("Paciente")
                                                .Include("Medico");
            return query;
        }

        public Consulta GetConsultaByIdIncludeAll(string id)
        {
            var query = Context.Set<Consulta>()                
                               .Include("Paciente")
                               .Include("Medico")
                               .Include("Receitas")
                               .Include("Exames")
                               .Include("Historicos")
                               .Where(prop => prop.Id == id)
                               .FirstOrDefault();

            return query;
        }
    }
}