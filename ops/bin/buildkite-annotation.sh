#!/usr/bin/env bash
url='https://tiffany-app.svc.platform.myobdev.com'
buildkite-agent annotate "<a href='$url'>$url</a> 🚀" --style 'success' --context 'ctx-success'
