#!/usr/bin/env bash
echo '--- :docker: Build app image'
docker-compose build app
echo '--- :ecr: Prepare repo in ECR'
aws cloudformation deploy --template-file ./aws/ecr-template.yaml --stack-name tiffany-ecr --region ap-southeast-2 
echo '--- :docker: Push app image to ECR'
docker-compose push app
echo '--- :docker: Remove app image'
docker-compose down --rmi all