using System;
using System.Collections.Generic;

namespace APICourse.Models
{
    public partial class Class
    {
        public Class()
        {
            Classstudent = new HashSet<Classstudent>();
            Phanbo = new HashSet<Phanbo>();
            Teachingclass = new HashSet<Teachingclass>();
        }

        public int Idclass { get; set; }
        public string Idcourse { get; set; }
        public string NameClass { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? FinishDay { get; set; }
        public int? Number { get; set; }
        public int? State { get; set; }

        public virtual Course IdcourseNavigation { get; set; }
        public virtual ICollection<Classstudent> Classstudent { get; set; }
        public virtual ICollection<Phanbo> Phanbo { get; set; }
        public virtual ICollection<Teachingclass> Teachingclass { get; set; }
    }
}
