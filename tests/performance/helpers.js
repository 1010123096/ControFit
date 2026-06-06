import http from 'k6/http';
import { check } from 'k6';

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5219/api';

export function getBaseUrl() {
  return BASE_URL;
}

export function loginAsGymAdmin() {
  const res = http.post(`${BASE_URL}/auth/login`, JSON.stringify({
    correo: 'gym@fitzone.com',
    contrasena: '123456',
  }), { headers: { 'Content-Type': 'application/json' } });
  check(res, { 'login gym admin OK': (r) => r.status === 200 });
  return res.json().token;
}

export function loginAsSuperAdmin() {
  const res = http.post(`${BASE_URL}/auth/login`, JSON.stringify({
    correo: 'admin@controlfit.com',
    contrasena: '123456',
  }), { headers: { 'Content-Type': 'application/json' } });
  check(res, { 'login super admin OK': (r) => r.status === 200 });
  return res.json().token;
}

export function authHeaders(token) {
  return {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
  };
}

export function randomString(length) {
  const chars = 'abcdefghijklmnopqrstuvwxyz0123456789';
  let result = '';
  for (let i = 0; i < length; i++) {
    result += chars.charAt(Math.floor(Math.random() * chars.length));
  }
  return result;
}
