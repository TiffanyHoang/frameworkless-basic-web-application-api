#!/usr/bin/env bash
set -eou pipefail
echo '--- :sunglasses: Prod endpoints test'
SECRET=$(aws ssm get-parameter --name "tiffany-app-secret" --query Parameter.Value --output text --region ap-southeast-2) \
ENDPOINT="https://tiffany-prod.fma.lab.myobdev.com" \
dotnet test Smoke_Test