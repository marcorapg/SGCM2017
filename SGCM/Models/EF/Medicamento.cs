using System;
using System.ComponentModel.DataAnnotations;

namespace SGCM.Models
{
    [MetadataType(typeof(MedicamentoMetadata))]
    public partial class Medicamento
    {
        public string Descricao
        {
            get
            {
                return string.Format("{0} - {1} - {2}", this.NomeGenerico, this.NomeFabrica, this.NomeFabricante);
            }
        }
    }

    public class MedicamentoMetadata
    {
        [Required]
        [Display(Name = "Nome genérico")]
        public string NomeGenerico;

        [Required]
        [Display(Name = "Nome de fábrica")]
        public string NomeFabrica;

        [Required]
        [Display(Name = "Nome do fabricante")]
        public string NomeFabricante;
    }
}