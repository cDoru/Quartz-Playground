@echo off
REM Exports the "QuartzPocContainer" Key Container to an XML File
if not exist %~dp0ExportedContainers\ mkdir %~dp0ExportedContainers\
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -px QuartzPocContainer %~dp0ExportedContainers\QuartzPocContainer.xml -pri
pause