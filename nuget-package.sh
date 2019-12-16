#!/usr/bin/env bash

error() {

  echo ">>>>>> Failed to build <<<<<<<<<"
  echo docker logs app

  exit 1
}

cleanup() {
  echo "....Cleaning up"

  if [ "$(docker images -f "dangling=true" -q | awk '{print $3}' | sort -u)x" != "x" ]
  then
    docker rmi $(docker images --filter "dangling=true" -q --no-trunc)
  fi

  echo ""
  echo "....Cleaning up done"
}

trap error ERR
trap cleanup EXIT

if [ -z "$GO_PIPELINE_COUNTER" ]; then
    export GO_PIPELINE_COUNTER=0
fi

if [ -z "$GO_STAGE_COUNTER" ]; then
    export GO_STAGE_COUNTER=0
fi

if [ -z "$NUGET_SUFFIX" ]; then
    export NUGET_SUFFIX=
fi

if [ -z "$NUGET_MAJOR_VERSION" ]; then
    export NUGET_MAJOR_VERSION=1
fi

echo
echo =============================================================================
echo Packaging NuGet .nupkg file
echo =============================================================================

VERSION=${NUGET_MAJOR_VERSION}.${GO_PIPELINE_COUNTER}.${GO_STAGE_COUNTER}${NUGET_SUFFIX}

NUGETDIR=`pwd`/deployment
SRCDIR=`pwd`

echo
echo =============================================================================
echo Building NuGet package!
echo =============================================================================

docker run --rm \
           -v "$SRCDIR/:/build" \
           -v "$NUGETDIR/:/packages" \
           --workdir /build/ \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet pack -p:PackageVersion=$VERSION -c Release -o /packages ./Core/src/Core.csproj

docker run --rm \
           -v "$SRCDIR/:/build" \
           -v "$NUGETDIR/:/packages" \
           --workdir /build/ \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet pack -p:PackageVersion=$VERSION -c Release -o /packages ./DependencyInjection.Autofac/src/DependencyInjection.Autofac.csproj

docker run --rm \
           -v "$SRCDIR/:/build" \
           -v "$NUGETDIR.gitignore/:/packages" \
           --workdir /build/ \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet pack -p:PackageVersion=$VERSION -c Release -o /packages ./DependencyInjection.Microsoft/src/DependencyInjection.Microsoft.csproj

docker run --rm \
           -v "$SRCDIR/:/build" \
           -v "$NUGETDIR/:/packages" \
           --workdir /build/ \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet pack -p:PackageVersion=$VERSION -c Release -o /packages ./Core.Stores.Console/Core.Stores.Console.csproj

docker run --rm \
           -v "$SRCDIR/:/build" \
           -v "$NUGETDIR/:/packages" \
           --workdir /build/ \
           --name app \
           microsoft/dotnet:2.1-sdk dotnet pack -p:PackageVersion=$VERSION -c Release -o /packages ./Core.Stores.log4net/Core.Stores.log4net.csproj

echo Built!