import { check, sleep } from 'k6';
import http from 'k6/http';
import { getBaseUrl, loginAsGymAdmin, authHeaders, randomString } from './helpers.js';

export const options = {
  stages: [
    { duration: '30s', target: 10 },
    { duration: '1m', target: 50 },
    { duration: '1m', target: 100 },
    { duration: '30s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(90)<3000', 'p(95)<5000'],
    http_req_failed: ['rate<0.05'],
  },
  summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(90)', 'p(95)', 'p(99)'],
};

const endpoints = ['dashboard', 'miembros', 'membresia', 'asignacion', 'Asistencia'];

export default function () {
  const BASE = getBaseUrl();
  const token = loginAsGymAdmin();
  const headers = authHeaders(token);

  // Hit a random endpoint each iteration
  const ep = endpoints[Math.floor(Math.random() * endpoints.length)];
  const res = http.get(`${BASE}/${ep}`, headers);
  check(res, { [`${ep} status 200`]: (r) => r.status === 200 });

  sleep(0.5);
}
