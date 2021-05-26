#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :docker: Build test image'
docker build -f Dockerfile.test -t test .
echo '--- :docker: Run end to end test'
VERSION=$(git describe --tags --always)
STACKNAME=TiffanyDeploymentStack
VARIABLE_NAME=$(aws cloudformation describe-stacks --stack-name $STACKNAME$VERSION --region ap-southeast-2 --query "Stacks[0].Outputs[?OutputKey=='LoadBalancerDNS'].OutputValue" --output text)
docker run -e ENDPOINT=http://$VARIABLE_NAME test --filter Server_Test
echo '--- :docker: Remove test image'
docker rmi test -f 