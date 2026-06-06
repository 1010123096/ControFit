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

  const nombre = `Test_${randomString(8)}`;
  const payload = JSON.stringify({
    nombre: nombre,
    correo: `${nombre}@test.com`,
    telefono: '999888777',
    gimnasioId: 1,
    fechaNacimiento: '1990-01-15',
  });
  let res = http.post(`${BASE}/miembros/Registro`, payload, headers);
  check(res, { 'crear miembro OK': (r) => r.status === 200 });

  res = http.get(`${BASE}/miembros/obtenerTodos`, headers);
  check(res, { 'list miembros OK': (r) => r.status === 200 });

  sleep(1);
}
