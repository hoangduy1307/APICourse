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
    public class RegisterController : ControllerBase
    {
        ContainerCourseContext db = new ContainerCourseContext();
        // GET: api/<controller>
        [HttpGet]
        [Route("GetAllRegister")]
        public IActionResult GetAllRegister()
        {
            try
            {
                var lstpd = db.RigistrationCourse;

                List<RegistrationCourseModel> lst = new List<RegistrationCourseModel>();
                foreach (var i in lstpd)
                {
                    RegistrationCourseModel temp = new RegistrationCourseModel(i);
                    lst.Add(temp);
                }
                return Ok(lst);
            }
            catch
            {
                return Ok(null);
            }
        }

       

        // POST api/<controller>
        [HttpPost]
        [Route("AddRegister")]
        public IActionResult AddRegister([FromBody]RegistrationCourseModel data)
        {
            var msg = new Message<RegistrationCourseModel>();
            try
            {
                RigistrationCourse rg = new RigistrationCourse();
                rg.Idregist = TaoMa();
                rg.NameParent = data.NameParent;
                rg.Phone = data.Phone;
                rg.Email = data.Email;
                rg.NameStudent = data.NameStudent;
                rg.Birthday = data.Birthday;
                rg.Address = data.Address;
                rg.Idcourse = data.Idcourse;
                rg.State = "Chưa Duyệt";

                db.RigistrationCourse.Add(rg);
                db.SaveChanges();

                RegistrationCourseModel temp = new RegistrationCourseModel(rg);
                msg.IsSuccess = true;
                msg.ReturnMessage = "Đăng ký thành công!";
                msg.Data = temp;
                return Ok(msg);
            }
            catch(Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Đăng ký không thành công! Lỗi!"+ ex;
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("UpdateRegister")]
        public IActionResult UpdateRegister([FromBody]RegistrationCourseModel data)
        {
            var msg = new Message<RegistrationCourseModel>();
            try
            {
                var check = db.RigistrationCourse.Where(x => x.Idregist == data.Idregist).SingleOrDefault();
                if(check != null)
                {
                    check.NameParent = data.NameParent;
                    check.Phone = data.Phone;
                    check.Email = data.Email;
                    check.NameStudent = data.NameStudent;
                    check.Birthday = data.Birthday;
                    check.Address = data.Address;
                    check.Idcourse = data.Idcourse;
                    check.State = data.State;

                    db.Entry(check).State = EntityState.Modified;
                    db.SaveChanges();

                    RegistrationCourseModel temp = new RegistrationCourseModel(check);
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Cập nhật thông tin đăng ký khóa học thành công!";
                    msg.Data = temp;
                    return Ok(msg);
                }
                else
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Không tìm thấy thông tin muốn sửa!";
                    return Ok(msg);
                }
               
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Cập nhật thông tin đăng ký khóa học không thành công! Lỗi!" + ex;
                return Ok(msg);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private int TaoMa()
        {
            int maID;
            Random rand = new Random();
            do
            {
                int a = db.RigistrationCourse.Max(r => r.Idregist);
                maID = a + 1;

            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(int maID)
        {
            var temp = db.RigistrationCourse.Find(maID);
            if (temp == null)
                return true;
            return false;
        }
    }
}
