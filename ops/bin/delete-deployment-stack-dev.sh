#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :aws: Delete test deployemnt stack on AWS'
VERSION=$(git describe --tags --always)
STACKNAME=TiffanyDeploymentStack  
aws cloudformation delete-stack --stack-name $STACKNAME$VERSION --region ap-southeast-2  
