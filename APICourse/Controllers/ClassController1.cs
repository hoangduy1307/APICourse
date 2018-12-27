using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APICourse.Models;
using APICourse.TranferModel;
using APICourse.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICourse.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController1 : ControllerBase
    {
        ContainerCourseContext db = new ContainerCourseContext();
        // GET: api/<controller>
     
        public IEnumerable<ClassModel> GetClass()
        {
            var list = (from x in db.Class
                        join y in db.Course
                        on x.Idcourse equals y.Idcourse
                        select new ClassModel
                        {
                            Idclass = x.Idclass,
                            NameClass = x.NameClass,
                            NameCourse = y.Name,
                            StartDay = x.StartDay,
                            FinishDay = x.FinishDay,
                            Number = x.Number,
                            State = x.State
                        }
                        );
            return list.ToList();
        }

        [HttpGet]
        [Route("GetAllClass")]
        public IActionResult GetAllClass()
        {
            var list = GetClass();
            return Ok(list);
        }

        [HttpPost]
        [Route("Search_Class")]
        public IActionResult Search_Class(string key)
        {
            var list = GetClass().Where(x => x.NameClass.Contains(key) || x.NameCourse.Contains(key)).ToList();
            return Ok(list);
        }
        public IEnumerable<Course> GetNameCourse()
        {
            var bien = (from a in db.Course
                        select a);
            return bien.ToList().Distinct();
        }
        public string GetNameCoursebyIDClass(int ID)
        {
            var bien = (from a in db.Course
                        join b in db.Class
                        on a.Idcourse equals b.Idcourse
                        where b.Idclass == ID
                        select a.Name);
            return bien.ToList().Distinct().ToString();
        }
        private int TaoMa()
        {
            int maID;
            Random rand = new Random();
            do
            {
                int a = db.Class.Max(r => r.Idclass);
                maID = a + 1;

            }
            while (!KiemtraID(maID));
            return maID;
        }
        private bool KiemtraID(int maID)
        {
            
                var temp = db.Class.Find(maID);
                if (temp == null)
                    return true;
                return false;
            
        }
        [HttpPost]
        [Route("AddClass")]
        public IActionResult AddClass([FromBody] ClassModel lop)
        {
            var mgs = new Message<ClassModel>();

            try
            {
                Class cl = new Class();
                cl.Idclass = TaoMa();
                cl.Idcourse = lop.Idcourse;
                cl.NameClass = lop.NameClass;
                cl.StartDay = lop.StartDay;
                cl.FinishDay = lop.FinishDay;
                cl.Number = lop.Number;
                cl.State = lop.State;
                db.Class.Add(cl);
                db.SaveChanges();
                ClassModel pm = new ClassModel(cl);
                pm.NameCourse = lop.NameCourse;
                mgs.IsSuccess = true;
                mgs.Data = pm;
                mgs.ReturnMessage = "Thêm thành công lớp học #" + pm.Idclass;
                return Ok(mgs);


            }
            catch (Exception ex)
            {
                mgs.IsSuccess = false;
                mgs.ReturnMessage = "Exception: " + ex;
                return Ok(mgs);
            }

            
        }
        [HttpPost]
        [Route("FindClassById")]
        public IActionResult FindClassById(int id)
        {
            var model = db.Class.Find(id);
            //set_viewbag();
            return Ok(model);
        }

        [HttpPost]
        [Route("UpdateClass")]
        public ActionResult UpdateClass([FromBody]ClassModel lop)
        {
            var mgs = new Message<ClassModel>();
            try
            {
                Class cl = db.Class.Find(lop.Idclass);
                if (cl == null)
                {
                    mgs.IsSuccess = false;
                    mgs.ReturnMessage = "Không tìm thấy lớp học #" + lop.Idclass;
                    return Ok(mgs);

                }
                else
                {
                    cl.Idclass = TaoMa();
                    cl.Idcourse = lop.Idcourse;
                    cl.NameClass = lop.NameClass;
                    cl.StartDay = lop.StartDay;
                    cl.FinishDay = lop.FinishDay;
                    cl.Number = lop.Number;
                    cl.State = lop.State;
                    db.SaveChanges();

                    ClassModel pm = new ClassModel(cl);
                    pm.NameCourse = lop.NameCourse;
                    mgs.IsSuccess = true;
                    mgs.Data = pm;
                    mgs.ReturnMessage = "Update thành công lớp học #" + pm.Idclass;
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
        [HttpPost]
        [Route("RemoveClass")]
        public IActionResult RemoveClass(int id)
        {
            var mgs = new Message<ClassModel>();
            try
            {
                Class cl = db.Class.Find(id);
                if (cl == null)
                {
                    mgs.IsSuccess = false;
                    mgs.ReturnMessage = "Không tìm thấy lớp học #" + id;
                    return Ok(mgs);
                }
                else
                {


                    db.Class.Remove(cl);
                    db.SaveChanges();
                    ClassModel pm = new ClassModel(cl);
                    pm.NameCourse = GetNameCoursebyIDClass(cl.Idclass);
                    mgs.IsSuccess = true;
                    mgs.Data = pm;
                    mgs.ReturnMessage = "Thêm thành công lớp học #" + pm.Idclass;
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
