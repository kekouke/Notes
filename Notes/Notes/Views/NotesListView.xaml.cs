﻿using Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListView : ContentPage
    {
        public NotesListViewModel ViewModel { get;  set; }

        public NotesListView()
        {
            InitializeComponent();
            ViewModel = new NotesListViewModel() { Navigation = this.Navigation, Spacing = l_stack.Spacing };
            BindingContext = ViewModel;
        }

        private void GestureScrollView_SwipeLeft(object sender, System.EventArgs e)
        {
            var noteFrame = (sender as ScrollView);
            ViewModel.DeleteNoteCommand.Execute(noteFrame.BindingContext);
        }

        private void GestureScrollView_SwipeRight(object sender, System.EventArgs e)
        {
            var noteFrame = (sender as ScrollView);
            ViewModel.DeleteNoteCommand.Execute(noteFrame?.BindingContext);
        }
    }
}