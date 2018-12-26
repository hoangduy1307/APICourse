using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;
using APICourse.TranferModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICourse.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachingClassController1 : ControllerBase
    {
        ContainerCourseContext db = new ContainerCourseContext();

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var model = from a in db.Class
                        join b in db.Teachingclass
                        on a.Idclass equals b.Idclass
                        join c in db.Teacher
                        on b.Idteacher equals c.Idteacher
                        where a.State == 1
                        select new TeachingClassModel
                        {
                            Id = b.Id,
                            Idclass = b.Idclass,
                            NameClass = a.NameClass,
                            Idteacher= b.Idteacher,
                            Nameteacher = c.Name,
                            Session = b.Session,
                            Day = b.Day,
                            State = a.State

                        };
            return Ok(model);
        }
        [HttpPost]
        [Route("GetbyDayAndIDClass")]
        public IActionResult GetbyDayAndIDClass(DateTime thoigian, int IdClass)
        {
            var model = from a in db.Class
                        join b in db.Teachingclass
                        on a.Idclass equals b.Idclass
                        join c in db.Teacher
                        on b.Idteacher equals c.Idteacher
                        where a.State == 1 && b.Day.Month == thoigian.Month && b.Day.Year == thoigian.Year && b.Idclass == IdClass
                        select new TeachingClassModel
                        {

                            Id = b.Id,
                            Idclass = b.Idclass,
                            NameClass = a.NameClass,
                            Idteacher = b.Idteacher,
                            Nameteacher = c.Name,
                            Session = b.Session,
                            Day = b.Day,
                            State = a.State

                        };
            return Ok(model);
        }
        [HttpPost]
        [Route("GetAllClass")]
        // chua chuyen tranfer
        public IActionResult GetAllClass()
        {
            var list = db.Class.ToList();

            return Ok(list);
        }
        [HttpPost]
        [Route("GetbyIDClass")]
        public IActionResult GetbyIDClass(int Idclass)
        {
            var model = from a in db.Class
                        join b in db.Teachingclass
                        on a.Idclass equals b.Idclass
                        join c in db.Teacher
                        on b.Idteacher equals c.Idteacher
                        where a.State == 1 && b.Idclass == Idclass
                        select new TeachingClassModel
                        {
                            Id = b.Id,
                            Idclass = b.Idclass,
                            NameClass = a.NameClass,
                            Idteacher = b.Idteacher,
                            Nameteacher = c.Name,
                            Session = b.Session,
                            Day = b.Day,
                            State = a.State

                        };
            return Ok(model);
        }

        
    }
}
