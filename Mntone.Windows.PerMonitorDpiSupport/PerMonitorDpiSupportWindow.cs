using Mntone.Windows.PerMonitorDpiSupport.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Mntone.Windows.PerMonitorDpiSupport
{
	[TemplatePart(Name = WindowResizeGrip, Type = typeof(FrameworkElement))]
	public class PerMonitorDpiSupportWindow : Window
	{
		private const string WindowResizeGrip = "WindowResizeGrip";

		static PerMonitorDpiSupportWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PerMonitorDpiSupportWindow), new FrameworkPropertyMetadata(typeof(PerMonitorDpiSupportWindow)));
		}

		private bool _hooked = false;
		private HwndSource _source = null;

		private bool _isPerMonitorDpiSupported = false;
		private Dpi _systemDpi;
		private Dpi _currentDpi;
		
		private bool _borderClicked = false;

		public Transform DpiScaleTransform
		{
			get { return (Transform)this.GetValue(DpiScaleTransformProperty); }
			set { this.SetValue(DpiScaleTransformProperty, value); }
		}

		public static readonly DependencyProperty DpiScaleTransformProperty
			= DependencyProperty.Register("DpiScaleTransform", typeof(Transform), typeof(PerMonitorDpiSupportWindow), new UIPropertyMetadata(Transform.Identity));

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			this._source = PresentationSource.FromVisual(this) as HwndSource;
			if (this._source == null) return;

			this._systemDpi = this._source.GetSystemDpi();
			this._isPerMonitorDpiSupported = PerMonitorDpiHelper.IsSupported;
			if (this._isPerMonitorDpiSupported)
			{
				this._currentDpi = this._source.GetMonitorDpi();
				this.OnDpiChanged(this._currentDpi, new NativeRect());
				this._source.AddHook(this.WindowProcedure);
				this._hooked = true;
			}
			else
			{
				this._currentDpi = this._systemDpi;
			}

		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			if (this._hooked)
			{
				this._source.RemoveHook(this.WindowProcedure);
				this._hooked = false;
			}
		}

		private IntPtr WindowProcedure(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			var lresult = IntPtr.Zero;
			switch ((WindowMessage)message)
			{
				case WindowMessage.NonClientAreaHitTest:
					handled = true;
					lresult = OnNonClientAreaHitTest(hwnd, message, wParam, lParam);
					break;

				case WindowMessage.DpiChanged:
					{
						var dpiX = wParam.ToLoWord();
						var dpiY = wParam.ToHiWord();
						var suggested = (NativeRect)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(NativeRect));
						this.OnDpiChanged(new Dpi(dpiX, dpiY), suggested);
						handled = true;
						break;
					}

				case WindowMessage.EnterSizeMove:
					this.OnEnterSizeMove();
					break;

				case WindowMessage.ExitSizeMove:
					this.OnExitSizeMove();
					break;
			}
			return lresult;
		}

		protected virtual IntPtr OnNonClientAreaHitTest(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam)
		{
			var ret = NativeMethods.DefWindowProc(hwnd, message, wParam, lParam);
			var ht = (HitTest)ret;
			if (ht == HitTest.Left || ht == HitTest.Right
				|| ht == HitTest.Top || ht == HitTest.TopLeft || ht == HitTest.TopRight
				|| ht == HitTest.Bottom || ht == HitTest.BottomLeft || ht == HitTest.BottomRight)
			{
				this._borderClicked = true;
			}
			return ret;
		}

		protected virtual void OnDpiChanged(Dpi suggestedDpi, NativeRect suggested)
		{
			this.DpiScaleTransform = (suggestedDpi == this._systemDpi)
				? Transform.Identity
				: new ScaleTransform((double)suggestedDpi.X / this._systemDpi.X, (double)suggestedDpi.Y / this._systemDpi.Y);

			if (!this._borderClicked)
			{
				this.Width = this.Width * suggestedDpi.X / this._currentDpi.X;
				this.Height = this.Height * suggestedDpi.Y / this._currentDpi.Y;
			}

			this._currentDpi = suggestedDpi;
		}

		protected virtual void OnEnterSizeMove()
		{ }

		protected virtual void OnExitSizeMove()
		{
			this._borderClicked = false;
		}
	}
}
