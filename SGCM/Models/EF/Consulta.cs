using System;
using System.ComponentModel.DataAnnotations;

namespace SGCM.Models
{
    [MetadataType(typeof(PacienteMetadata))]
    public partial class Paciente
    {
    }

    public class PacienteMetadata
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome;

        [Display(Name = "CPF")]
        public string CPF;

        [Display(Name = "Número identidade")]
        public string NumeroIdentidade;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de nascimento")]
        public System.DateTime DataNascimento { get; set; }

        [Display(Name = "Sexo")]
        public bool? Sexo { get; set; }

        [Display(Name = "Telefone fixo")]
        public string TelefoneFixo { get; set; }

        [Display(Name = "Telefone celular")]
        public string TelefoneCelular { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }
    }
}