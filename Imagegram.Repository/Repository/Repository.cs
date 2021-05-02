using Imagegram.Domain;
using Imagegram.Models.ResourceParameters;
using Imagegram.Repository.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imagegram.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ImagegramDbContext imagegramDbContext;
        private readonly DbSet<T> entities;

        public Repository(ImagegramDbContext imagegramDbContext)
        {
            this.imagegramDbContext = imagegramDbContext;
            entities = imagegramDbContext.Set<T>();
        }
        public T Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public PagedList<T> GetAll(BaseResourceParameters resourceParameters)
        {
            return PagedList<T>
                .Create(
                entities,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            SaveChanges();
        }

        public void DeleteAll(IList<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.RemoveRange(entity);
            SaveChanges();
        }

        public void RollbackTransaction()
        {
            if (imagegramDbContext.Database.CurrentTransaction != null)
                imagegramDbContext.Database.RollbackTransaction();
        }

        public void BeginTransaction()
        {
            imagegramDbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (imagegramDbContext.Database.CurrentTransaction != null)
                imagegramDbContext.Database.CommitTransaction();
        }

        public void SaveChanges()
        {
            imagegramDbContext.SaveChanges();
        }

        public void Dispose()
        {
            imagegramDbContext.Dispose();
        }
    }
}
