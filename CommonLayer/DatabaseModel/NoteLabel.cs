using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class NoteLabel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteLabelId { get; set; }
        public int NotesId { get; set; }
        public int LabelId { get; set; }
        public int UserModelID { get; set; }
        public  Label Label { get; set; }
        public  Note Note { get; set; }
        public  UserModel User { get; set; }
    }
}
