using System;
using System.Collections.Generic;

namespace APICourse.Models
{
    public partial class Classstudent
    {
        public int Idclass { get; set; }
        public int Idstudent { get; set; }
        public int Session { get; set; }
        public DateTime? Day { get; set; }
        public int? State { get; set; }

        public virtual Class IdclassNavigation { get; set; }
        public virtual Student IdstudentNavigation { get; set; }
    }
}
