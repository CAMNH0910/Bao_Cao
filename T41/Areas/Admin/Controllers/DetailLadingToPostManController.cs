using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class DetailLadingToPostManController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LadingToPostManDetailReport()
        {
            var userid = Convert.ToInt32(Session["userid"]);
            //Phân quyền đăng nhập
            if (userid == 1)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Phần show ra dữ liệu của Bưu Cục Phát
        public JsonResult ListPostCode(string unit_code)
        {
            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            return Json(bd13Repository.GetAllDeliveryPostCode(unit_code), JsonRequestBehavior.AllowGet);
            
        }

        //Phần show ra dữ liệu của Tuyến Phát theo bưu cục phát
        public JsonResult ListDeliveryRouteByPostCode(int delivery_post_code)
        {
            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            return Json(bd13Repository.GetDeliveryRouteCodeByDeliveryCode(delivery_post_code), JsonRequestBehavior.AllowGet);
            
        }

        //Phần show ra dữ liệu của bảng người dùng
        public ActionResult ListDetailedLADINGTOPOSTMANReport(int? page, int mabc_kt, int mabc, string ngay, int cakt, int chthu, int tuiso)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            DetailLadingToPostmanRepository bd13Repository = new DetailLadingToPostmanRepository();
            ReturnLADING ReturnLADING = new ReturnLADING();
            ReturnLADING = bd13Repository.LADING_TO_POSTMAN_DETAIL(currentPageIndex, page_size,  mabc_kt, mabc, common.DateToInt(ngay), cakt, chthu, tuiso);
            ViewBag.total = ReturnLADING.Total;
            ViewBag.total_page = (ReturnLADING.Total + page_size - 1) / page_size;
            return View(ReturnLADING);

        }

        
    }
}