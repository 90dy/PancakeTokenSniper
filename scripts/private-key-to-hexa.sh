#!/bin/sh
set -o pipefail # Use last non-zero exit code in a pipeline.
set -o nounset # Exit if you try to use an uninitialised variable.
set -o errexit # Exit the script if any statement returns a non-true return value.
set -o errtrace # Any trap on ERR is inherited by subshells.
set -o functrace # The DEBUG trap is inherited by subshells.
# set -o xtrace # Print commands and their arguments as they are executed.
set -o verbose # Print shell input lines as they are read.

# check if arg 1 exist
if [ -z "$1" ]; then
	echo "Usage: $0 <private_key_base64>"
	exit 1
fi

# Convert a private key to hexa base on arg 1.
echo "$1" | base64 -d | od -A n -t x1 | sed 's/ *//g' | tr -d '\n'
