using Notes.ViewModels;
using System.Collections.Generic;

namespace Notes.Helper
{
    interface ISaveable
    {
        void Save(object obj, LinkedList<NoteViewModel> notes);
    }
}
