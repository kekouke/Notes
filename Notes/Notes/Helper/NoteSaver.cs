using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Notes.Helper
{
    public sealed class NoteSaver
    {
        private static readonly Lazy<NoteSaver> _instance =
            new Lazy<NoteSaver>(() => new NoteSaver());

        public static NoteSaver Instance { get => _instance.Value; }

        private NoteSaver() {}

        public void ReadData()
        {
            using (var sr = new StreamReader(@"notes.json"))
            {
                JsonConvert.DeserializeObject<object>(sr.ReadToEnd());
            }

        }
    }
}
