using System;
using System.Collections.Generic;

namespace APICourse.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Teachingclass = new HashSet<Teachingclass>();
        }

        public int Idteacher { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Knowledge { get; set; }
        public int? Status { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Teachingclass> Teachingclass { get; set; }
    }
}
