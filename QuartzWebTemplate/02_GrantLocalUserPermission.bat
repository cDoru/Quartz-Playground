@echo off
REM Grant Local User Account Permission to the "QuartzPocContainer" Key Container
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -pa QuartzPocContainer "%userdomain%\%username%"
pause