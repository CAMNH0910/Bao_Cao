using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
       
    //Phần lấy dữ liệu của bảng crm_sales_order
    public class CheckDealTransfer_Detail
    {
        public int STT { get; set; }
        public string ID_CHUYEN_THU { get; set; }
        public string MA_BC_KHAI_THAC { get; set; }
        public string SO_CHUYEN_THU { get; set; }
        public string NGAY_DONG { get; set; }
        public string GIO_DONG { get; set; }
        public string TONG_SO_TUI { get; set; }
        public string TONG_SO_BP { get; set; }
        public string TONG_KL { get; set; }

        public string TONG_KLBP { get; set; }
        public string TONG_CUOC_COD { get; set; }
        public string TONG_CUOC_DV { get; set; }
        public string TONG_CUOC { get; set; }
        public string HH_PHAT_HANH { get; set; }
        public string HH_PHAT_TRA { get; set; }
        public String NGAY_HE_THONG { get; set; }
        //public String AMND_DATE { get; set; }
        public String IP_MAY_CHU { get; set; }
        public string MAILTRIP_KEY { get; set; }
        public String TONG_SO_BP_TEMP { get; set; }

       
    }

    public class CheckDealTransfer_Detail_Modal
    {
        public int STT { get; set; }
        public String ID_DUONG_THU { get; set; }
        public String ID_CHUYEN_THU { get; set; }

        public String ID_E2 { get; set; }
        public String ID_CA { get; set; }
        public String MA_BC_KHAI_THAC { get; set; }
        public String TUI_SO { get; set; }
        public String TONG_SO_BP { get; set; }
        public String MAILTRIP_KEY { get; set; }


    }

    public class CheckDealTransfer_Detail_NewTab
    {
        public int STT { get; set; }
        public String ID_E1 { get; set; }
        public String ID_DUONG_THU { get; set; }
        public String ID_CHUYEN_THU { get; set; }

        public String ID_E2 { get; set; }
        public String ID_CA { get; set; }
        public String MA_BC_KHAI_THAC { get; set; }
        public String MA_E1 { get; set; }
        public String NGAY_HE_THONG { get; set; }
        public String MAILTRIP_KEY { get; set; }


    }




    public class ReturnCheckDealTransfer
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        public string Value { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        //Detail
        public CheckDealTransfer_Detail CheckDealTransfer_Report { get; set; }
        public List<CheckDealTransfer_Detail> ListCheckDealTransfer_Report;

        //Modal
        public CheckDealTransfer_Detail_Modal CheckDealTransfer_Modal_Report { get; set; }
        public List<CheckDealTransfer_Detail_Modal> ListCheckDealTransfer_Modal_Report;

        //NewTab
        public CheckDealTransfer_Detail_NewTab CheckDealTransfer_NewTab_Report { get; set; }
        public List<CheckDealTransfer_Detail_NewTab> ListCheckDealTransfer_NewTab_Report;

    }
}