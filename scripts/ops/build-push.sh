echo 'Build & push app to ECR'

docker build -f Dockerfile -t 138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/frameworkless-basic-web-app-tiffany .

docker push 138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/frameworkless-basic-web-app-tiffany

docker rmi 138666658526.dkr.ecr.ap-southeast-2.amazonaws.com/frameworkless-basic-web-app-tiffany -f