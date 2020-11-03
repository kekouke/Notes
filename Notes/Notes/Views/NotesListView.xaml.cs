using Notes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListView : ContentPage
    {
        public NotesListViewModel ViewModel { get; set; }
        public NotesListView()
        {
            InitializeComponent();

            ViewModel = new NotesListViewModel()
            {
                Navigation = this.Navigation,
                LHeight = l_stack,
                RHeight = r_stack
            };

            BindingContext = ViewModel;
        }
    }
}