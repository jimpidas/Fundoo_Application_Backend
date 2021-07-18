using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        
        public int UserModelID { get; set; }
        public string LabelName { get; set; }

        public  UserModel User { get; set; }
        public virtual ICollection<NoteLabel> NoteLabels { get; set; }
    }
}
