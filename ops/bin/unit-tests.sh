#!/usr/bin/env bash
set -eou pipefail
echo '--- :docker: Build test image'
docker build -f Dockerfile.test -t test .
echo '--- :docker: Run unit test'
docker run test
echo '--- :docker: Remove test image'
docker rmi test -f 