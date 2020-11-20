using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Notes.ViewModels;

namespace Notes.Helper
{
    public sealed class NoteSaver
    {
        private static readonly Lazy<NoteSaver> _instance =
            new Lazy<NoteSaver>(() => new NoteSaver());

        public static NoteSaver Instance { get => _instance.Value; }

        private NoteSaver()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            filename = Path.Combine(documents, "notes.json");
        }

        private string filename = String.Empty;

        public LinkedList<NoteViewModel> ReadData()
        {

            using (var file = new StreamReader(new FileStream(filename, FileMode.OpenOrCreate)))
            {
                return JsonConvert.DeserializeObject<LinkedList<NoteViewModel>>(file.ReadToEnd()); 
            }

        }

        public async void SaveData(LinkedList<NoteViewModel> notes)
        {            
            using (var sw = new StreamWriter(new FileStream(filename, FileMode.Create)))
            {
                var t = await Task.Run(() => JsonConvert.SerializeObject(notes, Formatting.Indented));
                sw.Write(t);
            }
        }
    }
}
