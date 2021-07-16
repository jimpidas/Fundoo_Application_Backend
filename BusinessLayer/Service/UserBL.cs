using BusinessLayer.Service;
using BusinessLayer.Interfaces;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.ResponseModel;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        readonly UserDetailValidation userDetailValidation;


        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
            userDetailValidation = new UserDetailValidation();
        }

        public ResponseUserAccount RegisterUser(RegisterUserRequest entity)
        {
            try
            {
                if (userDetailValidation.ValidateFirstName(entity.FirstName) &&
                userDetailValidation.ValidateLastName(entity.LastName) &&
                userDetailValidation.ValidateEmailAddress(entity.Email) &&
                userDetailValidation.ValidatePassword(entity.Password))
                {
                  return userRL.RegisterUser(entity);
                }
                else
                {
                    throw new UserDetailException(UserDetailException.ExceptionType.ENTERED_INVALID_USER_DETAILS, "user details are invalid");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    

        public ResponseUserAccount AthenticateUser(LoginRequestModel loginUser)
        {
            try
            {
                if (userDetailValidation.ValidateEmailAddress(loginUser.Email) &&
                userDetailValidation.ValidatePassword(loginUser.Password))
                {
                    return userRL.AthenticateUser(loginUser);

                }
                else
                {
                    throw new UserDetailException(UserDetailException.ExceptionType.ENTERED_INVALID_USER_DETAILS, "user details are details");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public bool SendForgottenPasswordLink(ForgetPasswordModel user)
        {
            return userRL.SendForgottenPasswordLink(user);
        }
    }
}
