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

        private ObservableCollection<LyricsItem> lyricsItemSet;
        private Int32 wordsCount = 1;
        private Int32 lastWordsIndex = 0;
        private const Int32 wordsFocusShift = 15;
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

            Int32 CurrentPlayTotalSeconds = Mp3Player.Position.Minutes * 60 + Mp3Player.Position.Seconds;

            if (lyricsItemSet[wordsCount].StartSecond == -1)
            {
                wordsCount++;
            }

            //Debug.WriteLine("Target Start: " + LyricsItemSet[count].StartSecond);
            //Debug.WriteLine("Current : " + CurrentPlayTotalSeconds);
            if (lyricsItemSet[wordsCount].StartSecond == CurrentPlayTotalSeconds)
            {

                if (wordsCount == 1)
                {
                    lyricsItemSet[wordsCount].Size = 25;
                    lyricsItemSet[wordsCount].Color = "#FF0000";
                    lastWordsIndex = wordsCount;
                    wordsCount++;
                }
                else
                {
                    lyricsItemSet[lastWordsIndex].Size = 22;
                    lyricsItemSet[lastWordsIndex].Color = "#FFFFFF";

                    lyricsItemSet[wordsCount].Size = 25;
                    lyricsItemSet[wordsCount].Color = "#FF0000";

                    lastWordsIndex = wordsCount;
                    if (lyricsItemSet[wordsCount].EndSecond == -1)
                    {
                        wordsCount++;
                    }

                }
            }

            //if (count < LyricsItemSet.Count && LyricsItemSet[count].EndSecond != -1)
            //{
            //    //Debug.WriteLine("Target End: " + LyricsItemSet[count].EndSecond);
            //}
            if (wordsCount < lyricsItemSet.Count && wordsCount > 1 && lyricsItemSet[wordsCount].EndSecond == CurrentPlayTotalSeconds)
            {
                lyricsItemSet[wordsCount].Size = 22;
                lyricsItemSet[wordsCount].Color = "#FFFFFF";
                wordsCount++;
            }

            if (wordsCount + wordsFocusShift < lyricsItemSet.Count)
            {
                LyricsListBox.SelectedIndex = wordsCount + wordsFocusShift;
                ListBoxItem lbi = LyricsListBox.ItemContainerGenerator.
                    ContainerFromIndex(LyricsListBox.SelectedIndex) as ListBoxItem;

                if (lbi != null)
                {
                    lbi.Focus();
                }
            }

        }
        void OnTimerTick(object sender, EventArgs e)
        {

            //Debug.WriteLine("Target Start: " + LyricsItemSet[count].StartSecond);
            //Debug.WriteLine("Target End: " + LyricsItemSet[count].EndSecond);
            //count++;
            if (wordsCount < lyricsItemSet.Count)
            {
                HightLightWords();
            }
        }
        private void LoadLyricsInfo()
        {
            lyricsItemSet = new ObservableCollection<LyricsItem>();
            lyricsItemSet.Add(new LyricsItem("Not Your Kind Of People", 0) { Size = 30,Color = "#0000FF"});
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 17));
            lyricsItemSet.Add(new LyricsItem("You seem kind of phoney.", 23));
            lyricsItemSet.Add(new LyricsItem("Everything's a lie.", 27, 31));
            lyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 33));
            lyricsItemSet.Add(new LyricsItem("Something in your makeup.", 39));
            lyricsItemSet.Add(new LyricsItem("Don't see eye to eye.", 43, 48));
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 49));
            lyricsItemSet.Add(new LyricsItem("Don't want to be like you.", 55));
            lyricsItemSet.Add(new LyricsItem("Ever in our lives.", 59));
            lyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 65));
            lyricsItemSet.Add(new LyricsItem("We fight when you start talking.", 71));
            lyricsItemSet.Add(new LyricsItem("There's nothing but white noise", 74,79));
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("Ahhh.... Ahhh.... Ahhh.... Ahhh....", 80, 110));
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("Running around trying to fit in, Wanting to be loved.", 143, 148));
            lyricsItemSet.Add(new LyricsItem("It doesn't take much.", 150, 152));
            lyricsItemSet.Add(new LyricsItem("For someone to shut you down.", 154));
            lyricsItemSet.Add(new LyricsItem("When you build a shell,", 159));
            lyricsItemSet.Add(new LyricsItem("Build an army in your mind.", 160));
            lyricsItemSet.Add(new LyricsItem("You can't sit still.", 163));
            lyricsItemSet.Add(new LyricsItem("And you don't like hanging round the crowd.", 164));
            lyricsItemSet.Add(new LyricsItem("They don't understand", 169, 172));
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("You dropped by as I was sleeping.", 175));
            lyricsItemSet.Add(new LyricsItem("You came to see the whole commotion.", 183));
            lyricsItemSet.Add(new LyricsItem("And when I woke I started laughing.", 190));
            lyricsItemSet.Add(new LyricsItem("The jokes on me for not believing.", 198,204));
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 205));
            lyricsItemSet.Add(new LyricsItem("Speak a different language.", 210));
            lyricsItemSet.Add(new LyricsItem("We see through your lies.", 215));
            lyricsItemSet.Add(new LyricsItem("We are not your kind of people.", 221));
            lyricsItemSet.Add(new LyricsItem("Won't be cast as demons,", 226));
            lyricsItemSet.Add(new LyricsItem("Creatures you despise.", 230,235));
            lyricsItemSet.Add(new LyricsItem());
            lyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 236));
            lyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 243));
            lyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 251));
            lyricsItemSet.Add(new LyricsItem("We are extraordinary people.", 258,263));

            Model.Items = lyricsItemSet;
        }
        
    }
}