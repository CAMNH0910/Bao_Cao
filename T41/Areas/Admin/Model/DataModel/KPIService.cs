using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class GetStartProvince
    {
        public int StartProvince { get; set; }

        public string StartProvinceName { get; set; }

    }

    public class GetEndProvince
    {
        public int EndProvince { get; set; }

        public string EndProvinceName { get; set; }

    }


    public class PARAMETERKPIService
    {
        public string StartProvince { get; set; }
        public string EndProvince { get; set; }
        public bool IsService { get; set; }
        public bool IsCod { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }

    }



    //Lấy chi tiết tổng hợp các tỉnh
    public class KPIServiceDetail
    {
        public int STT { get; set; }
        public String StartProvince { get; set; }
        public String StartProvinceName { get; set; }
        public String EndProvince { get; set; }
        public String EndProvinceName { get; set; }
        public String Total { get; set; }
        public String J0 { get; set; }
        public String TLJ0 { get; set; }
        public String J05 { get; set; }
        public String TLJ05 { get; set; }
        public String J1 { get; set; }
        public String TLJ1 { get; set; }
        public String J15 { get; set; }
        public String TLJ15 { get; set; }
        public String J2 { get; set; }
        public String TLJ2 { get; set; }
        public String J25 { get; set; }
        public String TLJ25 { get; set; }
        public String J3 { get; set; }
        public String TLJ3 { get; set; }
        public String J35 { get; set; }
        public String TLJ35 { get; set; }
        public String J4 { get; set; }
        public String TLJ4 { get; set; }
        public String J5 { get; set; }
        public String TLJ5 { get; set; }

    }
   
    //Lấy ra chi tiết 1 tỉnh
    public class KPIServiceItemDetail
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string StartPostCode { get; set; }
        public string StartDistrict { get; set; }
        public string StartProvince { get; set; }
        public string EndPostCode { get; set; }
        public string EndDistrict { get; set; }
        public string EndProvince { get; set; }
        public string ServiceTypeNumber { get; set; }
        public string IsCod { get; set; }
        public string A1StatusDateTime { get; set; }
        public string A2StatusDateTime { get; set; }
        public string A3StatusDateTime { get; set; }
        public string A4StatusDateTime { get; set; }
        public string A5StatusDateTime { get; set; }
        public string A6StatusDateTime { get; set; }
        public string A7StatusDateTime { get; set; }
        public string J { get; set; }


    }

    public class ReturnKPIServiceItem
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIServiceItemDetail KPIServiceItemReport { get; set; }
        public List<KPIServiceItemDetail> ListKPIServiceItemReport;
        public MetaDataKPIService MetaDataKPIService { get; set; }

    }

    public class ReturnKPIService
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIServiceDetail KPIServiceReport { get; set; }
        public List<KPIServiceDetail> ListKPIServiceReport;
        public MetaDataKPIService MetaDataKPIService { get; set; }

    }
    public class MetaDataKPIService
    {
        public string from_to_date { get; set; }

    }

}