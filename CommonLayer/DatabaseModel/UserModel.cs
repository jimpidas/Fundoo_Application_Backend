using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class UserModel
    {
        public UserModel()
        {
            Labels = new HashSet<Label>();
            NoteLabels = new HashSet<NoteLabel>();
            Notes = new HashSet<Note>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserModelID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Label> Labels { get; set; }
        public virtual ICollection<NoteLabel> NoteLabels { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
