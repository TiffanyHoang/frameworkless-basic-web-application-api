#!/usr/bin/env bash
echo '--- :docker: Build app image'
docker-compose build app
echo '--- :docker: Push app image to ECR'
docker-compose push app
echo '--- :docker: Remove app image'
docker-compose down --rmi all