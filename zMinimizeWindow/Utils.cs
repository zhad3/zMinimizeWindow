using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace zMinimizeWindow
{
    public delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

    class Utils
    {
        private const int WM_SYSCOMMAND = 0x0112;
        private const uint WM_GETTEXT = 0x000D;
        private const int SW_MINIMIZE = 6;
        private const int SW_FORCEMINIMIZE = 11;
        private const int SW_HIDE = 0;
        private const int SC_MINIMIZE = 0xF020;


        private static List<IntPtr> handles = new List<IntPtr>();
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static List<IntPtr> GetWindows()
        {
            handles = new List<IntPtr>();
            EnumWindows((hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
            return handles;
        }

        internal static void ForceMinimizeWindow(ZProcess Proc)
        {
            ShowWindow(Proc.Handle, SW_FORCEMINIMIZE);
        }

        public static ObservableCollection<ZProcess> GetProcessesList()
        {
            ObservableCollection<ZProcess> procs = new ObservableCollection<ZProcess>();
            foreach (IntPtr hWnd in GetWindows())
            {
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder message = new StringBuilder(1000);
                    SendMessage(hWnd, WM_GETTEXT, message.Capacity, message);
                    procs.Add(new ZProcess(hWnd, message.ToString()));
                }
            }
            return procs;
        }

        internal static void SysHideWindow(ZProcess Proc)
        {
            SendMessage(Proc.Handle, WM_SYSCOMMAND, SC_MINIMIZE, null);
        }

        internal static void HideWindow(ZProcess Proc)
        {
            ShowWindow(Proc.Handle, SW_HIDE);
        }

        public static void UpdateProcessesList(ref ObservableCollection<ZProcess> procs)
        {
            procs.Clear();
            foreach (IntPtr hWnd in GetWindows())
            {
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder message = new StringBuilder(1000);
                    SendMessage(hWnd, WM_GETTEXT, message.Capacity, message);
                    procs.Add(new ZProcess(hWnd, message.ToString()));
                }
            }
        }

        public static void MinimizeWindow(ZProcess Proc)
        {
            ShowWindow(Proc.Handle, SW_MINIMIZE);
        }
    }

    public class ZProcess
    {
        public IntPtr Handle { get; set; }
        public String Name { get; set; }

        public ZProcess(IntPtr hWnd, String name)
        {
            Handle = hWnd;
            Name = name;
        }

        public override String ToString()
        {
            return Name + " [" + Handle + "]";
        }
    }
}
