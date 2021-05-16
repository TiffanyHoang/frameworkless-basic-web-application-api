#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS ECS'
commit=$(git describe --tags --always)
vpcId='vpc-8aefeced'
subnet1='subnet-6df91d25'
subnet2='subnet-5818f33e'
certificate='arn:aws:acm:ap-southeast-2:274387265859:certificate/c62856b3-deea-48e3-aa85-55531dfdcb25'
domainName='tiffany-prod.fma.lab.myobdev.com'
CNAMERecordName='_5a4fbae31879e3af450f355b4be7f2cd.tiffany-prod.fma.lab.myobdev.com'
CNAMERecordValue='_75d48eef67f56bc9b8a1ffea8399ede0.zzxlnyslwt.acm-validations.aws.'
ECRAccount='274387265859.dkr.ecr.ap-southeast-2.amazonaws.com'
aws cloudformation deploy \
    --template-file ./aws/deployment-template.yaml \
    --parameter-overrides \
        version=$commit \
        vpcId=$vpcId \
        subnet1=$subnet1 \
        subnet2=$subnet2 \
        certificate=$certificate \
        domainName=$domainName \
        CNAMERecordName=$CNAMERecordName \
        CNAMERecordValue=$CNAMERecordValue \
        ECRAccount=$ECRAccount \
    --stack-name tiffany-deployment-stack \
    --region ap-southeast-2