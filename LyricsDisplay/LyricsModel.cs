using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricsDisplay
{
       
        public class LyricsItem:BindableBase
        {

            public LyricsItem(String words,  Int32 startSecond)
            {
                Words = words;
                StartSecond = startSecond;
                EndSecond = -1;
                Size = 25;
                Color = "#FFFFFF";
                //Description = description;
            }
            public LyricsItem()
            {
                Words = "";
                StartSecond = -1;
                EndSecond = -1;
                Size = 25;
                Color = "#FFFFFF";
                //Description = description;
            }
            public LyricsItem(String words, Int32 startSecond, Int32 endSecond)
            {
                Words = words;
                StartSecond = startSecond;
                EndSecond = endSecond;
                Size = 25;
                Color = "#FFFFFF";
                //Description = description;
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
                    words = value;
                    Notify("Link");
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
                    sartSecond = value;
                    Notify("StartSecond");
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
                    endSecond = value;
                    Notify("EndSecond");
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
                    size = value;
                    Notify("Size");
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
                    color = value;
                    Notify("Color");
                }
            }
        }
        public class LyricsItemPageModel : BindableBase
        {

            public LyricsItemPageModel()
            {
                Items = new ObservableCollection<LyricsItem>();
            }
            private ObservableCollection<LyricsItem> LyricsItemSet = null;
            public ObservableCollection<LyricsItem> Items
            {
                get
                {
                    return LyricsItemSet;
                }
                set
                {
                    LyricsItemSet = value;
                    Notify("Items");
                }
            }
        }
    
}
