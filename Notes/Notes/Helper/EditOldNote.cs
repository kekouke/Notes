using Notes.ViewModels;
using System.Collections.Generic;

namespace Notes.Helper
{
    public class EditOldNote : ISaveable
    {
        public void Save(object obj, LinkedList<NoteViewModel> notes)
        {
            var new_note = obj as NoteViewModel;
            if (new_note.CompareDateTime())
            {
                notes.Remove(notes.Find(new_note));
                notes.AddFirst(new_note);
                new_note.Update();
            }
        }
    }
}
