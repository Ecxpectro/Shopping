using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }

        [Display(Name = "Categoria")]
        [MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string CategoryName { get; set; }
    }
}
