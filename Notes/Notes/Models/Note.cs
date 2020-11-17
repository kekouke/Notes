using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Note
    {
        public Note(DateTime creationTime)
        {
            CreationTime = creationTime;
            LastChangeTime = creationTime;
        }

        public string Text { get; set; }
        public DateTime CreationTime { get; }
        public DateTime OldChangeTime { get; set; }
        public DateTime LastChangeTime { get; set; }
        public int Lenght { get; set; }
    }
}
