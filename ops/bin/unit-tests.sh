#!/usr/bin/env bash
echo '--- :docker: Build unit tests image'
docker build -f Dockerfile.test -t test .
echo '--- :docker: Remove unit tests image'
docker rmi test -f