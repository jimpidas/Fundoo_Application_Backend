using BusinessLayer.NotesInterface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : Controller
    {
        readonly INoteBL noteBL;

        public NotesController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddUserNote(AddNote Note)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    NoteResponse result = noteBL.AddNote(Note, UserID);
                    return Ok(new { success = true, Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllNotes()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserModelID", StringComparison.InvariantCultureIgnoreCase));
            int UserId = Convert.ToInt32(idClaim.Value);
            var result = this.noteBL.GetAllNotes(UserId);
            if (result != null) return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}", data = result });
            return BadRequest(new { success = false, message = $"No such UserId Exist." });
        }


        [HttpGet("ActiveNotes")]
        public IActionResult GetActiveNotes()
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    var result = noteBL.GetActiveNotes(UserID);
                    return Ok(new { success = true, user = Email, Notes = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.InnerException });
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteNote(int NoteID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    bool result = this.noteBL.DeleteNote(UserID,NoteID).Result;
                    return Ok(new { success = true, user = Email, Notes = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateNotes(AddNote updateNoteRequest, int NodeID)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserModelID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                NoteResponse userUpdateData = noteBL.UpdateNote(updateNoteRequest, NodeID);
                if (userUpdateData == null)
                {
                    return Ok(new { success = false, message = $"Update Failed" });
                }
                else
                {
                    return Ok(new { success = true, message = "Notes Updated Successfully", userUpdateData });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }


        [Authorize]
        [HttpPut("Color")]
        public IActionResult UpdateColor(int noteID, ColorRequest color)
        {
            try
            {
                bool success = false, data;
                string message;

                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("UserModelID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = noteBL.UpdateColor(userId, noteID, color);

                if (data)
                {
                    success = true;
                    message = "Color Set Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = $"Update Failed";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        [Authorize]
        [HttpGet("Trash")]
        public IActionResult GetAllTrashedNotes()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("UserModelID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                List<NoteResponse> userNoteResponseDataList = noteBL.GetTrashedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {

                    return Ok(new { success = false, message = "No Trashed Notes" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [Authorize]
        [HttpPut("Trash")]
        public ActionResult UpdateTrash(int noteId, AddTrash UpdateTrash)
        {
            try
            {
                this.noteBL.UpdateTrash(noteId, UpdateTrash.Trash);
                return Ok(new { success = true, message = $"Trash Update Successfull" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"Update Failed" });
            }
        }


        [Authorize]
        [HttpPut("SetReminder")]
        public IActionResult SetNoteReminder(ReminderRequest Reminder)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    Reminder.UserModelID = UserID;
                    bool result = noteBL.SetNoteReminder(Reminder);
                    return Ok(new { success = true, Message = "note reminder added" });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


        [Authorize]
        [HttpPut("RemoveReminder")]
        public IActionResult RemoveReminder(int NoteID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    bool result = noteBL.RemoveReminder(UserID, NoteID);
                    return Ok(new { success = true, Message = "note reminder added" });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize]
        [HttpPut("Pin")]
        public IActionResult TogglePin(int NoteID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    bool result = noteBL.ToggleNotePin(NoteID, UserID);
                    return Ok(new { success = true, Message = "note pin toggled", Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize]
        [HttpGet("Pin")]
        public IActionResult GetPinnedNotes()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("UserModelID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                List<NoteResponse> userNoteResponseDataList = noteBL.GetPinnedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {

                    return Ok(new { success = false, message = "Not found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Archive")]
        public IActionResult ToggleArchive(int NoteID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    bool result = noteBL.ToggleArchive(NoteID, UserID);
                    return Ok(new { success = true, Message = "note archive toggled", Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize]
        [HttpGet("Archieve")]
        public IActionResult GetAllArchievedNotes()
        {
            try
            {
               
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("UserModelID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<NoteResponse> userNoteResponseDataList = noteBL.GetArchievedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    
                    return Ok(new { success=false, message= "Not found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
