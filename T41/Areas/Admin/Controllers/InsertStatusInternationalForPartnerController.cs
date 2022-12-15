using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Data;

namespace T41.Areas.Admin.Controllers
{
    public class InsertStatusInternationalForPartnerController : Controller
    {
        // GET: Admin/InsertStatusInternational
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertStatusInternationalForPartnerView()
        {
            return View();
        }

        //Controller lấy dữ liệu mã nước
        public JsonResult GetCountryCode()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.GetCountryCode(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountryCodeForPartner()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.GetCountryCodeForPartner(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu trạng thái
        public JsonResult GetListEventCode()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.GetListEventCodeForPartner(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertStatusInternationalCreate(ParameterInsertStatusInternational param)
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            ReponseEntity responseentity = new ReponseEntity();
            responseentity = InsertStatusInternationalRepository.StatusInternational_Create(param);
            return Json(responseentity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertStatusInternationalForPartnerCreate(string MAE1,string STATUS, string MANC,string NGAY, string GIO, string LYDO, string HANHDONG)
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            ReponseEntity responseentity = new ReponseEntity();
            responseentity = InsertStatusInternationalRepository.StatusInternationalForPartner_Create(MAE1, STATUS, MANC, NGAY, GIO, LYDO, HANHDONG);
            return Json(responseentity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisplayStatusInternational(string E1_Code)
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            ReturnStatusInternational returnStatusInternational = new ReturnStatusInternational();
            returnStatusInternational = InsertStatusInternationalRepository.DisplayStatusInternational(E1_Code);
            var jsonResult = Json(returnStatusInternational, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}