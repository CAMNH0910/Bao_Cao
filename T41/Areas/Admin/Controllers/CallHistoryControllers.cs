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

        public ActionResult ID_CallHistoryDetailReport()
        {
            return View();
        }

        //Phần controller Detail
        //Type : Json
        //Get data by EMS CODE
        public ActionResult ListCallHistoryReport(string code)
        {
            CallHistoryRepository callhistoryRepository = new CallHistoryRepository();
            ReturnCallHistory returncallhistory = new ReturnCallHistory();
            returncallhistory = callhistoryRepository.GET_CallHistory(code);
            return View(returncallhistory);
            //return Json(returncallhistory, JsonRequestBehavior.AllowGet);
        }

        //Get data by EMS ID
        public ActionResult ID_ListCallHistoryReport(int id_call_history)
        {
            CallHistoryRepository callhistoryRepository = new CallHistoryRepository();
            ReturnCallHistory returncallhistory = new ReturnCallHistory();
            returncallhistory = callhistoryRepository.GET_CallHistory_ID(id_call_history);
            return View(returncallhistory);
        }
    }
}