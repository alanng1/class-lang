@echo off

set InfraDemoProjectOutFold=.\Out\InfraDemo-Windows-Release
set InfraProjectOutFold=.\Out\Infra-Windows-Release
set WorkingFold=%cd%

pushd %InfraDemoProjectOutFold%

setlocal
set "PATH=%WorkingFold%\%InfraProjectOutFold%\release;%PATH%" && release\InfraDemo
endlocal

popd