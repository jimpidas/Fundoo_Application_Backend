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
    }
}
