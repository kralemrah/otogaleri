using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Marka : IEntity
	{
		public int Id { get ; set; }

		[StringLength(50)]
        [Required(ErrorMessage = "Burası boş geçilemez")]
        [Display(Name = "Marka Adı")]
        public string Adi { get; set; }
	}
}
