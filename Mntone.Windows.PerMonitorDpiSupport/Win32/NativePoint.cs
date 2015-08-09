namespace Mntone.Windows.PerMonitorDpiSupport.Win32
{
	public struct NativeRoint
	{
		private int _x, _y;

		public int X
		{
			get { return this._x; }
			set { this._x = value; }
		}
		public int Y
		{
			get { return this._y; }
			set { this._y = value; }
		}
	}
}