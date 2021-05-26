#!/usr/bin/env bash
echo '--- :aws: Delete test stacks on AWS'
VERSION=$(git describe --tags --always)
STACKNAME=TiffanyDeploymentStack
aws cloudformation delete-stack --stack-name $STACKNAME$VERSION --region ap-southeast-2