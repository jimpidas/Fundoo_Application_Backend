using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class NoteResponse
    {

        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public bool IsArchived { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserModelID { get; set; }
       
    }
}
