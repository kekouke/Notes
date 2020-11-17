using Notes.Models;
using System;

namespace Notes.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        private Note _note;
        private NotesListViewModel lvm;

        private bool _isNew;

        public NoteViewModel()
        {
            _note = new Note(DateTime.Now);
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

        public bool CheckCorrectData() => Text != null && Text.Trim() != String.Empty;

        private void ChangeLastEditDate()
        {
            _note.LastChangeTime = DateTime.Now;
            Date = _note.LastChangeTime.ToString("g");
        }

        public bool CompareDateTime() => _note.LastChangeTime != _note.OldChangeTime;

        private void UpdateSymbolsCount() => Lenght = Text.Length;

        public void Update() => _note.OldChangeTime = _note.LastChangeTime;

        public string Date
        {
            get => _note.LastChangeTime.ToString("g");
            set
            {
                var new_date = DateTime.Parse(value);
                if (_note.LastChangeTime != new_date)
                {
                    DateDate = new_date;
                    OnPropertyChange();
                }
            }
        }

        public DateTime DateDate { get; set; } // TODO

        // Убрать привязку к свойству и добавить все в xaml
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
