import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7054/api",
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
});
api.interceptors.response.use(
  (response) => {
    // Başarılı yanıtları olduğu gibi geçir
    return response;
  },
  (error) => {
    // Hata durumlarını kontrol et
    if (error.response) {

      // DURUM: 429 Rate Limit (Çok Fazla İstek)
      if (error.response.status === 429) {
        console.warn("Rate Limit Aşıldı!");

        // Backend'den gelen hata mesajı bazen JSON değil, düz string olabilir.
        // Komponentlerin (Login.jsx) kırılmaması için hata verisini standardize edelim:
        if (!error.response.data || typeof error.response.data !== 'object') {
          // Kendi standart hata formatını oluşturuyorsun
          error.response.data = {
            Errors: ["Çok fazla deneme yaptınız. Lütfen 1 dakika bekleyip tekrar deneyin."]
          };
        }
      }

      // DURUM: 401 Unauthorized (Oturum düşmüşse)
      if (error.response.status === 401) {
        // İsteğe bağlı: Kullanıcıyı login sayfasına yönlendirebilirsin
        // window.location.href = "/login";
      }
    }

    // Hatayı fırlat ki Login.jsx içindeki catch bloğu çalışsın
    return Promise.reject(error);
  }
);
export default api;
