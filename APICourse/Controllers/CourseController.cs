using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APICourse.Models;
using APICourse.TranferModel;
using APICourse.Utility;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICourse.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ContainerCourseContext _context = new ContainerCourseContext();
        
        // GET: api/<controller>
        [HttpGet]
        [Route("GetAllCourse")]
        public IActionResult GetAllCourse()
        {
            try
            {
                var lstpd = _context.Course;

                List<CourseModel> lst = new List<CourseModel>();
                foreach (var i in lstpd)
                {
                    CourseModel temp = new CourseModel(i);
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
        [HttpPost]
        [Route("GetCourse")]
        public IActionResult GetCourse(string id)
        {
            var msg = new Message<CourseModel>();
            try
            { 
                var bien = _context.Course.Where(x => x.Idcourse == id).SingleOrDefault();
                if (bien == null)
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Không tìm thấy thông tin khóa học!";
                    return Ok(msg);
                }
                else
                {
                    CourseModel temp = new CourseModel(bien);
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Thông tin khóa học #" + id;
                    msg.Data = temp;
                    return Ok(msg);
                }
            }
            catch(Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Exception: " + ex +" Lỗi!";
                return Ok(msg);
            }
            
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddCourse")]
        public IActionResult AddCourse([FromBody]CourseModel data)
        {
            var msg = new Message<CourseModel>();
            try
            {
                Course bien = new Course();
                bien.Idcourse = TaoMa();
                bien.Name = data.Name;
                bien.Age = data.Age;
                bien.Maxnumber = data.Maxnumber;
                bien.Time = data.Time;
                bien.Contents = data.Contents;
                bien.Fee = data.Fee;
                bien.Image = data.Image;

                _context.Course.Add(bien);
                _context.SaveChanges();
                CourseModel temp = new CourseModel(bien);
                msg.IsSuccess = true;
                msg.Data = temp;
                msg.ReturnMessage = "Thêm thành công!";
                return Ok(msg);
            }
            catch(Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Thêm khóa học không thành công! "+ex+" lỗi!";
                return Ok(msg);
            }
        }

        [HttpPost]
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
        [Route("FilterCoursesByAge")]
        public IActionResult FilterCoursesByAge(string age)
        {
            var msg = new Message<CourseModel>();
            int index = -1;
            index = age.IndexOf("-");
            if (index > -1)
            {
                int minage = 0;
                int maxage = 0;
                minage = int.Parse(age.Substring(0, index).Trim());
                maxage = int.Parse(age.Substring(index + 1).Trim());
                var result = _context.Course.Where(x => x.Age <= maxage && x.Age >= minage).SingleOrDefault();

                CourseModel temp = new CourseModel(result);
                msg.Data = temp;
                return Ok(msg);
            }
            else
            {
                int minage = int.Parse(age);
                var bien = _context.Course.Where(x => x.Age > minage).SingleOrDefault();

                CourseModel temp = new CourseModel(bien);
                msg.Data = temp;
                msg.IsSuccess = false;
                msg.ReturnMessage = "Không tìm thấy yêu cầu!";
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("FilterCoursesByPrice")]
        public IActionResult FilterCoursesByPrice(string price)
        {
            var msg = new Message<CourseModel>();
            int index = -1;
            index = price.IndexOf("-");
            if (index > -1)
            {
                decimal minprice = 0;
                decimal maxprice = 0;
                minprice = decimal.Parse(price.Substring(0, index).Trim());
                maxprice = decimal.Parse(price.Substring(index + 1).Trim());
                var result = _context.Course.Where(x => x.Fee < maxprice && x.Fee >= minprice).SingleOrDefault();

                CourseModel temp = new CourseModel(result);
                msg.Data = temp;
                return Ok(msg);
            }
            else
            {
                decimal minprice = decimal.Parse(price);
                var bien = _context.Course.Where(x => x.Fee >= minprice).SingleOrDefault();

                CourseModel temp = new CourseModel(bien);
                msg.Data = temp;
                msg.IsSuccess = false;
                msg.ReturnMessage = "Không tìm thấy yêu cầu!";
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(CourseModel data)
        {
            var msg = new Message<CourseModel>();
            try
            {
                var bien = _context.Course.Where(x => x.Idcourse == data.Idcourse).SingleOrDefault();
                if(bien != null)
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
            catch(Exception ex)
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
        [HttpPost]
        [Route("DeleteCourse")]
        public IActionResult DeleteCourse(string id)
        {
            var msg = new Message<CourseModel>();
            try
            {
                var bien = _context.Course.Where(x => x.Idcourse == id).SingleOrDefault();
                if(bien != null)
                {
                    if(Exitclass(id) == false)
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
            catch(Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Xóa khóa học #" + id + " không thành công! Lỗi!"+ex;
                return Ok(msg);
            }
        }

        private string TaoMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "KH";
                for (int i = 0; i < 8; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }
        private bool KiemtraID(string maID)
        {
            var temp = _context.Course.Where(x => x.Idcourse == maID).SingleOrDefault();
            if (temp == null)
                return true;
            return false;
        }
       
        private bool Exitclass(string id)
        {
            var bien = _context.Class.Where(x => x.Idcourse == id).SingleOrDefault();
            if (bien != null)
                return true;
            else
                return false;
        }
    }
}
