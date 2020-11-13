using Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListView : ContentPage
    {
        public NotesListViewModel ViewModel { get; private set; }

        public NotesListView()
        {
            InitializeComponent();
            ViewModel = new NotesListViewModel() { Navigation = this.Navigation };
            BindingContext = ViewModel;
        }

        private void GestureScrollView_SwipeLeft(object sender, System.EventArgs e)
        {
            DisplayAlert("Gesture Info", "Swipe Left Detected", "OK");
        }

        private void GestureScrollView_SwipeRight(object sender, System.EventArgs e)
        {
            DisplayAlert("Gesture Info", "Swipe Right Detected", "OK");
        }

        /*private async void PanGestureRecognizer_PanUpdated(object panSender, PanUpdatedEventArgs panArgs)
        {
            var newNoteFrame = (panSender as StackLayout).Parent as Frame;

            switch (panArgs.StatusType)
            {
                case GestureStatus.Canceled:
                    newNoteFrame.TranslationX = 0;
                    totalX = 0;
                    break;
                case GestureStatus.Started:
                    //степень перемещенеия элемента
                    newNoteFrame.TranslationX = 0;
                    totalX = 0;
                    break;
                case GestureStatus.Completed:
                    if (totalX > 0 && r_stack.Children.Contains(newNoteFrame))
                    {
                        if (await DisplayAlert("Подтверждение удаления", "Удалить заметку безвозвратно?", "Да", "Нет"))
                        {
                            r_stack.Children.Remove(newNoteFrame);
                            //checkCount();
                            //safeNotes();
                        }
                        totalX = 0;
                    }
                    else if (totalX < 0 && l_stack.Children.Contains(newNoteFrame))
                    {
                        if (await DisplayAlert("Подтверждение удаления", "Удалить заметку безвозвратно?", "Да", "Нет"))
                        {
                            l_stack.Children.Remove(newNoteFrame);
                            //checkCount();
                            //safeNotes();
                        }
                        totalX = 0;
                    }
                    newNoteFrame.TranslationX = 0;

                    break;
                case GestureStatus.Running:
                    if (panArgs.TotalX > 0 && r_stack.Children.Contains(newNoteFrame))
                    {
                        newNoteFrame.TranslationX = panArgs.TotalX;
                        totalX = panArgs.TotalX;
                    }
                    else if (panArgs.TotalX < 0 && l_stack.Children.Contains(newNoteFrame))
                    {
                        newNoteFrame.TranslationX = panArgs.TotalX;
                        totalX = panArgs.TotalX;
                    }
                    break;
            };*/
        // ..}

        /* private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
         {
             switch(e.Direction)
             {
                 case SwipeDirection.Left:
                     System.Console.WriteLine("Left");
                     break;
                 case SwipeDirection.Right:
                     System.Console.WriteLine("Right");
                     break;
             }
         }*/
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