#!/usr/bin/env bash
set -eou pipefail
echo '--- :docker: Build test image'
docker build -f ./ops/docker/Dockerfile.test -t test .
echo '--- :sunglasses: Prod endpoints test'
SECRET=$(aws ssm get-parameter --name "tiffany-app-secret" --query Parameter.Value --with-decryption --output text --region ap-southeast-2) 
docker run --rm --name smoke-test \
-e SECRET=$SECRET \
-e ENDPOINT="https://tiffany-app.svc.platform.myobdev.com" \
test Smoke_Test