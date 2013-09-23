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

        public MainPage()
        {
            InitializeComponent();
            DataContext = Model;

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
                    ListBoxItem lbItem = LyricsListBox.ItemContainerGenerator.ContainerFromIndex(LyricsListBox.SelectedIndex) as ListBoxItem;

                    if (lbItem != null)
                    {
                        lbItem.Focus();
                    }
                }
            }
        }

        private void OnPhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            Mp3Player.Source = new Uri("MP3/NotYourKindOfPeople.mp3", UriKind.Relative);
            Mp3Player.Play();
        }
    }
}