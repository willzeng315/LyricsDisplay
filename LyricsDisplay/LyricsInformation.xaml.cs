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
using System.Diagnostics;

namespace LyricsDisplay
{
    public partial class LyricsInformation : UserControl,INotifyPropertyChanged
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
       
        public LyricsInformation()
        {
            InitializeComponent();
            
        }

        public void SetTimeLinePosition(MediaElement Mp3Player)
        {
            TimeLineSlider.SetTimeLinePosition(Mp3Player);
        }

        public void SetDataContext()
        {
            DataContext = this;
        }


        private String imagePath;
        public String ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                SetProperty(ref imagePath, value, "ImagePath");
            }
        }

        private String songName;
        public String SongName
        {
            get
            {
                return songName;
            }
            set
            {
                SetProperty(ref songName, value, "SongName");
            }
        }

        public Boolean IsChangeToLyrics
        {
            set;
            get;
        }

        private void OnChangeToLyrics(Object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
          
            IsChangeToLyrics = true;
        }
    }
}
