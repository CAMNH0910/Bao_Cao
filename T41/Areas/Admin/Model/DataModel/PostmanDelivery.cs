using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{
    
    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class PostmanDeliveryDetail
    {
        public int STT { get; set; }       
        public String BuuCuc { get; set; }
        public String TenBuuCuc { get; set; }
        public String TuyenPhat { get; set; }
        public String TenTuyenPhat { get; set; }
        public String IDBuuTa { get; set; }
        public String TenBuuTa { get; set; }
        public String DichVu { get; set; }
        public int TongSL { get; set; }
        public int SanLuongPTC { get; set; }
      
        public int SanLuongPTC5H { get; set; }

        public int TOTAL5KG { get; set; }
        public Decimal TyLeTrong5H { get; set; }
        public int SanLuongPTCQUA5H { get; set; }
        public int TOTALNOT5KG { get; set; }
        public Decimal TyLeQua5H { get; set; }
       
        public int SanLuongKTT { get; set; }
      

       


    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
    public class PostmanItemDeliverySuccess5HDetail
    {
        public String ItemCode { get; set; }
        public String StartPostcode { get; set; }
        public String StartPostcodeName { get; set; }
        public String RouteCode { get; set; }
        public String RouteCodeName { get; set; }
        public String PostmanID { get; set; }
        public String PostManName { get; set; }
        public String C17StatusDate { get; set; }
        public String C17StatusTime { get; set; }
        public String StatusDate { get; set; }
        public String StatusTime { get; set; }
        public String ServiceTypeNumber { get; set; }
        public String Timeinterval { get; set; }
      

    }

    public class NotPostmanKPIDetail
    {
        public int STT { get; set; }
        public String ItemCode { get; set; }
        public String RouteCode { get; set; }
        public String RouteCodeName { get; set; }
        public String StartPostCode  { get; set; }
        public String StartPostcodeName { get; set; }
        public String PostmanID { get; set; }
        public String PostManName { get; set; }
        public String StatusDate { get; set; }
        public String StatusTime { get; set; }
        public String servicetypenumber { get; set; }
    }

    public class NotPODKPIDetail
    {
        public int STT { get; set; }
        public String ItemCode { get; set; }
        public String RouteCode { get; set; }
        public String RouteCodeName { get; set; }
        public String StartPostCode { get; set; }
        public String StartPostcodeName { get; set; }
        public String PostmanID { get; set; }
        public String PostManName { get; set; }
        public String StatusDate { get; set; }
        public String StatusTime { get; set; }
        public String servicetypenumber { get; set; }
    }

    public class ReturnPostman
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

        public PostmanDeliveryDetail PostmanDeliveryReport { get; set; }
        public List<PostmanDeliveryDetail> ListPostmanDeliveryReport;

        public PostmanItemDeliverySuccess5HDetail PostmanDeliverySuccess5HReport { get; set; }
        public List<PostmanItemDeliverySuccess5HDetail> ListPostmanDeliverySuccess5HReport;

        public NotPostmanKPIDetail NotPostmanKPIDetailReport { get; set; }
        public List<NotPostmanKPIDetail> ListNotPostmanKPIDetailReport;

        public NotPODKPIDetail NotPODKPIDetailReport { get; set; }
        public List<NotPODKPIDetail> ListNotPODKPIDetailReport;



    }
    


}