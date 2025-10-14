@echo off
setlocal enabledelayedexpansion

del headers.txt 2>nul

for %%f in (*.h) do (
    echo ---------------------------------------- >> headers.txt
    echo %%f >> headers.txt
    echo ---------------------------------------- >> headers.txt
    type "%%f" >> headers.txt
    echo. >> headers.txt
)

endlocal
