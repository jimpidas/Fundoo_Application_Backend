using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.LabelInterface
{
    interface ILabelBL
    {
        public bool AddUserLabel(int userID, string labelName);
        public bool ChangeLabelName(int userID, int labelID, string labelName);
    }
}
