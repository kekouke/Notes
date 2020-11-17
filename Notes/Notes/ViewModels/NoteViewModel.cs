using Notes.Models;
using System;

namespace Notes.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        private Note _note;
        public bool isEdited;
        private NotesListViewModel lvm;

        public NoteViewModel()
        {
            _note = new Note() { ChangeTime = DateTime.Now };
            isEdited = false;
        }

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

        public double Height { get; set; }

        public bool CheckCorrectData() => Text != null && Text.Trim() != String.Empty && !isEdited;

        private void ChangeLastEditDate() => Date = DateTime.Now.ToString("g");

        private void UpdateSymbolsCount() => Count = Text.Length;

        public string Date
        {
            get => _note.ChangeTime.ToString("g");
            set
            {
                var new_date = DateTime.Parse(value);
                if (_note.ChangeTime != new_date)
                {
                    DateDate = new_date;
                    _note.ChangeTime = new_date;
                    OnPropertyChange();
                }
            }
        }

        public DateTime DateDate { get; set; } // TODO

        public int Count //TODO: Rename
        {
            get => _note.Count;
            set
            {
                if (_note.Count != value)
                {
                    _note.Count = value;
                    OnPropertyChange();
                }
            }
        }

        public NotesListViewModel ListViewModel
        {
            get => lvm;
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChange();
                }
            }
        }
    }
}
