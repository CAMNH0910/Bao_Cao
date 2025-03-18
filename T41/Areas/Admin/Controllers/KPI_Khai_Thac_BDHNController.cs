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
    public class KPI_Khai_Thac_BDHNController : Controller
    {
        Convertion common = new Convertion();
        public ActionResult Index()
        {
            return View();
        }
        #region Chấp nhận
        [HttpPost]
        public JsonResult Chap_Nhan_TH(string startdate, string enddate)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPI_Khai_Thac_BDHNRepository kPI_CustomerRepository = new KPI_Khai_Thac_BDHNRepository();
            ReturnKhai_Thac_BDHN returnquality = new ReturnKhai_Thac_BDHN();
            returnquality = kPI_CustomerRepository.Chap_nhan_TH(common.DateToInt(startdate), common.DateToInt(enddate));

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult Chap_Nhan_CT(string startdate, string enddate,int postCode)
        {

            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            KPI_Khai_Thac_BDHNRepository kPI_CustomerRepository = new KPI_Khai_Thac_BDHNRepository();
            ReturnKhai_Thac_BDHN returnquality = new ReturnKhai_Thac_BDHN();
            returnquality = kPI_CustomerRepository.Chap_Nhan_CT(common.DateToInt(startdate), common.DateToInt(enddate), postCode);
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion
    }
}
