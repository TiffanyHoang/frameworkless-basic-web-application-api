#!/usr/bin/env bash
echo '--- :amazon: Deploy on AWS ECS'
aws cloudformation deploy --template-file ./aws/deployment-template.yaml --stack-name tiffany-deployment-stack --region ap-southeast-2