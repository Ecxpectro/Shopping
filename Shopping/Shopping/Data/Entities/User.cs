using Microsoft.AspNetCore.Identity;
using Shopping.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Document { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        [MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string LastName { get; set; }

        [Display(Name = "Cidade")]
        public City City { get; set; }

        [Display(Name = "Endereço")]
        [MaxLength(200, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7166/images/noimage.png"
            : $"https://shopping1.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }

}
