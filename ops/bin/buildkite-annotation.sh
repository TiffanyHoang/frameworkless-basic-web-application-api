#!/usr/bin/env bash
url='https://tiffany-prod.fma.lab.myobdev.com'
buildkite-agent annotate "<a href='$url'>$url</a> 🚀" --style 'success' --context 'ctx-success'
