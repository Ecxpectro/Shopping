using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Deve inserir um email válido.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MinLength(6, ErrorMessage = "O campo {0} deve ter pelo menos {1} carácteres.")]
        public string Password { get; set; }

        [Display(Name = "Manter Login")]
        public bool RememberMe { get; set; }

    }
}
