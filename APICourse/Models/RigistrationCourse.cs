using System;
using System.Collections.Generic;

namespace APICourse.Models
{
    public partial class RigistrationCourse
    {
        public int Idregist { get; set; }
        public string NameParent { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NameStudent { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Idcourse { get; set; }
        public string State { get; set; }

        public virtual Course IdcourseNavigation { get; set; }
    }
}
