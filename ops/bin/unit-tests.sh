#!/usr/bin/env bash
set -o xtrace
set -eou pipefail
echo '--- :docker: Build test image'
docker build -f Dockerfile.test -t test .
echo '--- :docker: Run unit test'
SECRET="secret"
docker run -e SECRET=$SECRET test
echo '--- :docker: Remove test image'
docker rmi test -f 