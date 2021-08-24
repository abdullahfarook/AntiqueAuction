using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiqueAuction.Core.Repository;
using AntiqueAuction.Shared.Domain;
using AntiqueAuction.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AntiqueAuction.Infrastructure.Repository
{
    // Framework Independent Repository based class
    // Contains all the shared functions which can be inherited by other repositories
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly AntiqueAuctionDbContext Context;
        protected DbSet<T> Entity;

        public Repository(AntiqueAuctionDbContext context)
        {
            this.Context = context;
            Entity = context.Set<T>();
        }
        public IEnumerable<T> Get()
        {
            DisableLazyLoading();
            return Entity.AsNoTracking().AsEnumerable();
        }

        public Task<T> Get(Guid id)
        {
            DisableLazyLoading();
            return Entity.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entity.Add(entity);
           await Context.SaveChangesAsync();
           return entity;
        }

        public Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            return Context.SaveChangesAsync();
        }
        public Task Update(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await Entity.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
            {
                throw NotFoundException.ForSystem($"Entity of Type: {GetType().Name} having ID: {id} not found");
            }
            Entity.Remove(entity);
            await Context.SaveChangesAsync();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entity.Remove(entity);
        }
        private void DisableLazyLoading() 
            => Context.ChangeTracker.LazyLoadingEnabled = false;
    }
}