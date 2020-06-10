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
    public class InternationalDetailController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/InternationalDetail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InternationalDetailReport()
        {
            return View();
        }

        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
        public JsonResult CountryCode()
        {
            InternationalDetailRepository internationaldetailRepository = new InternationalDetailRepository();
            return Json(internationaldetailRepository.GetCountryCode(), JsonRequestBehavior.AllowGet);
        }

        //Phần controller Detail
        //Type : Json
        public JsonResult ListInternationalDetail_Report(string fromdate, string todate, string countrycode)
        {
            InternationalDetailRepository internationaldetailRepository = new InternationalDetailRepository();
            ReturnInternationalDetail returninternationaldetail = new ReturnInternationalDetail();
            returninternationaldetail = internationaldetailRepository.INTERNATIONAL_DETAIL(common.DateToInt(fromdate), common.DateToInt(todate), countrycode);
            return Json(returninternationaldetail, JsonRequestBehavior.AllowGet);
        }
    }
}