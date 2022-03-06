using AmadeusScanner.Repository.Common;
using AmadeusScanner.Repository.Data;
using AmadeusScanner.Repository.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AmadeusScanner.Repository
{
    public class Repository<T, TEntity> : IRepository<T> where T : class where TEntity : class
    {
        protected readonly DbContext context;
        protected readonly IMapper mapper;

        public Repository(DbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task AddAsync(T entity)
        {
            var ent = mapper.Map<TEntity>(entity);
            await context.Set<TEntity>().AddAsync(ent);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            var entityList = mapper.Map<IEnumerable<TEntity>>(entities.ToList());
            await context.Set<TEntity>().AddRangeAsync(entityList);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var conv = new ExpConverter<T, TEntity>();
            var exp = (Expression<Func<TEntity, bool>>)conv.Convert(predicate);
            var req =  (await Task.FromResult(context.Set<TEntity>().Where(exp))).ToList();
            return mapper.Map<IEnumerable<T>>(req);
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            return mapper.Map<T>(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<T>>(await context.Set<TEntity>().AsNoTracking().ToListAsync());
        }

        public async Task RemoveAsync(T entity)
        {
            var ent = mapper.Map<TEntity>(entity);
            context.Set<TEntity>().Remove(ent);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            var ent = mapper.Map<IEnumerable<TEntity>>(entities);
            context.Set<TEntity>().RemoveRange(ent);
            await Task.CompletedTask;
        }

    }
}
