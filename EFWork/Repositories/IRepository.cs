using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWork.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        TEntity Add(TEntity entity);
        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        int Delete(TEntity entity);
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        TEntity Update(TEntity entity);
        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// 通过拉姆达表达式查找数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);
        /// <summary>
        /// 异步通过拉姆达表达式查找数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllList(Func<TEntity, bool> predicate);
        /// <summary>
        /// 通过分页查询数据
        /// </summary>
        /// <param name="predicateOrder">正序排列条件</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="Count">返回条数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页的大小</param>
        /// <returns></returns>
        IEnumerable<TEntity> PageRequestResult(Func<TEntity, bool> predicateOrder, Func<TEntity, bool> predicate, out int Count, int PageIndex = 1, int PageSize = 10);
        /// <summary>
        /// 其他方法扩展
        /// </summary>
        /// <returns></returns>
        DbSet<TEntity> OtherExtend();
    }

   
}
