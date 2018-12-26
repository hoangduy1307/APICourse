using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class TeachingClassModel
    {
        public int Id { get; set; }
        public int Idclass { get; set; }
        public string NameClass { get; set; }
        public int Idteacher { get; set; }
        public string Nameteacher { get; set; }
        public int Session { get; set; }
        public DateTime Day { get; set; }
        public int? State { get; set; }

        public TeachingClassModel() { }
        public TeachingClassModel(Teachingclass x)
        {
            this.Id = x.Id;
            this.Idclass = x.Idclass;
            this.Idteacher = x.Idteacher;
            this.Session = x.Session;
            this.Day = x.Day;
            this.State = x.State;
            this.Nameteacher = null;
            this.NameClass = null;
        }
    }
}
