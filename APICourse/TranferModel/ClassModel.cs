using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class ClassModel
    {
        public int Idclass { get; set; }
        public string Idcourse { get; set; }
        public string NameClass { get; set; }
        public string NameCourse { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? FinishDay { get; set; }
        public int? Number { get; set; }
        public int? State { get; set; }

        public ClassModel() { }
        public ClassModel(Class cl)
        {
            this.NameCourse = null;
            this.Idclass = cl.Idclass;
            this.Idcourse = cl.Idcourse;
            this.NameClass = cl.NameClass;
            this.StartDay = cl.StartDay;
            this.FinishDay = cl.FinishDay;
            this.Number = cl.Number;
            this.State = cl.State;
        }
    }
}
