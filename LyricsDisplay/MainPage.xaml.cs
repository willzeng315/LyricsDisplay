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
        // 建構函式

        private ObservableCollection<LyricsItem> LyricsItemSet;
        private Int32 count = 1;
        private LyricsItemPageModel model;
        public LyricsItemPageModel Model
        {
            get
            {
                if(model == null)
                {
                    model = new LyricsItemPageModel();
                }
                return model;
            }
        }
        public MainPage()
        {
            InitializeComponent();
            LoadLyricsInfo();
            DataContext = Model;
            
            Mp3Player.Source = new Uri("MP3/NotYourKindOfPeople.mp3", UriKind.Relative);
            Mp3Player.Play();
            //ListBox a;
            //a.Focus
            //ListBoxItem a = listbox.s;
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(0.5);
            tmr.Tick += OnTimerTick;
            tmr.Start();
        }
        void HightLightWords()
        {


            if (count < LyricsItemSet.Count && LyricsItemSet[count].StartSecond == -1)
            {
                LyricsItemSet[count - 1].Size = 25;
                LyricsItemSet[count - 1].Color = "#FFFFFF";
                count++;
            }
            Int32 CurrentPlayTotalSeconds = Mp3Player.Position.Minutes * 60 + Mp3Player.Position.Seconds;
            //Debug.WriteLine("Target Start: " + LyricsItemSet[count].StartSecond);
            //Debug.WriteLine("Current : " + CurrentPlayTotalSeconds);
            if (count < LyricsItemSet.Count && LyricsItemSet[count].StartSecond == CurrentPlayTotalSeconds)
            {

                if (count == 0)
                {
                    LyricsItemSet[count].Size = 30;
                    LyricsItemSet[count].Color = "#FF0000";
                    count++;
                }
                else
                {
                    LyricsItemSet[count - 1].Size = 25;
                    LyricsItemSet[count - 1].Color = "#FFFFFF";

                    LyricsItemSet[count].Size = 30;
                    LyricsItemSet[count].Color = "#FF0000";
                    if (LyricsItemSet[count].EndSecond == -1)
                    {
                        count++;
                    }

                }
            }


            if (count < LyricsItemSet.Count && LyricsItemSet[count].EndSecond != -1)
            {
                //Debug.WriteLine("Target End: " + LyricsItemSet[count].EndSecond);
            }
            if (count < LyricsItemSet.Count && count > 1 && LyricsItemSet[count].EndSecond == CurrentPlayTotalSeconds)
            {
               // Debug.WriteLine("Target End");
                LyricsItemSet[count].Size = 25;
                LyricsItemSet[count].Color = "#FFFFFF";
                count++;
            }

        }
        void OnTimerTick(object sender, EventArgs e)
        {

            //Debug.WriteLine("Target Start: " + LyricsItemSet[count].StartSecond);
            //Debug.WriteLine("Target End: " + LyricsItemSet[count].EndSecond);
            //count++;
            HightLightWords();
            //Debug.WriteLine(Mp3Player.Position.TotalMilliseconds);
            //Debug.WriteLine(Mp3Player.Position.Seconds);
            //if (count < LyricsItemSet.Count)
            //{
            //    if (count == 0)
            //    {
            //        LyricsItemSet[count].Size = 30;
            //        LyricsItemSet[count].Color = "#FF0000";
            //        count++;
            //    }
            //    else
            //    {
            //        LyricsItemSet[count - 1].Size = 25;
            //        LyricsItemSet[count - 1].Color = "#FFFFFF";

            //        LyricsItemSet[count].Size = 30;
            //        LyricsItemSet[count].Color = "#FF0000";
            //        count++;

            //    }
            //}
        }
        private void LoadLyricsInfo()
        {
            LyricsItemSet = new ObservableCollection<LyricsItem>();
            LyricsItemSet.Add(new LyricsItem("Not Your Kind Of People", 0));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 17));
            LyricsItemSet.Add(new LyricsItem("You seem kind of phoney.", 23));
            LyricsItemSet.Add(new LyricsItem("Everything's a lie.", 27, 31));
            LyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 33));
            LyricsItemSet.Add(new LyricsItem("Something in your makeup.", 39));
            LyricsItemSet.Add(new LyricsItem("Don't see eye to eye.", 43, 48));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 49));
            LyricsItemSet.Add(new LyricsItem("Don't want to be like you.", 55));
            LyricsItemSet.Add(new LyricsItem("Ever in our lives.", 59));
            LyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 65));
            LyricsItemSet.Add(new LyricsItem("We fight when you start talking.", 71));
            LyricsItemSet.Add(new LyricsItem("There's nothing but white noise", 74,79));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("Ahhh.... Ahhh.... Ahhh.... Ahhh....", 80, 110));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("Running around trying to fit in, Wanting to be loved.", 143, 148));
            LyricsItemSet.Add(new LyricsItem("It doesn't take much.", 150, 152));
            LyricsItemSet.Add(new LyricsItem("For someone to shut you down.", 154));
            LyricsItemSet.Add(new LyricsItem("When you build a shell,", 159));
            LyricsItemSet.Add(new LyricsItem("Build an army in your mind.", 160));
            LyricsItemSet.Add(new LyricsItem("You can't sit still.", 163));
            LyricsItemSet.Add(new LyricsItem("And you don't like hanging round the crowd.", 164));
            LyricsItemSet.Add(new LyricsItem("They don't understand", 169, 172));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("You dropped by as I was sleeping.", 175));
            LyricsItemSet.Add(new LyricsItem("You came to see the whole commotion.", 183));
            LyricsItemSet.Add(new LyricsItem("And when I woke I started laughing.", 190));
            LyricsItemSet.Add(new LyricsItem("The jokes on me for not believing.", 198,202));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 205));
            LyricsItemSet.Add(new LyricsItem("Speak a different language.", 210));
            LyricsItemSet.Add(new LyricsItem("We see through your lies.", 215));
            LyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 221));
            LyricsItemSet.Add(new LyricsItem("Won't be cast as demons,", 226));
            LyricsItemSet.Add(new LyricsItem("Creatures you despise.", 230,235));
            LyricsItemSet.Add(new LyricsItem());
            LyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 236));
            LyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 243));
            LyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 251));
            LyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 258));

            Model.Items = LyricsItemSet;
        }
        
    }
}