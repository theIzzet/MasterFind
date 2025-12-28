using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Yapılan tüm değişiklikleri veritabanına tek bir işlem olarak kaydeder.
        /// </summary>
        /// <returns>Etkilenen satır sayısı.</returns>
        Task<int> SaveChangesAsync();
    }
}
