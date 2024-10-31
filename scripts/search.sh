#!/usr/bin/bash

pattern="$1"

if [ -z "$pattern" ]; then
  pattern=""
fi

currWorkDir=$(pwd)
scriptDir=$(dirname "$0")
cd "$scriptDir" || exit 1
cd ..

files=$(find . -type f -name '*.cs')

for file in $files; do
  if grep -qi "$pattern" "$file"; then
    echo "$file"
  fi
done

cd "$currWorkDir" || exit 1
