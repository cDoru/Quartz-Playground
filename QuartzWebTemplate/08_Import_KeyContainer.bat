@echo off
REM Imports the "QuartzPocContainer" Key Container from an XML File
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -pi QuartzPocContainer %~dp0ExportedContainers\QuartzPocContainer.xml
pause