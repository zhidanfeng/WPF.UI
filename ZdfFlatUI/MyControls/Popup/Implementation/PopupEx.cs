using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace ZdfFlatUI
{
    public class MaskLayer : Popup
    {
        #region TopMost 设置Popup是否每次都置顶

        public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(MaskLayer), new FrameworkPropertyMetadata(false, OnTopmostChanged));
        public bool Topmost
        {
            get { return (bool)GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }
        private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as MaskLayer).UpdateWindow();
        }

        #endregion

        protected override void OnOpened(EventArgs e)
        {
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            var hwnd = ((HwndSource)PresentationSource.FromVisual(this)).Handle;
            RECT rect;
            if (GetWindowRect(hwnd, out rect))
            {
                FrameworkElement element = this.PlacementTarget as FrameworkElement;
                if(element != null)
                {
                    //第二个参数和最后一个参数为关键参数，设置为1表示保持窗口大小，网上的代码是0，如果设置为0会导致Popup弹出时同时更改了PlacementTarget的大小
                    SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)element.ActualWidth, (int)element.ActualHeight, 1);
                }
                else
                {
                    SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)this.Width, (int)this.Height, 1);
                }
            }
        }

        #region imports definitions
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32", EntryPoint = "SetWindowPos")]
        private static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        #endregion
    }
}
