using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUserRL
    {
        ResponseUserAccount RegisterUser(RegisterUserRequest user);
        ResponseUserAccount AthenticateUser(LoginRequestModel loginUser);
        bool SendForgottenPasswordLink(ForgetPasswordModel forgetPasswordModel); 
    }
}
