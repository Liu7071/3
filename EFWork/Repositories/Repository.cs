using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFWork.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly WebDbContext dbContext;
        private readonly DbSet<TEntity> DbSet;
        public Repository(WebDbContext _dbContext)
        {
            dbContext = _dbContext;
            DbSet = dbContext.Set<TEntity>();
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            var result = DbSet.Add(entity).Entity;
            dbContext.SaveChanges();
            return result;
        }
        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = (await DbSet.AddAsync(entity)).Entity;
            await dbContext.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        public int Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return dbContext.SaveChanges();

        }
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await Task.FromResult(Delete(entity));
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            var result = DbSet.Update(entity).Entity;
            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            dbContext.SaveChanges();
            return result;
        }
        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await Task.FromResult(Update(entity));
            return result;
        }
        /// <summary>
        /// 通过拉姆达表达式查找数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return DbSet.Where(predicate);
        }
        /// <summary>
        /// 异步通过拉姆达表达式查找数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllList(Func<TEntity, bool> predicate)
        {
            return await Task.FromResult(GetAll(predicate));
        }
        /// <summary>
        /// 通过分页查询数据
        /// </summary>
        /// <param name="predicateOrder">正序排列条件</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="Count">返回条数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页的大小</param>
        /// <returns></returns>
        public IEnumerable<TEntity> PageRequestResult(Func<TEntity, bool> predicateOrder, Func<TEntity, bool> predicate, out int Count, int PageIndex = 1, int PageSize = 10)
        {
            var result = DbSet.Where(predicate).OrderBy(predicateOrder).Take((PageIndex - 1) * PageSize).Skip(PageSize);
            Count = result.Count();
            return result;
        }
        /// <summary>
        /// 其他方法扩展
        /// </summary>
        /// <returns></returns>
        public DbSet<TEntity> OtherExtend()
        {
            var result = DbSet;
            return result;
        }

    }
}
