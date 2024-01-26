@echo off
set WORKSPACE=..
set GAMESPACE=../Assets/GameMain

set GEN_CLIENT=Tools\Luban\Luban.dll
set CONF_ROOT=%WORKSPACE%\LubanTools

dotnet %GEN_CLIENT% ^
    -t client ^
    -c cs-simple-json ^
    -d json  ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=%GAMESPACE%/Scripts/LubanGen ^
    -x outputDataDir=%GAMESPACE%/Data/ConfigData/json
rem    -x pathValidator.rootDir=%WORKSPACE% 
rem    -x l10n.textProviderFile=*@%WORKSPACE%\LubanTools\Datas\l10n\texts.json

pause
%0