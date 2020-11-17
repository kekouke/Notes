using Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteView : ContentPage
    {
        public NoteViewModel ViewModel { get; private set; }
        public NoteView(NoteViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing() => ViewModel.ListViewModel.SaveNoteCommand.Execute(ViewModel);
    }
}