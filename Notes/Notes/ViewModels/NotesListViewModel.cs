using Notes.Helper;
using Notes.Views;
using Syncfusion.DataSource.Extensions;
using System;
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

        public INavigation Navigation { get; set; }

        public double LHeight { get; set; } = 0;
        public double RHeight { get; set; } = 0;
        public double Spacing { get; set; } = 0;

        private LinkedList<NoteViewModel> Notes { get; set; }

        private NoteViewModel _selectedNote;

        private ISaveable _saver;

        public NotesListViewModel()
        {
            AddNoteCommand = new Command(AddNote);
            SaveNoteCommand = new Command(SaveNote);
            DeleteNoteCommand = new Command(DeleteNote);
            TapCommand = new Command((object obj) => { SelectedNote = obj as NoteViewModel; });

            LeftStack = new ObservableCollection<NoteViewModel>();
            RightStack = new ObservableCollection<NoteViewModel>();

            Notes = NoteSaver.Instance.ReadData() ?? new LinkedList<NoteViewModel>();
            Restore();
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
                    Navigation.PushAsync(new NoteView(temp));
                }
            }
        }

        private void AddNote()
        {
            SetSaveStrategy(new SaveNewNote());
            Navigation.PushAsync(new NoteView(new NoteViewModel() { ListViewModel = this }));
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

            if (await Application.Current.MainPage.DisplayAlert("Delete the note", "Do you wanna delete the note?", "Yes", "No"))
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
            CorrectHeightColumn();
        }

        private void CorrectHeightColumn()
        {
            foreach (var note in Notes.OrderByDescending(x => x.Date))
            {
                if (LHeight > RHeight)
                {
                    RightStack.Add(note);
                    RHeight += note.Height + Spacing;
                }
                else
                {
                    LeftStack.Add(note);
                    LHeight += note.Height + Spacing;
                }
            }
        }

        private void Restore()
        {
            Notes.ForEach(n => n.ListViewModel = this);
            Invalidate();
        }

        private void SetSaveStrategy(ISaveable saveStrategy) => _saver = saveStrategy;
        private void Back() => Navigation.PopAsync();
    }
}
