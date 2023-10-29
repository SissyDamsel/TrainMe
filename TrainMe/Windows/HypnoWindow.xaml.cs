/*
	Copyright (C) 2021 Damsel

	This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

	You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrainMe.Classes;

namespace TrainMe.Windows {
    /// <summary>
    /// Interaction logic for HypnoWindow.xaml
    /// </summary>
    public partial class HypnoWindow : Window {
        string[] files;
        int currentPos = 0;
        public HypnoWindow() {
            InitializeComponent();
            WindowServices.ToTransparentWindow(this);            
        }

        public void SetQueue(params string[] files) {
            this.files = files;
            ChangeVideo(0);
        }
        public void SetOpacity(double opacity) {
            this.Opacity = opacity;
            UserPreferences.SaveOpacity(opacity);
        }
        public void SetVolume(double volume) {
            FirstVideo.Volume = volume;
            UserPreferences.SaveVolume(volume);
        }
        public void PauseVideo() {
            FirstVideo.Pause();
        }
        public void ContinueVideo() {
            FirstVideo.Play();
        }
        private void ChangeVideo(int filePos) {
            FirstVideo.Source = new Uri("Videos/" + files[filePos], UriKind.Relative);
            FirstVideo.Position = TimeSpan.FromMilliseconds(1);
            FirstVideo.Play();
        }

        private void FirstVideo_MediaEnded(object sender, RoutedEventArgs e) {
            if(currentPos+1 < files.Length) {
                ChangeVideo(++currentPos);
            }
            else{
                currentPos = 0;
                ChangeVideo(currentPos);
            }          
        }

        private void Window_SourceInitialized(object sender, EventArgs e) {
            //this.WindowState = WindowState.Maximized;
        }
    }
}
