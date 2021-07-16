using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class NoteLabel
    {
        public int NoteLabelId { get; set; }
        public int NoteId { get; set; }
        public int LabelId { get; set; }
        public int UserModelID { get; set; }
    }
}
