using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class VisitorViewModel
    {
        public int Id { get; set; }

        [Remote(action: "HasProductName", controller: "Product")]
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz!")]
        [StringLength(50, ErrorMessage = "İsim alanına en fazla 50 karakter girilebilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz!")]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "Açıklama alanı en az 50 en fazla 300 karakter girilebilir.")]
        public string Comment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
