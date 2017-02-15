@echo off
REM This Creates a Key Container called "QuartzPocContainer" of your local computer
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -pc QuartzPocContainer -size 4096 -exp
pause