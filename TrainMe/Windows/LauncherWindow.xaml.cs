/*
	Copyright (C) 2021 Damsel

	This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

	You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrainMe.Classes;

namespace TrainMe.Windows {
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window {
        List<HypnoWindow> openedHypnosis = new List<HypnoWindow>();
        Random random = new Random();
        bool pauseClicked = false;
        public LauncherWindow() {
            InitializeComponent();
            /*if (File.Exists("pref.ini")) {
                VolumeSlider.Value = UserPreferences.Volume;
                OpacitySlider.Value = UserPreferences.Opacity;
            }*/
            DisableButtonIfNoSelection();
            VideoList.ItemsSource = GetAllFiles();

            MonitorPlayList.ItemsSource = WindowServices.GetAllScreenViewers();
            DehypnotizeButton.IsEnabled = false;
            PauseButton.IsEnabled = false;
        }

        private void Hypnotize_Click(object sender, RoutedEventArgs e) {
            if (ShuffleCheckBox.IsChecked == true) {
                GeneralHypnotize(GetSelectedRandomized);
            } else {
                GeneralHypnotize(GetSelected);
            }
        }

        private void GeneralHypnotize(Func<string[]> method) {
            string[] selected = method();
            foreach (ScreenViewer monitor in MonitorPlayList.SelectedItems) {
                openedHypnosis.Add(createHypnoWindow(monitor, selected));
            }
            DehypnotizeButton.IsEnabled = true;
            PauseButton.IsEnabled = true;
        }

        private HypnoWindow createHypnoWindow(ScreenViewer monitor, string[] videos) {
            HypnoWindow hypnoWindow = new HypnoWindow();          
            hypnoWindow.Show();
            WindowServices.MoveWindowToScreen(hypnoWindow, monitor.Screen);
            hypnoWindow.SetOpacity(OpacitySlider.Value);
            hypnoWindow.SetVolume(VolumeSlider.Value);
            hypnoWindow.SetQueue(videos);
            return hypnoWindow;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            if(System.Windows.MessageBox.Show("Exit program? All hypnosis will be terminated :(", "Exit program", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                App.Current.Shutdown();
            }           
        }

        private void Opacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            foreach (HypnoWindow window in openedHypnosis)
                window.SetOpacity(OpacitySlider.Value);
        }
        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            foreach (HypnoWindow window in openedHypnosis)
                window.SetVolume(VolumeSlider.Value);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        private List<string> GetAllFiles() {
            string[] files = Directory.GetFiles("Videos/");
            List<string> parsed = new List<string>();
            foreach (string file in files)
                parsed.Add(file.Split('/')[1]);
            return parsed;
        }

        private string[] GetSelected() {
            List<string> selected = new List<string>();
            foreach(string file in VideoList.SelectedItems) {
                selected.Add(file);
            }
            return selected.ToArray();
        }

        private string[] GetSelectedRandomized() {
            string[] tmp = GetSelected();

            return tmp.OrderBy(a => random.Next()).ToArray();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void Kofi_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start("https://ko-fi.com/damsel");
        }

        private void VideoList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DisableButtonIfNoSelection();
        }

        private void DisableButtonIfNoSelection() {
            if (VideoList.SelectedItems.Count < 1 || MonitorPlayList.SelectedItems.Count < 1) {
                HypnotizeButton.IsEnabled = false;
                HypnotizeButton.Content = "Select video and monitor first";
            } else {
                HypnotizeButton.IsEnabled = true;
                HypnotizeButton.Content = "TRAIN ME!";
            }
        }

        private void MonitorPlayList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DisableButtonIfNoSelection();
        }

        private void Dehypnotize_Click(object sender, RoutedEventArgs e) {
            DehypnotizeButton.IsEnabled = false;
            foreach(HypnoWindow window in openedHypnosis) {
                window.Close();
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e) {
            if(pauseClicked) {
                pauseClicked = false;
                PauseButton.Content = "Pause";
                foreach (HypnoWindow window in openedHypnosis)
                    window.ContinueVideo();
            } else {
                pauseClicked = true;
                PauseButton.Content = "Continue";
                foreach (HypnoWindow window in openedHypnosis)
                    window.PauseVideo();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e) {
            VideoList.ItemsSource = GetAllFiles();
        }
    }
}
