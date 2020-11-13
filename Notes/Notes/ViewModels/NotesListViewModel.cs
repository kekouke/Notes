using Notes.Views;
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

        private NoteViewModel _selectedNote;
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
            SaveNoteCommand = new Command(SaveMote);
            DeleteNoteCommand = new Command(DeleteNote);
            TapCommand = new Command((object obj) => { SelectedNote = obj as NoteViewModel; });

            LeftStack = new ObservableCollection<NoteViewModel>();
            RightStack = new ObservableCollection<NoteViewModel>();
        }

        // TODO
        private async void DeleteNote(object obj)
        {
            var note = obj as NoteViewModel;

            if (await Application.Current.MainPage.DisplayAlert("Delete the note", "Do you wanna delete the note?", "Yes", "No"))
            {
                if (LeftStack.Contains(note))
                {
                    LeftStack.Remove(note);
                }
                else if (RightStack.Contains(note))
                {
                    RightStack.Remove(note);
                }

                CorrectHeightColumn();
            }
        }

        // Очень сомнительная идея с флажком
        private void SaveMote(object obj) // Вот тут должен быть алгоритм фильтрации 
        {
            var note = obj as NoteViewModel;

            if (!note.isEdited)
            {
                if (LHeight > RHeight)
                {
                    RightStack.Add(note);
                } 
                else
                {
                    LeftStack.Add(note);
                }
            }

            Back();
        }

        private void AddNote()
        {
            Navigation.PushAsync(new NoteView(new NoteViewModel() { ListViewModel = this }));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void CorrectHeightColumn()
        {
            if (RightStack.Count > LeftStack.Count)
            {
                LeftStack.Add(RightStack.Last());
                RightStack.RemoveAt(RightStack.Count - 1);
            }
            if (LeftStack.Count - RightStack.Count > 1)
            {
                RightStack.Add(LeftStack.Last());
                LeftStack.RemoveAt(LeftStack.Count - 1);
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
                    temp.isEdited = true;
                    Navigation.PushAsync(new NoteView(temp));
                }
            } 
        }

    }
}
