using System.ComponentModel.DataAnnotations;

namespace AppQuinto.Models
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public int IdCli { get;  set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]

        public string NomeCli { get; set; }


        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        public string Tel { get; set;  }


        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "O campo nascimento é obrigatório")]
        [DataType(DataType.DateTime)]

        public DateTime DateNasc { get; set; }
    }
}
