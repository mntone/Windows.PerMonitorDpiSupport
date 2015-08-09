# Per-monitor DPI Support Library

[![NuGet](https://img.shields.io/nuget/v/Mntone.Windows.PerMonitorDpiSupport.svg?style=flat-square)](https://www.nuget.org/packages/Mntone.Windows.PerMonitorDpiSupport/) [![Downloads](https://img.shields.io/nuget/dt/Mntone.Windows.PerMonitorDpiSupport.svg?style=flat-square)](https://www.nuget.org/packages/Mntone.Windows.PerMonitorDpiSupport/) [![License](https://img.shields.io/github/license/mntone/Windows.PerMonitorDpiSupport.svg?style=flat-square)](https://github.com/mntone/Mntone.Windows.PerMonitorDpiSupport/blob/master/LICENSE.txt)

This library includes class `Window` that handles Per-monitor DPI.

## How to

### Required

1. Add app.manifest to a project.
2. Add next:

	```xml
	<?xml version="1.0" encoding="utf-8"?>
	<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
	  <application xmlns="urn:schemas-microsoft-com:asm.v3">
	    <windowsSettings>
	      <dpiAware xmlns="http://schemas.microsoft.com/SMI/2005/WindowsSettings">True/PM</dpiAware>
	    </windowsSettings>
	  </application>
	</assembly>
	```

### Better

1. Open App.config.
2. Add next:

	```xml
	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
		<runtime>
			<AppContextSwitchOverrides value="Switch.MS.Internal.DoNotApplyLayoutRoundingToMarginsAndBorderThickness=false" />
		</runtime>
		<appSettings>
			<add key="EnableMultiMonitorDisplayClipping" value="true" />
		</appSettings>
	</configuration>
	```


## Behaviors

### Case #1: Change settings

User can change DPI with using control panel. Then, Windows throw event `WM_DPICHANGED`.

In this case, it handles DPI change (monitor DPI) and resize (independence DPI settings).

### Case #2: Move window

Users can move Window with clicking caption bar. Then, Windows throw event `WM_DPICHANGED` when it detect DPI change.

In this case, it handles DPI change (monitor DPI) and resize (independence DPI settings). (the same as case #1)

### Case #3: Resize window

User can resize Window with clicking resize border. Then, Windows throw event `WMM_DPICHANGED` when it detect DPI change.

In this case, it handles DPI change (monitor DPI) only. It does NOT handle resize.

## License

Under MIT license.

## Contact

mntone (name: monotone): reply to [@mntone](https://twitter.com/mntone/) directly