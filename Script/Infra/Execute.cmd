@echo off

call Script\Infra\Var

set InfraDemoProjectOutFold=.\Out\InfraDemo-Windows-Release
set InfraProjectOutFold=.\Out\Infra-Windows-Release
set WorkingFold=%cd%

pushd %InfraDemoProjectOutFold%

setlocal
set "PATH=%WorkingFold%\%InfraProjectOutFold%\release;%PATH%" && release\InfraDemo
echo Status: %errorlevel%
endlocal

popd