using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Concrete
{
    public class CarRepository : Repository<Arac>, ICarRepository
    {
        public CarRepository(MalikindenDbContext context) : base(context)
        {

        }

        public async Task<Arac> GetCustomCar(int id)
        {
            return await _dbset.AsNoTracking().Include(x => x.Marka).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Arac>> GetCustomCarList()
        {
           return await _dbset.AsNoTracking().Include(x=>x.Marka).ToListAsync();
        }

        public async Task<List<Arac>> GetCustomCarList(Expression<Func<Arac, bool>> expression)
        {
            return await _dbset.Where(expression).AsNoTracking().Include(x => x.Marka).ToListAsync();
        }
    }
}
