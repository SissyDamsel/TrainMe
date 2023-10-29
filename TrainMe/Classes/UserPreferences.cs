/*
	Copyright (C) 2021 Damsel

	This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

	You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMe.Classes {
    public static class UserPreferences {
        public static double Opacity {
            get {
                return double.Parse(GetPreferences()[0], CultureInfo.InvariantCulture);               
            }
        }
        public static double Volume {
            get {               
                return double.Parse(GetPreferences()[1], CultureInfo.InvariantCulture);
            }
        }
        const string filename = "preferences.ini";

        public static void SavePreferences(double opacity, double volume) {
            File.WriteAllText(filename, opacity + ";" + volume);
        }

        public static void SaveOpacity(double opacity) {
            double volume = Volume;
            File.WriteAllText(filename, opacity + ";" + volume);
        }
        public static void SaveVolume(double volume) {
            double opacity = Opacity;
            File.WriteAllText(filename, opacity + ";" + volume);
        }

        public static string[] GetPreferences() {
            if(File.Exists(filename))
                return File.ReadAllText(filename).Split(';');
            return new string[] { "0.2", "0.5" };
        }
    }
}
