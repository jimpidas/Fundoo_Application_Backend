using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Repository.NotesInterface;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.NotesServices
{
    public class NoteRL: INoteRL
    {
        readonly UsersContext _usersContext;
       
        public NoteRL(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public NoteResponse AddNote(AddNote note)
        {
            try
            {
                var user = _usersContext.Users.FirstOrDefault(e => e.UserModelID == note.UserModelID);
                if (user != null)
                {
                    Note Addnotes = new Note
                    {
                        //UserModelID = note.UserModelID,
                        Title = note.Title,
                        Body = note.Body,
                        Reminder = note.Reminder,
                        Color = note.Color,
                        IsArchived = note.IsArchived,
                        IsTrash = note.IsTrash,
                        IsPin = note.IsPin,
                        CreatedDate = note.CreatedDate,
                        ModifiedDate = note.ModifiedDate,
                        //User = user
                    };

                    _usersContext.Notes.Add(Addnotes);
                    _usersContext.SaveChanges();

                    NoteResponse noteResponseData = new NoteResponse()
                    {
                        NotesId = Addnotes.NotesId,
                        UserModelID=Addnotes.UserModelID,
                        Title = Addnotes.Title,
                        Body = Addnotes.Body,
                        Reminder = Addnotes.Reminder,
                        Color = Addnotes.Color,
                        IsPin = Addnotes.IsPin,
                        IsArchived = Addnotes.IsArchived,
                        IsTrash = Addnotes.IsTrash,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    return noteResponseData;
                }
                else
                {
                    throw new Exception("UserId Not Exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Note> GetAllNotes(int UserId)
        {
            try
            {
                var list = _usersContext.Notes.Where(e => e.UserModelID == UserId).ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
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
                if (_usersContext.Notes.Any(n => n.NotesId == noteID))
                {
                    var note = _usersContext.Notes.Find(noteID);
                    if (note.IsTrash)
                    {
                        _usersContext.Entry(note).State = EntityState.Deleted;
                    }
                    else
                    {
                        note.IsTrash = true;
                        note.IsPin = false;
                        note.IsArchived = false;
                    }
                    _usersContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
