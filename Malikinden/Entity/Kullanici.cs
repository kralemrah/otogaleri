using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Kullanici : IEntity
	{
		
		public int Id { get ; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "Ad")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "SoyAd")]
        public string Soyadi { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
		public string Email { get; set; }

        [Display(Name = "Telefon ")]
        [StringLength(20)]
		public string? Telefon{ get; set; }


		[StringLength(50)]
        [Display(Name = "Kullanıcı Adı")]
        public string? KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(500)]
        [Display(Name = "Şifre")]	
        public string Sifre { get; set; }

		[Display(Name = "Eklenme Tarihi" ), ScaffoldColumn(false)] //scaffoldcolumn kolonun ekranda gözükmemesini sağlar.
		public DateTime? EklenmeTarihi { get; set; }=DateTime.Now;
        public bool AktifMi { get; set; }

        [Display(Name = "Kullanıcı Rolü")]
        public int RolId { get; set; }
        public virtual Rol? Rol { get; set; }
        

    }
}
