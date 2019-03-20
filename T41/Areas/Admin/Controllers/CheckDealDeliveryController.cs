using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class CheckDealDeliveryController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/CheckDealDelivery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckDealDeliveryDetailReport()
        {
            var userid = Convert.ToInt32(Session["userid"]);
            //Phân quyền đăng nhập
            if (userid == 1 || userid == 2)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        //Phần controller Detail
        //Type : Json
        public JsonResult ListCheckDealDelivery_Report(string fromdate, string todate, int ma_bc_khai_thac)
        {
            CheckDealDeliveryRepository checkdealdeliveyRepository = new CheckDealDeliveryRepository();
            ReturnCheckDealDelivery returncheckdealdelivery = new ReturnCheckDealDelivery();
            returncheckdealdelivery = checkdealdeliveyRepository.CHECK_DEAL_DELIVERY_DETAIL(common.DateToInt(fromdate), common.DateToInt(todate), ma_bc_khai_thac);
            return Json(returncheckdealdelivery, JsonRequestBehavior.AllowGet);
        }
    }
}