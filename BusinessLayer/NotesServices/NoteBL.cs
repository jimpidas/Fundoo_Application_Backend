using BusinessLayer.NotesInterface;
using BusinessLayer.RedisCacheService;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Repository.NotesInterface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.NotesServices
{
    public class NoteBL : INoteBL
    {
        readonly INoteRL noteRL;
        private readonly IDistributedCache distributedCache;
        readonly RedisCacheServiceBL redis;

        public NoteBL(INoteRL noteRL, IDistributedCache distributedCache)
        {
            this.noteRL = noteRL;
            this.distributedCache = distributedCache;
            redis = new RedisCacheServiceBL(this.distributedCache);
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
        public async Task<bool> DeleteNote(int UserID,int noteID)
        {
            try
            {
                await redis.RemoveNotesRedisCache(UserID);
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


        public async Task<ICollection<NoteResponse>> GetActiveNotes(int UserID)
        {
            var cacheKey = UserID.ToString();
            string serializedNotes;
            ICollection<NoteResponse> Notes;
            try
            {
                var redisNoteCollection = await distributedCache.GetAsync(cacheKey);
                if (redisNoteCollection != null)
                {
                    serializedNotes = Encoding.UTF8.GetString(redisNoteCollection);
                    Notes = JsonConvert.DeserializeObject<List<NoteResponse>>(serializedNotes);
                }
                else
                {
                    Notes = noteRL.GetNotes(UserID, false, false);
                    await redis.AddNotesRedisCache(cacheKey, Notes);
                }
                return Notes;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
