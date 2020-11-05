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
            _note = new Note() { Date = DateTime.Now };
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
                    OnPropertyChange();
                }
            }
        }

        public DateTime Date
        {
            get => _note.Date;
            set
            {
                if (_note.Date != Date)
                {
                    _note.Date = value;
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
