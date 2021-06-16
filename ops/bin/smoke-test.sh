#!/usr/bin/env bash
set -eou pipefail
echo '--- :docker: Build test image'
docker build -f Dockerfile.test -t test .
echo '--- :sunglasses: Prod endpoints test'
SECRET=$(aws ssm get-parameter --name "tiffany-app-secret" --query Parameter.Value --output text --region ap-southeast-2) 
docker run \
-e SECRET=$SECRET \
-e ENDPOINT="https://tiffany-prod.fma.lab.myobdev.com" \
test Smoke_Test
echo '--- :docker: Remove test image'
docker rmi test -f 

