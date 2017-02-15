@echo off
REM Encrypt the ConnectionStrings section of the web.config in
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -pef "connectionStrings" "%cd%" -prov RSAEncryptionProvider
pause