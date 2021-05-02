using Imagegram.Domain;
using Imagegram.Models.ResourceParameters;
using Imagegram.Repository.Helpers;
using System;
using System.Collections.Generic;

namespace Imagegram.Repository.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Guid id);
        PagedList<T> GetAll(BaseResourceParameters resourceParameters);
        void Insert(T entity);
        void Delete(T entity);
        void DeleteAll(IList<T> entity);
        void SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void Dispose();
    }
}
