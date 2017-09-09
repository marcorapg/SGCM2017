using System.ComponentModel.DataAnnotations;

namespace SGCM.Models
{
    [MetadataType(typeof(EspecialidadeMetadata))]
    public partial class Especialidade
    {
    }

    public class EspecialidadeMetadata
    {
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao;
    }
}