using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.NotesInterface
{
    public interface INoteBL
    {
        NoteResponse AddNote(AddNote note, int UserID);
        List<Note> GetAllNotes(int UserId);
        bool DeleteNote(int noteID);

        void UpdateTitle(int nodeID, string title);
    }
}
