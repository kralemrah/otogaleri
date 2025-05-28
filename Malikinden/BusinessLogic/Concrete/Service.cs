using BusinessLogic.Abstract;
using Data;
using Data.Abstract;
using Data.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
	public class Service<T> : Repository<T>,IService<T> where T : class, IEntity, new()
	{
		public Service(MalikindenDbContext context) : base(context)
		{

		}
	}
}
