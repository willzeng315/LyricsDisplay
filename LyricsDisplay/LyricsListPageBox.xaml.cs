using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace LyricsDisplay
{
    public class LyricsItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public LyricsItem(String words, Int32 startSecond)
        {
            Words = words;
            StartSecond = startSecond;
            EndSecond = -1;
            IsPlaying = false;
        }

        public LyricsItem()
        {
            Words = "";
            StartSecond = -1;
            EndSecond = -1;
            IsPlaying = false;
        }
        public LyricsItem(String words, Int32 startSecond, Int32 endSecond)
        {
            Words = words;
            StartSecond = startSecond;
            EndSecond = endSecond;
            IsPlaying = false;

        }

        private String words;
        public String Words
        {
            get
            {
                return words;
            }
            set
            {
                SetProperty(ref words, value, "Words");
            }
        }

        private Int32 sartSecond;
        public Int32 StartSecond
        {
            get
            {
                return sartSecond;
            }
            set
            {
                SetProperty(ref sartSecond, value, "StartSecond");
            }
        }

        private Int32 endSecond;
        public Int32 EndSecond
        {
            get
            {
                return endSecond;
            }
            set
            {
                SetProperty(ref endSecond, value, "EndSecond");
            }
        }

        private Boolean isPlaying;
        public Boolean IsPlaying
        {
            get
            {
                return isPlaying;
            }
            set
            {
                SetProperty(ref isPlaying, value, "IsPlaying");
            }
        }
    }
    public class LyricsItemPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public LyricsItemPageModel()
        {
            Items = new ObservableCollection<LyricsItem>();

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
            Items.Add(new LyricsItem("The jokes on me for not believing.", 198, 202));
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
        }

        private ObservableCollection<LyricsItem> items = null;
        public ObservableCollection<LyricsItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                SetProperty(ref items, value, "Items");
            }
        }

        public Int32 CurrentPlayingIndex = 0;
        public Int32 LastIndex = 0;
        public const Int32 FocusShift = 8;

        public void HightLightWords(TimeSpan position)
        {
            Int32 CurrentPlayTotalSeconds = (Int32)position.TotalSeconds;

            if (Items[CurrentPlayingIndex].StartSecond == -1)
            {
                CurrentPlayingIndex++;
            }

            if (Items[CurrentPlayingIndex].StartSecond == CurrentPlayTotalSeconds)
            {

                if (CurrentPlayingIndex == 1)
                {
                    Items[CurrentPlayingIndex].IsPlaying = true;
                    LastIndex = CurrentPlayingIndex;
                    CurrentPlayingIndex++;
                }
                else
                {
                    Items[LastIndex].IsPlaying = false;
                    Items[CurrentPlayingIndex].IsPlaying = true;
                    LastIndex = CurrentPlayingIndex;

                    if (Items[CurrentPlayingIndex].EndSecond == -1)
                    {
                        CurrentPlayingIndex++;
                    }

                }
            }

            if (CurrentPlayingIndex < Items.Count && CurrentPlayingIndex > 1 && Items[CurrentPlayingIndex].EndSecond == CurrentPlayTotalSeconds)
            {
                Items[CurrentPlayingIndex].IsPlaying = false;
                CurrentPlayingIndex++;
            }
        }
    }
    public partial class LyricsListPageBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
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
        public LyricsListPageBox()
        {
            InitializeComponent();
            DataContext = Model;

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(3);
            Timer.Tick += OnTimerTick;
            Timer.Start();
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine(Model.Items[0].Words);
             if (Model.CurrentPlayingIndex < Model.Items.Count)
            {
                //Model.HightLightWords(Mp3Player.Position);

                if (Model.CurrentPlayingIndex + LyricsItemPageModel.FocusShift < Model.Items.Count)
                {
                    LyricsListBox.SelectedIndex = Model.CurrentPlayingIndex + LyricsItemPageModel.FocusShift;
                    SetFocusListBoxItem(LyricsListBox.SelectedIndex);
                }
                //timelineSlider.Maximum = Mp3Player.NaturalDuration.TimeSpan.TotalSeconds;
                //timelineSlider.Value = Mp3Player.Position.TotalSeconds;
                //TimeLineMinute.Text = Mp3Player.Position.Minutes.ToString();
                //TimeLineSecond.Text = Mp3Player.Position.Seconds.ToString();

            }
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
