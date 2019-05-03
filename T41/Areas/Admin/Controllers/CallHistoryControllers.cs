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
    public class CallHistoryController : Controller
    {
        public ActionResult CallHistoryDetailReport()
        {
            return View();
        }

        //Phần controller Detail
        //Type : Json
        public ActionResult ListCallHistoryReport(string code)
        {
            CallHistoryRepository callhistoryRepository = new CallHistoryRepository();
            ReturnCallHistory returncallhistory = new ReturnCallHistory();
            returncallhistory = callhistoryRepository.GET_CallHistory(code);
            return View(returncallhistory);
            //return Json(returncallhistory, JsonRequestBehavior.AllowGet);
        }
    }
}