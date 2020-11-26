using Notes.ViewModels;
using System.Collections.Generic;

namespace Notes.Helper
{
    public class EditOldNote : ISaveable
    {
        public bool Save(object obj, LinkedList<NoteViewModel> notes)
        {
            var new_note = obj as NoteViewModel;
            if (new_note.CompareDateTime())
            {
                notes.Remove(notes.Find(new_note));
                if (new_note.CheckCorrectData())
                {
                    notes.AddFirst(new_note);
                    new_note.Update();
                }

                return true;
            }

            return false;
        }
    }
}
