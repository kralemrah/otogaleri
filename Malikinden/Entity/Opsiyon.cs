using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Opsiyon : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Araç")]
        public int AracId { get; set; }

        [Display(Name = "Müşteri")]
        public int MusteriId { get; set; }
        public int OpsiyonFiyati { get; set; }
        public DateTime? OpsiyonTarihi { get; set; } = DateTime.Now;
        public DateTime? OpsiyonBitisTarihi { get; set; }
        public virtual Arac? Arac { get; set; }
        public virtual Musteri Musteri { get; set; }
        public bool EpostaGonderildiMi { get; set; }
    }
}
