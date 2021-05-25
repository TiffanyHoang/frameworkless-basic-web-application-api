#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS'
hostedZoneId='Z03276602ECCECZUV6V61'
domainName='tiffany-prod.fma.lab.myobdev.com'
CNAMERecordName='_5a4fbae31879e3af450f355b4be7f2cd.tiffany-prod.fma.lab.myobdev.com'
CNAMERecordValue='_75d48eef67f56bc9b8a1ffea8399ede0.zzxlnyslwt.acm-validations.aws.'
aws cloudformation deploy \
    --template-file ./aws/records-template.yaml \
    --parameter-overrides \
        hostedZoneId=$hostedZoneId \
        domainName=$domainName \
        CNAMERecordName=$CNAMERecordName \
        CNAMERecordValue=$CNAMERecordValue \
    --stack-name tiffany-test-records \
    --region ap-southeast-2