using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{
		internal MalikindenDbContext _context;
		internal DbSet<T> _dbset;
	

		public Repository (MalikindenDbContext context)
		{
			_context = context;
			_dbset=_context.Set<T>();
		}

		public void Add(T entity)
		{
			_dbset.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			await _dbset.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_dbset.Remove(entity);
		}

		public T Find(int id)
		{
			return _dbset.Find(id);
		}

		public async Task<T> FindAsync(int id)
		{
			return await _dbset.FindAsync(id);
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return _dbset.FirstOrDefault(expression);
		}

		public List<T> GetAll()
		{
			return _dbset.ToList();
		}

		public List<T> GetAll(Expression<Func<T, bool>> expression)
		{
			return _dbset.Where(expression).ToList();
		}

        public List<T> GetAll(Kullanici kullanici)
        {
            return _dbset.ToList();
        }

        public async Task<List<T>> GetAllAsync()
		{
			return await _dbset.ToListAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbset.Where(expression).ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbset.FirstOrDefaultAsync(expression);
		}

		public int Save()
		{
			return _context.SaveChanges();
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			_dbset.Update(entity);	
		}
	}
}

