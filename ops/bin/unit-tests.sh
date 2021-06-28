
#!/usr/bin/env bash
set -eou pipefail
echo '--- :docker: Build test image'
docker build -f ./ops/docker/Dockerfile.test -t test .
echo '--- :docker: Run unit test'
SECRET="secret"
docker run --rm -e SECRET=$SECRET test WebApplication_Tests