using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.NotesInterface
{
    public interface INoteBL
    {
        NoteResponse AddNote(AddNote note, int UserID);
        List<Note> GetAllNotes(int UserId);
        public Task<ICollection<NoteResponse>> GetActiveNotes(int UserID);
        public  Task<bool> DeleteNote(int UserID, int noteID);
        public NoteResponse UpdateNote(AddNote updateNote, int NotesID);
        bool SetNoteReminder(ReminderRequest reminder);
        bool RemoveReminder(int userID, int noteID);
        public bool UpdateColor(int userID, int noteID, ColorRequest color);
        bool ToggleNotePin(int noteID, int userID);
        public List<NoteResponse> GetPinnedNotes(int userID);
        bool ToggleArchive(int noteID, int userID);
        List<NoteResponse> GetArchievedNotes(int userID);
        public void UpdateTrash(int noteId, bool Trash);    
        List<NoteResponse> GetTrashedNotes(int userID);
    }
}
