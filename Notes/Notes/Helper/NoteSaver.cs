using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Notes.ViewModels;

namespace Notes.Helper
{
    public sealed class NoteSaver
    {
        private static readonly Lazy<NoteSaver> _instance =
            new Lazy<NoteSaver>(() => new NoteSaver());

        public static NoteSaver Instance { get => _instance.Value; }

        private NoteSaver() { }

        public object ReadData()
        {
            using (var file = new StreamReader(new FileStream("notes.txt", FileMode.OpenOrCreate)))
            {
                return JsonConvert.DeserializeObject<object>(file.ReadToEnd()); 
            }

        }

        public void SaveData(LinkedList<NoteViewModel> notes)
        {
            using (var sw = new StreamWriter(new FileStream("notes.txt", FileMode.OpenOrCreate)))
            {
                sw.Write(JsonConvert.SerializeObject(notes));
            }
        }
    }
}
