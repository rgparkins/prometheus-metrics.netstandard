#!/usr/bin/env bash

error() {

  echo ">>>>>> Failed to build <<<<<<<<<"
  echo docker logs app

  exit 1
}

cleanup() {
  echo "....Cleaning up"
  
  # remove untagged images (these are left behind when docker run fails)
  if [ $(docker images | grep '^<none>' | wc -c) -gt 0 ]; then
    docker images | grep "^<none>" | tr -s " " " " | cut -f3 -d" " | ifne xargs docker rmi
  fi
  echo ""
  echo "....Cleaning up done"
}

trap error ERR
trap cleanup EXIT

echo
echo ===========================================================
echo Building containers
echo ===========================================================
echo

SRCDIR=`pwd`

docker run --rm \
           -v "$SRCDIR/:/build/" \
           --workdir /build/Core.Tests \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet test

docker run --rm \
           -v "$SRCDIR/:/build/" \
           --workdir /build/DependencyInjection.Autofac.Tests \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet test

docker run --rm \
           -v "$SRCDIR/:/build/" \
           --workdir /build/DependencyInjection.Microsoft.Tests \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet test