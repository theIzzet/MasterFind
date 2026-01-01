
## Özellikler

### 1. Presentation Layer 
**Teknoloji:** React 19 + Vite  
**Açıklama:** Kullanıcı deneyimini en üst düzeye çıkarmak için modern SPA (Single Page Application) mimarisi kullanıldı. Bileşen tabanlı yapı ile Login, Register, MasterProfile gibi sayfalar modüler hale getirildi. State yönetimi için React Hooks ve Context API kullanıldı. Tasarımda CSS modülleri ile stil izolasyonu sağlandı.

**Sorumlu Kişi:** [Salih Can Turan]  

**İlgili Branch:** presentation

### 2. Bussiness Layer
**Teknoloji:** .NET Class Library, AutoMapper  
**Açıklama:** Projenin beyni olan bu katmanda saf iş kuralları barındırılır. Services katmanında iş mantığı (validasyonlar, hesaplamalar) yürütülür. AutoMapper kullanılarak Entity nesneleri ile DTO nesneleri arasındaki dönüşüm otomatikleştirildi, böylece API dışarıya ham veri modelini açmaz.

**Sorumlu Kişi:** [Kerem Kartal]  

**İlgili Branch:** business-layer

### 3. Data Layer
**Teknoloji:** Entity Framework Core 8, SQLite  
**Açıklama:** Veri tutarlılığını sağlamak ve veritabanı işlemlerini soyutlamak için Repository Pattern kullanıldı. Code-First yaklaşımı ile C# sınıflarından veritabanı şeması türetildi. IGenericRepository arayüzü ile CRUD işlemleri standartlaştırıldı. DataSeeder sınıfı ile varsayılan Admin kullanıcısı ve Roller otomatik oluşturulur.

**Sorumlu Kişi:** [Kerem Kartal]  

**İlgili Branch:** data-layer

### 4. Web Service
**Teknoloji:** ASP.NET Core Web API  
**Açıklama:** İstemci ve sunucu arasındaki iletişim, REST mimarisi ile sağlandı. Controller'lar sadece gelen isteği karşılayıp Servis katmanına iletir. HTTP durum kodları doğru semantik ile kullanıldı. API dokümantasyonu için Swagger (OpenAPI) entegre edildi.

**Sorumlu Kişi:** [Kerem Kartal] 

**İlgili Branch:** api-layer

### 5. Rol Tabanlı Erişim Kontrolü (RBAC)
**Teknoloji:** ASP.NET Core Identity  
**Açıklama:** Uygulama güvenliği için kullanıcıların yetkileri roller üzerinden yönetildi. Admin ve User olmak üzere iki temel rol tanımlandı. Controller seviyesinde [Authorize(Roles = "Admin")] gibi attribute'lar kullanılarak, yetkisiz kullanıcıların kritik endpoint'lere erişmesi engellendi.

**Sorumlu Kişi:** [İzzet Esener]  

**İlgili Branch:** rbac

### 6. Authorization Implementation
**Teknoloji:** JSON Web Token (JWT)  
**Açıklama:** Kimlik doğrulama süreci JWT ile kurgulandı. Kullanıcı giriş yaptığında, sunucu kullanıcının ID'sini ve Rollerini içeren şifreli bir Token üretir. Bu token, User Secrets içerisinde saklanan gizli anahtar ile imzalanır. Her istekte bu imza doğrulanır.

**Sorumlu Kişi:** [İzzet Esener]  
**İlgili Branch:** auth

### 7. Session / Cookie Management
**Teknoloji:** HttpOnly Cookies  
**Açıklama:** JWT'nin güvenli saklanması için "Stateless Secure Cookie" yapısı tercih edildi. JWT token'ı, LocalStorage yerine HttpOnly ve Secure işaretli Cookie'ler içerisine gömüldü. Bu sayede XSS saldırılarına karşı koruma sağlandı.

**Sorumlu Kişi:** [Salih Can Turan]  
**İlgili Branch:** cookie-session

### 8. Extension / Third Party Library Using
**Teknoloji:** react-avatar-editor (Frontend), System.IO (Backend)  
**Açıklama:** Kullanıcıların portfolyo görsellerini yönetebilmesi için özelleştirmeler yapıldı. Frontend'de kullanıcıların yükledikleri resimleri kırpması ve ölçeklemesi sağlandı. Backend'de ise bu dosyalar benzersiz isimlendirme ile sunucunun dosya sistemine güvenli bir şekilde kaydedildi.

**Sorumlu Kişi:** Salih Can Turan  
**İlgili Branch:** extension-library

### 9. Web Güvenliği Uygulamaları
**Teknoloji:** RateLimiter Middleware, User Secrets, CORS  
**Açıklama:** Uygulama, yaygın web saldırılarına karşı birden fazla savunma hattı ile güçlendirildi. Rate Limiting ile brute-force saldırıları önlendi. User Secrets ile hassas veriler kod deposundan çıkarıldı. CORS ile sadece belirlenen Frontend domaininden gelen isteklere izin verildi.

**Sorumlu Kişi:** [İzzet Esener]  
**İlgili Branch:** web-security

### Cloud Service (AI) Using
**Teknoloji:** OpenRouter API, Google Gemma Model  
**Açıklama:** Kullanıcı deneyimini iyileştirmek için Generative AI entegrasyonu yapıldı. Ustalar profillerini oluştururken; meslek, tecrübe yılı ve şehir bilgilerini girerler. "AI ile Yaz" butonuna tıklandığında, bu veriler özel bir Prompt ile LLM'e gönderilir. Dönen profesyonel biyografi yazısı otomatik olarak forma işlenir.

**Sorumlu Kişi:** [İzzet Esener]  
**İlgili Branch:** cloud-service

---

## Kurulum ve Çalıştırma

### Ön Koşullar
- Node.js (v18 veya üzeri)
- .NET 8 SDK
- SQLite

### Frontend Kurulumu
```bash
cd client
npm install
npm run dev
```

### Backend Kurulumu
```bash
cd API
dotnet restore
dotnet run
```

---

## Güvenlik Özellikleri
- JWT ile token tabanlı kimlik doğrulama
- HttpOnly ve Secure cookies
- Rate limiting (dakikada 5 istek limiti)
- CORS politikaları
- Hassas veriler için User Secrets yönetimi
- SQL injection koruması (Entity Framework)
