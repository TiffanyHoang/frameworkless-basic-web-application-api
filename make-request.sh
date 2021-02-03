#bin/bash

for (( i=1; i<=10; i++ ));
do
  curl -i http://localhost:8080?name=${i}
done