#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :kubernetes: Deploy on Jupiter'
ENV=$1
GITHASH=$(git describe --tags --always)
if [ $ENV = dev ]
then
NAME=tiffany-app-$GITHASH
SECRET="secret"
else
NAME=tiffany-app
SECRET=$(kubectl get secret/tiffany-app-secret -n fma --template={{.data.value}})
fi
ktmpl ./kube/templates/deployment.yaml \
    --parameter name "$NAME" \
    --parameter imageTag "$GITHASH" \
    --parameter host "$NAME.svc.platform.myobdev.com" \
    --parameter secret "$SECRET" \
| kubectl apply -f -

timeout 30 kubectl rollout status --watch=true deployment $NAME -n fma