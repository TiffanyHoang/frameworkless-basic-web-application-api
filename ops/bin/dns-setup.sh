#!/usr/bin/env bash
echo '--- :aws: Deploy on AWS'
HOSTEDZONEID='Z03276602ECCECZUV6V61'
DOMAINNAME='tiffany-prod.fma.lab.myobdev.com'
CNAMERECORDNAME='_5a4fbae31879e3af450f355b4be7f2cd.tiffany-prod.fma.lab.myobdev.com'
CNAMERECORDVALUE='_75d48eef67f56bc9b8a1ffea8399ede0.zzxlnyslwt.acm-validations.aws.'
aws cloudformation deploy \
    --template-file ./aws/templates/records-template.yaml \
    --parameter-overrides \
        hostedZoneId=$HOSTEDZONEID \
        domainName=$DOMAINNAME \
        CNAMERecordName=$CNAMERECORDNAME \
        CNAMERecordValue=$CNAMERECORDVALUE \
    --stack-name tiffany-hosted-zone-records \
    --region ap-southeast-2