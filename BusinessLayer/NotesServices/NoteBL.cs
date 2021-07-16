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

        public NoteResponse AddNote(AddNote note)
        {
            try
            {
                return this.noteRL.AddNote(note);
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
        public bool DeleteNote(int noteID)
        {
            try
            {
                bool result = noteRL.DeleteNote(noteID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
