using Repositories.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Master
{
    public class MasterProfile:BaseEntity
    {
        public string? CompanyName { get; set; } // Firma Adı (Bireysel ise boş olabilir)
        public string Description { get; set; } = string.Empty; // Usta hakkında genel açıklama
        public int YearsOfExperience { get; set; } // Tecrübe Yılı
        public string? TaxNumber { get; set; } // Vergi Numarası (Firma ise zorunlu olabilir)
        public string? ProfileImageUrl { get; set; } // Profil Fotoğrafının URL'si
        public string? CertificateUrl { get; set; } // Ustalık belgesinin URL'si

        // AppUser ile Bire-Bir ilişki kurmak için Foreign Key
        public string AppUserId { get; set; } = string.Empty;
        [ForeignKey("AppUserId")]
        public required virtual AppUser AppUser { get; set; }

        // MasterProfile'ın portfolyo öğeleri (Bire-Çok ilişki)
        public virtual ICollection<PortfolioItem> PortfolioItems { get; set; } = new List<PortfolioItem>();

        // MasterProfile'ın verdiği hizmetler (EF Core tarafından yönetilen Çoka-Çok ilişki)
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();

        // MasterProfile'ın hizmet verdiği konumlar (EF Core tarafından yönetilen Çoka-Çok ilişki)
        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    }
}
