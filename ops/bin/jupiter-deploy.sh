#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :kubernetes: Deploy on Jupiter'
GITHASH=$(git describe --tags --always)
NAME=tiffany-app
SECRET=$(kubectl get secret/tiffany-app-secret -n fma --template={{.data.value}})
ktmpl ./kube/templates/deployment.yaml \
    --parameter name "$NAME" \
    --parameter imageTag "$GITHASH" \
    --parameter host "$NAME.svc.platform.myobdev.com" \
    --parameter secret "$SECRET" \
    --parameter dbName "$NAME-db-admin" \
| kubectl apply -f -

timeout 30 kubectl rollout status --watch=true deployment $NAME -n fma