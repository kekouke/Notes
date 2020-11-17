using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Notes.Helper
{
    public class SaveNewNote : ISaveable
    {
        public void Save(object obj, LinkedList<NoteViewModel> notes)
        {
            notes.AddFirst(obj as NoteViewModel);
            (obj as NoteViewModel).Update();
        }
    }
}
