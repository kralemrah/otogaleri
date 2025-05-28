using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class AracKriter : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Yakıt Tüketimi (Lt/100km)")]
        public decimal YakitTuketimi { get; set; }

        [Required]
        [Display(Name = "Segment")]
        public string Segment { get; set; }

        [Required]
        [Display(Name = "Vites Tipi")]
        public string VitesTipi { get; set; }

        [Required]
        [Display(Name = "Motor Hacmi")]
        public decimal MotorHacmi { get; set; }

        [Required]
        [Display(Name = "Beygir Gücü")]
        public int BeygirGucu { get; set; }

        [Required]
        [Display(Name = "Yakıt Tipi")]
        public string YakitTipi { get; set; }

        public int AracId { get; set; }
        public virtual Arac Arac { get; set; }
    }
} 