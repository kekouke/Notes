using Notes.Helper;
using Notes.Views;
using Syncfusion.DataSource.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NotesListViewModel : BaseViewModel
    {
        public ObservableCollection<NoteViewModel> LeftStack { get; set; } 
        public ObservableCollection<NoteViewModel> RightStack { get; set; }

        public ICommand AddNoteCommand { get; protected set; }
        public ICommand SaveNoteCommand { get; protected set; }
        public ICommand TapCommand { get; protected set; }
        public ICommand DeleteNoteCommand { get; protected set; }
        public ICommand SearchNoteCommand { get; protected set; }

        public INavigation Navigation { get; set; }

        public double LHeight { get; set; }
        public double RHeight { get; set; }
        public double Spacing { get; set; }

        private LinkedList<NoteViewModel> Notes { get; set; }
        private IEnumerable<NoteViewModel> SearchedNotes { get; set; }

        private ISaveable _saver;

        private bool _isButtonEnabled = true;
        public bool IsButtonEnabled 
        { 
            get => _isButtonEnabled; 
            set
            {
                if (_isButtonEnabled != value)
                {
                    _isButtonEnabled = value;
                    OnPropertyChange();
                }
            }
        }

        public NotesListViewModel()
        {
            AddNoteCommand = new Command(AddNote, () => IsButtonEnabled);
            TapCommand = new Command(EditNote, (_) => IsButtonEnabled);

            SaveNoteCommand = new Command(SaveNote);
            DeleteNoteCommand = new Command(DeleteNote);
            SearchNoteCommand = new Command(SearchNote);
            
            LeftStack = new ObservableCollection<NoteViewModel>();
            RightStack = new ObservableCollection<NoteViewModel>();
            
            Notes = NoteSaver.Instance.ReadData() ?? new LinkedList<NoteViewModel>();

            Restore();
        }

        private void SearchNote(object obj)
        {
            var text = obj as string;
            if (text.Length >= 1)
            {
                SearchedNotes = Notes.Where(n => n.Text.ToLower().Contains(text.ToLower()));
                Invalidate();
            }
            else
            {
                SearchedNotes = null;
                Invalidate();
            }
        }

        private void AddNote()
        {
            IsButtonEnabled = false;
            SetSaveStrategy(new SaveNewNote());
            Navigation.PushAsync(new NoteView(new NoteViewModel() { ListViewModel = this }));
        }

        private void EditNote(object note)
        {
            IsButtonEnabled = false;
            NoteViewModel temp = note as NoteViewModel;
            SetSaveStrategy(new EditOldNote());
            Navigation.PushAsync(new NoteView(temp));
        }

        private void SaveNote(object obj)
        {
            if (_saver.Save(obj, Notes))
            {
                Invalidate();
                NoteSaver.Instance.SaveData(Notes);
            }

            Back();
        }

        private async void DeleteNote(object obj)
        {
            var note = obj as NoteViewModel;

            if (await Application.Current.MainPage.DisplayAlert("Deleting note", "Do you want to delete the note?", "Yes", "No"))
            {
                Notes.Remove(Notes.Find(note));
                Invalidate();
                NoteSaver.Instance.SaveData(Notes);
            }
        }


        private void Invalidate()
        {
            RHeight = 0;
            LHeight = 0;
            LeftStack.Clear();
            RightStack.Clear();
            CorrectHeightColumn(SearchedNotes ?? Notes);
        }

        private void CorrectHeightColumn(IEnumerable<NoteViewModel> notes)
        {
            foreach (var note in notes.OrderByDescending(x => x.Date))
            {

                if (LHeight > RHeight)
                {
                    RightStack.Add(note);
                    RHeight += note.ActualHeight + Spacing;
                }
                else
                {
                    LeftStack.Add(note);
                    LHeight += note.ActualHeight + Spacing;
                }

            }

        }

        public void Restore()
        {
            Notes.ForEach(n => n.ListViewModel = this);
            Invalidate();
        }

        private void SetSaveStrategy(ISaveable saveStrategy) => _saver = saveStrategy;
        private void Back()
        {
            IsButtonEnabled = true;
            Navigation.PopAsync();
        }
    }
}
