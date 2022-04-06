using System.Linq.Expressions;
using ath_server.Services;

namespace ath_server.Interfaces;

public interface IRepositoryService<T> where T: IEntity<int>
{
    IQueryable<T> GetAllRecords();
    T GetSingle(int id);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    ServiceResult Add(T entity);
    ServiceResult Delete(T entity);
    ServiceResult Edit(T entity);
    ServiceResult Save();
}