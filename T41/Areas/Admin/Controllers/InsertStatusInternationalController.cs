using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Data;

namespace T41.Areas.Admin.Controllers
{
    public class InsertStatusInternationalController : Controller
    {
        // GET: Admin/InsertStatusInternational
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertStatusInternationalView()
        {
            return View();
        }

        //Controller lấy dữ liệu mã nước
        public JsonResult GetCountryCode()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.GetCountryCode(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu trạng thái
        public JsonResult GetListEventCode()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.GetListEventCode(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu hướng xử lý
        public JsonResult Get_UPU_ACTION_CODE()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.Get_UPU_ACTION_CODE(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu lý do
        public JsonResult Get_UPU_REASON_CODE()
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            return Json(InsertStatusInternationalRepository.Get_UPU_REASON_CODE(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertStatusInternationalCreate(ParameterInsertStatusInternational param)
        {
            InsertStatusInternationalRepository InsertStatusInternationalRepository = new InsertStatusInternationalRepository();
            ReponseEntity responseentity = new ReponseEntity();
            responseentity = InsertStatusInternationalRepository.StatusInternational_Create(param);
            return Json(responseentity, JsonRequestBehavior.AllowGet);
        }
    }
}