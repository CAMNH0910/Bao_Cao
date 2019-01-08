using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    
    //Parameter truyền vào DB để xem chi tiết BD13 đi
    public class PARAMETER_LADING_TO_POSTMAN
    {
        public int PAGE_INDEX { get; set; }
        public int PAGE_SIZE { get; set; }
        public int MABC_KT { get; set; }
        public int MABC { get; set; }
        public string NGAY { get; set; }
        public int CAKT { get; set; }
        
    }
    

    //Phần lấy dữ iệu của bảng business_profile
    public class LADING_TO_POSTMAN_DETAIL
    {
        public String MAE1 { get; set; }
        public String MABCTRA { get; set; }
        public String MABCNHAN { get; set; }
        public String KHOILUONG { get; set; }
        public String CUOCCS { get; set; }
        public String CUOCDV { get; set; }
        public String TRANGTHAI { get; set; }
        public String DIACHI { get; set; }
        public String MABC { get; set; }
        public String CHTHU { get; set; }
        public String TUISO { get; set; }
        public String NGAY { get; set; }
        public String MABC_KT { get; set; }
        public String AMND_DATE { get; set; }


    }

    public class ReturnLADING
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
        public int Total { get; set; }

        public LADING_TO_POSTMAN_DETAIL LADING_TO_POSTMAN_Report { get; set; }
        public List<LADING_TO_POSTMAN_DETAIL> List_LADING_TO_POSTMAN_Report;
        
        public MetaData MetaData { get; set; }


    }
}