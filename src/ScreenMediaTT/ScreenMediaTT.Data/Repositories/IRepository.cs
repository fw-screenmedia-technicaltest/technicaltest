using System.Linq;
using System.Threading.Tasks;

namespace ScreenMediaTT.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}
