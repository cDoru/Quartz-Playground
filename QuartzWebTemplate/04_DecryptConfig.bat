@echo off
REM Decrypt the connectionStrings section of the web.config in
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -pdf "connectionStrings" "%cd%"
pause