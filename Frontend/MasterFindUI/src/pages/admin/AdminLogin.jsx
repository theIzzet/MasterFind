import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authService } from '../../services/authService';
import { useAuth } from '../../context/AuthContext';

const AdminLogin = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    // 🔑 BACKEND'E UYUMLU PAYLOAD
    const payload = {
      password,
      rememberMe: false,
    };

    if (email.trim()) {
      payload.email = email.trim();
    }

    try {
      const res = await authService.login(payload);
      const token = res.data.token;

      // token kaydet
      login(token);

      // role kontrol
      const decoded = JSON.parse(atob(token.split('.')[1]));
      const role =
        decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      if (role !== 'Admin') {
        setError('Bu hesap admin yetkisine sahip değil.');
        return;
      }

      navigate('/admin');
    } catch (err) {
      console.error(err);
      setError(
        err.response?.data?.Errors?.join(', ') ||
          'Admin girişi başarısız.'
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="auth-page">
      <form onSubmit={handleSubmit} className="auth-card">
        <h2>Admin Girişi</h2>

        {error && <p className="auth-error">{error}</p>}

        <input
          type="email"
          placeholder="Admin Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />

        <input
          type="password"
          placeholder="Şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        <button disabled={loading}>
          {loading ? 'Giriş Yapılıyor...' : 'Giriş Yap'}
        </button>
      </form>
    </div>
  );
};

export default AdminLogin;
