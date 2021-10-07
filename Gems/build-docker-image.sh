#!/bin/sh
#
# Run this script from the Gems installation directory to to create a docker image
# Edit servers.xml to configure EMS server details (or copy any preconfigured file) before building the image.
# Requires access the the EMS client libraries, set TIBEMS_ROOT the EMS root installation directory
#
# Example command to run Gems in docker container with display on X11 server:
#
#	docker run -it -e DISPLAY=<X11 Server>:0.0 tibco-gems 
#
# To be able to save configuration changes outside container run with volume set to Gems install dir where servers.xml is located, eg:
#
#	docker run -it -v `pwd`:/tmp/gems -e DISPLAY=<X11 Server>:0.0 tibco-gems /opt/tibco/Gems/rungems.sh -s /tmp/gems/servers.xml
#

DOCKER_BUILD_DIR=/tmp/gems.docker.build

if [ -z $TIBEMS_ROOT ]
then
    TIBEMS_ROOT=../install/tibco/8.6
fi

if [ ! -r $TIBEMS_ROOT ]
then
    echo "Set TIBEMS_ROOT environment variable according to your EMS installation"
    exit 1
fi

rm -rf $DOCKER_BUILD_DIR
mkdir -p $DOCKER_BUILD_DIR

if [ ! -r $DOCKER_BUILD_DIR ]
then
    echo "Failed to create build directory:" $DOCKER_BUILD_DIR
    exit 1
fi

if [ ! -r ./Gems.jar ]
then
    echo "Please run this script from you Gems install directory"
    exit 1
fi

cp -R . $DOCKER_BUILD_DIR/Gems

cp $TIBEMS_ROOT/lib/*.jar $DOCKER_BUILD_DIR/Gems/lib


cat > ${DOCKER_BUILD_DIR}/Dockerfile <<DOCKERFILE

FROM ubuntu:16.04

RUN apt-get update && apt-get -y upgrade
RUN apt-get install -y openjdk-8-jdk

COPY . /opt/tibco

ENV PATH /bin:/usr/bin:/opt/tibco/Gems
ENV TIBEMS_ROOT /opt/tibco/Gems

LABEL "com.tibco.gems.image" ""
LABEL "com.tibco.gems.image.version" "5.1"

WORKDIR /opt/tibco/Gems
CMD ["rungems.sh"]
DOCKERFILE

docker build -f ${DOCKER_BUILD_DIR}/Dockerfile -t tibco-gems ${DOCKER_BUILD_DIR}
