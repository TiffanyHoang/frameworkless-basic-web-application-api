#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS ECS'
aws cloudformation deploy \
    --template-file ./aws/deployment-template.yaml \
    --parameter-overrides version=$(git describe --tags --always) \
    --stack-name tiffany-deployment-stack \
    --region ap-southeast-2