//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGCM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exame
    {
        public string Id { get; set; }
        public string TipoExameId { get; set; }
        public string PacienteId { get; set; }
        public Nullable<SGCM.Models.SituacaoExame> Situacao { get; set; }
        public string MedicoId { get; set; }
    
        public virtual Paciente Paciente { get; set; }
        public virtual TipoExame TipoExame { get; set; }
        public virtual Medico Medico { get; set; }
    }
}
