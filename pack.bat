@echo off
msbuild Mntone.Windows.PerMonitorDpiSupport/Mntone.Windows.PerMonitorDpiSupport.4.0.csproj /p:Configuration=Release /t:Clean;Build
nuget pack Mntone.Windows.PerMonitorDpiSupport/Mntone.Windows.PerMonitorDpiSupport.csproj -Properties Configuration=Release
pause