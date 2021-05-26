#!/usr/bin/env bash
url="tiffany-prod.fma.lab.myobdev.com"
msg <<- @
<h4>Endpoint:</h4>
<a href="http://$url">$url</a>
@
buildkite-agent annotate "$msg" --style "success" \
    --context "endpoint" 
