using Notes.Models;

namespace Notes.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        private Note _note;
        public bool isEdited;
        private NotesListViewModel lvm;

        public NoteViewModel()
        {
            _note = new Note();
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
