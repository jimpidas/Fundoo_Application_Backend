using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class ReminderRequest
    {
        public int NotesId { get; set; }
        public int UserModelID { get; set; }
        public DateTime Reminder { set; get; }
    }
}
