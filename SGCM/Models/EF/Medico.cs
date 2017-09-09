using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGCM.Models
{
    [MetadataType(typeof(MedicoMetadata))]
    public partial class Medico
    {
    }

    public class MedicoMetadata
    {
        [Required]
        public string Nome { get; set; }
    }
}