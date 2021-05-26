#!/usr/bin/env bash 
echo '--- :docker: Build app image'
version=$(git describe --tags --always) ecrAcc='138666658526.dkr.ecr.ap-southeast-2.amazonaws.com' docker-compose build app
echo '--- :docker: Push app image to ECR'
version=$(git describe --tags --always) ecrAcc='138666658526.dkr.ecr.ap-southeast-2.amazonaws.com' docker-compose push app
echo '--- :docker: Remove app image'
docker-compose down --rmi all