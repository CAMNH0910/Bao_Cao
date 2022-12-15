using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class GETPROVINCECODE
    {
        public int PROVINCECODE { get; set; }

        public string PROVINCENAME { get; set; }

    }
    public class PARAMETERKPITransport
    {
        public string Hub { get; set; }
        public string EndProvince { get; set; }
        public string IsService { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }

    //Lấy chi tiết của bảng tổng hợp sản lượng vận chuyển
    public class KPITransportTotal
    {
        public int STT { get; set; }
        public string WorkCenter { get; set; }
        public string WorkCenterName { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        //public string IsService { get; set; }
        public string Total { get; set; }
        public string TLSuccess { get; set; }
        public string IsFailStatus { get; set; }
        public string TLIsFailStatus { get; set; }
        public string NotKPI { get; set; }
        public string TLNotKPI { get; set; }

    }

    public class THHubDetail
    {
        public int STT { get; set; }
        public String WorkCenter { get; set; }
        public String EndProvinceName { get; set; }
        public String Total { get; set; }
        public String SLSuccess { get; set; }
        public String TLSuccess { get; set; }
        public String SLFail { get; set; }
        public String TLFail { get; set; }
        public String Total0_16 { get; set; }
        public String SL0_16 { get; set; }
        public String TL0_16 { get; set; }
        public String SLFail0_16 { get; set; }
        public String TLFail0_16 { get; set; }
        public String Total16_24 { get; set; }
        public String SL16_24 { get; set; }
        public String TL16_24 { get; set; }
        public String SLFail16_24 { get; set; }
        public String TLFail16_24 { get; set; }

    }


    public class KPITransportNotKPI
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string WorkCenter { get; set; }
        public string WorkCenterName { get; set; }
        public string StartProvince { get; set; }
        public string StartProvinceName { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string StatusDateTime { get; set; }
        public string IsService { get; set; }
        public string IsType { get; set; }
    }

    public class ReturnKPITransportNotKPI
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPITransportNotKPI KpiTransportNotKpiReport { get; set; }
        public List<KPITransportNotKPI> ListKpiTransportNotKpiReport;     
    }

    public class KPITransportFail
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string isService { get; set; }
        public string WorkCenter { get; set; }
        public string WorkCenterName { get; set; }
        public string A1FromPosCode { get; set; }
        public string A1ToPosCode { get; set; }
        public string StartProvinceName { get; set; }
        public string EndProvinceName { get; set; }
        public string A1StatusDateTime { get; set; }
        public string A2PosCode { get; set; }
        public string A2StatusDateTime { get; set; }
        public string A3StatusDateTime { get; set; }
        public string TimeHub1 { get; set; }
        public string A4PosCode { get; set; }
        public string A4StatusDateTime { get; set; }
        public string A5StatusDateTime { get; set; }
        public string TimeHub2 { get; set; }
        public string A6PosCode { get; set; }
        public string A6StatusDateTime { get; set; }
        public string TimeInterval { get; set; }
        public string Note { get; set; }
    }

    public class ReturnKPITransportFail
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPITransportFail KpiTransportFailReport { get; set; }
        public List<KPITransportFail> ListKpiTransportFailReport;
    }
    public class ReturnKPITransportTotal
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPITransportTotal KpiTransportReport { get; set; }
        public List<KPITransportTotal> ListKpiTransportReport;
        public THHubDetail THHubDetailReport { get; set; }
        public List<THHubDetail> ListTHHubDetailReport;
        public MetaDataKPITransportTotal MetaDataKPITransportTotal { get; set; }


    }
    public class MetaDataKPITransportTotal
    {
        public string from_to_date { get; set; }
      
    }


}