using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Shopping.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Document { get; set; }

        [Display(Name = "Nomes")]
        [MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenomes")]
        [MaxLength(50, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string LastName { get; set; }

        [Display(Name = "Endereço")]
        [MaxLength(200, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Address { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "O campo {0} tem o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7057/images/noimage.png"
            : $"https://shopping1.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "País")]
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar um país.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar um estado.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int StateId { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "Ciuadad")]
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar uma cidade.")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

    }
}
