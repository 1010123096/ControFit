import { check, sleep } from 'k6';
import http from 'k6/http';
import { getBaseUrl } from './helpers.js';

export const options = {
  stages: [
    { duration: '10s', target: 10 },
    { duration: '30s', target: 50 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<2000', 'avg<500'],
    http_req_failed: ['rate<0.01'],
  },
  summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(90)', 'p(95)', 'p(99)'],
};

export default function () {
  const BASE = getBaseUrl();

  const payload = JSON.stringify({
    correo: 'gym@fitzone.com',
    contrasena: '123456',
  });

  const res = http.post(`${BASE}/auth/login`, payload, {
    headers: { 'Content-Type': 'application/json' },
  });

  check(res, {
    'login status 200': (r) => r.status === 200,
    'login has token': (r) => r.json('token') !== undefined,
  });

  sleep(1);
}
