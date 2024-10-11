using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Controllers;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {

        public int Id { get; set; }


        [Remote(action:"HasProductName", controller:"Product")]
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz!")]
        [StringLength(50, ErrorMessage = "İsim alanına en fazla 50 karakter girilebilir.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş bırakılamaz!")]
    //    [RegularExpression (@"/ ^([1 - 9][0 - 9]{, 2}
    //(,[0 - 9]{3})*| [0 - 9] +)(\.[0 - 9]{ 1,9})?$/" , ErrorMessage = "Fiyat alanında virgülden sonra en fazla 2 basamak olmalıdır. Örn: 999,99")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stok alanı boş bırakılamaz!")]
        [Range (0,200, ErrorMessage = "Stok 200 adetten fazla olamaz")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz!")]
        [StringLength(300, MinimumLength = 50,  ErrorMessage = "Açıklama alanı en az 50 en fazla 300 karakter girilebilir.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Renk alanı boş bırakılamaz!")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Yayınlanma Tarihi boş bırakılamaz!")]
        public DateTime? PublishDate { get; set; } 
        public bool isPublish { get; set; }


        [Required(ErrorMessage = "Lütfen bir geçerlilik süresi seçin.")]
        public int Expire { get; set; }



        // E-mail address validation

        //[EmailAddress (ErrorMessage = "Email adresi uygun formatta değil")]
        //public string Email { get; set; }  

        // RegularExpression özelleştirilmiş kısıtlamalar için kullanılır  

    }
}
