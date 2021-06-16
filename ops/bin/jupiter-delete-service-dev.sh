#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :kubernetes: Deploy on Jupiter'
GITHASH=$(git describe --tags --always)
NAME=tiffany-app-$GITHASH
SECRET="secret"
ktmpl ./kube/templates/deployment.yaml \
    --parameter name "$NAME" \
    --parameter imageTag "$GITHASH" \
    --parameter host "$NAME.svc.platform.myobdev.com" \
    --parameter secret "$SECRET" \
| kubectl delete -f -