using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Arac : IEntity
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Model alanı zorunludur")]
		[StringLength(50)]

		public string Model { get; set; }

		[Required(ErrorMessage = "Model yılı zorunludur")]

		public int ModelYili { get; set; }

		[Required(ErrorMessage = "Fiyat alanı zorunludur")]

		public decimal Fiyat { get; set; }

		[Required(ErrorMessage = "Kilometre alanı zorunludur")]

		public int Kilometre { get; set; }

		[Required(ErrorMessage = "Kasa tipi zorunludur")]

		public string KasaTipi { get; set; }

		[Required(ErrorMessage = "Yakıt tipi zorunludur")]

		public string YakitTipi { get; set; }

		[Required(ErrorMessage = "Vites tipi zorunludur")]

		public string VitesTipi { get; set; }

		[Required(ErrorMessage = "Motor hacmi zorunludur")]

		public double MotorHacmi { get; set; }

		[Required(ErrorMessage = "Beygir gücü zorunludur")]

		public int BeygirGucu { get; set; }

		[Required(ErrorMessage = "Yakıt tüketimi zorunludur")]

		public double YakitTuketimi { get; set; }

		[Required(ErrorMessage = "Renk alanı zorunludur")]
		
		public string Renk { get; set; }

		public string? Notlar { get; set; }

		public string? Resim1 { get; set; }
		public string? Resim2 { get; set; }
		public string? Resim3 { get; set; }

		public bool SatistaMi { get; set; }
		public bool Anasayfa { get; set; }
		public DateTime EklenmeTarihi { get; set; }

		public int MarkaId { get; set; }
		public virtual Marka? Marka { get; set; }
	}
}
