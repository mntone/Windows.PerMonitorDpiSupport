using Mntone.Windows.PerMonitorDpiSupport.Win32;
using System;
using System.Windows.Interop;

namespace Mntone.Windows.PerMonitorDpiSupport
{
	public static class PerMonitorDpiHelper
	{
		public static bool IsSupported
		{
			get
			{
				var version = Environment.OSVersion.Version;
				if (version.Major == 6 && version.Minor >= 3 || version.Major >= 7)
				{
					var awareness = ProcessDpiAwareness.DpiUnaware;
					NativeMethods.GetProcessDpiAwareness(IntPtr.Zero, out awareness);
					return awareness == ProcessDpiAwareness.PerMonitorDpiAware;
				}
				return false;
			}
		}

		public static Dpi GetSystemDpi(this HwndSource hwndSource)
		{
			return new Dpi(
				(ushort)(Dpi.Default.X * hwndSource.CompositionTarget.TransformToDevice.M11),
				(ushort)(Dpi.Default.Y * hwndSource.CompositionTarget.TransformToDevice.M22));
		}

		public static Dpi GetMonitorDpi(this HwndSource hwndSource,
			MonitorDefaultTo defaultTo = MonitorDefaultTo.Nearest,
			MonitorDpiType dpiType = MonitorDpiType.Default)
		{
			var hmonitor = NativeMethods.MonitorFromWindow(
				hwndSource.Handle,
				defaultTo);

			uint dpiX = 1, dpiY = 1;
			NativeMethods.GetDpiForMonitor(hmonitor, dpiType, ref dpiX, ref dpiY);

			return new Dpi((ushort)dpiX, (ushort)dpiY);
		}
	}
}
