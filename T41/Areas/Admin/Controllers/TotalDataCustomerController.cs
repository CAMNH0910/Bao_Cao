using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Controllers
{

    public class TotalDataCustomerController : Controller
    {
        Convertion common = new Convertion();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TotalDataCustomerDetailReport()
        {
            return View();
        }
        public JsonResult Provincenhan()
        {
            TotalDataCustomerRepository totaldatacustomerRepository = new TotalDataCustomerRepository();
            return Json(totaldatacustomerRepository.GetallProvinceNhan(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Provincetra()
        {
            TotalDataCustomerRepository totaldatacustomerRepository = new TotalDataCustomerRepository();
            return Json(totaldatacustomerRepository.GetallProvinceTra(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
     

        //Controller lấy dữ liệu bưu cục đóng, bưu cục nhận
        public JsonResult PosCode()
        {
            TotalDataCustomerRepository totaldatacustomerRepository = new TotalDataCustomerRepository();
            return Json(totaldatacustomerRepository.GetPOSCODE(), JsonRequestBehavior.AllowGet);
        }

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        [HttpGet]
        public ActionResult ListDetailedTotalDataCustomerReport(string customercode, string startdate, string enddate, int startprovince, int endprovince, int isservice)
        {
            TotalDataCustomerRepository totaldatacustomerRepository = new TotalDataCustomerRepository();
            ReturnTotalDataCustomer returntotaldatacustomer = new ReturnTotalDataCustomer();
            returntotaldatacustomer = totaldatacustomerRepository.TOTAL_DATA_CUSTOMER_DETAIL(customercode, common.DateToInt(startdate), common.DateToInt(enddate), startprovince, endprovince, isservice);
            return View(returntotaldatacustomer);
        }

    }
}