using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class StudentModel
    {
        public int Idstudent { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime? Born { get; set; }
        public string NameParent { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public StudentModel() { }
        public StudentModel(Student st)
        {
            this.Idstudent = st.Idstudent;
            this.Name = st.Name;
            this.Sex = st.Sex;
            this.Born = st.Born;
            this.NameParent = st.NameParent;
            this.Phone = st.Phone;
            this.Address = st.Address;
            this.Email = st.Email;
        }
    }
}
