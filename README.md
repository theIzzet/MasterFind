Özellikler
1. Modern Frontend Geliştirme
Teknoloji: React 19 + Vite
Açıklama: Kullanıcı deneyimini en üst düzeye çıkarmak için modern SPA (Single Page Application) mimarisi kullanıldı. Bileşen tabanlı yapı ile Login, Register, MasterProfile gibi sayfalar modüler hale getirildi. State yönetimi için React Hooks ve Context API kullanıldı. Tasarımda CSS modülleri ile stil izolasyonu sağlandı.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-frontend

2. İş Mantığı Katmanı
Teknoloji: .NET Class Library, AutoMapper
Açıklama: Projenin beyni olan bu katmanda saf iş kuralları barındırılır. Services katmanında iş mantığı (validasyonlar, hesaplamalar) yürütülür. AutoMapper kullanılarak Entity nesneleri ile DTO nesneleri arasındaki dönüşüm otomatikleştirildi, böylece API dışarıya ham veri modelini açmaz.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-business-layer

3. Veri Katmanı ve ORM
Teknoloji: Entity Framework Core 8, SQLite
Açıklama: Veri tutarlılığını sağlamak ve veritabanı işlemlerini soyutlamak için Repository Pattern kullanıldı. Code-First yaklaşımı ile C# sınıflarından veritabanı şeması türetildi. IGenericRepository arayüzü ile CRUD işlemleri standartlaştırıldı. DataSeeder sınıfı ile varsayılan Admin kullanıcısı ve Roller otomatik oluşturulur.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-data-layer

4. RESTful API
Teknoloji: ASP.NET Core Web API
Açıklama: İstemci ve sunucu arasındaki iletişim, REST mimarisi ile sağlandı. Controller'lar sadece gelen isteği karşılayıp Servis katmanına iletir. HTTP durum kodları doğru semantik ile kullanıldı. API dokümantasyonu için Swagger (OpenAPI) entegre edildi.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-api

5. Rol Tabanlı Erişim Kontrolü (RBAC)
Teknoloji: ASP.NET Core Identity
Açıklama: Uygulama güvenliği için kullanıcıların yetkileri roller üzerinden yönetildi. Admin ve User olmak üzere iki temel rol tanımlandı. Controller seviyesinde [Authorize(Roles = "Admin")] gibi attribute'lar kullanılarak, yetkisiz kullanıcıların kritik endpoint'lere erişmesi engellendi.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-rbac

6. Kimlik Doğrulama ve Yetkilendirme
Teknoloji: JSON Web Token (JWT)
Açıklama: Kimlik doğrulama süreci JWT ile kurgulandı. Kullanıcı giriş yaptığında, sunucu kullanıcının ID'sini ve Rollerini içeren şifreli bir Token üretir. Bu token, User Secrets içerisinde saklanan gizli anahtar ile imzalanır. Her istekte bu imza doğrulanır.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-authentication

7. Oturum ve Cookie Yönetimi
Teknoloji: HttpOnly Cookies
Açıklama: JWT'nin güvenli saklanması için "Stateless Secure Cookie" yapısı tercih edildi. JWT token'ı, LocalStorage yerine HttpOnly ve Secure işaretli Cookie'ler içerisine gömüldü. Bu sayede XSS saldırılarına karşı koruma sağlandı.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-session-management

8. Dosya Yönetimi ve Üçüncü Parti Kütüphaneler
Teknoloji: react-avatar-editor (Frontend), System.IO (Backend)
Açıklama: Kullanıcıların portfolyo görsellerini yönetebilmesi için özelleştirmeler yapıldı. Frontend'de kullanıcıların yükledikleri resimleri kırpması ve ölçeklemesi sağlandı. Backend'de ise bu dosyalar benzersiz isimlendirme ile sunucunun dosya sistemine güvenli bir şekilde kaydedildi.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-file-management

9. Web Güvenliği Uygulamaları
Teknoloji: RateLimiter Middleware, User Secrets, CORS
Açıklama: Uygulama, yaygın web saldırılarına karşı birden fazla savunma hattı ile güçlendirildi. Rate Limiting ile brute-force saldırıları önlendi. User Secrets ile hassas veriler kod deposundan çıkarıldı. CORS ile sadece belirlenen Frontend domaininden gelen isteklere izin verildi.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-security

10. Bulut Servisi (Yapay Zeka) Entegrasyonu
Teknoloji: OpenRouter API, Google Gemma Model
Açıklama: Kullanıcı deneyimini iyileştirmek için Generative AI entegrasyonu yapıldı. Ustalar profillerini oluştururken; meslek, tecrübe yılı ve şehir bilgilerini girerler. "AI ile Yaz" butonuna tıklandığında, bu veriler özel bir Prompt ile LLM'e gönderilir. Dönen profesyonel biyografi yazısı otomatik olarak forma işlenir.

Sorumlu Kişi: [İsim Soyisim]
İlgili Branch: develop-ai-integration

Teknik Detaylar
Frontend Yapısı
text
src/
├── components/     # Tekrar kullanılabilir bileşenler
├── contexts/      # Context API yönetimi (AuthContext)
├── pages/         # Sayfa bileşenleri
├── services/      # API servisleri
├── styles/        # CSS modülleri
└── utils/         # Yardımcı fonksiyonlar
Backend Yapısı
text
API/
├── Controllers/   # API endpoint'leri
├── Services/      # İş mantığı katmanı
├── Data/          # Veri erişim katmanı
├── DTOs/          # Data Transfer Objects
├── Models/        # Veritabanı modelleri
└── Middlewares/   # Özel middleware'ler
