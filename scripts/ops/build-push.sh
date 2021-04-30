#!/usr/bin/env bash
echo '--- :docker: Build app image'
docker build --target run -t 138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/frameworkless-basic-web-app-tiffany .
echo '--- :docker: Push app image to ECR'
docker push 138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/frameworkless-basic-web-app-tiffany
echo '--- :docker: Remove app image'
docker rmi 138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/frameworkless-basic-web-app-tiffany -f