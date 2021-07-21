using CommonLayer.DatabaseModel;
using Repository.LabelInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Services.LabelServices
{
    public class LabelRL: ILabelRL
    {
        readonly UsersContext _userContext;
        public LabelRL(UsersContext userContext)
        {
            _userContext = userContext;
        }
        public bool AddNewUserLabel(int userID, string labelName)
        {
            try
            {
                if (!_userContext.Labels.Any(N => N.UserModelID == userID && N.LabelName == labelName))
                {
                    _userContext.Labels.Add(new Label { UserModelID = userID, LabelName = labelName });
                    _userContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("Label already exist");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool ChangeLabelName(int userID, int labelID, string labelName)
        {
            try
            {
                if (_userContext.Labels.Any(N => N.UserModelID == userID && N.LabelId == labelID))
                {
                    if (_userContext.Labels.Any(N => N.LabelId == labelID && N.LabelName == labelName))
                    {
                        return true;
                    }
                    if (!_userContext.Labels.Any(N => N.UserModelID == userID && N.LabelName == labelName))
                    {
                        _userContext.Labels.First(N => N.LabelId == labelID).LabelName = labelName;
                        _userContext.SaveChanges();
                        return true;
                    }
                    throw new Exception("Label already exist");
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
       
       
        
    }
}
