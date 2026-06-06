import { check, sleep } from 'k6';
import http from 'k6/http';
import { getBaseUrl, loginAsGymAdmin, authHeaders, randomString } from './helpers.js';

export const options = {
  stages: [
    { duration: '10s', target: 5 },
    { duration: '30s', target: 15 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<2000'],
    http_req_failed: ['rate<0.02'],
  },
  summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(90)', 'p(95)', 'p(99)'],
};

export default function () {
  const BASE = getBaseUrl();
  const token = loginAsGymAdmin();
  const headers = authHeaders(token);

  // Create
  const nombre = `Test_${randomString(8)}`;
  const payload = JSON.stringify({
    nombreCompleto: nombre,
    correo: `${nombre}@test.com`,
    telefono: '999888777',
    gimnasioId: 1,
  });
  let res = http.post(`${BASE}/miembros`, payload, headers);
  check(res, { 'crear miembro OK': (r) => r.status === 200 || r.status === 201 });
  const miembroId = res.json('data.id') || 1;

  // Read
  res = http.get(`${BASE}/miembros`, headers);
  check(res, { 'list miembros OK': (r) => r.status === 200 });

  sleep(1);
}
