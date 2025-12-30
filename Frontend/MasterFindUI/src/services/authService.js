import api from './api';

export const authService = {
  login: (loginData) => api.post('/auth/login', loginData),
  register: (registerData) => api.post('/auth/register', registerData),
};
