using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }

        [Display(Name = "País")]
        [MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string CountryName { get; set; }
        public ICollection<State> States { get; set; }
		[Display(Name = "Estados")]
		public int StatesNumber => States == null ? 0 : States.Count;
    }
}