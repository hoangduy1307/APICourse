using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class TeacherModel
    {
        public int Idteacher { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Knowledge { get; set; }
        public int? Status { get; set; }

        public TeacherModel() { }
        public TeacherModel(Teacher tc)
        {
            this.Idteacher = tc.Idteacher;
            this.Name = tc.Name;
            this.Sex = tc.Sex;
            this.Phone = tc.Phone;
            this.Address = tc.Address;
            this.Email = tc.Email;
            this.Knowledge = tc.Knowledge;
            this.Status = tc.Status;
        }
    }
}
