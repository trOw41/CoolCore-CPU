@echo off

setlocal
set "ProgramFiles=%ProgramFiles(x86)%"
set "CoolCore-CPUExePath=%ProgramFiles(x86)%\CoolCore-CPU\CoolCore.exe
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "CoolCore-CPU"  /d "%CoolCore-CPUExePath%" /f



