using Core;
using EFWork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class AsyncCrudAppService<TEntity> :IAsyncCrudAppService<TEntity> where TEntity : class
    {
        

        private readonly IRepository<TEntity> repository;
        public AsyncCrudAppService(IRepository<TEntity> _repository)
        {
            repository = _repository;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            var result = repository.Add(entity);
            return result;
        }
        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await repository.AddAsync(entity);
            return result;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        public int Delete(TEntity entity)
        {

            return repository.Delete(entity);

        }
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await repository.DeleteAsync(entity);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            var result = repository.Update(entity);
            return result;
        }
        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await repository.UpdateAsync(entity);
            return result;
        }
        /// <summary>
        /// 通过拉姆达表达式查找数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return repository.GetAll(predicate);
        }
        /// <summary>
        /// 异步通过拉姆达表达式查找数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllList(Func<TEntity, bool> predicate)
        {
            return await repository.GetAllList(predicate);
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
            var result = repository.PageRequestResult(predicateOrder,predicate,out Count,PageIndex,PageSize);           
            return result;
        }

    }
    public interface IAsyncCrudAppService<TEntity> where TEntity : class
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

    }
}
