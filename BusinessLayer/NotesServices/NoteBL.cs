using BusinessLayer.NotesInterface;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Repository.NotesInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.NotesServices
{
    public class NoteBL : INoteBL
    {
        readonly INoteRL noteRL;

        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public NoteResponse AddNote(AddNote note, int UserID)
        {
            try
            {
                return this.noteRL.AddNote(note, UserID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Note> GetAllNotes(int UserId)
        {
            try
            {
                return this.noteRL.GetAllNotes(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteNote(int UserID,int noteID)
        {
            try
            {
                bool result = noteRL.DeleteNote(UserID, noteID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteResponse UpdateNote(AddNote updateNote, int NotesID)
        {
            try
            {
               return this.noteRL.UpdateNote(updateNote, NotesID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateTitle(int nodeID, string title)
        {
            try
            {
                this.noteRL.UpdateTitle( nodeID, title);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool UpdateBody(int userId, int noteId, AddBody addBody)
        {
            bool responseData = noteRL.UpdateBody(userId, noteId, addBody);
            return responseData;
        }
        public List<NoteResponse> GetTrashedNotes(int userID)
        {
            List<NoteResponse> userTrashedData = noteRL.GetTrashedNotes(userID);
            return userTrashedData;
        }


        


       
        public bool UpdateColor(int userID, int noteID, ColorRequest color)
        {
            bool responseData = noteRL.UpdateColor(userID, noteID, color);
            return responseData;
        }

        
               
        public void UpdateTrash(int noteId, bool Trash)
        {
            try
            {
                this.noteRL.UpdateTrash(noteId, Trash);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public List<NoteResponse> GetArchievedNotes(int userID)
        {
            List<NoteResponse> userArchievedData = noteRL.GetArchievedNotes(userID);
            return userArchievedData;
        }

       
        public List<NoteResponse> GetPinnedNotes(int userID)
        {
            List<NoteResponse> userNoteResponseData = noteRL.GetPinnedNotes(userID);
            return userNoteResponseData;
        }

        public bool RemoveReminder(int userID, int noteID)
        {
            try
            {
                if (noteID == default)
                {
                    throw new Exception("NoteID missing");
                }
                return noteRL.RemoveReminder(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetNoteReminder(ReminderRequest reminder)
        {
            try
            {
                if (reminder.Reminder < DateTime.Now)
                {
                    throw new Exception("Time is passed");
                }
                if (reminder.NotesId == default)
                {
                    throw new Exception("NoteID missing");
                }
                return noteRL.SetNoteReminder(reminder);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ToggleArchive(int noteID, int userID)
        {
            try
            {
                return noteRL.ToggleArchive(noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public bool ToggleNotePin(int noteID, int userID)
        {
            try
            {
                return noteRL.ToggleNotePin(noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
