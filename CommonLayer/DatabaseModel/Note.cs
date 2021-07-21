using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? Reminder { get; set; }
        public string Color { get; set; }
        public bool IsArchived { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserModelID { get; set; }
        public  UserModel User { get; set; }
        //public  ICollection<NoteLabel> NoteLabels { get; set; }

    }
}
