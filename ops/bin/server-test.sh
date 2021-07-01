#!/usr/bin/env bash
set -eou pipefail
echo '--- :docker: Run server test'
VERSION=$(git describe --tags --always) 
ECRACC='274387265859.dkr.ecr.ap-southeast-2.amazonaws.com'
APPIMAGE=$ECRACC/tiffany-frameworkless-basic-web-app-api:$VERSION
appImage=$APPIMAGE docker-compose -f ./ops/docker/docker-compose-server-test.yaml up --abort-on-container-exit 
echo '--- :docker: Remov containers'
appImage=$APPIMAGE docker-compose -f ./ops/docker/docker-compose-server-test.yaml down
