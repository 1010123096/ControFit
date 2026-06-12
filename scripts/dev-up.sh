#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$ROOT"

if ! docker info >/dev/null 2>&1; then
  echo "❌ Docker no está corriendo."
  echo "   Abre Docker Desktop y vuelve a ejecutar este script."
  exit 1
fi

echo "▶ Iniciando SQL Server (puerto 1433)..."
docker compose up -d sqlserver

echo "⏳ Esperando SQL Server (~25s)..."
sleep 25

echo "▶ Iniciando API en http://localhost:5219 ..."
cd ControlFit.Api
dotnet run --launch-profile http
