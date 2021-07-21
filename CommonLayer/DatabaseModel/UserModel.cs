using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserModelID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Note> Notes { get; set; }
        public  ICollection<Label> Labels { get; set; }
       // public  ICollection<NoteLabel> NoteLabels { get; set; } 
    }
}
