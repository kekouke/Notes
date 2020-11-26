using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Notes.Helper
{
    public class SaveNewNote : ISaveable
    {
        public bool Save(object obj, LinkedList<NoteViewModel> notes)
        {
            if ((obj as NoteViewModel).CheckCorrectData())
            {
                notes.AddFirst(obj as NoteViewModel);
                (obj as NoteViewModel).Update();
                return true;
            }
            return false;
        }
    }
}
