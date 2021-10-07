@echo off

rem set the EMS root installation directory here (only client libraries required)

set TIBEMS_ROOT=C:\tibco\EMS\8.5
set JMS_JAR=jms-2.0.jar

rem Uncomment if EMS version is pre 8.0
rem set JMS_JAR=jms.jar

set PROPS_FILE=gems.props
set SERVERS_FILE=servers.xml

IF NOT "%1"=="" set PROPS_FILE=%1

rem ## 
rem ## Set classpath to client libs (EMS client and JFreeChart jars required)
rem ## 
echo TIBEMS_ROOT=%TIBEMS_ROOT%
IF EXIST %TIBEMS_ROOT%\clients\java set TIBEMS_JAVA=%TIBEMS_ROOT%\clients\java
IF EXIST %TIBEMS_ROOT%\lib set TIBEMS_JAVA=%TIBEMS_ROOT%\lib

if NOT EXIST %TIBEMS_JAVA%\tibjms.jar goto badenv
if NOT EXIST %TIBEMS_JAVA%\tibjmsadmin.jar goto badenv
if NOT EXIST %TIBEMS_JAVA%\%JMS_JAR% goto badjms

set CLASSPATH=Gems.jar;%TIBEMS_JAVA%\%JMS_JAR%;%TIBEMS_JAVA%\tibjms.jar;%TIBEMS_JAVA%\tibjmsadmin.jar

rem ## Only required for pre EMS 8.4
if EXIST %TIBEMS_JAVA%\tibcrypt.jar (
    set CLASSPATH=%CLASSPATH%;%TIBEMS_JAVA%\tibcrypt.jar
) 

rem ## Libs required for SSL connections and password encryption:
if EXIST %TIBEMS_JAVA%\slf4j-api-1.5.2.jar (
    set CLASSPATH=%CLASSPATH%;%TIBEMS_JAVA%\slf4j-api-1.5.2.jar;%TIBEMS_JAVA%\slf4j-simple-1.5.2.jar
) else if EXIST %TIBEMS_JAVA%\slf4j-api-1.4.2.jar (
    set CLASSPATH=%CLASSPATH%;%TIBEMS_JAVA%\slf4j-api-1.4.2.jar;%TIBEMS_JAVA%\slf4j-simple-1.4.2.jar
)

rem ## Charting libs required, download from www.jfree.org/jfreechart and place Gems lib folder
set CLASSPATH=%CLASSPATH%;lib\jcommon-1.0.23.jar;lib\jfreechart-1.0.19.jar

java -classpath %CLASSPATH% -Xmx512m -Dswing.metalTheme=steel -DPlastic.defaultTheme=DesertBluer com.tibco.gems.Gems -p %PROPS_FILE% -s %SERVERS_FILE%

rem JGoodies L&F theme may be set on the command line as below:
rem java -classpath %CLASSPATH% -Xmx128m -DPlastic.defaultTheme=DesertBluer com.tibco.gems.Gems gems.props
rem JGoodies themes available:
rem BrownSugar, DarkStar, DesertBlue, DesertBluer, DesertGreen, DesertRed,
rem DesertYellow, ExperienceBlue, ExperienceGreen, ExperienceRoyale, LightGray,
rem Silver, SkyBlue, SkyBluer, SkyGreen, SkyKrupp, SkyPink, SkyRed, SkyYellow,

IF ERRORLEVEL 1 goto err


goto end

:badenv
echo .
echo Error: TIBEMS_ROOT variable is not set or does not correctly specify
echo the root directory of the TIBCO Enterprise Message Service software.
echo Please correct the TIBEMS_ROOT variable at the beginning of this script.
echo .
pause
goto end

:badjms
echo .
echo Error: JMS_JAR variable is not set or does not correctly specify
echo the JMS jar file in the EMS installation.
echo Please correct the JMS_JAR variable at the beginning of this script.
echo .
pause
goto end

:err
echo .
echo Error starting Gems
echo Ensure you have Java 1.6 or higher in your path (1.7 for EMS8)
echo .
pause

:end
