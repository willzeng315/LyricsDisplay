﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricsDisplay
{
    public class LyricsItem : BindableBase
    {
        public LyricsItem(String words, Int32 startSecond)
        {
            Words = words;
            StartSecond = startSecond;
            EndSecond = -1;
            Size = 22;
            Color = "#FFFFFF";
        }

        public LyricsItem()
        {
            Words = "";
            StartSecond = -1;
            EndSecond = -1;
            Size = 22;
            Color = "#FFFFFF";
        }
        public LyricsItem(String words, Int32 startSecond, Int32 endSecond)
        {
            Words = words;
            StartSecond = startSecond;
            EndSecond = endSecond;
            Size = 22;
            Color = "#FFFFFF";
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
                SetProperty(ref words, value, "Link");
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

        private Int32 size;
        public Int32 Size
        {
            get
            {
                return size;
            }
            set
            {
                SetProperty(ref size, value, "Size");
            }
        }

        private String color;
        public String Color
        {
            get
            {
                return color;
            }
            set
            {
                SetProperty(ref color, value, "Color");
            }
        }
    }

    public class LyricsItemPageModel : BindableBase
    {
        public LyricsItemPageModel()
        {
            Items = new ObservableCollection<LyricsItem>();

            Items.Add(new LyricsItem("Not Your Kind Of People", 0) { Size = 30, Color = "#0000FF" });
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

        private Int32 count = 2;

        public void HightLightWords(TimeSpan position)
        {
            if (count < Items.Count && Items[count].StartSecond == -1)
            {
                Items[count - 1].Size = 22;
                Items[count - 1].Color = "#FFFFFF";
                count++;
            }
            Int32 CurrentPlayTotalSeconds = position.Minutes * 60 + position.Seconds;
            if (count < Items.Count && Items[count].StartSecond == CurrentPlayTotalSeconds)
            {

                if (count == 0)
                {
                    Items[count].Size = 26;
                    Items[count].Color = "#FF0000";
                    count++;
                }
                else
                {
                    Items[count - 1].Size = 22;
                    Items[count - 1].Color = "#FFFFFF";

                    Items[count].Size = 26;
                    Items[count].Color = "#FF0000";
                    if (Items[count].EndSecond == -1)
                    {
                        count++;
                    }

                }
            }

            if (count < Items.Count && count > 1 && Items[count].EndSecond == CurrentPlayTotalSeconds)
            {
                Items[count].Size = 25;
                Items[count].Color = "#FFFFFF";
                count++;
            }
        }
    }
    
}
