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
        ICollection<NoteResponse> noteResponse;

        public NoteRL(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public NoteResponse AddNote(AddNote note,int UserID)
        {
            try
            {
                
                var user = _usersContext.Users.FirstOrDefault(e => e.UserModelID == UserID);
                if (user != null)
                {
                    var Addnotes = new Note
                    {
                        UserModelID = UserID,
                        Title = note.Title,
                        Body = note.Body,
                        Reminder = note.Reminder,
                        Color = note.Color,
                        IsArchived = note.IsArchived,
                        IsTrash = note.IsTrash,
                        IsPin = note.IsPin,
                        CreatedDate = note.CreatedDate,
                        ModifiedDate = note.ModifiedDate,
                       
                    };

                    _usersContext.Notes.Add(Addnotes);
                    _usersContext.SaveChanges();

                    var noteResponseData = new NoteResponse
                    {
                        NotesId = Addnotes.NotesId,
                        UserModelID=Addnotes.UserModelID,
                        Title = Addnotes.Title,
                        Body = Addnotes.Body,
                        Reminder = (DateTime)Addnotes.Reminder,
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

        public ICollection<NoteResponse> GetNotes(int UserID, bool IsTrash, bool IsArchieve)
        {
            try
            {
                noteResponse = _usersContext.Notes.Where(N => N.UserModelID.Equals(UserID)
                && N.IsTrash == IsTrash && N.IsArchived == IsArchieve).Select(N =>
                    new NoteResponse
                    {
                        UserModelID = N.UserModelID,
                        NotesId = N.NotesId,
                        Title = N.Title,
                        Body = N.Body,
                        Reminder = (DateTime)N.Reminder,
                        Color = N.Color,
                        IsArchived = N.IsArchived,
                        IsPin = N.IsPin,
                        IsTrash = N.IsTrash,
                       
                    }
                    ).ToList();
                return noteResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteNote(int UserID,int noteID)
        {
            try
            {
                if (_usersContext.Notes.Any(n => n.NotesId == noteID && n.UserModelID==UserID))
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


        public NoteResponse UpdateNote(AddNote updateNoteRequest,int NotesID)
        {
            try
            {
                NoteResponse userNoteResponseData = null;
                var userData = _usersContext.Notes.FirstOrDefault(u => u.NotesId == NotesID);
                userData.Title = updateNoteRequest.Title;
                userData.Body = updateNoteRequest.Body;
                
                userNoteResponseData = new NoteResponse()
                {
                    NotesId=userData.NotesId,
                    UserModelID=userData.UserModelID,
                    Title = userData.Title,
                    Body = userData.Body,
                    Color = userData.Color,
                    IsPin = userData.IsPin,
                    IsArchived = userData.IsArchived,
                    IsTrash = userData.IsTrash,
                    Reminder= (DateTime)userData.Reminder,
                    ModifiedDate = DateTime.Now,
                    CreatedDate=userData.CreatedDate,
                };
                _usersContext.SaveChanges();
                return userNoteResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<NoteResponse> GetTrashedNotes(int userID)
        {
            try
            {
                List<NoteResponse> userNoteLists = _usersContext.Notes.
                    Where(u => u.UserModelID == userID && u.IsTrash == true).
                    Select(u => new NoteResponse
                    {
                        NotesId = u.NotesId,
                        Title = u.Title,
                        Body = u.Body,
                        Reminder = (DateTime)u.Reminder,
                        Color = u.Color,
                        IsPin = u.IsPin,
                        IsArchived = u.IsArchived,
                        IsTrash = u.IsTrash,
                        CreatedDate = u.CreatedDate,
                        ModifiedDate = u.ModifiedDate

                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool UpdateReminder(int userID, int noteID, ReminderRequest reminder)
        {
            try
            {
                var userData = _usersContext.Notes.FirstOrDefault(user => user.UserModelID == userID && user.NotesId == noteID);
                if (userData != null)
                {
                    userData.Reminder = reminder.Reminder;
                    _usersContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        public bool UpdateColor(int userID, int noteID, ColorRequest color)
        {
            try
            {
                var userData = _usersContext.Notes.FirstOrDefault(user => user.UserModelID == userID && user.NotesId == noteID);
                if (userData != null)
                {
                    userData.Color = color.Color;
                    _usersContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public void UpdateTrash(int noteId, bool Trash)
        {
            try
            {
                var result = _usersContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.IsTrash = Trash;
                    _usersContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       
        public List<NoteResponse> GetArchievedNotes(int userID)
        {
            try
            {
                List<NoteResponse> userNoteLists = _usersContext.Notes.
                    Where(user => user.UserModelID == userID && user.IsArchived == true).
                    Select(user => new NoteResponse
                    {
                        NotesId = user.NotesId,
                        Title = user.Title,
                        Body = user.Body,
                        Reminder = (DateTime)user.Reminder,
                        Color = user.Color,
                        IsPin = user.IsPin,
                        IsArchived = user.IsArchived,
                        IsTrash = user.IsTrash,
                        CreatedDate = user.CreatedDate,
                        ModifiedDate = user.ModifiedDate
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<NoteResponse> GetPinnedNotes(int userID)
        {
            try
            {
                List<NoteResponse> userNoteLists = _usersContext.Notes.
                    Where(user => user.UserModelID == userID && user.IsPin == true).
                    Select(user => new NoteResponse
                    {
                        NotesId = user.NotesId,
                        Title = user.Title,
                        Body = user.Body,
                        Reminder = (DateTime)user.Reminder,
                        Color = user.Color,
                        IsPin = user.IsPin,
                        IsArchived = user.IsArchived,
                        IsTrash = user.IsTrash,
                        CreatedDate = user.CreatedDate,
                        ModifiedDate = user.ModifiedDate
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool SetNoteReminder(ReminderRequest reminder)
        {
            try
            {
                _usersContext.Notes.FirstOrDefault(
                    N => N.NotesId == reminder.NotesId && N.UserModelID == reminder.UserModelID).Reminder = reminder.Reminder;
                _usersContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool RemoveReminder(int userID, int noteID)
        {
            try
            {
                if (_usersContext.Notes.Any(N => N.UserModelID == userID && N.NotesId == noteID))
                {
                    _usersContext.Notes.FirstOrDefault(N => N.NotesId == noteID).Reminder = null;
                    _usersContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
        public bool ToggleArchive(int noteID, int userID)
        {
            try
            {
                var note = _usersContext.Notes.FirstOrDefault(N => N.NotesId == noteID && N.UserModelID == userID);
                if (note.IsArchived)
                {
                    note.IsArchived = false;
                }
                else
                {
                    note.IsArchived = true;
                }
                _usersContext.SaveChanges();
                return true;
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
                var note = _usersContext.Notes.FirstOrDefault(N => N.NotesId == noteID && N.UserModelID == userID);
                if (note.IsPin)
                {
                    note.IsPin = false;
                }
                else
                {
                    note.IsPin = true;
                }
                _usersContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }   
}
