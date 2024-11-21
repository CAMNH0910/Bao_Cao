using System;
using System.Linq;
using System.Web.Mvc;
using T41.Areas.Admin.Model.BusinessModel;

namespace T41.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DTPowerDBContext db = new DTPowerDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session["CaptchaCode"] = GenerateRandomCaptcha(); // Tạo mã xác nhận khi vào trang đăng nhập
            ViewBag.CaptchaCode = Session["CaptchaCode"];
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult Login(string userid, string password, string captcha)
        {
            // Kiểm tra nếu người dùng không nhập mã CAPTCHA
            if (string.IsNullOrEmpty(captcha))
            {
                return Json(new { error = "Vui lòng nhập mã xác nhận." });
            }

            // Kiểm tra mã xác nhận
            if (Session["CaptchaCode"] == null || Session["CaptchaCode"].ToString() != captcha)
            {
                // Tạo mã xác nhận mới nếu mã xác nhận sai
                string newCaptcha = GenerateRandomCaptcha();
                Session["CaptchaCode"] = newCaptcha; // Lưu mã mới vào session

                return Json(new { error = "Mã xác nhận sai. Vui lòng nhập lại.", newCaptcha });
            }

            // Kiểm tra tên đăng nhập và mật khẩu
            string passwordMd5 = Common.Security.CreatPassWordHash(userid + password + "6688");
            var user = db.Administrator.SingleOrDefault(x => x.UserName == userid && x.PassWord == passwordMd5 && x.Active == 1);

            if (user != null)
            {
                // Xử lý đăng nhập thành công
                Session["userid"] = user.UserId;
                Session["username"] = user.UserName;
                Session["fullname"] = user.FullName;
                Session["avatar"] = user.Avatar;
                Session["isadmin"] = user.IsAdmin;
                Session["Role"] = user.Role;
                Session["Email"] = user.Email;

                return Json(new { redirectUrl = Url.Action("Index") });
            }
            else
            {
                // Tạo mã xác nhận mới nếu mật khẩu hoặc tài khoản sai
                string newCaptcha = GenerateRandomCaptcha();
                Session["CaptchaCode"] = newCaptcha; // Lưu mã mới vào session

                return Json(new { error = "Đăng nhập sai hoặc không có quyền truy cập", newCaptcha });
            }
        }

        private string GenerateRandomCaptcha()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString(); // Tạo mã xác nhận 5 số
        }

        public EmptyResult Alive()
        {
            return new EmptyResult();
        }
    }
}
