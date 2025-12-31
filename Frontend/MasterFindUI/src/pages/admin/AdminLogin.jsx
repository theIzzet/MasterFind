import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

const AdminLogin = () => {
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const { login, logout } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    const payload = {
      email: email.trim() || undefined,
      phoneNumber: phoneNumber.trim() || undefined,
      password,
      rememberMe: false,
    };

    try {
      const me = await login(payload); // ✅ session aç + me dön

      const roles = me?.roles || [];
      if (!roles.includes('Admin')) {
        await logout();
        setError('Bu hesap admin yetkisine sahip değil.');
        return;
      }

      navigate('/admin', { replace: true });
    } catch (err) {
      setError(err.response?.data?.Errors?.join(', ') || 'Admin girişi başarısız.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={styles.page}>
      <form style={styles.card} onSubmit={handleSubmit}>
        <h2 style={styles.title}>Admin Panel Girişi</h2>
        <p style={styles.subtitle}>Yetkili kullanıcılar içindir</p>

        {error && <div style={styles.errorBox}>{error}</div>}

        <input
          style={styles.input}
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <input
          style={styles.input}
          type="text"
          placeholder="Telefon (opsiyonel)"
          value={phoneNumber}
          onChange={(e) => setPhoneNumber(e.target.value)}
        />

        <input
          style={styles.input}
          type="password"
          placeholder="Şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        <button style={styles.button} disabled={loading}>
          {loading ? 'Giriş Yapılıyor...' : 'Giriş Yap'}
        </button>
      </form>
    </div>
  );
};

const styles = {
  page: {
    minHeight: '100vh',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    background: 'linear-gradient(135deg, #4f46e5, #6366f1)',
    fontFamily: 'Inter, system-ui, sans-serif',
  },
  card: {
    width: 380,
    background: '#fff',
    padding: 32,
    borderRadius: 16,
    boxShadow: '0 20px 40px rgba(0,0,0,0.2)',
  },
  title: { textAlign: 'center', marginBottom: 4 },
  subtitle: { textAlign: 'center', marginBottom: 24, color: '#6b7280', fontSize: 14 },
  errorBox: {
    background: '#fee2e2',
    color: '#b91c1c',
    padding: 10,
    borderRadius: 8,
    marginBottom: 12,
    textAlign: 'center',
    fontSize: 14,
  },
  input: {
    width: '100%',
    padding: 12,
    borderRadius: 10,
    border: '1px solid #d1d5db',
    marginBottom: 14,
    outline: 'none',
    fontSize: 14,
  },
  button: {
    width: '100%',
    padding: 12,
    borderRadius: 10,
    border: 'none',
    background: '#4f46e5',
    color: '#fff',
    fontSize: 15,
    fontWeight: 600,
    cursor: 'pointer',
    marginTop: 8,
  },
};

export default AdminLogin;
