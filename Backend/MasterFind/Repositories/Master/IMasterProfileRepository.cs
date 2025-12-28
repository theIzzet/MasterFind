namespace Repositories.Master
{
    public interface IMasterProfileRepository : IGenericRepository<MasterProfile>
    {
        /// <summary>
        /// Belirli bir hizmet kategorisi ve/veya konuma göre ustaları arar.
        /// İlişkili verileri (Hizmetler, Konumlar) de yükler.
        /// </summary>
        Task<IEnumerable<MasterProfile>> SearchMastersAsync(int? serviceCategoryId, int? locationId);

        /// <summary>
        /// Bir kullanıcının AppUserId'sine göre Usta profilini getirir.
        /// </summary>
        Task<MasterProfile?> GetByAppUserIdAsync(string appUserId); 
        Task<IEnumerable<ServiceCategory>> GetServiceCategoryWithServicesAsync();
        Task<MasterProfile?> GetByIdWithAllDetailsAsync(int masterProfileId);

    }
}
