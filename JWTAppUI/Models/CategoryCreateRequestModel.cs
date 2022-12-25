using System.ComponentModel.DataAnnotations;

namespace JWTAppUI.Models
{
    public class CategoryCreateRequestModel
    {
        [Required(ErrorMessage = "Kategori Adı Boş Bırakılmaz")]
        public string Definition { get; set; }

    }
}
