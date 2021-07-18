using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class NoteLabel
    {
        public int NoteLabelId { get; set; }
        public int NotesId { get; set; }
        public int LabelId { get; set; }
        public int UserModelID { get; set; }

        public virtual Label Label { get; set; }
        public virtual Note Note { get; set; }
        public virtual UserModel User { get; set; }
    }
}
