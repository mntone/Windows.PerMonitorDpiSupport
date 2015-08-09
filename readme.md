# Per-monitor DPI Support Library

This library includes class `Window` that handles Per-monitor DPI.

## Case #1: Change settings

User can change DPI with using control panel. Then, Windows throw event `WM_DPICHANGED`.

In this case, it handles DPI change (monitor DPI) and resize (independence DPI settings).

## Case #2: Move window

Users can move Window with clicking caption bar. Then, Windows throw event `WM_DPICHANGED` when it detect DPI change.

In this case, it handles DPI change (monitor DPI) and resize (independence DPI settings). (the same as case #1)

## Case #3: Resize window

User can resize Window with clicking resize border. Then, Windows throw event `WMM_DPICHANGED` when it detect DPI change.

In this case, it handles DPI change (monitor DPI) only. It does NOT handle resize.

## License

Under MIT license.

## Contact

mntone (name: monotone): reply to [@mntone](https://twitter.com/mntone/) directly