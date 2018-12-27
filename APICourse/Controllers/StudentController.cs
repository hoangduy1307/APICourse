using System;
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

    public class StudentController : Controller
    {
        ContainerCourseContext _context = new ContainerCourseContext();
        // GET: api/<controller>
        [HttpGet]
        [Route("GetAllStudent")]
        public IActionResult GetAllStudent()
        {
            try
            {
                var sts = _context.Student;
                List<StudentModel> lst = new List<StudentModel>();
                foreach (var i in sts)
                {
                    StudentModel temp = new StudentModel(i);
                    lst.Add(temp);
                }
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return Ok(null);
            }
        }

        [HttpPost]
        [Route("Search_Student")]
        public IActionResult Search_Student(string id)
        {
            var msg = new Message<List<StudentModel>>();
            try
            {
                var bien = _context.Student.Where(x => x.Name.Contains(id) || x.Address.Contains(id) || x.NameParent.Contains(id)).ToList();
                if (bien == null)
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Không tìm thấy thông tin khóa học!";
                    return Ok(msg);
                }
                else
                {
                    List<StudentModel> lst = new List<StudentModel>();
                    foreach (var i in bien)
                    {
                        StudentModel t = new StudentModel(i);
                        lst.Add(t);
                    }
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Thông tin sinh viên #" + id;
                    return Ok(lst);
                }
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Exception: " + ex + " Lỗi!";
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("Insert_Student")]
        public IActionResult Insert_Student([FromBody]StudentModel data)
        {
            var msg = new Message<StudentModel>();
            try
            {
                Student bien = new Student();
                bien.Idstudent = TaoMa();
                bien.Name = data.Name;
                bien.Sex = data.Sex;
                bien.Born = data.Born;
                bien.NameParent = data.NameParent;
                bien.Phone = data.Phone;
                bien.Address = data.Address;
                bien.Email = data.Email;

                _context.Student.Add(bien);
                _context.SaveChanges();
                StudentModel temp = new StudentModel(bien);
                msg.IsSuccess = true;
                msg.Data = temp;
                msg.ReturnMessage = "Thêm thành công!";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Thêm học sinh không thành công! " + ex + " lỗi!";
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("Update_Student")]
        public IActionResult Update_Student(StudentModel data)
        {
            var msg = new Message<StudentModel>();
            try
            {
                var bien = _context.Student.Where(x => x.Idstudent == data.Idstudent).SingleOrDefault();
                if (bien != null)
                {
                    bien.Name = data.Name;
                    bien.Name = data.Name;
                    bien.Sex = data.Sex;
                    bien.Born = data.Born;
                    bien.NameParent = data.NameParent;
                    bien.Phone = data.Phone;
                    bien.Address = data.Address;
                    bien.Email = data.Email;

                    _context.Entry(bien).State = EntityState.Modified;
                    _context.SaveChanges();
                    StudentModel temp = new StudentModel(bien);
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Cập nhật thông tin học sinh thành công!";
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

        [HttpPost]
        [Route("Delete_Student")]
        public IActionResult Delete_Student(int id)
        {
            var msg = new Message<StudentModel>();
            try
            {
                Student bien = _context.Student.Find(id);
                //var bien1 = _context.Student.Where(x => x.Idstudent == id).SingleOrDefault();
                Classstudent bien1 = _context.Classstudent.Where(x => x.Idstudent == id).SingleOrDefault();
                if (bien != null)
                {
                    if (bien1 == null)
                    {
                        _context.Student.Remove(bien);
                        _context.SaveChanges();
                        StudentModel temp = new StudentModel(bien);
                        msg.IsSuccess = true;
                        msg.ReturnMessage = "Xóa học sinh #" + id + " thành công!";
                        msg.Data = temp;
                        return Ok(msg);
                    }
                    else
                    {
                        msg.IsSuccess = false;
                        msg.ReturnMessage = "Không thể xóa sinh viên #" + id + " này!";
                        return Ok(msg);
                    }

                }
                else
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Xóa sinh viên #" + id + " không thành công!";
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

        [HttpPost]
        [Route("Class_student")]
        public IActionResult Class_student(int id_student, int id_class)
        {
            var msg = new Message<Classstudent>();
            try
            {
                
                if (Exit_idstudent(id_student))
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        Classstudent add_class = new Classstudent();
                        add_class.Idclass = id_class;
                        add_class.Idstudent = id_student;
                        add_class.Session = i;
                        add_class.Day = null;
                        add_class.State = 0;
                        _context.Classstudent.Add(add_class);
                        _context.SaveChanges();

                        var bien = _context.Class.Find(id_class);
                        if (bien != null)
                        {
                            bien.Number = bien.Number + 1;
                            _context.SaveChanges();
                        }
                    }
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Thêm sinh viên #" + id_student + "vào lớp #" + id_class + " thành công!";
                    //msg.Data = temp;
                    return Ok(msg);
                }
                else
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Thêm sinh viên #" + id_student + "vào lớp #" + id_class + " thất bại!";
                    //msg.Data = temp;
                    return Ok(msg);
                }
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Thêm lớp học không thành công! Lỗi!" + ex;
                return Ok(msg);
            }
            
        }
        public bool Check(int id_student, int id_class)
        {
            var bien = _context.Classstudent.Where(x => x.Idstudent == id_student && x.Idclass == id_class).SingleOrDefault();
            if (bien != null)
                return true;
            else
                return false;
        }
        private int TaoMa()
        {
            int maID=_context.Student.Count();
            Random rand = new Random();
            do
            {
                    maID++;
            }
            while (Exit_idstudent(maID));
            return maID;
        }
        private bool Exit_idstudent(int maID)
        {
            var temp = _context.Student.Where(x => x.Idstudent == maID).SingleOrDefault();
            if (temp != null)
                return true;
            return false;
        }


        // GET api/<controller>/5
        
    }
}
