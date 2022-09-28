﻿using FreeSql;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZhonTai.Admin.Core.Auth;
using ZhonTai.Admin.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;

namespace ZhonTai.Admin.Core.Repositories
{
    public class RepositoryBase<TEntity, TKey> : BaseRepository<TEntity, TKey>, IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        public IUser User { get; set; }

        public RepositoryBase(IFreeSql freeSql) : base(freeSql, null, null)
        {
        }
        public RepositoryBase(IFreeSql fsql, Expression<Func<TEntity, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable) { }

        public virtual Task<TDto> GetAsync<TDto>(TKey id)
        {
            return Select.WhereDynamic(id).ToOneAsync<TDto>();
        }

        public virtual Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync<TDto>();
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync();
        }

        public virtual async Task<bool> SoftDeleteAsync(TKey id)
        {
            await UpdateDiy
                .SetDto(new
                {
                    IsDeleted = true,
                    ModifiedUserId = User.Id,
                    ModifiedUserName = User.UserName
                })
                .WhereDynamic(id)
                .ExecuteAffrowsAsync();

            return true;
        }

        public virtual async Task<bool> SoftDeleteAsync(TKey[] ids)
        {
            await UpdateDiy
                .SetDto(new
                {
                    IsDeleted = true,
                    ModifiedUserId = User.Id,
                    ModifiedUserName = User.UserName
                })
                .WhereDynamic(ids)
                .ExecuteAffrowsAsync();

            return true;
        }

        public virtual async Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
        {
            await UpdateDiy
                .SetDto(new
                {
                    IsDeleted = true,
                    ModifiedUserId = User.Id,
                    ModifiedUserName = User.UserName
                })
                .Where(exp)
                .DisableGlobalFilter(disableGlobalFilterNames)
                .ExecuteAffrowsAsync();

            return true;
        }

        public virtual async Task<bool> DeleteRecursiveAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
        {
            await Select
            .Where(exp)
            .DisableGlobalFilter(disableGlobalFilterNames)
            .AsTreeCte()
            .ToDelete()
            .ExecuteAffrowsAsync();

            return true;
        }

        public virtual async Task<bool> SoftDeleteRecursiveAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
        {
            await Select
            .Where(exp)
            .DisableGlobalFilter(disableGlobalFilterNames)
            .AsTreeCte()
            .ToUpdate()
            .SetDto(new
            {
                IsDeleted = true,
                ModifiedUserId = User.Id,
                ModifiedUserName = User.UserName
            })
            .ExecuteAffrowsAsync();

            return true;
        }
    }

    public class RepositoryBase<TEntity> : RepositoryBase<TEntity, long>, IRepositoryBase<TEntity> where TEntity : class
    {
        public RepositoryBase(UnitOfWorkManagerCloud uowm) : this(DbKeys.MasterDbKey, uowm) { }
        public RepositoryBase(string db, UnitOfWorkManagerCloud uowm) : this(uowm.GetUnitOfWorkManager(db)) { }
        RepositoryBase(UnitOfWorkManager uowm) : base(uowm.Orm)
        {
            uowm.Binding(this);
        }
    }
}