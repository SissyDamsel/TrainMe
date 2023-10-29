/*
	Copyright (C) 2021 Damsel

	This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

	You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainMe.Classes {
    public class ScreenViewer {
        public Screen Screen;
        public int ID;
        internal static int Last_ID = 0;
        public ScreenViewer(Screen screen) {
            Screen = screen;
            ID = Last_ID;
            Last_ID++;
        }

        public override string ToString() {
            return "Monitor " + ID;
        }
    }
}
