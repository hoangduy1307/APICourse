using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class RegistrationCourseModel
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

        public RegistrationCourseModel() { }
        public RegistrationCourseModel(RigistrationCourse rg)
        {
            this.Idregist = rg.Idregist;
            this.NameParent = rg.NameParent;
            this.Phone = rg.Phone;
            this.Email = rg.Email;
            this.NameStudent = rg.NameStudent;
            this.Birthday = rg.Birthday;
            this.Address = rg.Address;
            this.Idcourse = rg.Idcourse;
            this.State = rg.State;
        }
    }
}
