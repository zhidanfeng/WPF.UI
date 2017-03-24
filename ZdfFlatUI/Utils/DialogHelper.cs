using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace ZdfFlatUI.Utils
{
    public class DialogHelper
    {
        static Window GetWindowFromHwnd(IntPtr hwnd)
        {
            return (Window)HwndSource.FromHwnd(hwnd).RootVisual;
        }

        //GetForegroundWindow API
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //调用GetForegroundWindow然后调用GetWindowFromHwnd
        static Window GetTopWindow()
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == null)
                return null;

            return GetWindowFromHwnd(hwnd);
        }

        //显示对话框并自动设置Owner
        public static void ShowDialog(Window win)
        {
            win.Owner = GetTopWindow();
            win.ShowInTaskbar = false;
            win.ShowDialog();
        }

        /// <summary>
        /// 适用于MessageBox形式的Window
        /// </summary>
        /// <param name="win"></param>
        /// <param name="dependencyObject"></param>
        public static void ShowDialog(Window win, DependencyObject dependencyObject)
        {
            win.Owner = Window.GetWindow(dependencyObject);
            win.ShowInTaskbar = false;
            win.ShowActivated = true;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }
    }
}
