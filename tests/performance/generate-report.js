const fs = require('fs');
const path = require('path');

const summaryFiles = process.argv.slice(2);
if (summaryFiles.length === 0) {
  console.error('Usage: node generate-report.js <summary1.json> [summary2.json ...]');
  process.exit(1);
}

const metrics = [];
const allChecks = [];

for (const file of summaryFiles) {
  const raw = fs.readFileSync(file, 'utf-8');
  const data = JSON.parse(raw);
  const name = path.basename(file, '.json').replace('performance-', '');

  const rows = [];
  const checks = [];

  if (data.metrics) {
    for (const [key, val] of Object.entries(data.metrics)) {
      if (key.includes('checks')) continue;
      const rates = [];
      if (val.rate !== undefined) rates.push({ label: 'Rate', value: (val.rate * 100).toFixed(1) + '%' });
      if (val.passes !== undefined) rates.push({ label: 'Pass', value: val.passes });
      if (val.fails !== undefined) rates.push({ label: 'Fail', value: val.fails });
      if (val.avg !== undefined) rates.push({ label: 'Avg', value: ms(val.avg) });
      if (val.min !== undefined) rates.push({ label: 'Min', value: ms(val.min) });
      if (val.max !== undefined) rates.push({ label: 'Max', value: ms(val.max) });
      if (val.p95 !== undefined) rates.push({ label: 'P95', value: ms(val.p95) });
      if (val.med !== undefined) rates.push({ label: 'Med', value: ms(val.med) });
      if (val.count !== undefined && !key.includes('duration')) rates.push({ label: 'Count', value: val.count });
      rows.push({ metric: key, rates });
    }
  }

  // Collect checks from k6 summary export
  // Try nested groups first, then fall back to aggregate metrics
  function collectChecks(group, prefix) {
    if (!group) return;
    const groupName = prefix ? `${prefix} / ${group.name}` : group.name;
    if (Array.isArray(group.checks)) {
      for (const check of group.checks) {
        const fullName = groupName ? `${groupName}: ${check.name}` : check.name;
        checks.push({ name: fullName, passes: check.passes, fails: check.fails });
        allChecks.push({ test: name, name: fullName, passes: check.passes, fails: check.fails });
      }
    }
    if (Array.isArray(group.groups)) {
      for (const g of group.groups) collectChecks(g, groupName);
    }
  }
  collectChecks(data.root_group, '');

  // Fallback: read aggregate check metrics
  if (checks.length === 0 && data.metrics) {
    const checkMetric = data.metrics.checks;
    if (checkMetric && checkMetric.values) {
      const passes = checkMetric.values.passes || 0;
      const fails = checkMetric.values.fails || 0;
      if (passes > 0 || fails > 0) {
        checks.push({ name: 'All checks', passes, fails });
        allChecks.push({ test: name, name: 'All checks', passes, fails });
      }
    }
  }

  metrics.push({ name, rows, checks });
}

