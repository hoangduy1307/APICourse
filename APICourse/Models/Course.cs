using System;
using System.Collections.Generic;

namespace APICourse.Models
{
    public partial class Course
    {
        public Course()
        {
            Class = new HashSet<Class>();
            RigistrationCourse = new HashSet<RigistrationCourse>();
        }

        public string Idcourse { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? Maxnumber { get; set; }
        public string Time { get; set; }
        public string Contents { get; set; }
        public decimal? Fee { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<RigistrationCourse> RigistrationCourse { get; set; }
    }
}
