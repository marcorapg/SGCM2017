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
    
    public partial class ItemReceita
    {
        public string Id { get; set; }
        public string ReceitaId { get; set; }
        public string MedicamentoId { get; set; }
        public string Observacao { get; set; }
    
        public virtual Medicamento Medicamento { get; set; }
        public virtual Receita Receita { get; set; }
    }
}
