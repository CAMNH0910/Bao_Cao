using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
       
    //Phần lấy dữ liệu của bảng crm_sales_order
    public class ListCustomer_Detail
    {
        public int STT { get; set; }
        public String Ma_Tham_Chieu { get; set; }
        public String Ma_Van_Don { get; set; }
        public String Ma_bc_phat { get; set; }
        public String Nguoi_Nhan { get; set; }
        public String Dien_Thoai_Nhan { get; set; }
        public String Dia_Chi_Nhan { get; set; }
        public String Tinh_Nhan { get; set; }

        public String Trang_Thai_Hien_Tai { get; set; }

        public String Trang_Thai_1 { get; set; }
        public String Ghi_chu_1 { get; set; }
        public String Ngay_giao_1 { get; set; }
        public String Gio_Giao_1 { get; set; }

        public String Trang_Thai_2 { get; set; }
        public String Ghi_chu_2 { get; set; }
        public String Ngay_giao_2 { get; set; }
        public String Gio_Giao_2 { get; set; }

        public String Trang_Thai_3 { get; set; }
        public String Ghi_chu_3 { get; set; }
        public String Ngay_giao_3 { get; set; }
        public String Gio_Giao_3 { get; set; }

        public String Trang_Thai_4 { get; set; }
        public String Ghi_chu_4 { get; set; }
        public String Ngay_giao_4 { get; set; }
        public String Gio_Giao_4 { get; set; }

        public String Lan_Phat { get; set; }

        //public String PICKUP_FULL_ADDRESS { get; set; }

    }

    
    public class ReturnListCustomer
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

        public List<ListCustomer_Detail> ListCustomer_Report_Detail;

        


    }
}