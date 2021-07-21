using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.LabelInterface
{
    interface ILabelRL
    {
        public bool AddNewUserLabel(int userID, string labelName);
        public bool ChangeLabelName(int userID, int labelID, string labelName);
    }
}
