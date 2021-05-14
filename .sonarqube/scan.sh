#!/usr/bin/env bash
set -euo pipefail

function die { echo "$*" 1>&2 ; exit 1; }
function ssm-get {
	var=$(aws ssm get-parameter --name "$1" --with-decryption --query Parameter.Value --output text 2>/dev/null)
	[[ -z "$var" ]] && die "Error while retrieving $1"
	echo "$var"
}

PROJECT_KEY="sonar-proteges:tiffany-kata-frameworkless-basic-web-application-api"
PROJECT_NAME="Tiffany Frameworkless Web App Api"
TOKEN=$(ssm-get "/etc/tokens/sonarqube")
SOLUTION_FILE="FrameworklessBasicWebApplication.sln"

docker build \
  -f .sonarqube/Dockerfile \
  --build-arg PROJECT_KEY="$PROJECT_KEY" \
  --build-arg PROJECT_NAME="$PROJECT_NAME" \
  --build-arg TOKEN="$TOKEN" \
  --build-arg SOLUTION_FILE="$SOLUTION_FILE" \
  .