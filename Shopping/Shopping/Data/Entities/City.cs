using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
	public class City
	{
		[Key]
		public int IdCity { get; set; }

		[Display(Name = "Cidade")]
		[MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string CityName { get; set; }

		public State State { get; set; }

	}
}
