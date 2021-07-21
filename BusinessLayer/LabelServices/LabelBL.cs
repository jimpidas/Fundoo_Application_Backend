using BusinessLayer.LabelInterface;
using Repository.Services.LabelServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.LabelServices
{
    public class LabelBL: ILabelBL
    {
        readonly LabelRL labelRL;
        public LabelBL(LabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public bool AddUserLabel(int userID, string labelName)
        {
            try
            {
                return labelRL.AddNewUserLabel(userID, labelName);
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
                return labelRL.ChangeLabelName(userID, labelID, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
