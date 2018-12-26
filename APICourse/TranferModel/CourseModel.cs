using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class CourseModel
    {
        public string Idcourse { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? Maxnumber { get; set; }
        public string Time { get; set; }
        public string Contents { get; set; }
        public decimal? Fee { get; set; }
        public string Image { get; set; }

        public CourseModel() { }
        public CourseModel(Course course)
        {
            this.Idcourse = course.Idcourse;
            this.Name = course.Name;
            this.Age = course.Age;
            this.Maxnumber = course.Maxnumber;
            this.Time = course.Time;
            this.Contents = course.Contents;
            this.Fee = course.Fee;
            this.Image = course.Image;
        }
    }
}
