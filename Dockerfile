FROM centos:latest
ADD ./rpm/ /rpm/
RUN yum install -y rpm/*.rpm
WORKDIR /shared
ENTRYPOINT ["/opt/tibco/ems/8.6/bin/tibemsd"]
EXPOSE 7222