using Notes.Helper;
using Notes.Views;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            AddNoteCommand = new Command(AddNote);
            SaveNoteCommand = new Command(SaveNote);
            DeleteNoteCommand = new Command(DeleteNote);
            TapCommand = new Command(EditNote);
            
            LeftStack = new ObservableCollection<NoteViewModel>();
            RightStack = new ObservableCollection<NoteViewModel>();

            Notes = NoteSaver.Instance.ReadData() ?? new LinkedList<NoteViewModel>();
            Restore();
        }

        private void AddNote()
        {
            IsButtonEnabled = false;
            SetSaveStrategy(new SaveNewNote());
            Navigation.PushAsync(new NoteView(new NoteViewModel() { ListViewModel = this }));
        }

        private void EditNote(object note)
        {
            if (IsButtonEnabled)
            {
                IsButtonEnabled = false;
                NoteViewModel temp = note as NoteViewModel;
                SetSaveStrategy(new EditOldNote());
                Navigation.PushAsync(new NoteView(temp));
            }
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
        private void Back()
        {
            IsButtonEnabled = true;
            Navigation.PopAsync();
        }
    }
}
