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

    public class TrackingOrderController : Controller
    {
        Convertion common = new Convertion();
        
        public ActionResult Index()
        {
            return View();
        }
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TrackingOrderDetailReport()
        {
            return View();
        }

        public ActionResult ListTrackingOrderDetailReport()
        {
            return View();
        }

        //Controller gọi đến chi tiết của bảng tra cứu đơn hàng
        public ActionResult ListDetailedTrackingOrderReport(string startdate, string enddate, string customercode, int type)
        {

            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrder returntrackingorder = new ReturnTrackingOrder();
            returntrackingorder = trackingorderRepository.TRACKING_ORDER_DETAIL(common.DateToInt(startdate), common.DateToInt(enddate) , customercode, type);
            return View(returntrackingorder);

        }

        //Controller gọi đến Header của bảng tra cứu đơn hàng
        public ActionResult ListDetailedHeaderTrackingOrderReport(string startdate, string enddate, string customercode)
        {

            TrackingOrderRepository trackingorderRepository = new TrackingOrderRepository();
            ReturnTrackingOrder returntrackingorder = new ReturnTrackingOrder();
            returntrackingorder = trackingorderRepository.HEADER_TRACKING_ORDER_DETAIL(startdate, enddate, customercode);
            return View(returntrackingorder);

        }

    }
}