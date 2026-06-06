const fs = require('fs');
const path = require('path');

const summaryFiles = process.argv.slice(2);
if (summaryFiles.length === 0) {
  console.error('Usage: node generate-report.js <summary1.json> [summary2.json ...]');
  process.exit(1);
}

const allTests = [];
let grandTotalChecks = 0;
let grandTotalPassed = 0;
let grandTotalFailed = 0;

for (const file of summaryFiles) {
  const raw = fs.readFileSync(file, 'utf-8');
  const data = JSON.parse(raw);
  const testName = path.basename(file, '.json').replace('performance-', '');

  // Find all checks anywhere in the JSON structure
  const checks = [];
  function findChecks(obj, depth = 0) {
    if (!obj || typeof obj !== 'object' || depth > 10) return;
    
    if (Array.isArray(obj.checks)) {
      for (const check of obj.checks) {
        if (check.name) {
          const checkVals = { ...(typeof check === 'object' && check !== null ? check : {}), ...(check.values || {}) };
          checks.push({
            name: check.name,
            passes: checkVals.passes || 0,
            fails: checkVals.fails || 0
          });
        }
      }
    }
    
    for (const v of Object.values(obj)) {
      if (Array.isArray(v)) {
        for (const item of v) findChecks(item, depth + 1);
      } else if (typeof v === 'object' && v !== null) {
        findChecks(v, depth + 1);
      }
    }
  }
  findChecks(data);

  // Fallback: read aggregate checks from metrics if no individual checks found
  if (checks.length === 0 && data.metrics) {
    const checksMetric = data.metrics.checks;
    if (checksMetric) {
      const vals = { ...(typeof checksMetric === 'object' && checksMetric !== null ? checksMetric : {}), ...(checksMetric.values || {}) };
      const passes = vals.passes || vals.count || 0;
      const fails = vals.fails || 0;
      if (passes > 0 || fails > 0) {
        checks.push({ name: 'All checks', passes, fails });
      }
    }
  }

  const testPassed = checks.reduce((s, c) => s + c.passes, 0);
  const testFailed = checks.reduce((s, c) => s + c.fails, 0);
  grandTotalChecks += checks.length;
  grandTotalPassed += testPassed;
  grandTotalFailed += testFailed;

  // Parse metrics - handle both metric.values and metric direct formats
  const metricsList = [];
  if (data.metrics) {
    const importantKeys = [
      'http_req_duration',
      'http_reqs', 
      'http_req_failed',
      'iterations',
      'data_received',
      'data_sent',
      'vus',
      'vus_max',
      'iteration_duration'
    ];
    
    for (const key of importantKeys) {
      const metric = data.metrics[key];
      if (!metric) continue;
      
      // Merge metric.values and direct metric properties (handles empty metric.values object)
      const vals = { ...(typeof metric === 'object' && metric !== null ? metric : {}), ...(metric.values || {}) };
      const tags = [];
      
      // Duration/counter metrics
      if (vals.avg !== undefined) tags.push({ label: 'Avg', value: formatDuration(vals.avg) });
      if (vals.min !== undefined) tags.push({ label: 'Min', value: formatDuration(vals.min) });
      if (vals.med !== undefined) tags.push({ label: 'Med', value: formatDuration(vals.med) });
      if (vals.max !== undefined) tags.push({ label: 'Max', value: formatDuration(vals.max) });
      if (vals['p(90)'] !== undefined) tags.push({ label: 'P90', value: formatDuration(vals['p(90)']) });
      if (vals['p(95)'] !== undefined) tags.push({ label: 'P95', value: formatDuration(vals['p(95)']) });
      
      // Counters and rates
      if (vals.count !== undefined) tags.push({ label: 'Count', value: vals.count.toLocaleString() });
      if (vals.rate !== undefined) {
        const rateStr = (vals.rate * 100).toFixed(2) + '%';
        let cls = '';
        if (key === 'http_req_failed') {
          if (vals.rate === 0) cls = 'good';
          else if (vals.rate > 0.05) cls = 'bad';
          else cls = 'warn';
        }
        tags.push({ label: 'Rate', value: rateStr, class: cls });
      }
      
      // Thresholds
      const thresholds = [];
      if (metric.thresholds) {
        for (const [tKey, tVal] of Object.entries(metric.thresholds)) {
          const ok = tVal.ok === true || tVal.ok === 'true' || tVal.ok === 1;
          thresholds.push({
            expression: tKey,
            passed: ok
          });
        }
      }
      
      if (tags.length > 0 || thresholds.length > 0) {
        metricsList.push({ name: key, tags, thresholds });
      }
    }
  }

  allTests.push({
    name: testName,
    checks,
    testPassed,
    testFailed,
    metricsList
  });
}

