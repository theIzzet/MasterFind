import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { authService } from '../../services/authService';
import '../../css/Auth.css';

const Register = () => {
  const [registerData, setRegisterData] = useState({
    name: '',
    surName: '',
    username: '',
    email: '',
    phoneNumber: '',
    password: '',
    confirmPassword: ''
  });

  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRegisterData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    if (registerData.password !== registerData.confirmPassword) {
      setError('Şifreler eşleşmiyor');
      setLoading(false);
      return;
    }

    const payload = {
      name: registerData.name,
      surName: registerData.surName,
      username: registerData.username,
      password: registerData.password,
      confirmPassword: registerData.confirmPassword,
    };

    if (registerData.email.trim()) payload.email = registerData.email;
    if (registerData.phoneNumber.trim()) payload.phoneNumber = registerData.phoneNumber;

    try {
      const response = await authService.register(payload);

      const token = response.data?.token || response.data?.Token;
      if (!token) throw new Error('Token gelmedi');

      login(token);

      // login yaptıktan sonra tekrar login'e gitmek yerine dashboard
      navigate('/master/dashboard', { replace: true });
    } catch (err) {
      if (err.response) {
        setError(err.response.data?.Errors?.join(', ') || 'Kayıt başarısız');
      } else if (err.request) {
        setError('Sunucuya bağlanılamadı');
      } else {
        setError('Kayıt sırasında bir hata oluştu');
      }
    } finally {
      setLoading(false);
    }
  };

  const isFormValid = () => {
    return (
      registerData.name &&
      registerData.surName &&
      registerData.username &&
      registerData.password &&
      registerData.confirmPassword &&
      (registerData.email || registerData.phoneNumber)
    );
  };

  return (
    <div className="auth-page">
      <div className="auth-container">
        <form className="auth-card" onSubmit={handleSubmit} noValidate>
          <header className="auth-header">
            <div className="auth-logo small" aria-hidden="true">U</div>
            <h2 className="auth-title">Kayıt Ol</h2>
          </header>

          {error && (
            <div className="auth-alert" role="alert" aria-live="assertive">
              {error}
            </div>
          )}

          {/* Ad & Soyad */}
          <div className="form-row">
            <div className="form-group">
              <label htmlFor="name">Ad <span className="req">*</span></label>
              <input
                type="text"
                id="name"
                name="name"
                value={registerData.name}
                onChange={handleChange}
                required
                disabled={loading}
              />
            </div>

            <div className="form-group">
              <label htmlFor="surName">Soyad <span className="req">*</span></label>
              <input
                type="text"
                id="surName"
                name="surName"
                value={registerData.surName}
                onChange={handleChange}
                required
                disabled={loading}
              />
            </div>
          </div>

          {/* Kullanıcı adı */}
          <div className="form-group">
            <label htmlFor="username">Kullanıcı Adı <span className="req">*</span></label>
            <input
              type="text"
              id="username"
              name="username"
              value={registerData.username}
              onChange={handleChange}
              required
              disabled={loading}
            />
          </div>

          {/* Email & Telefon */}
          <div className="form-row">
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input
                type="email"
                id="email"
                name="email"
                value={registerData.email}
                onChange={handleChange}
                disabled={loading}
              />
            </div>

            <div className="form-group">
              <label htmlFor="phoneNumber">Telefon Numarası</label>
              <input
                type="tel"
                id="phoneNumber"
                name="phoneNumber"
                value={registerData.phoneNumber}
                onChange={handleChange}
                disabled={loading}
              />
            </div>
          </div>

          {/* Şifre & Tekrar */}
          <div className="form-row">
            <div className="form-group">
              <label htmlFor="password">Şifre <span className="req">*</span></label>
              <input
                type="password"
                id="password"
                name="password"
                value={registerData.password}
                onChange={handleChange}
                required
                disabled={loading}
              />
            </div>

            <div className="form-group">
              <label htmlFor="confirmPassword">Şifre (Tekrar) <span className="req">*</span></label>
              <input
                type="password"
                id="confirmPassword"
                name="confirmPassword"
                value={registerData.confirmPassword}
                onChange={handleChange}
                required
                disabled={loading}
              />
            </div>
          </div>

          <button
            type="submit"
            className="auth-button"
            disabled={loading || !isFormValid()}
          >
            {loading ? 'Kayıt yapılıyor...' : 'Kayıt Ol'}
          </button>

          <div className="auth-footer">
            Zaten hesabınız var mı?{' '}
            <Link to="/login" className="link-strong">Giriş Yap</Link>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
