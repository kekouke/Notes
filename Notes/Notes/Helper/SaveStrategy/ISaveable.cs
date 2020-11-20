using Notes.ViewModels;
using System.Collections.Generic;

namespace Notes.Helper
{
    interface ISaveable
    {
        bool Save(object obj, LinkedList<NoteViewModel> notes);
    }
}
