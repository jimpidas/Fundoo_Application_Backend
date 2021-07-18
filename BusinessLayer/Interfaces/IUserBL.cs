using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public ResponseUserAccount RegisterUser(RegisterUserRequest user);

        public ResponseUserAccount AthenticateUser(LoginRequestModel loginUser);

        bool SendForgottenPasswordLink(ForgetPasswordModel forgetPasswordModel);
    }
}
