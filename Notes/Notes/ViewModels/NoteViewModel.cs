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
            _note = new Note() { Date = DateTime.Now.ToString("g") };
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

        private void UpdateSymbolsCount()
        {
            Count = Text.Length;
        }

        private void ChangeLastEditDate() => Date = DateTime.Now.ToString("g");

        public string Date
        {
            get => _note.Date;
            set
            {
                if (_note.Date != value)
                {
                    _note.Date = value;
                    OnPropertyChange();
                }
            }
        }

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
