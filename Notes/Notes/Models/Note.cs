using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Note
    {
        public string Text { get; set; }
        public DateTime CreateTime { get; }
        public DateTime ChangeTime { get; set; }
        public int Count { get; set; } //Rename
    }
}
