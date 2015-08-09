using System;

namespace Mntone.Windows.PerMonitorDpiSupport.Win32
{
	public static class NativeExtensions
	{
		public static ushort ToLoWord(this IntPtr dword)
		{
			return (ushort)((uint)dword & 0xffff);
		}

		public static ushort ToHiWord(this IntPtr dword)
		{
			return (ushort)((uint)dword >> 16);
		}
	}
}
