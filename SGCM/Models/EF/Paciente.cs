using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGCM.Models
{
    [MetadataType(typeof(ConsultaMetadata))]
    public partial class Consulta
    {
        public string Data
        {
            get
            {
                return DataInicio.Value.ToShortDateString();
            }
        }

        public string HoraInicio
        {
            get
            {
                return DataInicio.Value.ToShortTimeString();
            }
        }

        public string HoraFim
        {
            get
            {
                return DataFim.Value.ToShortTimeString();
            }
        }
    }

    public class ConsultaMetadata
    {
        [Display(Name = "Hora início")]
        public string HoraInicio;

        [Display(Name = "Hora fim")]
        public string HoraFim;

        [Display(Name = "Situação")]
        public SituacaoConsulta Situacao;
    }
}