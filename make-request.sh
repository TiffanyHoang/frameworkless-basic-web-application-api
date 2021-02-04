#bin/bash

for (( i=1; i<=10; i++ ));
do
  sleep 0.1
  # without &: request in order and wait for each response then do next request
  # with &: request in order and not wait for response
  curl -i http://localhost:8080?index=${i} & 
done