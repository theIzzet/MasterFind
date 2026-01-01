1. Presentation Layer (Sunum Katmanı)
Kullanıcı deneyimini en üst düzeye çıkarmak için modern bir SPA (Single Page Application) mimarisi tercih edildi.
•	Teknoloji: React 19 ve Vite build aracı.
•	Uygulama: Bileşen tabanlı (Component-based) yapı kullanılarak Login, Register, MasterProfile gibi sayfalar modüler hale getirildi. State yönetimi için React Hooks (useState, useEffect, useContext) ve Context API (AuthContext) kullanıldı. Tasarımda CSS modülleri ile stil izolasyonu sağlandı.
2. Business Layer (İş Katmanı)
Projenin beyni olan bu katman, veri erişiminden bağımsız saf iş kurallarını barındırır.
•	Teknoloji: .NET Class Library, AutoMapper.
•	Uygulama: Services katmanında iş mantığı (validasyonlar, hesaplamalar) yürütülür. AutoMapper kütüphanesi kullanılarak Entity nesneleri (Veritabanı modelleri) ile DTO (Data Transfer Object) nesneleri arasındaki dönüşüm otomatikleştirildi, böylece API dışarıya asla ham veri modelini açmaz.
3. Data Layer (Veri Katmanı)
Veri tutarlılığını sağlamak ve veritabanı işlemlerini soyutlamak için Repository Pattern kullanıldı.
•	Teknoloji: Entity Framework Core 8, SQLite.
•	Uygulama: Code-First yaklaşımı ile C# sınıflarından veritabanı şeması (Migrations) türetildi. IGenericRepository arayüzü ile CRUD işlemleri standartlaştırıldı. DataSeeder sınıfı ile uygulama ilk ayağa kalktığında varsayılan Admin kullanıcısı ve Roller otomatik olarak oluşturulur.
4. Web Service Implementation
İstemci ve sunucu arasındaki iletişim, endüstri standardı REST mimarisi ile sağlandı.
•	Teknoloji: ASP.NET Core Web API.
•	Uygulama: Controller'lar sadece gelen isteği karşılayıp Servis katmanına iletir. HTTP durum kodları (200 OK, 400 BadRequest, 401 Unauthorized) doğru semantik ile kullanıldı. API dokümantasyonu için Swagger (OpenAPI) entegre edildi, böylece endpoint'ler görsel olarak test edilebilir hale geldi.
5. RBAC (Role-Based Access Control)
Uygulama güvenliği için kullanıcıların yetkileri roller üzerinden yönetildi.
•	Teknoloji: ASP.NET Core Identity.
•	Uygulama: Admin ve User olmak üzere iki temel rol tanımlandı. Controller seviyesinde [Authorize(Roles = "Admin")] gibi attribute'lar kullanılarak, yetkisiz kullanıcıların kritik endpoint'lere (örneğin Dashboard istatistikleri veya Kullanıcı silme) erişmesi engellendi.
6. Authorization & Authentication
Kimlik doğrulama süreci modern ve güvenli bir standart olan JWT ile kurgulandı.
•	Teknoloji: JSON Web Token (JWT).
•	Uygulama: Kullanıcı giriş yaptığında, sunucu kullanıcının ID'sini ve Rollerini içeren şifreli bir Token üretir. Bu token, sunucu tarafında User Secrets içerisinde saklanan gizli bir anahtar (Secret Key) ile imzalanır. Her istekte bu imza doğrulanır.
7. Session / Cookie Management
JWT'nin güvenli saklanması için "Stateful Session" yerine "Stateless Secure Cookie" yapısı tercih edildi.
•	Teknoloji: HttpOnly Cookies.
•	Uygulama: JWT token'ı, LocalStorage yerine HttpOnly ve Secure işaretli Cookie'ler içerisine gömüldü. Bu sayede XSS (Cross-Site Scripting) saldırılarında saldırganların JavaScript kodu çalıştırarak kullanıcının oturum anahtarını çalması engellendi.
8. Extension / Third Party Library
Kullanıcıların portfolyo görsellerini yönetebilmesi için hem Frontend hem Backend tarafında özelleştirmeler yapıldı.
•	Teknoloji: react-avatar-editor (Frontend), System.IO (Backend).
•	Uygulama: Frontend'de kullanıcıların yükledikleri resimleri kırpması (crop) ve ölçeklemesi sağlandı. Backend'de ise bu dosyalar benzersiz isimlendirme (GUID) ile sunucunun dosya sistemine (wwwroot/uploads) güvenli bir şekilde kaydedildi.
9. Web Security Implementation
Uygulama, yaygın web saldırılarına karşı birden fazla savunma hattı ile güçlendirildi.
•	Teknoloji: RateLimiter Middleware, User Secrets, CORS.
•	Uygulama:
o	Rate Limiting: Brute-force saldırılarını önlemek için Login endpoint'ine dakika başına istek sınırı (örn: 5 istek) getirildi.
o	User Secrets: API Key ve JWT Secret gibi hassas veriler kod deposundan çıkarılarak geliştirici ortamında şifreli saklandı.
o	CORS: Sadece belirlenen Frontend domaininden gelen isteklere izin verildi.
10. Cloud Service (AI) Integration
Kullanıcı deneyimini iyileştirmek için Üretken Yapay Zeka (Generative AI) entegrasyonu yapıldı.
•	Teknoloji: OpenRouter API, Google Gemma Model.
•	Uygulama: Ustalar profillerini oluştururken; meslek, tecrübe yılı ve şehir bilgilerini girerler. "AI ile Yaz" butonuna tıklandığında, Backend bu verileri alıp özel bir Prompt ile LLM'e (Büyük Dil Modeli) gönderir. Dönen profesyonel biyografi yazısı otomatik olarak forma işlenir. Serialize ve Deserialize işlemleri için Newtonsoft Kütüphanesi kullanılmıştır. 
