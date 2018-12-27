using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;
using APICourse.TranferModel;
using APICourse.Utility;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICourse.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhanboController : Controller
    {
        ContainerCourseContext db = new ContainerCourseContext();
        [HttpGet]
        [Route("GetAllPhanphoi")]
        public IActionResult GetAllPhanphoi()
        {
            var model = from a in db.Teacher
                        join b in db.Phanbo
                        on a.Idteacher equals b.Idteacher
                        join c in db.Class
                        on b.Idclass equals c.Idclass
                        join d in db.Course
                        on c.Idcourse equals d.Idcourse
                        select new PhanboModel
                        {
                            Idclass = c.Idclass,
                            NameClass = c.NameClass,
                            NameCourse = d.Name,
                            Idteacher = b.Idteacher,
                            NameTeacher = a.Name,
                            Number = c.Number,
                            StartDay = c.StartDay,
                            FinishDay = c.FinishDay,
                        };
            return Ok(model);
        }
        [HttpGet]
        [Route("GetAllClass")]
        public IActionResult GetAllClass()
        {
            var list = db.Class.ToList();

            return Ok(list);
        }
        [HttpGet]
        [Route("GetAllTeacher")]
        public IActionResult GetAllTeacher()
        {
            var list = db.Teacher.ToList();
            return Ok(list);
        }
        [HttpPost]
        [Route("GetphabobyIdlopandIdteacher")]
        public IActionResult GetphabobyIdlopandIdteacher([FromBody]int[] id)
        {
            //idclass = id[0] ; idteacher = id[1]
            var model = from a in db.Teacher
                        join b in db.Phanbo
                        on a.Idteacher equals b.Idteacher
                        join c in db.Class
                        on b.Idclass equals c.Idclass
                        join d in db.Course
                        on c.Idcourse equals d.Idcourse
                        where b.Idclass == id[0] && b.Idteacher == id[1]
                        select new PhanboModel
                        {
                            Idclass = c.Idclass,
                            NameClass = c.NameClass,
                            NameCourse = d.Name,
                            Idteacher = b.Idteacher,
                            NameTeacher = a.Name,
                            Number = c.Number,
                            StartDay = c.StartDay,
                            FinishDay = c.FinishDay,
                        };
            return Ok(model); 
        }
        public PhanboModel GetbyIdlopandIdteacher(int Idclass, int Idteacher)
        {
            var model = (from a in db.Teacher
                        join b in db.Phanbo
                        on a.Idteacher equals b.Idteacher
                        join c in db.Class
                        on b.Idclass equals c.Idclass
                        join d in db.Course
                        on c.Idcourse equals d.Idcourse
                        where b.Idclass == Idclass && b.Idteacher == Idteacher
                        select new PhanboModel
                        {
                            Idclass = c.Idclass,
                            NameClass = c.NameClass,
                            NameCourse = d.Name,
                            Idteacher = b.Idteacher,
                            NameTeacher = a.Name,
                            Number = c.Number,
                            StartDay = c.StartDay,
                            FinishDay = c.FinishDay,
                        }).Distinct().SingleOrDefault();
            return model;
        }
        // xem lại distinct
        [HttpPost]
        [Route("GetphabobyIDlop")]
        public IActionResult GetphabobyIDlop(int Idclass)
        {
            var model = from a in db.Teacher
                        join b in db.Phanbo
                        on a.Idteacher equals b.Idteacher
                        join c in db.Class
                        on b.Idclass equals c.Idclass
                        join d in db.Course
                        on c.Idcourse equals d.Idcourse
                        where b.Idclass == Idclass
                        select new PhanboModel
                        {
                            Idclass = c.Idclass,
                            NameClass = c.NameClass,
                            NameCourse = d.Name,
                            Idteacher = b.Idteacher,
                            NameTeacher = a.Name,
                            Number = c.Number,
                            StartDay = c.StartDay,
                            FinishDay = c.FinishDay,
                        };
            return Ok(model);
        }
        public string getNameCousebyIdClass(int idclass)
        {
            var name = (from a in db.Course
                        join b in db.Class
                        on a.Idcourse equals b.Idcourse
                        where b.Idclass == idclass
                        select a.Name).ToString();
            return name;
        }

        [HttpPost]
        [Route("AddPhanbo")]
        public IActionResult AddPhanbo([FromBody] int[] id)

        {
            // id[0] =Idclass ;id[1]=IdTeacher;
            var mgs = new Message<PhanboModel>();
            var model = db.Phanbo.Where(x => x.Idclass == id[0] && x.Idteacher == id[1]).SingleOrDefault();
            if (model != null)
            {
                try
                {
                    Phanbo phanbo = new Phanbo();
                    phanbo.Idclass = id[0];
                    phanbo.Idteacher = id[1];
                    db.Phanbo.Add(phanbo);
                    db.SaveChanges();
                    PhanboModel pb = new PhanboModel();
                    pb = GetbyIdlopandIdteacher(phanbo.Idclass, phanbo.Idteacher);
                    mgs.IsSuccess = true;
                    mgs.Data = pb;
                    mgs.ReturnMessage = "Thêm thành công Phân bổ #" + pb.Idclass;
                    return Ok(mgs);

                }
                catch (Exception ex)
                {
                    mgs.IsSuccess = false;
                    mgs.ReturnMessage = "Exception: " + ex;
                    return Ok(mgs);
                }
            }
            else
            {
                mgs.IsSuccess = false;
                mgs.ReturnMessage = "Đã tồn tại phân bố giang viên #" + id[1].ToString() + "cho lớp #" +id[0].ToString();
                return Ok(mgs);
            }

        }
        [HttpPost]
        [Route("RemovePhanbo")]
        public IActionResult RemovePhanbo([FromBody] int[] id)
        {
            // id[0]=Idclass, id[1]=IdTeacher
            var mgs = new Message<PhanboModel>();
            try
            {
                Phanbo cl = db.Phanbo.Where(x => x.Idclass == id[0] && x.Idteacher == id[1]).SingleOrDefault();
                if (cl == null)
                {
                    mgs.IsSuccess = false;
                    mgs.ReturnMessage = "Không tìm thấy Sự phan bổ này #";
                    return Ok(mgs);
                }
                else
                {


                    db.Phanbo.Remove(cl);
                    db.SaveChanges();
                    PhanboModel pm = new PhanboModel();
                    pm = GetbyIdlopandIdteacher(id[0], id[1]);
                    mgs.IsSuccess = true;
                    mgs.Data = pm;
                    mgs.ReturnMessage = "Xóa Phân bổ thành công ";
                    return Ok(mgs);

                }

            }
            catch (Exception ex)
            {
                mgs.IsSuccess = false;
                mgs.ReturnMessage = "Exception: " + ex;
                return Ok(mgs);

            }
        }
    }
}
