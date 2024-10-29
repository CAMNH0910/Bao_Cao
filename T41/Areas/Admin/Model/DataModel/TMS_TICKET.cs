using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class TMS_TICKET
    {
        public int STT { get; set; }
        public string ID { get; set; }
        public string Ten_NV { get; set; }
        public string Phan_User { get; set; }
        public string Phan_Tinh { get; set; }
        public string Nhom_Tinh { get; set; }
        public string Ghi_Chu { get; set; }
        public string isdelete { get; set; }
    }
    public class LIST_TICKET
    {
        public int STT { get; set; }
        public string TTK_CODE { get; set; }
        public string TTK_TYPE { get; set; }
        public string PARCEL_ID { get; set; }
        public string TTK_STATUS { get; set; }
        public string TTK_EXPIRATION { get; set; } // Use nullable if it can be null
        public string NEXT_ORG { get; set; }
        public string NEXT_ORG_NAME { get; set; }
        public string MANAGED_ORG { get; set; }
        public string REF_ORG { get; set; }
        public string REF_ORG_NAME { get; set; }
        public string CREATED_DATE { get; set; } // Use nullable if it can be null
        public string CREATED_ORG { get; set; }
        public string ACT_CONTENT { get; set; }
        public string PHAN_TINH { get; set; }
        public string TEN_NV { get; set; }
    }

    public class Get_User
    {
        public string Nhom_Tinh { get; set; }
        public string Ten_Nhom_Tinh { get; set; }

    }

    public class Get_Nhom_Tinh
    {
        public string ID { get; set; }
        public string TEN_NV { get; set; }

    }

    public class Get_Nhan_Vien
    {
        public string ID { get; set; }
        public string TEN_NV { get; set; }

    }

    public class ReturnTMS_TICKET
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

        public TMS_TICKET TMS_TICKET { get; set; }
        public List<TMS_TICKET> List_TMS_TICKET;

        public LIST_TICKET LIST_TICKET { get; set; }
        public List<LIST_TICKET> ListTICKET;


        public MetaData MetaData { get; set; }

    }
}