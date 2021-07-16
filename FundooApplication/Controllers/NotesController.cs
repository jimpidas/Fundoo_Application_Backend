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
        [HttpPost("AddNote")]

        public IActionResult AddUserNote(AddNote Note)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    Note.UserModelID = UserID;
                    NoteResponse result = noteBL.AddNote(Note);
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
        [HttpGet("Get")]
        public ActionResult GetAllNotes(int UserId)
        {
            var result = this.noteBL.GetAllNotes(UserId);
            if (result != null) return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}", data = result });
            return BadRequest(new { success = false, message = $"No such UserId Exist." });
        }


        [Authorize]
        [HttpDelete("Delete/{NoteID}")]
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

                    bool result = noteBL.DeleteNote(NoteID);
                    return Ok(new { success = true, user = Email, Notes = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
