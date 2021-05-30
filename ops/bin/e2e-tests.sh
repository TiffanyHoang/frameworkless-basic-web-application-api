#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :docker: Build test image'
docker build -f Dockerfile.test -t test .
echo '--- :docker: Run end to end test'
VERSION=$(git describe --tags --always)
docker run -e ENDPOINT=https://$VERSION.tiffany-dev.fma.lab.myobdev.com test --filter Server_Test
echo '--- :docker: Remove test image'
docker rmi test -f 