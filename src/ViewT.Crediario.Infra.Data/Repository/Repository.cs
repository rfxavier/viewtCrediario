using System;
using System.Data.Entity;
using ViewT.Crediario.Domain.Core.Interfaces;
using ViewT.Crediario.Infra.Data.Context;

namespace ViewT.Crediario.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected CrediarioContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(CrediarioContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}