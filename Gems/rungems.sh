#!/bin/sh

# set the EMS root installation directory here (only client libraries required)
if [ -z $TIBEMS_ROOT ]
then
    TIBEMS_ROOT=../install/tibco/8.6
fi

if [ ! -r $TIBEMS_ROOT ]
then
    echo "Set TIBEMS_ROOT environment variable according to your EMS installation"
    exit 1
fi

TIBEMS_JAVA=${TIBEMS_ROOT}/lib
#Change to jms.jar for EMS7 or lower
CLASSPATH=${TIBEMS_JAVA}/jms-2.0.jar:${TIBEMS_JAVA}/jndi.jar
CLASSPATH=./Gems.jar:looks-2.3.1.jar:jcommon-1.0.16.jar:jfreechart-1.0.13.jar:${TIBEMS_JAVA}/tibjms.jar:${TIBEMS_JAVA}/tibcrypt.jar:${TIBEMS_JAVA}/tibjmsadmin.jar:${CLASSPATH}

# For pre EMS8.4 use these libs for SSL:
CLASSPATH=${CLASSPATH}:${TIBEMS_JAVA}/slf4j-api-1.5.2.jar:${TIBEMS_JAVA}/slf4j-simple-1.5.2.jar
# For pre EMS8.3 use these libs for SSL:
#CLASSPATH=${CLASSPATH}:${TIBEMS_JAVA}/slf4j-api-1.4.2.jar:${TIBEMS_JAVA}/slf4j-simple-1.4.2.jar

## Charting libs required, download from www.jfree.org/jfreechart and place Gems lib folder
CLASSPATH=${CLASSPATH}:lib/jcommon-1.0.23.jar:lib/jfreechart-1.0.19.jar

#echo ${CLASSPATH}
java -classpath ${CLASSPATH} -Xmx512m -DPlastic.defaultTheme=DesertBluer com.tibco.gems.Gems -p gems.props $@
