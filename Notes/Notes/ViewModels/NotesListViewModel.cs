using Notes.Helper;
using Notes.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NotesListViewModel : BaseViewModel
    {
        public ObservableCollection<NoteViewModel> LeftStack { get; set; } 
        public ObservableCollection<NoteViewModel> RightStack { get; set; }

        private LinkedList<NoteViewModel> Notes { get; set; }

        private NoteViewModel _selectedNote;

        private ISaveable _saver;

        public ICommand AddNoteCommand { get; protected set; }
        public ICommand SaveNoteCommand { get; protected set; }
        public ICommand TapCommand { get; protected set; }
        public ICommand DeleteNoteCommand { get; protected set; }

        public INavigation Navigation { get; set; }

        public double LHeight { get; set; } = 0;
        public double RHeight { get; set; } = 0;

        public NotesListViewModel()
        {
            AddNoteCommand = new Command(AddNote);

            SaveNoteCommand = new Command(SaveNote);
            DeleteNoteCommand = new Command(DeleteNote);
            TapCommand = new Command((object obj) => { SelectedNote = obj as NoteViewModel; });

            LeftStack = new ObservableCollection<NoteViewModel>();
            RightStack = new ObservableCollection<NoteViewModel>();
            Notes = new LinkedList<NoteViewModel>();
        }

        // TODO
        private async void DeleteNote(object obj)
        {
            var note = obj as NoteViewModel;

            if (await Application.Current.MainPage.DisplayAlert("Delete the note", "Do you wanna delete the note?", "Yes", "No"))
            {
                Notes.Remove(Notes.Find(note));
                Invalidate();
            }
        }


        private void Invalidate()
        {
            RHeight = 0;
            LHeight = 0;
            LeftStack.Clear();
            RightStack.Clear();
            CorrectHeightColumn();
        }

        private void SaveNote(object obj)
        {
            _saver.Save(obj, Notes);
            Invalidate();
            Back();
        }

        private void SetSaveStrategy(ISaveable saveStrategy)
        {
            _saver = saveStrategy;
        }

        private void AddNote()
        {
            SetSaveStrategy(new SaveNewNote());
            Navigation.PushAsync(new NoteView(new NoteViewModel() { ListViewModel = this }));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void CorrectHeightColumn()
        {
            foreach (var note in Notes.OrderByDescending(x => x.DateDate)) //TODO
            {
                if (LHeight > RHeight)
                {
                    RightStack.Add(note);
                    RHeight += note.Height + 6;
                }
                else
                {
                    LeftStack.Add(note);
                    LHeight += note.Height + 6;
                }
            }
        }

        public NoteViewModel SelectedNote 
        { 
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    NoteViewModel temp;
                    _selectedNote = null;
                    OnPropertyChange();
                    temp = value;
                    SetSaveStrategy(new EditOldNote());
/*                    temp.isEdited = true;
*/                    Navigation.PushAsync(new NoteView(temp));
                }
            } 
        }

    }
}
