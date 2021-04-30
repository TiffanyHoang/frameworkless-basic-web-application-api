#!/usr/bin/env bash
echo '--- :docker: Build unit tests image'
docker-compose build test
echo '--- :docker: Remove unit tests image'
docker-compose down --rmi all