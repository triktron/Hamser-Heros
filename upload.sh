#!/bin/sh

set -x

export buildfolder="$(find . -regex '.\/temp[^\/]*\/WebGL\/Build' -print -quit)"
if [ -z "$buildfolder" ]; then
  echo "Could not find build folder"
  exit 1
fi

if [ ! -d ./tmp ]; then
  git clone "https://${nickname}:${githubkey}@github.com/${nickname}/${repositorie}"-b WebGL ./tmp
fi
cp -r "$buildfolder" ./tmp
cd ./tmp
git add Build
git config --global user.email "$githubemail"
git config --global user.name "$nickname"
git commit -m "unity cloud build $(date '+%d/%m/%Y %H:%M:%S')"
git push --force
