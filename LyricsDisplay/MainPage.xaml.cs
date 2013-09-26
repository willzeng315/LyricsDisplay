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
        // this is master trunk
        private Boolean isPlayClicked = true;

        public MainPage()
        {
            
            InitializeComponent();

            LyricsDisplayUC.Items = LyricsReader1();
            LyricsDisplayUC.SetDataContext();
            LyricsInfo.SetDataContext();

            appbarPreviousButton = ApplicationBar.Buttons[0] as ApplicationBarIconButton; 
            appbarPlayAndPauseButton = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
            appbarStopButton = ApplicationBar.Buttons[2] as ApplicationBarIconButton;
            appbarNextButton = ApplicationBar.Buttons[3] as ApplicationBarIconButton;
            
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(0.5);
            Timer.Tick += OnTimerTick;
            Timer.Start();
        }

        private void OnTimerTick(Object sender, EventArgs e)
        {
            LyricsTimeLineSlider.SetTimeLinePosition(Mp3Player);
            LyricsInfo.SetTimeLinePosition(Mp3Player);
            LyricsDisplayUC.DoHightLightWords((Int32)Mp3Player.Position.TotalSeconds);
            if (Mp3Player.CurrentState.ToString() == "Paused")
            {
                ResetPlayIcon();
            }
        }

        private ObservableCollection<LyricsItem> LyricsReader2()
        {
            Mp3Player.Source = new Uri("MP3/紅豆.mp3", UriKind.Relative);
            ObservableCollection<LyricsItem> Items = new ObservableCollection<LyricsItem>();

            Items.Add(new LyricsItem("紅豆", 0));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("還沒好好的感受　雪花綻放的氣候", 22));
            Items.Add(new LyricsItem("我們一起顫抖　會更明白　甚麼是溫柔", 30));
            Items.Add(new LyricsItem("還沒跟你牽著手　走過荒蕪的沙丘", 38));
            Items.Add(new LyricsItem("可能從此以後　學會珍惜　天長和地久", 47,55));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("有時候　有時候", 55));
            Items.Add(new LyricsItem("我會相信一切有盡頭", 60));
            Items.Add(new LyricsItem("相聚離開　都有時候", 64));
            Items.Add(new LyricsItem("沒有甚麼會永垂不朽", 69));
            Items.Add(new LyricsItem("可是我　有時候", 73));
            Items.Add(new LyricsItem("寧願選擇留戀不放手", 77));
            Items.Add(new LyricsItem("等到風景都看透", 81));
            Items.Add(new LyricsItem("也許你會陪我　看細水長流", 84, 93));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("還沒為你把紅豆　熬成纏綿的傷口", 108));
            Items.Add(new LyricsItem("然後一起分享　會更明白　相思的哀愁",116));
            Items.Add(new LyricsItem("還沒好好的感受　醒著親吻的溫柔", 124));
            Items.Add(new LyricsItem("可能在我左右　你才追求　孤獨的自由", 133, 141));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("有時候　有時候", 144));
            Items.Add(new LyricsItem("我會相信一切有盡頭", 148));
            Items.Add(new LyricsItem("相聚離開　都有時候", 152));
            Items.Add(new LyricsItem("沒有甚麼會永垂不朽", 156));
            Items.Add(new LyricsItem("可是我　有時候", 161));
            Items.Add(new LyricsItem("寧願選擇留戀不放手",165));
            Items.Add(new LyricsItem("等到風景都看透", 169));
            Items.Add(new LyricsItem("也許你會陪我　看細水長流", 173,180));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("有時候　有時候", 193));
            Items.Add(new LyricsItem("我會相信一切有盡頭", 197));
            Items.Add(new LyricsItem("相聚離開　都有時候", 201));
            Items.Add(new LyricsItem("沒有甚麼會永垂不朽", 206));
            Items.Add(new LyricsItem("可是我　有時候", 210));
            Items.Add(new LyricsItem("寧願選擇留戀不放手", 214));
            Items.Add(new LyricsItem("等到風景都看透", 219));
            Items.Add(new LyricsItem("也許你會陪我　看細水長流", 223, 231));

            LyricsInfo.SongName = Items[0].Words;
            LyricsInfo.ImagePath = "Images/王菲_紅豆.jpg";

            return Items;
        }

        private ObservableCollection<LyricsItem> LyricsReader1()
        {
            Mp3Player.Source = new Uri("MP3/NotYourKindOfPeople.mp3", UriKind.Relative);
            ObservableCollection<LyricsItem> Items = new ObservableCollection<LyricsItem>();

            Items.Add(new LyricsItem("Not Your Kind Of People", 0));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("We are not your kind of people.", 17));
            Items.Add(new LyricsItem("You seem kind of phoney.", 23));
            Items.Add(new LyricsItem("Everything's a lie.", 27, 31));
            Items.Add(new LyricsItem("We are not your kind of people.", 33));
            Items.Add(new LyricsItem("Something in your makeup.", 39));
            Items.Add(new LyricsItem("Don't see eye to eye.", 43, 48));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("We are not your kind of people.", 49));
            Items.Add(new LyricsItem("Don't want to be like you.", 55));
            Items.Add(new LyricsItem("Ever in our lives.", 59));
            Items.Add(new LyricsItem("We are not your kind of people.", 65));
            Items.Add(new LyricsItem("We fight when you start talking.", 71));
            Items.Add(new LyricsItem("There's nothing but white noise", 74, 79));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("Ahhh.... Ahhh.... Ahhh.... Ahhh....", 80, 110));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("Running around trying to fit in, Wanting to be loved.", 143, 148));
            Items.Add(new LyricsItem("It doesn't take much.", 150, 152));
            Items.Add(new LyricsItem("For someone to shut you down.", 154));
            Items.Add(new LyricsItem("When you build a shell,", 159));
            Items.Add(new LyricsItem("Build an army in your mind.", 160));
            Items.Add(new LyricsItem("You can't sit still.", 163));
            Items.Add(new LyricsItem("And you don't like hanging round the crowd.", 164));
            Items.Add(new LyricsItem("They don't understand", 169, 172));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("You dropped by as I was sleeping.", 175));
            Items.Add(new LyricsItem("You came to see the whole commotion.", 183));
            Items.Add(new LyricsItem("And when I woke I started laughing.", 190));
            Items.Add(new LyricsItem("The jokes on me for not believing.", 198, 204));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("We are not your kind of people.", 205));
            Items.Add(new LyricsItem("Speak a different language.", 210));
            Items.Add(new LyricsItem("We see through your lies.", 215));
            Items.Add(new LyricsItem("We are not your kind of people.", 221));
            Items.Add(new LyricsItem("Won't be cast as demons,", 226));
            Items.Add(new LyricsItem("Creatures you despise.", 230, 235));
            Items.Add(new LyricsItem());
            Items.Add(new LyricsItem("We are extraordinary people.", 236));
            Items.Add(new LyricsItem("We are extraordinary people.", 243));
            Items.Add(new LyricsItem("We are extraordinary people.", 251));
            Items.Add(new LyricsItem("We are extraordinary people.", 258, 263));

            LyricsInfo.SongName = Items[0].Words;
            LyricsInfo.ImagePath = "Images/Garbage_NotYouKindOfPeople.jpg";

            return Items;
        }

        private void OnAppbarPlayAndPauseClick(Object sender, EventArgs args)
        {
           if (!isPlayClicked)
            {   
                Mp3Player.Play();
                appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.pause.rest.png", UriKind.Relative);
                appbarPlayAndPauseButton.Text = "Play";
                isPlayClicked = true;
            }
            else
            {
                Mp3Player.Pause();
                appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.play.rest.png", UriKind.Relative);
                appbarPlayAndPauseButton.Text = "Pause";
                isPlayClicked = false;
            }
        }

        private void OnAppbarStopClick(Object sender, EventArgs args)
        {
            Mp3Player.Stop();
            ResetPlayIcon();
        }
        private void OnAppbarNextClick(Object sender, EventArgs args)
        {
            LyricsDisplayUC.Items = LyricsReader2();
        }
        private void OnAppbarPreviousClick(Object sender, EventArgs args)
        {
            LyricsDisplayUC.Items = LyricsReader1();
        }

        private void ResetPlayIcon()
        {
            appbarPlayAndPauseButton.IconUri = new Uri("Images/appbar.transport.play.rest.png", UriKind.Relative);
            appbarPlayAndPauseButton.Text = "play";
            isPlayClicked = false;
        }

        private void OnPivotManipulationStarted(Object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            Pivot pivot = sender as Pivot;
            if (LyricsInfo.IsChangeToLyrics)
            {
                pivot.SelectedIndex = 1;
                LyricsInfo.IsChangeToLyrics = false;
            }
        }
    }
}