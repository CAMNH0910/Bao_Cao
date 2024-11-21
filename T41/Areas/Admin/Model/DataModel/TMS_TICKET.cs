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
        public string So_HS { get; set; }
        public string So_Phieu_KN { get; set; }
        public string Ma_BG { get; set; }
        public string Dich_Vu_SD { get; set; }
        public string Ma_DV_TN { get; set; } // Use nullable if it can be null
        public string Ten_DV_TN { get; set; }
        public string Loai_KN { get; set; }
        public string Ngay_Tao { get; set; }
        public string Trang_Thai { get; set; }
        public string Noi_Dung { get; set; }
        public string Nguoi_KN { get; set; } // Use nullable if it can be null
        public string Dia_Chi { get; set; }
        public string Dien_Thoai { get; set; }
        public string Email { get; set; }
        public string Ma_DV_Chu_Tri { get; set; }
        public string Ten_DV_Chu_Tri { get; set; }
        public string Ngay_XL_Cuoi { get; set; }
        public string Ngay_Het_han { get; set; }
        public string Dich_Vu { get; set; }
        public string Ly_Do { get; set; }
        public string Hinh_Thuc { get; set; }
        public string Ket_Qua { get; set; }
        public string Ngay_Dong { get; set; }
        public string So_Tien { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
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