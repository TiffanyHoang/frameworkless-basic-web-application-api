#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS ECS'
commit=$(git describe --tags --always)
vpcId='vpc-d88094bf'
subnet1='subnet-0ecc2546'
subnet2='subnet-316e9257'
aws cloudformation deploy \
    --template-file ./aws/deployment-template.yaml \
    --parameter-overrides \
        version=$commit \
        vpcId=$vpcId \
        subnet1=$subnet1 \
        subnet2=$subnet2 \
    --stack-name tiffany-deployment-stack \
    --region ap-southeast-2