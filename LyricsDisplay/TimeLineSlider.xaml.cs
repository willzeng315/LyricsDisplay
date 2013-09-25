using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LyricsDisplay
{
    public partial class TimeLineSlider : UserControl
    {
        public TimeLineSlider()
        {
            InitializeComponent();
        }
        public void SetTimeLinePosition(MediaElement Mp3Player)
        {
            TimelineSlider.Maximum = Mp3Player.NaturalDuration.TimeSpan.TotalSeconds;
            TimelineSlider.Value = Mp3Player.Position.TotalSeconds;
            CurrentTimeLineMinute.Text = Mp3Player.Position.Minutes.ToString();
            CurrentTimeLineSecond.Text = Mp3Player.Position.Seconds.ToString();
            TotalTimeLineMinute.Text = Mp3Player.NaturalDuration.TimeSpan.Minutes.ToString();
            TotalTimeLineSecond.Text = Mp3Player.NaturalDuration.TimeSpan.Seconds.ToString();
        }
    }
}
