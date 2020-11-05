using Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListView : ContentPage
    {
        public NotesListViewModel ViewModel { get; private set; }
        public double totalX { get; private set; }

        public NotesListView()
        {
            InitializeComponent();
            ViewModel = new NotesListViewModel() { Navigation = this.Navigation };
            BindingContext = ViewModel;
        }

        private async void PanGestureRecognizer_PanUpdated(object panSender, PanUpdatedEventArgs panArgs)
        {
            var newNoteFrame = panSender as Frame;
            switch (panArgs.StatusType)
            {
                case GestureStatus.Canceled:
                case GestureStatus.Started:
                    newNoteFrame.TranslationX = 0;
                    break;
                case GestureStatus.Completed:
                    if (totalX > 0 && r_stack.Children.Contains(panSender as Frame))  // < 0 for left
                    {
                        if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                        {
                            ViewModel.DeleteNoteCommand.Execute((panSender as Frame).BindingContext);
                        }
                        totalX = 0;
                    }
                    else if (totalX < 0 && l_stack.Children.Contains(panSender as Frame))  // < 0 for left
                    {
                        if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                        {
                            ViewModel.DeleteNoteCommand.Execute((panSender as Frame).BindingContext);
                        }
                        totalX = 0;
                    }
                    newNoteFrame.TranslationX = 0;
                    break;
                case GestureStatus.Running:
                    if (panArgs.TotalX > 0 && r_stack.Children.Contains(panSender as Frame))  // < 0 for left
                    {
                        newNoteFrame.TranslationX = panArgs.TotalX;
                        totalX = panArgs.TotalX;
                    }
                    else if (panArgs.TotalX < 0 && l_stack.Children.Contains(panSender as Frame))  // < 0 for left
                    {
                        newNoteFrame.TranslationX = panArgs.TotalX;
                        totalX = panArgs.TotalX;
                    }
                    break;
            }
            /*            System.Console.WriteLine("Привет");
                        switch (e.StatusType)
                        {
                            case GestureStatus.Started:
                                (sender as Frame).TranslationX = 0;
                                break;
                            case GestureStatus.Running: // TODO:
                                if (e.TotalX > 0 && r_stack.Children.Contains(sender as Frame))
                                {
                                    (sender as Frame).TranslationX = e.TotalX;
                                    TotalX = e.TotalX;
                                } 
                                else if (e.TotalX < 0 && l_stack.Children.Contains(sender as Frame))
                                {
                                    (sender as Frame).TranslationX = e.TotalX;
                                    TotalX = e.TotalX;
                                }

                                break;
                            case GestureStatus.Completed:

                                if (TotalX != 0)
                                {
                                    if (await DisplayAlert("Delete Note?", "Do you want to delete the note?", "Yes", "No"))
                                    {
                                        ViewModel.DeleteNoteCommand.Execute((sender as Frame).BindingContext);
                                    }
                                }


                                (sender as Frame).TranslationX = 0;
                                break;
                        }*/
        }
    }
}