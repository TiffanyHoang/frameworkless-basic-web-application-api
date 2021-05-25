#!/usr/bin/env bash
echo '--- :aws: Deploy on DEV'
version=$(git describe --tags --always)
vpcId='vpc-d88094bf'
subnet1='subnet-0ecc2546'
subnet2='subnet-316e9257'
certificate='arn:aws:acm:ap-southeast-2:138666658526:certificate/026583fe-93ca-4b50-9735-575fcf92a19d'
aws cloudformation deploy \
    --template-file ./aws/deployment-template.yaml \
    --parameter-overrides \
        version=$version \
        imageVersion=$version \
        vpcId=$vpcId \
        subnet1=$subnet1 \
        subnet2=$subnet2 \
        certificate=$certificate \
    --stack-name tiffany-deployment-stack-$version \
    --region ap-southeast-2