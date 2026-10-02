using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
            protected AppDbContext _context = context;
            public IQueryable<T> GetQuery { get => _context.Set<T>().AsQueryable(); }

            public void Delete(T entity)
            {
                _context.Set<T>().Remove(entity);
            }

        public void DeleteMany(List<T> entities)
        {
             _context.Set<T>().RemoveRange(entities);
        }

        public async Task<List<T>> GetAll()
            {
                return await _context.Set<T>().ToListAsync();
            }
            public async Task<T?> GetById(Guid id, bool track)
            {
                var query = _context.Set<T>().AsQueryable();

                if (!track)
                    query = query.AsNoTracking();

                return await query.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == id);
            }
            public async Task<T?> GetById<P>(P id)
            {
                return await _context.Set<T>().FindAsync(id);
            }

            public async Task Insert(T entity)
            {
               await  _context.Set<T>().AddAsync(entity);
            }

            public void Update(T entity)
            {
                _context.Update(entity);
            }
        }
    }

