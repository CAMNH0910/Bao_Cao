using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class GetPosCodeGD
    {
        public int PosCode { get; set; }

        public string PosName { get; set; }

    }
   
    public class PARAMETERKPIPickup
    {
        public string Zone { get; set; }
        public string PosCode { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }

    }
    
    //Lấy chi tiết tổng hợp
    public class KPIPickupTotal
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string Total { get; set; }
        public string TimeintervalUpTo6 { get; set; }
        public string TLTimeintervalUpTo6 { get; set; }
        public string TimeintervalUpTo10 { get; set; }
        public string TLTimeintervalUpTo10 { get; set; }
        public string TimeintervalExceed10 { get; set; }
        public string TLTimeintervalExceed10 { get; set; }
        public string TimeintervalUpTo3GD { get; set; }
        public string TimeintervalUpTo3VC { get; set; }

    }

    public class ReturnKPIPickup
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIPickupTotal KPIPickupReport { get; set; }
        public List<KPIPickupTotal> ListKPIPickupReport;
        public MetaDataKPIPickup MetaDataKPIPickup { get; set; }

    }

    //Lấy ra chi tiết
    public class KPIPickupItem
    {
        public int STT { get; set; }
        public string ItemCode { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string A1StatusDate { get; set; }
        public string A1StatusTime { get; set; }
        public string PosCode { get; set; }
        public string PosName { get; set; }
        public string A2StatusDate { get; set; }
        public string A2StatusTime { get; set; }
        public string A3StatusDate { get; set; }
        public string A3StatusTime { get; set; }
        public string TimeintervalGD { get; set; }
        public string TimeintervalVC { get; set; }
        public string Timeinterval { get; set; }
    
}
    public class ReturnKPIPickupItem
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public KPIPickupItem KPIPickupItemReport { get; set; }
        public List<KPIPickupItem> ListKPIPickupItemReport;
       
    }

    public class MetaDataKPIPickup
    {
        public string from_to_date { get; set; }

    }

}