using MyToDo.API.Entity;
using MyToDo.API.Repositories;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected  IBaseRepository<TEntity> iBaseRepository;


        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
            =>await iBaseRepository.DeleteAsync(whereExpression);

        public async Task<bool> DeleteByIdAsync(dynamic id)
        {
            TEntity entity = await iBaseRepository.GetByIdAsync(id);
            if (entity is not null) 
                return await iBaseRepository.DeleteByIdAsync(id);
            else
                return false;
        }


        public async Task<bool> DeleteByIdsAsync(dynamic[] ids)
            => await iBaseRepository.DeleteByIdsAsync(ids);

        public async Task<TEntity> GetByIdAsync(object id)
            =>await iBaseRepository.GetByIdAsync(id);

        public async Task<List<TEntity>> GetListAsync()
            => await GetListAsync();

        public virtual async Task<bool> InsertAsync(TEntity entity)
            =>await iBaseRepository.InsertAsync(entity);

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
            =>await iBaseRepository.QueryAsync(page, size, total);
        public virtual async Task<bool> UpdateAsync(TEntity entity)
            =>await iBaseRepository.UpdateAsync(entity);

        public virtual async Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<TEntity, TEntity>> updateExpression, Expression<Func<TEntity, bool>> whereExpression)
            =>false;
    }
}
