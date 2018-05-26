# zMinimizeWindow
Quick and dirty minimizer for applications on windows that cannot be normally minimized.

# Windows Binary
[Download](https://github.com/zhad3/zMinimizeWindow/releases/download/1.0/zMinimizeWindow_1.0.zip)

# What the buttons do
Button|Function
---|---
Get Windows|Fetch processes of visible windows
Minimize selected window(s)|Win32 API - ShowWindow(hWnd, SW\_HIDE)
Force Minimize|Win32 API - ShowWindow(hWnd, SW\_FORCEMINIMIZE)
Hide Window|Win32 API - ShowWindow(hWnd, SW\_HIDE)
Still visible?!|Win32 API - SendMessage(hWnd, WM\_SYSCOMMAND, SC\_MINIMIZE, null)

