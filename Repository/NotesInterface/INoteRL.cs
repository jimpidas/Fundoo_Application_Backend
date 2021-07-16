using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.NotesInterface
{
    public interface INoteRL
    {
        NoteResponse AddNote(AddNote note);
        List<Note> GetAllNotes(int UserId);
        bool DeleteNote(int noteID);
    }
}
