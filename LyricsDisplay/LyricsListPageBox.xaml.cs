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

        private Int32 currentPlayingIndex = 2;
        private Int32 lastIndex = 0;
        private const Int32 focusShift = 8;

        public LyricsListPageBox()
        {
            InitializeComponent();

        }
        
        public void DoHightLightWords(Int32 TimeLine)
        {
            if ( currentPlayingIndex < Items.Count)
            {
                HightLightWords(TimeLine);

                if (TimeLine != 0 && currentPlayingIndex + focusShift < Items.Count)
                {
                    LyricsListBox.SelectedIndex = currentPlayingIndex + focusShift;
                    SetFocusListBoxItem(LyricsListBox.SelectedIndex);
                }
            }
        }

        public void SetDataContext()
        {
            DataContext = this;
        }

        public void HightLightWords(Int32 CurrentPlayTotalSeconds)
        {
            if (CurrentPlayTotalSeconds == 0)
            {
                Items[(Int32)Math.Max((currentPlayingIndex - 1), 1)].IsPlaying = false;
                currentPlayingIndex = 0;
                lastIndex = 0;
            }

            if (Items[currentPlayingIndex].StartSecond == -1)
            {
                currentPlayingIndex++;
            }

            if (Items[currentPlayingIndex].StartSecond == CurrentPlayTotalSeconds)
            {

                if (currentPlayingIndex == 1)
                {
                    Items[currentPlayingIndex].IsPlaying = true;
                    lastIndex = currentPlayingIndex;
                    currentPlayingIndex++;
                }
                else
                {
                    Items[lastIndex].IsPlaying = false;
                    Items[currentPlayingIndex].IsPlaying = true;
                    lastIndex = currentPlayingIndex;

                    if (Items[currentPlayingIndex].EndSecond == -1)
                    {
                        currentPlayingIndex++;
                    }
                }
            }

            if (currentPlayingIndex < Items.Count && currentPlayingIndex > 1 && Items[currentPlayingIndex].EndSecond == CurrentPlayTotalSeconds)
            {
                Items[currentPlayingIndex].IsPlaying = false;
                currentPlayingIndex++;
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

        private String lyricsTitle = null;
        public String LyricsTitle
        {
            get
            {
                return lyricsTitle;
            }
            set
            {
                lyricsTitle = value;
                SetProperty(ref lyricsTitle, value, "LyricsTitle");
            }
        }

        //public static readonly DependencyProperty TimeLineProperty = DependencyProperty.Register("TimeLine", typeof(Int32), typeof(LyricsListPageBox), new PropertyMetadata(0, OnTimeLineChanged));
        //public Int32 TimeLine
        //{
        //    get
        //    {
        //        return (Int32)GetValue(TimeLineProperty);
        //    }
        //    set
        //    {
        //        SetValue(TimeLineProperty, value);
        //    }
        //}
        //static void OnTimeLineChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        //{
        //    //DoHightLightWords((Int32)args.NewValue);
        //    //Debug.WriteLine();
        //}
    
    }
}