const html = `<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>k6 Performance Report</title>
<style>
  * { box-sizing: border-box; margin: 0; padding: 0; }
  body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif; background: #0d1117; color: #c9d1d9; padding: 24px; }
  h1 { color: #58a6ff; margin-bottom: 24px; font-size: 1.8rem; }
  h2 { color: #f0f6fc; margin-bottom: 12px; font-size: 1.3rem; }
  .summary { display: flex; gap: 16px; flex-wrap: wrap; margin-bottom: 24px; }
  .card { background: #161b22; border: 1px solid #30363d; border-radius: 8px; padding: 16px 24px; flex: 1; min-width: 180px; }
  .card h3 { color: #8b949e; font-size: 0.8rem; text-transform: uppercase; letter-spacing: 0.5px; margin-bottom: 4px; }
  .card .value { font-size: 1.8rem; font-weight: 700; color: #f0f6fc; }
  .card .value.pass { color: #3fb950; }
  .card .value.fail { color: #f85149; }
  .card .value.warn { color: #d29922; }
  .test-section { background: #161b22; border: 1px solid #30363d; border-radius: 8px; margin-bottom: 16px; overflow: hidden; }
  .test-header { background: #1c2128; padding: 12px 16px; border-bottom: 1px solid #30363d; font-weight: 600; font-size: 1.1rem; color: #f0f6fc; }
  table { width: 100%; border-collapse: collapse; }
  th { text-align: left; padding: 10px 16px; font-size: 0.75rem; color: #8b949e; text-transform: uppercase; letter-spacing: 0.5px; border-bottom: 1px solid #30363d; background: #1c2128; }
  td { padding: 8px 16px; border-bottom: 1px solid #21262d; font-size: 0.9rem; }
  tr:hover td { background: #1c2128; }
  .metric-name { font-weight: 500; color: #c9d1d9; white-space: nowrap; }
  .tags { display: flex; gap: 6px; flex-wrap: wrap; }
  .tag { background: #1f2937; color: #d1d5db; padding: 2px 8px; border-radius: 4px; font-size: 0.75rem; white-space: nowrap; }
  .tag.good { background: #0f2d1a; color: #3fb950; }
  .tag.bad { background: #2d0f0f; color: #f85149; }
  .checks { margin: 12px 16px; padding: 8px 12px; background: #0d1117; border-radius: 6px; }
  .check-item { display: flex; justify-content: space-between; padding: 4px 0; font-size: 0.85rem; }
  .check-name { color: #c9d1d9; }
  .check-result { font-weight: 600; }
  .check-result.pass { color: #3fb950; }
  .check-result.fail { color: #f85149; }
</style>
</head>
<body>
<h1>🏋️ k6 Performance Report</h1>
<div class="summary" id="summary"></div>
<div id="tests"></div>
<script>
const metrics = ${JSON.stringify(metrics)};
const allChecks = ${JSON.stringify(allChecks)};

const totalPassed = allChecks.filter(c => c.fails === 0).length;
const totalFailed = allChecks.filter(c => c.fails > 0).length;
const totalChecks = allChecks.length;

const summaryHtml = \`
  <div class="card"><h3>Tests</h3><div class="value">\${metrics.length}</div></div>
  <div class="card"><h3>Checks Passed</h3><div class="value pass">\${totalPassed}</div></div>
  <div class="card"><h3>Checks Failed</h3><div class="value fail">\${totalFailed}</div></div>
  <div class="card"><h3>Total Checks</h3><div class="value">\${totalChecks}</div></div>
\`;
document.getElementById('summary').innerHTML = summaryHtml;

let testsHtml = '';
for (const test of metrics) {
  testsHtml += \`<div class="test-section"><div class="test-header">\${test.name}</div><table><tr><th>Metric</th><th>Values</th></tr>\`;
  for (const row of test.rows) {
    let tagsHtml = '';
    for (const r of row.rates) {
      const isGood = r.label === 'Rate' && parseFloat(r.value) >= 90;
      const isBad = r.label === 'Rate' && parseFloat(r.value) < 90;
      const cls = isGood ? 'good' : isBad ? 'bad' : '';
      tagsHtml += \`<span class="tag \${cls}">\${r.label}: \${r.value}</span>\`;
    }
    testsHtml += \`<tr><td class="metric-name">\${row.metric}</td><td><div class="tags">\${tagsHtml}</div></td></tr>\`;
  }
  testsHtml += '</table>';
  if (test.checks.length > 0) {
    testsHtml += '<div class="checks">';
    for (const c of test.checks) {
      const cls = c.fails === 0 ? 'pass' : 'fail';
      testsHtml += \`<div class="check-item"><span class="check-name">\${c.name}</span><span class="check-result \${cls}">\${c.passes}✓ \${c.fails}✗</span></div>\`;
    }
    testsHtml += '</div>';
  }
  testsHtml += '</div>';
}
document.getElementById('tests').innerHTML = testsHtml;
</script>
</body>
</html>`;

const outFile = 'performance-report.html';
fs.writeFileSync(outFile, html);
console.log(`Report generated: ${outFile}`);

function ms(v) {
  if (v === undefined) return '-';
  if (v < 1000) return v.toFixed(1) + 'ms';
  if (v < 60000) return (v / 1000).toFixed(2) + 's';
  return (v / 60000).toFixed(1) + 'm';
}