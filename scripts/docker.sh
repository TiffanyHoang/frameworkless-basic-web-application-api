docker build --target build -t test-build ..
docker build --target run -t test-run ..

docker run -e PORT=123 -p 8080:123 test-run