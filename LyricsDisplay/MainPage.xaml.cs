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

            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(0.5);
            tmr.Tick += OnTimerTick;
            tmr.Start();
        }

        private void OnPhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            Mp3Player.Source = new Uri("MP3/NotYourKindOfPeople.mp3", UriKind.Relative);
            Mp3Player.Play();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Model.HightLightWords(Mp3Player.Position);
        }
    }
}