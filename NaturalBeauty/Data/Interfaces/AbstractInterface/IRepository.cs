
using Data.Common;
using System.Threading.Tasks;

namespace Data.Interfaces.AbstractInterface
{
    public interface IRepository<T, TEntityId>
       where T : Entity<TEntityId>
       where TEntityId : class
    {
        Task CreateAsync(T entity);
        Task<T> GetByIdAsync(TEntityId id);
        //Task DeleteAsync(T entity);
        //Task UpdateAsync(T entity);
    }
}
