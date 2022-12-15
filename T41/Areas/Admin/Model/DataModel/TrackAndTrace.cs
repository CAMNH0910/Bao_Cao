using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class TBL_INFO
    {
        public string Nuoc_Chapnhan { get; set; }

        public string Nuoc_Phat { get; set; }

        public string BC_GUI { get; set; }

        public string BC_PHAT { get; set; }

        public string HO_TEN_GUI { get; set; }

        public string DIA_CHI_GUI { get; set; }

        public string SenderMobile { get; set; }

        public string KL_QUI_DOI { get; set; }

        public string HO_TEN_NHAN { get; set; }

        public string DIA_CHI_NHAN { get; set; }

        public string ReceiverMobile { get; set; }

        public string CMT_Nhan { get; set; }

        public string CMT_NgayCap_Nhan { get; set; }

        public string LO { get; set; }

        public string ID { get; set; }

        public string BC16 { get; set; }

        public string isCOD { get; set; }

        public string ExecuteOrder { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerGroupCode { get; set; }

        public string ServiceCode { get; set; }

        public string DataCode { get; set; }

        public string Weight { get; set; }

        public string TotalFreightVAT { get; set; }

        public string SendingContent { get; set; }

        public string ReceiverTel { get; set; }

        public string VAS { get; set; }

        public string isDomestic { get; set; }

        public string isAffair { get; set; }

        public string OriItemCode { get; set; }

        public string AccPOS { get; set; }

        public string CountryCode { get; set; }

        public string MainFreight { get; set; }

        public string FuelSurchargeFreight { get; set; }

        public string FarRegionFreight { get; set; }

        public string SubFreight { get; set; }
        public string TotalFreight { get; set; }
        public string TotalFreightdiscountvat { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public string ValueAddedServiceName { get; set; }

        public string sotien_nhothu { get; set; }

    }

    public class TBL_DINH_VI
    {
        public string TraceDate { get; set; }
        public string Date { get; set; }
        public string TimeDetail { get; set; }
        public string StatusText { get; set; }
        public string VI_TRI { get; set; }
        public string POSTel { get; set; }
       
    }

    public class TBL_CHUYEN_THU
    {
        public string DATE { get; set; }
        public string TIMEDETAIL { get; set; }
        public string BCG { get; set; }
        public string BCN { get; set; }
        public string CHUYEN_THU { get; set; }
    }

    public class TBL_DELIVERY
    {
        public string DATE { get; set; }
        public string TIMEDETAIL { get; set; }
        public string NGAY_PHAT { get; set; }
        public string NGAY_NHAP { get; set; }
        public string NGAY_TRUYEN { get; set; }
        public string NGAY_CN { get; set; }
        public string VI_TRI { get; set; }
        public string STATUSTEXT { get; set; }
        public string IsDeliverable { get; set; }
        public string CauseCode { get; set; }
    }

    public class TBL_BD10
    {
        public string DATE { get; set; }
        public string TIMEDETAIL { get; set; }
        public string BCG { get; set; }
        public string BCN { get; set; }
        public string BC37_INFO { get; set; }
        public string SendingTime { get; set; }
        public string BC37Date { get; set; }
    }

    public class TBL_BD10_STATUS
    {
        public string DATE { get; set; }
        public string TIMEDETAIL1 { get; set; }
        public string TIMEDETAIL { get; set; }
        public string STATUSTEXT { get; set; }
        public string VI_TRI { get; set; }
        public string ConfirmDate { get; set; }
        public string BC37Date { get; set; }
    }

    public class ReturnTrackAndTrace
    {
        public string Code { get; set; }

        public string Message { get; set; }

        //Phần lấy list TBL_INFO
        public TBL_INFO TBL_INFO_Report { get; set; }

        public List<TBL_INFO> List_TBL_INFO_Report;

        //Phần lấy list TBL_DINH_VI
        public TBL_DINH_VI TBL_DINH_VI_Report { get; set; }

        public List<TBL_DINH_VI> List_TBL_DINH_VI_Report;

        //Phần lấy list TBL_CHUYEN_THU
        public TBL_CHUYEN_THU TBL_CHUYEN_THU_Report { get; set; }

        public List<TBL_CHUYEN_THU> List_TBL_CHUYEN_THU_Report;

        //Phần lấy list TBL_DELIVERY
        public TBL_DELIVERY TBL_DELIVERY_Report { get; set; }

        public List<TBL_DELIVERY> List_TBL_DELIVERY_Report;

        //Phần lấy list TBL_BD10
        public TBL_BD10 TBL_BD10_Report { get; set; }

        public List<TBL_BD10> List_TBL_BD10_Report;

        //Phần lấy list TBL_BD10_STATUS
        public TBL_BD10_STATUS TBL_BD10_STATUS_Report { get; set; }

        public List<TBL_BD10_STATUS> List_TBL_BD10_STATUS_Report;
    }

    
}