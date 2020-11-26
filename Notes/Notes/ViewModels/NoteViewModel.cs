using Newtonsoft.Json;
using Notes.Models;
using System;

namespace Notes.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        [JsonProperty]
        private Note _note;
        private NotesListViewModel lvm;

        public NoteViewModel()
        {
            _note = new Note(DateTime.Now);
        }

        [JsonIgnore]
        public string Text
        {
            get => _note.Text;
            set
            {
                if (_note.Text != value)
                {
                    _note.Text = value;
                    ChangeLastEditDate();
                    UpdateSymbolsCount();
                    OnPropertyChange();
                }
            }
        }

        [JsonIgnore]
        public DateTime Date
        {
            get => _note.LastChangeTime;
            set
            {
                if (_note.LastChangeTime != value)
                {
                    _note.LastChangeTime = value;
                    OnPropertyChange();
                }
            }
        }

        [JsonIgnore]
        public int Lenght
        {
            get => _note.Lenght;
            set
            {
                if (_note.Lenght != value)
                {
                    _note.Lenght = value;
                    OnPropertyChange();
                }
            }
        }

        [JsonIgnore]
        public NotesListViewModel ListViewModel
        {
            get => lvm;
            set => lvm = value;
        }

        [JsonIgnore]
        public double Height { get; set; }

        public double ActualHeight { get; set; }

        public bool CheckCorrectData() => Text != null && Text.Trim() != String.Empty;

        public void Update()
        {
            ActualHeight = Height;
            _note.OldChangeTime = _note.LastChangeTime;
        }

        private void ChangeLastEditDate() => Date = DateTime.Now;

        public bool CompareDateTime() => _note.LastChangeTime != _note.OldChangeTime;

        private void UpdateSymbolsCount() => Lenght = Text.Length;
    }
}
