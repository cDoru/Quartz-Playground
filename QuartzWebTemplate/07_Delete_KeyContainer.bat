@echo off
REM Deletes the Key Container called "QuartzPocContainer" on your local computer
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -pz QuartzPocContainer

REM Also delete the file:
del /s %~dp0ExportedContainers\QuartzPocContainer.xml
pause