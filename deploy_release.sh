#!/usr/bin/env bash
set -euo pipefail

PROJECT="/var/www/back-admin/CB.Web/CB.Web.csproj"
OUT="/tmp/back-admin-release"
PUBLISH="/var/www/back-admin/publish"
SHARED_UPLOADS="/var/www/back-admin/shared/uploads"

echo "1) Publish -> $OUT"
rm -rf "$OUT"
dotnet publish "$PROJECT" -c Release -o "$OUT"

echo "2) Stop service"
sudo systemctl stop back-admin

echo "3) Safety check: shared uploads must exist"
if [ ! -d "$SHARED_UPLOADS" ]; then
  echo "ERROR: $SHARED_UPLOADS not found. Aborting."
  exit 1
fi

echo "4) Replace publish folder (uploads won't be deleted because it's outside publish)"
sudo rm -rf "$PUBLISH"
sudo mv "$OUT" "$PUBLISH"

echo "5) Recreate uploads symlink"
sudo rm -rf "$PUBLISH/wwwroot/uploads" || true
sudo ln -s "$SHARED_UPLOADS" "$PUBLISH/wwwroot/uploads"

echo "6) Start service"
sudo systemctl start back-admin

echo "OK: Deploy completed"
sudo systemctl is-active back-admin
ls -ld "$PUBLISH/wwwroot/uploads"
