using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entity
{
	public class Satis : IEntity
	{
		public int Id { get ; set ; }

      
        [Display(Name = "Araç")]
        public int AracId { get; set; }
     
        [Display(Name = "Müşteri")]
        public int MusteriId { get; set; }
		public int SatisFiyati { get; set;}
        public DateTime? SatisTarihi { get; set; }=DateTime.Now;
        public virtual Arac? Arac  { get; set; }
        public virtual Musteri? Musteri { get; set; }
    }
}
