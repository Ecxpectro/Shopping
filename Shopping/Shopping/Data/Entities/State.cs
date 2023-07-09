using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shopping.Data.Entities
{
	public class State
	{
		[Key]
		public int IdState { get; set; }

		[Display(Name = "Estado")]
		[MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string StateName { get; set; }
		[JsonIgnore]
		public Country Country { get; set; }
		public ICollection<City> Cities { get; set; }
		[Display(Name = "Cidades")]
		public int CitiesNumber => Cities == null ? 0 : Cities.Count;
	}
}
