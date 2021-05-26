#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS'
ENV=$1
STACKNAME=TiffanyDeploymentStack
if [ $ENV = dev ]
then
VERSION=$(git describe --tags --always)
IMAGEVERSION=$(git describe --tags --always)
VPCID='vpc-d88094bf'
SUBNET1='subnet-0ecc2546'
SUBNET2='subnet-316e9257'
CERTIFICATE='arn:aws:acm:ap-southeast-2:138666658526:certificate/026583fe-93ca-4b50-9735-575fcf92a19d'
else
VERSION='prod'
IMAGEVERSION=$(git describe --tags --always)
VPCID='vpc-8aefeced'
SUBNET1='subnet-6df91d25'
SUBNET2='subnet-5818f33e'
CERTIFICATE='arn:aws:acm:ap-southeast-2:274387265859:certificate/c62856b3-deea-48e3-aa85-55531dfdcb25'
fi

aws cloudformation deploy \
    --template-file ./aws/templates/deployment-template.yaml \
    --parameter-overrides \
        version=$VERSION \
        imageVersion=$IMAGEVERSION \
        vpcId=$VPCID \
        subnet1=$SUBNET1 \
        subnet2=$SUBNET2 \
        certificate=$CERTIFICATE \
    --stack-name $STACKNAME$VERSION \
    --region ap-southeast-2