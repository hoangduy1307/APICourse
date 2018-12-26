﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;
using APICourse.TranferModel;
using APICourse.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICourse.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        ContainerCourseContext _context = new ContainerCourseContext();

        // GET: api/<controller>
        [HttpGet]
        [Route("GetAllTeacher")]
        public IActionResult GetAllTeacher()
        {
            try
            {
                var lstpd = _context.Teacher;

                List<TeacherModel> lst = new List<TeacherModel>();
                foreach (var i in lstpd)
                {
                    TeacherModel temp = new TeacherModel(i);
                    lst.Add(temp);
                }
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return Ok(null);
            }
        }

        // GET api/<controller>/5
        [HttpPost("{id}")]
        [Route("GetTeacher")]
        public IActionResult GetTeacher(int id)
        {
            var msg = new Message<TeacherModel>();
            try
            {
                var bien = _context.Teacher.Find(id);
                if (bien == null)
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Không tìm thấy thông tin giáo viên!";
                    return Ok(msg);
                }
                else
                {
                    TeacherModel temp = new TeacherModel(bien);
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Thông tin giáo viên #" + id;
                    msg.Data = temp;
                    return Ok(msg);
                }
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Exception: " + ex + " Lỗi!";
                return Ok(msg);
            }

        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddTeacher")]
        public IActionResult AddTeacher([FromBody]TeacherModel data)
        {
            var msg = new Message<TeacherModel>();
            try
            {
                var check = Exist_Teacher(data.Email);
                if(check == -1)
                {
                    Teacher bien = new Teacher();
                    bien.Name = data.Name;
                    bien.Sex = data.Sex;
                    bien.Phone = data.Phone;
                    bien.Address = data.Address;
                    bien.Email = data.Email;
                    bien.Knowledge = data.Knowledge;
                    bien.Status = data.Status;

                    _context.Teacher.Add(bien);
                    _context.SaveChanges();

                    var checkAdd = Exist_Teacher(data.Email);
                    if(checkAdd != -1)
                    {

                    }
                    TeacherModel temp = new TeacherModel(bien);
                    msg.IsSuccess = true;
                    msg.Data = temp;
                    msg.ReturnMessage = "Thêm thành công!";
                    return Ok(msg);
                }
                
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Thêm khóa học không thành công! " + ex + " lỗi!";
                return Ok(msg);
            }
        }

        [HttpGet]
        [Route("SearchCourse")]
        public IActionResult SearchCourse(string key)
        {
            var msg = new Message<CourseModel>();
            try
            {
                var data = _context.Course.Where(x => x.Name.Contains(key) || x.Contents.Contains(key)).SingleOrDefault();
                if (data != null)
                {
                    CourseModel result = new CourseModel(data);
                    msg.IsSuccess = true;
                    msg.Data = result;
                    return Ok(msg);
                }
                else
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Không tìm thấy thông tin yêu cầu! ";
                    return Ok(msg);
                }
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Không tìm thấy thông tin yêu cầu! Lỗi!" + ex;
                return Ok(msg);
            }
        }


        [HttpPost]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(Course data)
        {
            var msg = new Message<CourseModel>();
            try
            {
                var bien = _context.Course.Where(x => x.Idcourse == data.Idcourse).SingleOrDefault();
                if (bien != null)
                {
                    bien.Name = data.Name;
                    bien.Age = data.Age;
                    bien.Maxnumber = data.Maxnumber;
                    bien.Time = data.Time;
                    bien.Contents = data.Contents;
                    bien.Fee = data.Fee;
                    bien.Image = data.Image;

                    _context.Entry(bien).State = EntityState.Modified;
                    _context.SaveChanges();
                    CourseModel temp = new CourseModel(bien);
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Cập nhật thông tin khóa học thành công!";
                    msg.Data = temp;
                    return Ok(msg);
                }
                else
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Cập nhật không thành công!";
                    return Ok(msg);
                }
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Cập nhật không thành công! Lỗi! " + ex;
                return Ok(msg);
            }
        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpPost("{id}")]
        [Route("DeleteCourse")]
        public IActionResult DeleteCourse(string id)
        {
            var msg = new Message<CourseModel>();
            try
            {
                var bien = _context.Course.Where(x => x.Idcourse == id).SingleOrDefault();
                if (bien != null)
                {
                    if (Exitclass(id) == false)
                    {
                        _context.Course.Remove(bien);
                        _context.SaveChanges();
                        CourseModel temp = new CourseModel(bien);
                        msg.IsSuccess = true;
                        msg.ReturnMessage = "Xóa khóa học #" + id + " thành công!";
                        msg.Data = temp;
                        return Ok(msg);
                    }
                    else
                    {
                        msg.IsSuccess = false;
                        msg.ReturnMessage = "Không thể xóa khóa học #" + id + " này!";
                        return Ok(msg);
                    }

                }
                else
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Xóa khóa học #" + id + " không thành công!";
                    return Ok(msg);
                }
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Xóa khóa học #" + id + " không thành công! Lỗi!" + ex;
                return Ok(msg);
            }
        }

        
        private bool KiemtraID(string maID)
        {
            var temp = _context.Course.Where(x => x.Idcourse == maID).SingleOrDefault();
            if (temp == null)
                return true;
            return false;
        }

        public int Exist_Teacher(string email)
        {
            var _teacher = _context.Teacher.Where(x => x.Email == email).SingleOrDefault();
            if (_teacher != null)
            {
                return _teacher.Idteacher;
            }
            return -1;
        }
    }
}
