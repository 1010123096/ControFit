import { check, sleep } from 'k6';
import http from 'k6/http';
import { getBaseUrl, loginAsGymAdmin, loginAsSuperAdmin, authHeaders } from './helpers.js';

export const options = {
  vus: 1,
  duration: '30s',
  thresholds: {
    http_req_duration: ['p(95)<1000'],
    http_req_failed: ['rate<0.01'],
  },
  summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(90)', 'p(95)', 'p(99)'],
};

export default function () {
  const BASE = getBaseUrl();

  // Super Admin login
  const superToken = loginAsSuperAdmin();

  // Super Admin dashboard
  let res = http.get(`${BASE}/dashboard/super-admin`, authHeaders(superToken));
  check(res, { 'super admin dashboard': (r) => r.status === 200 });

  // Gym Admin login
  const gymToken = loginAsGymAdmin();

  // Gym Admin dashboard
  res = http.get(`${BASE}/dashboard/gym-admin`, authHeaders(gymToken));
  check(res, { 'gym admin dashboard': (r) => r.status === 200 });

  // Miembros
  res = http.get(`${BASE}/miembros`, authHeaders(gymToken));
  check(res, { 'list miembros': (r) => r.status === 200 });

  // Membresias
  res = http.get(`${BASE}/membresia`, authHeaders(gymToken));
  check(res, { 'list membresias': (r) => r.status === 200 });

  // Asignaciones
  res = http.get(`${BASE}/asignacion`, authHeaders(gymToken));
  check(res, { 'list asignaciones': (r) => r.status === 200 });

  // Asistencias
  res = http.get(`${BASE}/Asistencia`, authHeaders(gymToken));
  check(res, { 'list asistencias': (r) => r.status === 200 });

  // Gimnasios (super admin only)
  res = http.get(`${BASE}/gimnasios/obtenerTodos`, authHeaders(superToken));
  check(res, { 'list gimnasios': (r) => r.status === 200 });

  sleep(1);
}
