#!/usr/bin/env bash
set -eou pipefail
echo '--- :docker: Build test image'
docker build -f Dockerfile.test -t test .
echo '--- :sunglasses: Prod endpoints test'
SECRET=$(kubectl get secret/tiffany-app-secret -n fma --template={{.data.value}}) 
docker run \
-e SECRET=$SECRET \
-e ENDPOINT="https://tiffany-app.svc.platform.myobdev.com" \
test Smoke_Test
echo '--- :docker: Remove test image'
docker rmi test -f 

