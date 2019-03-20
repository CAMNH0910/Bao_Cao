using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Phần lấy dữ liệu chi tiết
    public class CheckDealDelivery_Detail
    {
        public int STT { get; set; }
        public string ID_BAO_PHAT { get; set; }
        public string MA_E1 { get; set; }
        public string NUOC_NHAN { get; set; }
        public string MA_BC_KHAI_THAC { get; set; }
        public string MA_BC_PHAT { get; set; }
        public string NGAY_PHAT { get; set; }
        public string GIO_PHAT { get; set; }
        public string NGAY_NHAP { get; set; }

        public string GIO_NHAP { get; set; }
        public string NGUOI_PHAT { get; set; }
        public string NGUOI_NHAN { get; set; }
        public string KHOI_LUONG { get; set; }
        public string MA_LY_DO { get; set; }
        public string MA_XU_LY { get; set; }

      
        public string PHAT_DUOC { get; set; }
        
        public string ID_NGUOI_DUNG { get; set; }
        public string NGAY_HE_THONG { get; set; }
        public string TRUYEN_KHAI_THAC { get; set; }

        public string GHI_CHU { get; set; }
        public string PHAT_HOAN { get; set; }
        public string DIEN_THOAI_NHAN { get; set; }

        public string ID_CA { get; set; }
        public string IDSESSION { get; set; }

    }

    //Phần lấy dữ liệu tổng
    public class CheckDealDelivery_TOTAL
    {
        public int TONG_SO_BAO_PHAT_TRUYEN { get; set; }
        public int TONG_SO_MA_E1_TRUYEN { get; set; }
        public int TONG_SO_BAO_PHAT_DOI_SOAT { get; set; }
        public int TONG_SO_MA_E1_DOI_SOAT { get; set; }
    }


    public class ReturnCheckDealDelivery
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
        public CheckDealDelivery_TOTAL CheckDealDelivery_Total { get; set; }

        //Detail
        public CheckDealDelivery_Detail CheckDealDelivery_Report { get; set; }
        public List<CheckDealDelivery_Detail> ListCheckDealDelivery_Report;

        

    }
}