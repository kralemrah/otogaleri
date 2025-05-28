using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Rol : IEntity
	{
		public int Id { get ; set; }

		[StringLength(50)]
		public string Adi { get; set; }
	}
}
