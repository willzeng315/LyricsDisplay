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

        private Boolean playClicked = false;

        public MainPage()
        {
            
            InitializeComponent();
            LyricsReader();
            appbarPlayAndPauseButton = this.ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            appbarStopButton = this.ApplicationBar.Buttons[1] as ApplicationBarIconButton;

            
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(0.5);
            Timer.Tick += OnTimerTick;
            Timer.Start();
            
        }

        private void OnTimerTick(Object sender, EventArgs e)
        {
            timelineSlider.Maximum = Mp3Player.NaturalDuration.TimeSpan.TotalSeconds;
            timelineSlider.Value = Mp3Player.Position.TotalSeconds;
            LyricsDisplayUC.DoHightLightWords((Int32)Mp3Player.Position.TotalSeconds);
        }

        private void LyricsReader()
        {
            LyricsDisplayUC.Items = new ObservableCollection<LyricsItem>();

            LyricsDisplayUC.Items.Add(new LyricsItem("Not Your Kind Of People", 0));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("We are not your kind of people.", 17));
            LyricsDisplayUC.Items.Add(new LyricsItem("You seem kind of phoney.", 23));
            LyricsDisplayUC.Items.Add(new LyricsItem("Everything's a lie.", 27, 31));
            LyricsDisplayUC.Items.Add(new LyricsItem("We are not your kind of people.", 33));
            LyricsDisplayUC.Items.Add(new LyricsItem("Something in your makeup.", 39));
            LyricsDisplayUC.Items.Add(new LyricsItem("Don't see eye to eye.", 43, 48));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("We are not your kind of people.", 49));
            LyricsDisplayUC.Items.Add(new LyricsItem("Don't want to be like you.", 55));
            LyricsDisplayUC.Items.Add(new LyricsItem("Ever in our lives.", 59));
            LyricsDisplayUC.Items.Add(new LyricsItem("We are not your kind of people.", 65));
            LyricsDisplayUC.Items.Add(new LyricsItem("We fight when you start talking.", 71));
            LyricsDisplayUC.Items.Add(new LyricsItem("There's nothing but white noise", 74, 79));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("Ahhh.... Ahhh.... Ahhh.... Ahhh....", 80, 110));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("Running around trying to fit in, Wanting to be loved.", 143, 148));
            LyricsDisplayUC.Items.Add(new LyricsItem("It doesn't take much.", 150, 152));
            LyricsDisplayUC.Items.Add(new LyricsItem("For someone to shut you down.", 154));
            LyricsDisplayUC.Items.Add(new LyricsItem("When you build a shell,", 159));
            LyricsDisplayUC.Items.Add(new LyricsItem("Build an army in your mind.", 160));
            LyricsDisplayUC.Items.Add(new LyricsItem("You can't sit still.", 163));
            LyricsDisplayUC.Items.Add(new LyricsItem("And you don't like hanging round the crowd.", 164));
            LyricsDisplayUC.Items.Add(new LyricsItem("They don't understand", 169, 172));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("You dropped by as I was sleeping.", 175));
            LyricsDisplayUC.Items.Add(new LyricsItem("You came to see the whole commotion.", 183));
            LyricsDisplayUC.Items.Add(new LyricsItem("And when I woke I started laughing.", 190));
            LyricsDisplayUC.Items.Add(new LyricsItem("The jokes on me for not believing.", 198, 204));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("We are not your kind of people.", 205));
            LyricsDisplayUC.Items.Add(new LyricsItem("Speak a different language.", 210));
            LyricsDisplayUC.Items.Add(new LyricsItem("We see through your lies.", 215));
            LyricsDisplayUC.Items.Add(new LyricsItem("We are not your kind of people.", 221));
            LyricsDisplayUC.Items.Add(new LyricsItem("Won't be cast as demons,", 226));
            LyricsDisplayUC.Items.Add(new LyricsItem("Creatures you despise.", 230, 235));
            LyricsDisplayUC.Items.Add(new LyricsItem());
            LyricsDisplayUC.Items.Add(new LyricsItem("We are extraordinary people.", 236));
            LyricsDisplayUC.Items.Add(new LyricsItem("We are extraordinary people.", 243));
            LyricsDisplayUC.Items.Add(new LyricsItem("We are extraordinary people.", 251));
            LyricsDisplayUC.Items.Add(new LyricsItem("We are extraordinary people.", 258, 263));
            LyricsDisplayUC.SetDataContext();
        }

        private void OnPhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("OnPhoneApplicationPageLoaded");
        }

        private void OnAppbarPlayAndPauseClick(object sender, EventArgs args)
        {
            //DataContext = LyricsDisplayUC;
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
            timelineSlider.Value = 0;
        }
        private void ResetPlayIcon()
        {
            appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.play.rest.png", UriKind.Relative);
            appbarPlayAndPauseButton.Text = "Pause";
            playClicked = false;
            appbarStopButton.IsEnabled = false;
        }
     
    }
}