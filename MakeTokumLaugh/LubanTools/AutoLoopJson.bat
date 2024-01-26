@echo off

cd /d %~dp0
set ROOT_PATH=%~dp0
set WORKSPACE=..
set INPUT_DATA_DIR=%ROOT_PATH%\LubanTools\Datas
set OUTPUT_VERSION_DIR=%ROOT_PATH%..\GenerateDatas\LubanConfig
set LUBANCOM=.\
call %LubanCom%LoopJson.bat
pause