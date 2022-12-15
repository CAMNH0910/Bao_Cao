using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{
    //Lấy mã bưu cục phát 
    public class GetProvince
    {
        public int ma_tinh { get; set; }

        public string ten_tinh { get; set; }
        
    }
    //Lấy mã tuyết phát
   
    public class PARAMETERKH
    {
        public int StartProvince { get; set; }
        public int EndProvince { get; set; }
        public int Isservice { get; set; }
        public string CustomerCode { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }

    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class QualityDeliveryDetailKH
    {
        public int STT { get; set; }
        public String StartProvince { get; set; }
        public String EndProvince { get; set; }
        public String StartProvinceName { get; set; }
        //public int BuuCuc { get; set; }
        public String EndProvinceName { get; set; }
        public int C17total { get; set; }
        //public int Total { get; set; }
        public int J1 { get; set; }
        public Decimal TLJ1 { get; set; }              
        public int J2 { get; set; }
        public Decimal TLJ2 { get; set; }
        public int J25 { get; set; }
        public Decimal TLJ25 { get; set; }
        public int J3 { get; set; }
        public Decimal TLJ3 { get; set; }
        public int J35 { get; set; }
        public Decimal TLJ35 { get; set; }
        public int J4 { get; set; }
        public Decimal TLJ4 { get; set; }
        public int J5 { get; set; }
        public Decimal TLJ5 { get; set; }
        public int TotalKTT { get; set; }
        public Decimal TLKTT { get; set; }


    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
    public class QualityDeliverySuccessNotItemKH
    {
        public String ItemCode { get; set; }
        public String CustomerCode { get; set; }
        public int StartPostcode { get; set; }
        public String StartPostcodeName { get; set; }
        public int EndPostcode { get; set; }
        public String EndPostcodeName { get; set; }
        public String StatusDate { get; set; }
        public String StatusTime { get; set; }
        public String ServiceTypeNumber { get; set; }
      
    }

    public class ReturnQualityKH
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>

        public int Total { get; set; }

        public QualityDeliveryDetailKH QualityDeliveryKHReport { get; set; }
        public List<QualityDeliveryDetailKH> ListQualityDeliveryKHReport;

        public QualityDeliverySuccessNotItemKH QualityDeliverySuccessNotItemKH { get; set; }
        public List<QualityDeliverySuccessNotItemKH> ListQualityDeliverySuccessNotItemReport;

        public MetaDataKH MetaData1 { get; set; }


    }
    public class MetaDataKH
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }


}