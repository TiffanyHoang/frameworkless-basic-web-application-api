#!/usr/bin/env bash 
set -eou pipefail
set -o xtrace
echo '--- :docker: Build app image'
VERSION=$(git describe --tags --always) 
ECRACC='274387265859.dkr.ecr.ap-southeast-2.amazonaws.com'
version=$VERSION ecrAcc=$ECRACC docker-compose build app
echo '--- :docker: Push app image to ECR'
version=$VERSION ecrAcc=$ECRACC docker-compose push app
echo '--- :docker: Remove app image'
version=$VERSION ecrAcc=$ECRACC docker-compose down --rmi all