using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LyricsDisplay.Resources;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace LyricsDisplay
{
    public partial class MainPage : PhoneApplicationPage
    {
        private LyricsItemPageModel model;
        public LyricsItemPageModel Model
        {
            get
            {
                if (model == null)
                {
                    model = new LyricsItemPageModel();
                }
                return model;
            }
        }
        private Boolean playClicked = false;
        public MainPage()
        {
            InitializeComponent();
            DataContext = Model;

           // appbarRewindButton = this.ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            appbarPlayAndPauseButton = this.ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            appbarStopButton = this.ApplicationBar.Buttons[1] as ApplicationBarIconButton;
            //appbarEndButton = this.ApplicationBar.Buttons[3] as ApplicationBarIconButton;
            
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(0.5);
            Timer.Tick += OnTimerTick;
            Timer.Start();
        }
        
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (Model.CurrentPlayingIndex < Model.Items.Count)
            {
                Model.HightLightWords(Mp3Player.Position);

                if (Model.CurrentPlayingIndex + LyricsItemPageModel.FocusShift < Model.Items.Count)
                {
                    LyricsListBox.SelectedIndex = Model.CurrentPlayingIndex + LyricsItemPageModel.FocusShift;
                    SetFocusListBoxItem(LyricsListBox.SelectedIndex);
                }
                timelineSlider.Maximum = Mp3Player.NaturalDuration.TimeSpan.TotalSeconds;
                timelineSlider.Value = Mp3Player.Position.TotalSeconds;
                TimeLineMinute.Text = Mp3Player.Position.Minutes.ToString();
                TimeLineSecond.Text = Mp3Player.Position.Seconds.ToString();

            }
        }

        private void OnPhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            Mp3Player.Source = new Uri("MP3/NotYourKindOfPeople.mp3", UriKind.Relative);
            
        }
        // ApplicationBar buttons
        private void OnAppbarPlayAndPauseClick(object sender, EventArgs args)
        {
            if (!playClicked)
            {
                Mp3Player.Play();
                appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.pause.rest.png", UriKind.Relative);
                appbarPlayAndPauseButton.Text = "Play";
                appbarStopButton.IsEnabled = true;
                playClicked = true;
            }
            else
            {
                Mp3Player.Pause();
                appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.play.rest.png", UriKind.Relative);
                appbarPlayAndPauseButton.Text = "Pause";
                playClicked = false;
            }
        }
        private void OnAppbarStopClick(object sender, EventArgs args)
        {
            Mp3Player.Stop();
            ResetPlayIcon();
            SetFocusListBoxItem(0);
            Model.CurrentPlayingIndex = 0;
            timelineSlider.Value = 0;
        }
        private void ResetPlayIcon()
        {
            appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.play.rest.png", UriKind.Relative);
            appbarPlayAndPauseButton.Text = "Pause";
            playClicked = false;
            appbarStopButton.IsEnabled = false;
        }
        private void SetFocusListBoxItem(Int32 FocusIndex)
        {
            ListBoxItem lbItem = LyricsListBox.ItemContainerGenerator.ContainerFromIndex(FocusIndex) as ListBoxItem;

            if (lbItem != null)
            {
                lbItem.Focus();
            }
        }
    }
}