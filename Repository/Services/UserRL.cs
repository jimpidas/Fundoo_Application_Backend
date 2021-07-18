using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using FundooApplication;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Services
{

    public class UserRL : IUserRL
    {
        readonly UsersContext _userContext;
        readonly PasswordEncryption passwordEncryption;
        readonly UserAuthenticationJWT userAuthentication;
        private readonly IConfiguration config;
        readonly MSMQService msmq;
        public UserRL(UsersContext context, IConfiguration config)
        {
            _userContext = context;
            passwordEncryption = new PasswordEncryption();
            this.config = config;
            userAuthentication = new UserAuthenticationJWT(this.config);
            msmq = new MSMQService(config);

        }
        
        
        public ResponseUserAccount RegisterUser(RegisterUserRequest user)
        {
            if (!_userContext.Users.Any(u => u.Email == user.Email))
            {
                user.Password = passwordEncryption.CalculateHash(user.Password);
                _userContext.Users.Add(
                    new UserModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.Password
                    });
                _userContext.SaveChanges();
                return _userContext.Users.Where(u => u.Email.Equals(user.Email)).Select(u => new ResponseUserAccount
                {
                    UserModelID = u.UserModelID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                }).ToList().First();
            }
            else
            {
                throw new Exception("email id already registered");
            }
        }
        public ResponseUserAccount AthenticateUser(LoginRequestModel loginUser)
        {
            if (_userContext.Users.Any(U => U.Email.Equals(loginUser.Email)))
            {
                string Password = passwordEncryption.CalculateHash(loginUser.Password);
                if (_userContext.Users.FirstOrDefault(u => u.Email == loginUser.Email).Password.Equals(Password))
                {

                    ResponseUserAccount User = _userContext.Users.Where(u => u.Email == loginUser.Email).
                        Select(u => new ResponseUserAccount
                        {
                            UserModelID = u.UserModelID,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                }).ToList().First();
                    return User;
                }
                else
                    throw new Exception("wrong password");
            }
            else
            {
                throw new Exception("email address is not registered");
            }
        }

        public bool SendForgottenPasswordLink(ForgetPasswordModel user)
        {
            try
            {
                ResponseUserAccount u = GetUserAccount(user);
                if (u != null)
                {
                    var jwt = userAuthentication.GeneratePasswordResetJWT(u);
                    user.JwtToken = jwt;
                    msmq.SendPasswordResetLink(user);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseUserAccount GetUserAccount(ForgetPasswordModel user)
        {
            if (_userContext.Users.Any(U => U.Email.Equals(user.Email)))
            {
                return _userContext.Users.Where(U => U.Email.Equals(user.Email)).Select(U =>
                    new ResponseUserAccount
                    {
                        UserModelID = U.UserModelID,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Email = U.Email
                    }).ToList().First();
            }
            else
            {
                throw new Exception("email address is not registered");
            }
        }
    }
}
