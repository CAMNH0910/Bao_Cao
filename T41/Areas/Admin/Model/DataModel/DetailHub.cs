using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class ProvinceEms
    {
        public int ProvinceCode { get; set; }

        public string ProvinceName { get; set; }

    }


    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class TotalHub
    {
        public int STT { get; set; }
        public string StartProvince { get; set; }
        public string StartProvinceName { get; set; }
        public string IsStartDistrict { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string IsEndDistrict { get; set; }

        public string IsSDistrict { get; set; }

        public string IsEDistrict { get; set; }

        public string IsserviceNumber { get; set; }

        public string Isservice { get; set; }
        
        public string Targets { get; set; }
        public string Total { get; set; }
        public string SLSuccess { get; set; }
        public string TLSuccess { get; set; }
        public string IsFailStatus { get; set; }
        public string TLIsFailStatus { get; set; }
        //public int BuuCuc { get; set; }   
    }

    public class TotalHub_Excel
    {
        public int STT { get; set; }
        public string StartProvince { get; set; }
        public string StartProvinceName { get; set; }
        public string IsStartDistrict { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string IsEndDistrict { get; set; }

       // public string IsSDistrict { get; set; }

       // public string IsEDistrict { get; set; }

       // public string IsserviceNumber { get; set; }

        public string Isservice { get; set; }

        public string Targets { get; set; }
        public string Total { get; set; }
        public string SLSuccess { get; set; }
        public string TLSuccess { get; set; }
        public string IsFailStatus { get; set; }
        public string TLIsFailStatus { get; set; }
        //public int BuuCuc { get; set; }   
    }


    public class DetailHubFail
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string Servicetypechar { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostCodeName { get; set; }
        public string StartDistrict { get; set; }
        public string StartDistrictName { get; set; }
        public string StartProvince { get; set; }
        public string StartProvinceName { get; set; }
        public string A1statusdatetime { get; set; }
        public string EndPosCode { get; set; }
        public string EndPosCodeName { get; set; }
        public string EndDistrict { get; set; }
        public string EndDistrictName { get; set; }
        public string EndProvince { get; set; }
        public string EndProvinceName { get; set; }
        public string A2statusdatetime { get; set; }
        public string Timeinterval { get; set; }
        public string J { get; set; }
        public string IsFailstatus { get; set; }
    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
   
    public class ReturnDetailHub
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public TotalHub TotalHubReport { get; set; }
        public List<TotalHub> ListTotalHubReport;
        public TotalHub_Excel TotalHub_ExcelReport { get; set; }
        public List<TotalHub_Excel> ListTotalHub_ExcelReport;
        public DetailHubFail DetailHubFailReport { get; set; }
        public List<DetailHubFail> ListDetailHubFailReport;
        public MetaDataDetailHub MetaDataDetailHub { get; set; }


    }
    public class MetaDataDetailHub
    {
        public string from_to_date { get; set; }

    }


}