#!/usr/bin/env bash 
set -eou pipefail
echo '--- :docker: Build app image'
VERSION=$(git describe --tags --always) 
ECRACC='274387265859.dkr.ecr.ap-southeast-2.amazonaws.com'
APPIMAGE=$ECRACC/tiffany-frameworkless-basic-web-app-api:$VERSION
appImage=$APPIMAGE docker-compose -f ./ops/docker/docker-compose.yaml build app
echo '--- :docker: Push app image to ECR'
appImage=$APPIMAGE docker-compose -f ./ops/docker/docker-compose.yaml push app
echo '--- :docker: Remove app image'
appImage=$APPIMAGE docker-compose -f ./ops/docker/docker-compose.yaml down --rmi all