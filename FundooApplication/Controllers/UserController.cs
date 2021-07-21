using BusinessLayer.Interfaces;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
   
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        readonly IUserBL _userBL;
        readonly UserAuthenticationJWT userAuthentication;
        private readonly IConfiguration config;
        
        public UserController(IUserBL userBL, IConfiguration config)
        {
            _userBL = userBL;
            this.config = config;
            userAuthentication = new UserAuthenticationJWT(this.config);
        }
       

      
        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserRequest user)
        {
            if (user == null)
            {
                return BadRequest("user is null.");
            }
            try
            {
                ResponseUserAccount result = _userBL.RegisterUser(user);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "User Registration Successful", user = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "User Registration Unsuccessful" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        
        
        [HttpPost("Login")]
        public IActionResult AuthenticateUser(LoginRequestModel loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("user is null.");
            }
            try
            {
                ResponseUserAccount user = _userBL.AthenticateUser(loginUser);
                if (user != null)
                {
                    var tokenString = userAuthentication.GenerateSessionJWT(user);
                    return Ok(new {success=true, Message = "User Login Successful", user ,token=tokenString});
                }
                return BadRequest(new { success = false, Message = "User Login Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ResetForgottenPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                bool result = _userBL.SendForgottenPasswordLink(forgetPasswordModel);

                if (result)
                {

                    return Ok(new { success = true, Message = "password reset link has been sent to your email id", email = forgetPasswordModel.Email });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "email id don't exist" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

    }
}