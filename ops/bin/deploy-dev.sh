#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS ECS'
commit=$(git describe --tags --always)
vpcId='vpc-d88094bf'
subnet1='subnet-0ecc2546'
subnet2='subnet-316e9257'
certificate='arn:aws:acm:ap-southeast-2:138666658526:certificate/026583fe-93ca-4b50-9735-575fcf92a19d'
domainName='tiffany.fma.lab.myobdev.com'
CNAMERecordName='_4956d49111f1819354882aa5f1f592fb.tiffany.fma.lab.myobdev.com'
CNAMERecordValue='_eae68a1120e0e48d65223094bd79688e.bbfvkzsszw.acm-validations.aws.'
ECRAccount='138666658526.dkr.ecr.ap-southeast-2.amazonaws.com'
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