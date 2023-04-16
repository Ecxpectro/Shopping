using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
	public class StateViewModel
	{
		public int IdState { get; set; }

		[Display(Name = "Estado")]
		[MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string StateName { get; set; }
		public int IdCountry { get; set; }
	}
}
