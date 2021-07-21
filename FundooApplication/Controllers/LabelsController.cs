using BusinessLayer.LabelServices;
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
    public class LabelsController : Controller
    {


        readonly LabelBL labelBL;

        public LabelsController(LabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddUserLabel(string LabelName)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    bool result = labelBL.AddUserLabel(UserID, LabelName);
                    return Ok(new { success = true, user = Email, LabelAdded = result });
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
        public IActionResult ChangeLabelName(int LabelID, string LabelName)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    int UserID = Convert.ToInt32(claims.Where(p => p.Type == "UserModelID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    bool result = labelBL.ChangeLabelName(UserID, LabelID, LabelName);
                    return Ok(new { success = true, user = Email, LabelChanged = result });
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
