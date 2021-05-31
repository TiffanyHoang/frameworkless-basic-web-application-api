#!/usr/bin/env bash
set -eou pipefail
set -o xtrace
echo '--- :aws: Deploy on AWS'
ENV=$1
STACKNAME=TiffanyDeploymentStack
GITHASH=$(git describe --tags --always)
VPCID='vpc-8aefeced'
SUBNET1='subnet-6df91d25'
SUBNET2='subnet-5818f33e'
IMAGEVERSION=$GITHASH
HOSTEDZONEID='Z03276602ECCECZUV6V61'
if [ $ENV = dev ]
then
VERSION=$GITHASH
CERTIFICATE='arn:aws:acm:ap-southeast-2:274387265859:certificate/7c22e6e5-f370-4893-b40e-ebdfddf73f87'
DOMAINNAME=$VERSION.tiffany-dev.fma.lab.myobdev.com
CNAMERECORDNAME='_11e5c2f4dd01a22721bacb444f57c71b.tiffany-dev.fma.lab.myobdev.com'
CNAMERECORDVALUE='_9053780dc4bbad6b9e746ecb898ecb43.jddtvkljgg.acm-validations.aws.'
else
VERSION='prod'
CERTIFICATE='arn:aws:acm:ap-southeast-2:274387265859:certificate/c62856b3-deea-48e3-aa85-55531dfdcb25'
DOMAINNAME='tiffany-prod.fma.lab.myobdev.com'
CNAMERECORDNAME='_5a4fbae31879e3af450f355b4be7f2cd.tiffany-prod.fma.lab.myobdev.com'
CNAMERECORDVALUE='_75d48eef67f56bc9b8a1ffea8399ede0.zzxlnyslwt.acm-validations.aws.'
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
        hostedZoneId=$HOSTEDZONEID \
        domainName=$DOMAINNAME \
        CNAMERecordName=$CNAMERECORDNAME \
        CNAMERecordValue=$CNAMERECORDVALUE \
    --stack-name $STACKNAME$VERSION \
    --region ap-southeast-2