using Notes.Views;
using Notes.Visual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class NotesListViewModel : BaseViewModel
    {
        public ObservableCollection<NoteViewModel> LeftStack { get; set; } = new ObservableCollection<NoteViewModel>();
        public ObservableCollection<NoteViewModel> RightStack { get; set; } = new ObservableCollection<NoteViewModel>();

        private NoteViewModel _selectedNote;
        public ICommand AddNoteCommand { get; protected set; }
        public ICommand SaveNoteCommand { get; protected set; }
        public INavigation Navigation { get; set; }

        public double LHeight { get; set; } = 0;
        public double RHeight { get; set; } = 0;

        public NotesListViewModel()
        {
            AddNoteCommand = new Command(AddNote);
            SaveNoteCommand = new Command(SaveMote);
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
