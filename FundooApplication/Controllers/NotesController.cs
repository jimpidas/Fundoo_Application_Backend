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
                    //Note.UserModelID = UserID;
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
        public ActionResult GetAllNotes(int UserId)
        {
            var result = this.noteBL.GetAllNotes(UserId);
            if (result != null) return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}", data = result });
            return BadRequest(new { success = false, message = $"No such UserId Exist." });
        }


        [Authorize]
        [HttpDelete("{NoteID}")]
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

                    bool result = this.noteBL.DeleteNote(NoteID);
                    return Ok(new { success = true, user = Email, Notes = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPut("Title/{NoteID}")]
        public IActionResult UpdateTitle(int NoteID, NoteTitle noteTitle)
        {
            try
            {
                this.noteBL.UpdateTitle(NoteID, noteTitle.Title);
                return Ok(new { succes = true, message=$"Title updated successfully" });
            }
            catch(Exception)
            {
                return BadRequest(new { success = false, message = $"Update fail" });
            }
        }

    }
}
