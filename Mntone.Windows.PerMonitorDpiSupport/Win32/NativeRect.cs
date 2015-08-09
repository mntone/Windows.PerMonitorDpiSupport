namespace Mntone.Windows.PerMonitorDpiSupport.Win32
{
	public struct NativeRect
	{
		private int _left, _top, _right, _bottom;

		public int Left
		{
			get { return this._left; }
			set { this._left = value; }
		}
		public int Top
		{
			get { return this._top; }
			set { this._top = value; }
		}
		public int Right
		{
			get { return this._right; }
			set { this._right = value; }
		}
		public int Bottom
		{
			get { return this._bottom; }
			set { this._bottom = value; }
		}

		public int Width => this.Right - this.Left;
		public int Height => this.Bottom - this.Top;
	}
}