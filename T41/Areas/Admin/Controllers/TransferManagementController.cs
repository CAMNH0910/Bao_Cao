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

    public class TransferManagementController : Controller
    {
        Convertion common = new Convertion();
        
        public ActionResult Index()
        {
            return View();
        }
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TransferManagementDetailReport()
        {
            return View();

        }

        public ActionResult E2_TransferManagementDetailReport()
        {
            return View();

        }
        public ActionResult MCT_E2_TransferManagementDetailReport()
        {
            return View();

        }

        public ActionResult SOTUITRONGCHTHU()
        {
            return View();

        }
        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
        public JsonResult ProvinceCode()
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            return Json(transfermanagementRepository.GETPROVINCE(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu bưu cục đóng, bưu cục nhận
        public JsonResult PosCode(int province)
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            return Json(transfermanagementRepository.GetCRPOSCODE(province), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }



        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedTransferManagementReport(int fromprovince, int toprovince, int fromposcode, int toposcode, string fromdate, string todate, int typecomunication)
        {
            ViewBag.typecomunication = typecomunication;

            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_DETAIL(fromprovince, toprovince, fromposcode, toposcode, fromdate, todate,  typecomunication);
            //return View(returntransfermanagement.ListTransferManagementReport);
            return View(returntransfermanagement);

        }
        //Controller gọi đến chi tiết theo từng chuyến thư hoặc túi số của bảng chi tiết
        public ActionResult ListTransferManagement_CTTS_Report(int fromposcode, int toposcode, string date, int type, int typecomunication)
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_CT_TS_DETAIL(fromposcode, toposcode, common.DateToInt(date), type, typecomunication);
            return Json(returntransfermanagement, JsonRequestBehavior.AllowGet);
            
        }

        //Controller gọi đến chi tiết số túi theo từng chuyến thư của bảng chi tiết
        public ActionResult ListTransferManagement_SOTUI_Report(int fromposcode, int toposcode, string date, int mailtrip, int typecomunication)
        {
            ViewBag.typecomunication = typecomunication;

            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_SOTUI_DETAIL(fromposcode, toposcode, common.DateToInt(date), mailtrip, typecomunication);
            return View(returntransfermanagement);

        }

        //Controller gọi đến chi tiết của bảng in bản kê E2 theo chuyến thư túi số
        public ActionResult ListDetailedTransferManagement_E2_Report(int fromposcode, int toposcode, string date, int mailtrip, int postbag, int typecomunication)
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_E2_DETAIL(fromposcode, toposcode, common.DateToInt(date), mailtrip, postbag, typecomunication);
            return View(returntransfermanagement);

        }

        //Controller gọi đến chuyến thư túi số trong phần in bản kê E2 theo chuyến thư túi số
        public ActionResult ListDetailedTransferManagement_CTTS_E2_Report(int fromposcode, int toposcode, string date, int mailtrip, int postbag, int typecomunication)
        {
           
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_CTTS_E2_DETAIL(fromposcode, toposcode, common.DateToInt(date), mailtrip, postbag, typecomunication);
            
            return View(returntransfermanagement);
            
        }

        //Controller gọi đến chi tiết của bảng in bản kê E2 theo mã cổ túi
        public ActionResult ListDetailedTransferManagement_MCT_E2_Report(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_MCT_E2_DETAIL(fromposcode, toposcode, date, mailtrip, postbag, typecomunication);
            return View(returntransfermanagement);

        }

        //Controller gọi đến chuyến thư túi số trong phần in bản kê E2 theo mã cổ túi
        public ActionResult ListDetailedTransferManagement_CTTS_MCT_E2_Report(int fromposcode, int toposcode, int date, int mailtrip, int postbag,int typecomunication)
        {

            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransferManagement returntransfermanagement = new ReturnTransferManagement();
            returntransfermanagement = transfermanagementRepository.TRANSFER_MANAGEMENT_CTTS_MCT_E2_DETAIL(fromposcode, toposcode, date, mailtrip, postbag, typecomunication);

            return View(returntransfermanagement);

        }

        //Controller truyền lại dữ liệu từ Center về BCP
        [HttpPost]
        public ActionResult TransmitDataCenterToBcp(int fromposcode, int toposcode, int date, int mailtrip, int posbag)
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransmited returntransmition = new ReturnTransmited();
            returntransmition = transfermanagementRepository.TransmitDataCentertoBCP(fromposcode, toposcode, date, mailtrip, posbag);
            return Json(returntransmition, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult TransmitHubToCenter(int fromposcode, int toposcode, int date, int mailtrip, int posbag)
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransmited returntransmition = new ReturnTransmited();
            returntransmition = transfermanagementRepository.TransmitDataHubtoCenter(fromposcode, toposcode, date, mailtrip, posbag);
            return Json(returntransmition, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult TransmitDataCentertoBCCP(int fromposcode, int toposcode, int date, int mailtrip)
        {
            TransferManagementRepository transfermanagementRepository = new TransferManagementRepository();
            ReturnTransmited returntransmition = new ReturnTransmited();
            returntransmition = transfermanagementRepository.TransmitDataCentertoBCCP(fromposcode, toposcode, date, mailtrip);
            return Json(returntransmition, JsonRequestBehavior.AllowGet);

        }
    }
}