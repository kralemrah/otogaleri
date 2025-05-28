using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Slider :IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string? Baslik { get; set; }

        [Display(Name = "Açıklama")]
        public string? Acıklama { get; set; }

        [Display(Name = "Resmi")]
        public string? Resim { get; set; }

        [Display(Name = "Linki")]
        public string? Link { get; set; }

    }
}
