using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Model.BusinessModel;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class AdministratorsController : Controller
    {
        private DTPowerDBContext db = new DTPowerDBContext();

        // GET: Admin/Administrators

        public ActionResult Index(int page = 1)
        {
            int pageSize = 20;  // Số dòng mỗi trang
            int skip = (page - 1) * pageSize;

            // Lấy danh sách người dùng phân trang
            var model = db.Administrator
                          .OrderBy(a => a.UserId)  // Sắp xếp theo UserId
                          .Skip(skip)  // Bỏ qua số dòng tương ứng với trang
                          .Take(pageSize)  // Lấy 10 dòng
                          .ToList();

            // Tính số trang hiện tại và tổng số trang
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(db.Administrator.Count() / (double)pageSize);  // Tính tổng số trang

            return View(model);
        }


        // GET: Admin/Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Admin/Administrators/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,PassWord,FullName,Email,Avatar,IsAdmin,Active,Role")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                administrator.PassWord = Common.Security.CreatPassWordHash(administrator.UserName + administrator.PassWord + "6688");
                db.Administrator.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }

        // GET: Admin/Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Admin/Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,PassWord,FullName,Email,Avatar,IsAdmin,Active,Role")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                administrator.PassWord = Common.Security.CreatPassWordHash(administrator.UserName + administrator.PassWord + "6688");
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        // GET: Admin/Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Admin/Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrator.Find(id);
            db.Administrator.Remove(administrator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ChangePassword()
        {
            var userName = Session["UserName"] as string;  // Lấy UserName từ session (kiểu string)
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account");  // Nếu chưa đăng nhập, chuyển hướng tới trang đăng nhập
            }

            var administrator = db.Administrator.FirstOrDefault(a => a.UserName == userName);
            if (administrator == null)
            {
                return HttpNotFound();
            }

            // Tạo model mới để trả về view
            var model = new ChangePasswordModel { UserId = administrator.UserId };
            return View(model);  // Truyền model vào view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model )
        {
            if (ModelState.IsValid)
            {
                var userName = Session["UserName"] as string;
                if (string.IsNullOrEmpty(userName))
                {
                    return RedirectToAction("Login", "Account");  // Nếu chưa đăng nhập, chuyển hướng tới trang đăng nhập
                }

                var administrator = db.Administrator.FirstOrDefault(a => a.UserName == userName);
                if (administrator == null)
                {
                    return HttpNotFound();
                }
                var test = Common.Security.CreatPassWordHash(userName + model.OldPassword +  "6688");
                // Kiểm tra mật khẩu cũ
                if (administrator.PassWord != Common.Security.CreatPassWordHash(userName + model.OldPassword +  "6688"))
                {
                    ModelState.AddModelError("OldPassword", "Mật khẩu cũ không chính xác.");
                    return View(model);
                }

                // Cập nhật mật khẩu mới
                administrator.PassWord = Common.Security.CreatPassWordHash(userName + model.NewPassword + "6688");
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Message = "Mật khẩu đã được thay đổi thành công.";
                return RedirectToAction("Index", "Home");  // Sau khi đổi mật khẩu, chuyển hướng đến trang chính
            }

            return View(model);  // Trả về view nếu có lỗi trong quá trình thay đổi mật khẩu
        }

    }
}