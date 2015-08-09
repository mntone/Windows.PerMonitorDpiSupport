using System;
using System.Runtime.InteropServices;

namespace Mntone.Windows.PerMonitorDpiSupport.Win32
{
	public static class NativeMethods
	{
		private const string User32Library = "user32.dll";
		private const string ShCoreLibrary = "ShCore.dll";

		[DllImport(User32Library)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);

		[DllImport(ShCoreLibrary, SetLastError = false, PreserveSig = false)]
		public static extern void GetProcessDpiAwareness(IntPtr hprocess, out ProcessDpiAwareness value);

		[DllImport(ShCoreLibrary, SetLastError = false)]
		public static extern int SetProcessDpiAwareness(ProcessDpiAwareness value);

		[DllImport(ShCoreLibrary, SetLastError = false, PreserveSig = false)]
		public static extern void GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, ref uint dpiX, ref uint dpiY);

		[DllImport(User32Library, SetLastError = true)]
		public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorDefaultTo dwFlags);
	}
}