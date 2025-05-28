using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Musteri : IEntity
    {
        public Musteri()
        {
            Opsiyonlar = new List<Opsiyon>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [Display(Name = "Araç No")]
        public int AracId { get; set; }
        public int TalepId { get; set; }


        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "Adı")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "Soyadı")]
        public string Soyadi { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "T.C. No ")]
        public string? TcNo { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        [StringLength(50)]
        [Display(Name = "Telefon Numarası")]
        public string TelNo { get; set; }

        [Required(ErrorMessage = "Burası boş geçilemez")]
        public string Email { get; set; }
        public string? Adres { get; set; }
        public string? Notlar { get; set; } 
        public virtual Arac? Arac { get; set; }

        public virtual Talep? Talep { get; set; }
        public virtual ICollection<Opsiyon> Opsiyonlar { get; set; } // Navigasyon özelliği


    }
}
