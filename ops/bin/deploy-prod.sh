#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS PROD'
version='Prod'
imageVersion=e0b5ca9
vpcId='vpc-8aefeced'
subnet1='subnet-6df91d25'
subnet2='subnet-5818f33e'
certificate='arn:aws:acm:ap-southeast-2:274387265859:certificate/c62856b3-deea-48e3-aa85-55531dfdcb25'
aws cloudformation deploy \
    --template-file ./aws/deployment-template.yaml \
    --parameter-overrides \
        version=$version \
        imageVersion=$imageVersion \
        vpcId=$vpcId \
        subnet1=$subnet1 \
        subnet2=$subnet2 \
        certificate=$certificate \
    --stack-name TiffanyDeploymentStack$version \
    --region ap-southeast-2