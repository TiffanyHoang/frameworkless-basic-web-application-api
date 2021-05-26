#!/usr/bin/env bash
echo '--- :ecr: Prepare repo in ECR'
aws cloudformation deploy \
    --template-file ./aws/templates/ecr-template.yaml \
    --stack-name tiffany-ecr \
    --region ap-southeast-2     