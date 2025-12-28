
using System.Linq.Expressions;

namespace Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// ID'ye göre bir entity'yi bulur.
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Bir tablodaki tüm kayıtları getirir.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Belirli bir koşula uyan kayıtları sorgulanabilir bir formatta (IQueryable) döner.
        /// Bu, üzerine ek sorgular (OrderBy, Include vb.) eklememizi sağlar.
        /// </summary>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Yeni bir entity ekler.
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Bir entity'yi günceller.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Bir entity'yi siler.
        /// </summary>
        void Delete(T entity);
    }
}
