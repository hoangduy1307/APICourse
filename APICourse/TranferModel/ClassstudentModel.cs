using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class ClassstudentModel
    {
        public int Idclass { get; set; }
        public int Idstudent { get; set; }
        public int Session { get; set; }
        public DateTime? Day { get; set; }
        public int? State { get; set; }

        public ClassstudentModel() { }
        public ClassstudentModel(Classstudent x)
        {
            this.Idclass = x.Idclass;
            this.Idstudent = x.Idstudent;
            this.Session = x.Session;
            this.Day = x.Day;
            this.State = x.State;
        }
    }
}
