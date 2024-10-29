using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{

    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class KpiBD13DeliveryDetail
    {
        public int STT { get; set; }
        public String KhuVuc { get; set; }
        //public int BuuCuc { get; set; }
        public String BuuCuc { get; set; }
        public String TenBuuCuc { get; set; }
        public int TongSL { get; set; }
        public int SanLuongPTC { get; set; }
        public Decimal TLPTC { get; set; }
        public int TC72 { get; set; }
        public Decimal TLPTC72 { get; set; }

        public int TC48 { get; set; }
        public Decimal TLPTC48 { get; set; }
        public int SanLuongKTT { get; set; }
        public int SanLuongPTC6H { get; set; }
        public int SanLuongPTCQUA6H { get; set; }
        public Decimal TyLeTrong6H { get; set; }
        public Decimal TyLeQua6H { get; set; }
        public int TCKXD { get; set; }

    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
    public class KpiBD13DeliverySuccess6HDetail
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

    public class ReturnKpiBD13
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

        public KpiBD13DeliveryDetail KpiBD13DeliveryReport { get; set; }
        public List<KpiBD13DeliveryDetail> ListKpiBD13DeliveryReport;

        public KpiBD13DeliverySuccess6HDetail KpiBD13DeliverySuccess6HReport { get; set; }
        public List<KpiBD13DeliverySuccess6HDetail> ListKpiBD13DeliverySuccess6HReport;



    }



}