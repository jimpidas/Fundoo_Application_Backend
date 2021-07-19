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
        NoteResponse AddNote(AddNote note, int UserID);
        List<Note> GetAllNotes(int UserId);
        bool DeleteNote(int UserID,int noteID);

        public NoteResponse UpdateNote(AddNote updateNote, int NotesID);

        void UpdateTitle(int nodeID, string title);

        public bool UpdateBody(int userId, int noteId, AddBody addBody);

        bool SetNoteReminder(ReminderRequest reminder);

        bool ToggleNotePin(int noteID, int userID);

        bool ToggleArchive(int noteID, int userID);

        public bool UpdateColor(int userID, int noteID, ColorRequest color);


        public void UpdateTrash(int noteId, bool Trash);


        List<NoteResponse> GetTrashedNotes(int userID);

        
        List<NoteResponse> GetArchievedNotes(int userID);

        
        public List<NoteResponse> GetPinnedNotes(int userID);

        bool RemoveReminder(int userID, int noteID);
    }
}
