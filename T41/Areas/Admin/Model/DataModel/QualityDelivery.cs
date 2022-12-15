using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{
    //Lấy mã bưu cục phát 
    public class GETPOSCODE
    {
        public int POSCODE { get; set; }

        public string POSNAME { get; set; }
        
    }
    //Lấy mã tuyết phát
    public class GETROUTECODE
    {
        public int POSCODE { get; set; }

        public string POSNAME { get; set; }

    }

    public class GETPOSTMAN
    {
        public string POSTMAN_ID { get; set; }

        public string POSTMAN_NAME { get; set; }

    }
    public class PARAMETER
    {
        public int endpostcode { get; set; }
        public int routecode { get; set; }
        public int zone { get; set; }
        public int service { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }

    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class QualityDeliveryDetail
    {
        public int STT { get; set; }
        public String KhuVuc { get; set; }
        //public int BuuCuc { get; set; }
        public String BuuCuc { get; set; }
        public String TenBuuCuc { get; set; }
        public int TongSL { get; set; }
        public Decimal TylePTC { get; set; }
        public int SanLuongPTC { get; set; }
        public int SanLuongKTT { get; set; }
        public int SanLuongPTC6H { get; set; }
        public int SanLuongPTCQUA6H { get; set; }
        public Decimal TyLeTrong6H { get; set; }
        public Decimal TyLeQua6H { get; set; }
        public int TCKXD { get; set; }
        
    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
    public class QualityDeliverySuccess6HDetail
    {
        public String ItemCode { get; set; }
        public int EndPostCode { get; set; }
        public int RouteCode { get; set; }
        public String StatusDate { get; set; }
        public String C17StatusDate { get; set; }
        public String StatusTime { get; set; }
        public String C17StatusTime { get; set; }
        public String TimeInterval { get; set; }
       
    }

    public class ReturnQuality
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

        public QualityDeliveryDetail QualityDeliveryReport { get; set; }
        public List<QualityDeliveryDetail> ListQualityDeliveryReport;

        public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaData1 MetaData1 { get; set; }


    }
    public class MetaData1
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }


}