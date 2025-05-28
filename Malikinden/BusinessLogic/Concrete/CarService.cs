using BusinessLogic.Abstract;
using Data;
using Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    public class CarService : CarRepository , ICarService
    {
        public CarService(MalikindenDbContext context) : base(context)
        {
        }
    }
}
