#!/usr/bin/env bash
echo '--- :aws: Delete test stacks on AWS'
version=$(git describe --tags --always)
aws cloudformation delete-stack --stack-name tiffany-deployment-stack-$version --region ap-southeast-2