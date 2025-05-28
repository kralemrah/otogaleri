using System.ComponentModel.DataAnnotations;

namespace Malikinden.Models
{
    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
    }
}
