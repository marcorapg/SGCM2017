using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGCM.Models
{
    [MetadataType(typeof(ReceitaMetadata))]
    public partial class Receita
    {
        public string Descricao
        {
            get
            {
                string descricao = string.Empty;

                foreach (var itemReceita in this.ItemReceita)
                {
                    descricao += itemReceita.Medicamento.NomeGenerico + " - ";
                }

                if (descricao.Length > 4)
                    descricao.Remove(descricao.Length - 4);

                return descricao;
            }
        }
    }

    public class ReceitaMetadata
    {
    }
}