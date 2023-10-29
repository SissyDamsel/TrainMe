/*
	Copyright (C) 2021 Damsel

	This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

	You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace TrainMe.Classes {
    public static class WindowServices {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        public static void ToTransparentWindow(this Window x) {

            x.SourceInitialized +=
                delegate {
                // Get this window's handle
                IntPtr hwnd = new WindowInteropHelper(x).Handle;

                // Change the extended window style to include WS_EX_TRANSPARENT
                int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

                    SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
                };


        }
        public static Screen[] GetAllScreens()
        {
            return Screen.AllScreens;
        }
        public static int GetNumberOfScreens()
        {
            return Screen.AllScreens.Length;
        }

        public static List<ScreenViewer> GetAllScreenViewers() {
            List<ScreenViewer> list = new List<ScreenViewer>();
            foreach(Screen screen in GetAllScreens()) {
                list.Add(new ScreenViewer(screen));
            }
            return list;
        }

        public static void MoveWindowToScreen(Window window, Screen screen) {
            System.Drawing.Rectangle r = screen.WorkingArea;
            window.Top = r.Top;
            window.Left = r.Left;
            window.Width = r.Width;
            window.Height = r.Height;
        }
    }
}
