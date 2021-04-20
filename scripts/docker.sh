docker build -t test-run ..

docker run -e PORT=123 -p 8080:123 test-run