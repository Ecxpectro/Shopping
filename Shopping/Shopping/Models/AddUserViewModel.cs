using Shopping.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Shopping.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Deve inserir um email válido.")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter pelo menos {1} carácteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter pelo menos {1} carácteres.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas são diferentes.")]
        [Display(Name = "Confirmação de senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter pelo menos entre {2} e {1} carácteres.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

    }
}
