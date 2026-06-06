import { check, sleep } from 'k6';
import http from 'k6/http';
import { getBaseUrl, loginAsGymAdmin, loginAsSuperAdmin, authHeaders } from './helpers.js';

export const options = {
  stages: [
    { duration: '10s', target: 5 },
    { duration: '30s', target: 20 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<1500'],
    http_req_failed: ['rate<0.01'],
  },
  summaryTrendStats: ['avg', 'min', 'med', 'max', 'p(90)', 'p(95)', 'p(99)'],
};

export default function () {
  const BASE = getBaseUrl();
  const token = loginAsGymAdmin();
  const headers = authHeaders(token);

  const res = http.get(`${BASE}/dashboard/gym-admin`, headers);
  check(res, { 'dashboard data': (r) => r.status === 200 });

  sleep(1);
}
