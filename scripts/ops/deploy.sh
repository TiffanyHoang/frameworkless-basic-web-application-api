#!/usr/bin/env bash
echo '--- :amazon-ecs: Deploy on AWS ECS'
aws ecs --no-cli-pager update-service --cluster tiffany-cluster --service tiffany-service --force-new-deployment