// Generate HTML
const html = `<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>k6 Performance Report</title>
<style>
  * { box-sizing: border-box; margin: 0; padding: 0; }
  body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif; background: #0d1117; color: #c9d1d9; padding: 32px; line-height: 1.6; }
  .container { max-width: 1200px; margin: 0 auto; }
  
  h1 { color: #58a6ff; margin-bottom: 8px; font-size: 2rem; font-weight: 700; }
  .subtitle { color: #8b949e; margin-bottom: 32px; font-size: 0.95rem; }
  
  .summary-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 16px; margin-bottom: 32px; }
  .summary-card { background: #161b22; border: 1px solid #30363d; border-radius: 12px; padding: 24px; text-align: center; }
  .summary-card:hover { border-color: #58a6ff; }
  .summary-card h3 { color: #8b949e; font-size: 0.85rem; text-transform: uppercase; letter-spacing: 1px; margin-bottom: 12px; font-weight: 500; }
  .summary-card .value { font-size: 2.5rem; font-weight: 700; color: #f0f6fc; line-height: 1; }
  .summary-card .value.pass { color: #3fb950; }
  .summary-card .value.fail { color: #f85149; }
  .summary-card .detail { color: #8b949e; font-size: 0.85rem; margin-top: 8px; }
  
  .test-section { background: #161b22; border: 1px solid #30363d; border-radius: 12px; margin-bottom: 24px; overflow: hidden; }
  .test-header { background: #1c2128; padding: 20px 24px; border-bottom: 1px solid #30363d; display: flex; justify-content: space-between; align-items: center; }
  .test-header h2 { color: #f0f6fc; font-size: 1.25rem; font-weight: 600; margin: 0; }
  .test-status { display: flex; gap: 8px; }
  .status-badge { padding: 4px 12px; border-radius: 20px; font-size: 0.8rem; font-weight: 600; }
  .status-badge.pass { background: #0f2d1a; color: #3fb950; }
  .status-badge.fail { background: #2d0f0f; color: #f85149; }
  
  .test-body { padding: 24px; }
  
  .metrics-table { width: 100%; border-collapse: collapse; margin-bottom: 24px; }
  .metrics-table th { text-align: left; padding: 12px 16px; font-size: 0.8rem; color: #8b949e; text-transform: uppercase; letter-spacing: 0.5px; border-bottom: 2px solid #30363d; background: #0d1117; }
  .metrics-table td { padding: 14px 16px; border-bottom: 1px solid #21262d; font-size: 0.9rem; }
  .metrics-table tr:hover td { background: #1c2128; }
  .metric-name { font-weight: 600; color: #f0f6fc; font-family: 'SF Mono', monospace; font-size: 0.85rem; }
  
  .tags { display: flex; gap: 8px; flex-wrap: wrap; }
  .tag { background: #1f2937; color: #d1d5db; padding: 4px 10px; border-radius: 6px; font-size: 0.8rem; font-weight: 500; }
  .tag.good { background: #0f2d1a; color: #3fb950; }
  .tag.warn { background: #2d1f0f; color: #d29922; }
  .tag.bad { background: #2d0f0f; color: #f85149; }
  
  .checks-section { background: #0d1117; border-radius: 8px; padding: 16px; margin-top: 16px; }
  .checks-section h4 { color: #8b949e; font-size: 0.85rem; text-transform: uppercase; letter-spacing: 0.5px; margin-bottom: 12px; }
  .check-item { display: flex; justify-content: space-between; align-items: center; padding: 10px 12px; border-radius: 6px; margin-bottom: 4px; }
  .check-item:hover { background: #161b22; }
  .check-name { color: #c9d1d9; font-size: 0.9rem; }
  .check-stats { display: flex; gap: 12px; font-size: 0.85rem; }
  .check-pass { color: #3fb950; font-weight: 600; }
  .check-fail { color: #f85149; font-weight: 600; }
  
  .thresholds-section { margin-top: 20px; }
  .thresholds-section h4 { color: #8b949e; font-size: 0.85rem; text-transform: uppercase; letter-spacing: 0.5px; margin-bottom: 12px; }
  .threshold-item { display: flex; align-items: center; gap: 12px; padding: 10px 12px; border-radius: 6px; margin-bottom: 4px; }
  .threshold-item:hover { background: #161b22; }
  .threshold-icon { font-size: 1.2rem; width: 24px; text-align: center; }
  .threshold-pass { color: #3fb950; }
  .threshold-fail { color: #f85149; }
  .threshold-text { flex: 1; color: #c9d1d9; font-size: 0.9rem; }
  .threshold-metric { color: #8b949e; font-size: 0.8rem; font-family: monospace; }
</style>
</head>
<body>
<div class="container">
  <h1>🏋️ k6 Performance Report</h1>
  <p class="subtitle">Load test results across smoke, login, and stress scenarios</p>
  
  <div class="summary-grid">
    <div class="summary-card">
      <h3>Test Scenarios</h3>
      <div class="value">${allTests.length}</div>
      <div class="detail">scenarios executed</div>
    </div>
    <div class="summary-card">
      <h3>Total Checks</h3>
      <div class="value">${grandTotalPassed + grandTotalFailed}</div>
      <div class="detail">assertions evaluated</div>
    </div>
    <div class="summary-card">
      <h3>Passed</h3>
      <div class="value pass">${grandTotalPassed}</div>
      <div class="detail">successful checks</div>
    </div>
    <div class="summary-card">
      <h3>Failed</h3>
      <div class="value fail">${grandTotalFailed}</div>
      <div class="detail">failed checks</div>
    </div>
  </div>

  ${allTests.map(test => `
  <div class="test-section">
    <div class="test-header">
      <h2>${test.name.charAt(0).toUpperCase() + test.name.slice(1)} Test</h2>
      <div class="test-status">
        ${test.testFailed === 0 
          ? `<span class="status-badge pass">✓ All Passed</span>`
          : `<span class="status-badge fail">✗ ${test.testFailed} Failed</span>`
        }
      </div>
    </div>
    <div class="test-body">
      <table class="metrics-table">
        <thead>
          <tr>
            <th>Metric</th>
            <th>Values</th>
          </tr>
        </thead>
        <tbody>
          ${test.metricsList.map(row => `
          <tr>
            <td><span class="metric-name">${row.name}</span></td>
            <td>
              <div class="tags">
                ${row.tags.map(tag => `<span class="tag ${tag.class || ''}">${tag.label}: ${tag.value}</span>`).join('')}
              </div>
            </td>
          </tr>
          `).join('')}
        </tbody>
      </table>
      
      ${test.checks.length > 0 ? `
      <div class="checks-section">
        <h4>Checks (${test.testPassed} passed, ${test.testFailed} failed)</h4>
        ${test.checks.map(check => `
        <div class="check-item">
          <span class="check-name">${check.name}</span>
          <div class="check-stats">
            <span class="check-pass">${check.passes} ✓</span>
            ${check.fails > 0 ? `<span class="check-fail">${check.fails} ✗</span>` : ''}
          </div>
        </div>
        `).join('')}
      </div>
      ` : ''}
      
      ${test.metricsList.some(m => m.thresholds.length > 0) ? `
      <div class="thresholds-section">
        <h4>Thresholds</h4>
        ${test.metricsList.flatMap(m => m.thresholds.map(t => `
        <div class="threshold-item">
          <span class="threshold-icon ${t.passed ? 'threshold-pass' : 'threshold-fail'}">${t.passed ? '✓' : '✗'}</span>
          <span class="threshold-text">${t.expression}</span>
          <span class="threshold-metric">${m.name}</span>
        </div>
        `)).join('')}
      </div>
      ` : ''}
    </div>
  </div>
  `).join('')}
</div>
</body>
</html>`;

const outFile = 'performance-report.html';
fs.writeFileSync(outFile, html);
console.log(`Report generated: ${outFile}`);

function formatDuration(v) {
  if (v === undefined || v === null) return '-';
  if (v < 1000) return v.toFixed(1) + 'ms';
  if (v < 60000) return (v / 1000).toFixed(2) + 's';
  return (v / 60000).toFixed(1) + 'm';
}
