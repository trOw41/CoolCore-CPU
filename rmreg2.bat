@echo off 
 
setlocal


reg delete "HKCU\Software\Microsoft\Windows\CurrentVersion\Run" /v "CoolCore-CPU" /f
