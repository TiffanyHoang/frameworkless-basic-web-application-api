docker build -f ./ops/docker/Dockerfile.Test -t test .

docker build -t app ..

docker run -e PORT=123 -p 8080:123 app