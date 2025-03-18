using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class TMS_TICKET
    {
        public int STT { get; set; }
        public string Id { get; set; }
        public string MA_NV { get; set; }
        public string Ten_NV { get; set; }
        public string Phan_User { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
        public string Khu_Vuc { get; set; }
        public string Nhom_Tinh { get; set; }
        public string Ngay_Khoi_Tao { get; set; }
        public string Ngay_Ket_Thuc { get; set; }
        public string Ghi_Chu { get; set; }
        public string isdelete { get; set; }
    }
    public class DM_Nhan_Vien
    {
        public int STT { get; set; }
        public string Phan_User { get; set; }
        public string Khu_Vuc { get; set; }
        public string MA_NV { get; set; }
        public string Ten_NV { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
       
    }

    public class List_Ra_Soat
    {
        public int STT { get; set; }
        public string LUOT { get; set; }
        public string Ngay { get; set; }
        public string Thoi_Gian { get; set; }
        public string TimeImport { get; set; }
        public string Tong_So { get; set; }
        public string So_HS { get; set; }
        public string USERS { get; set; }
        public string USERS_IMPORT { get; set; }
        
    }
    public class DM_Ra_Soat
    {
        public int STT { get; set; }
        public string ID { get; set; }
        public string LUOT { get; set; }
        public string Thoi_Gian { get; set; }
        public string Khoang_TG { get; set; }

    }
    public class DM_Ra_Soat_Sua
    {
        public int STT { get; set; }
        public string Id { get; set; }
        public string LUOT { get; set; }
        public string THOI_GIAN { get; set; }
        public string TU_GIO { get; set; }
        public string DEN_GIO { get; set; }
        public string Isdelete { get; set; }
    }

    public class TMS_TICKET_Sua
    {
        public int STT { get; set; }
        public string Id { get; set; }
        public string Ten_NV { get; set; }
        public string MA_NV { get; set; }
        public string Phan_User { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
        public string Khu_Vuc { get; set; }
        public string Nhom_Tinh { get; set; }
        public string Ngay_Khoi_Tao { get; set; }
        public string Ngay_Ket_Thuc { get; set; }
        public string Ghi_Chu { get; set; }
    }

    public class Lay_Itemcode
    {
        public int STT { get; set; }
        public string So_HS { get; set; }
        public string Ma_BG { get; set; }
        public string Trang_Thai { get; set; }
        public string ID_NV { get; set; }
        public string Ten_NV { get; set; }
    }
    public class LIST_TICKET
    {
        public int STT { get; set; }
        public string So_HS { get; set; }
        public string Ma_BG { get; set; }
        public string Ngay_Tao { get; set; }
        public string Trang_Thai { get; set; }
        public string Ma_DV_Chu_Tri { get; set; }
        public string Ten_DV_Chu_Tri { get; set; }
        public string Ngay_Het_han { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
        public string TEN_NV { get; set; }
        public string STATUS { get; set; }
        public string STATUS_HS { get; set; }
        public string Luot { get; set; }
        public string USERS { get; set; }
    }
    
    public class LIST_TICKET_TH {
        public int STT { get; set; }
        public string Khu_Vuc { get; set; }
        public string Nhom_Tinh { get; set; }
        public string Id { get; set; }
        public string Ten_NV { get; set; }
        public string Tong_So { get; set; }
        public string DXL { get; set; }
        public string DCKQ { get; set; }
        public string HS_Moi { get; set; }
        public string HS_Cu { get; set; }
        public string Dong { get; set; }

    }

    public class LIST_TICKET_ND
    {
        public int STT { get; set; }
        public string So_HS { get; set; }
        public string Ma_BG { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
        public string Ngay_Tao { get; set; }
        public string Trang_Thai { get; set; }
        public string Ma_DV_Chu_Tri { get; set; }
        public string Ngay_Het_han { get; set; }
        public string NGAY_XL_CUOI { get; set; }
        public string TEN_NV { get; set; }
        public string UPDATE_TT { get; set; }
        public string UPDATE_DV { get; set; }
        public string STATUS { get; set; }
        public string STATUS_HS { get; set; }
        public string TT_CN { get; set; }
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

    public class QL_PhanCong_HT
    {
        public int STT { get; set; }
        public string Id { get; set; }
        public string NGAY { get; set; }
        public string HINH_THUC { get; set; }
        public string CA_LAM { get; set; }
        public string NOI_DUNG { get; set; }
        public string PHE_DUYET { get; set; }
        public string NGUOI_TAO { get; set; }
        public string DATECREATE { get; set; }
        public string NGUOI_DUYET { get; set; }
        public string DATEUPDATE { get; set; }

    }
    public class PhanCong_Xin_Nghi
    {
        public string NGAY { get; set; }
        public string HINH_THUC { get; set; }
        public string CA_LAM { get; set; }
        public string NOI_DUNG { get; set; }
    }

    public class THPhanCong_HT
    {
        public int STT { get; set; }
        public string Id { get; set; }
        public string NGUOI_TAO { get; set; }
        public string So_Lan { get; set; }
        public string PHE_DUYET { get; set; }
        public string NGUOI_DUYET { get; set; }
        public string DATEUPDATE { get; set; }
    }

    public class PhanCong_HT
    {
        public int STT { get; set; }
        public string SO_HS { get; set; }
        public string MA_BG { get; set; }
        public string ID_NV { get; set; }
    }

    public class TK_TICKET_UpdaDate
    {
        public int STT { get; set; }
        public string MA_NV { get; set; }
        public string TEN_NV { get; set; }
        public string SO_HS { get; set; }
        public string MA_TT { get; set; }
        public string DATEUPDATE { get; set; }
        public string USERS_UPDATE { get; set; }
    }
    public class TH_TICKET_TK
    {
        public int STT { get; set; }
        public string Khu_Vuc { get; set; }
        public string Nhom_Tinh { get; set; }
        public string Id { get; set; }
        public string Ten_NV { get; set; }
        public string Tong_So { get; set; }
        public string DXL { get; set; }
        public string DCKQ { get; set; }
        public string Dong { get; set; }
        public string DHT { get; set; }
    }
    public class CT_TICKET_TK
    {
        public int STT { get; set; }
        public string So_HS { get; set; }
        public string Ma_BG { get; set; }
        public string Ngay_Tao { get; set; }
        public string Trang_Thai { get; set; }
        public string Ma_DV_Chu_Tri { get; set; }
        public string Ngay_Het_han { get; set; }
        public string Tinh_Nhan { get; set; }
        public string Tinh_Tra { get; set; }
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
        public DM_Nhan_Vien DM_Nhan_Vien { get; set; }
        public List<DM_Nhan_Vien> ListDM_Nhan_Vien;

        
        public List_Ra_Soat List_Ra_Soat { get; set; }
        public List<List_Ra_Soat> ListL_Ra_Soat;
        public DM_Ra_Soat DM_Ra_Soat { get; set; }
        public List<DM_Ra_Soat> ListDM_Ra_Soat;
        
        public DM_Ra_Soat_Sua DM_Ra_Soat_Sua { get; set; }
        public List<DM_Ra_Soat_Sua> ListDM_Ra_Soat_Sua;
        
        public TMS_TICKET_Sua TMS_TICKET_Sua { get; set; }
        public List<TMS_TICKET_Sua> ListTMS_TICKET_Sua;

        public LIST_TICKET LIST_TICKET { get; set; }
        public List<LIST_TICKET> ListTICKET;


        public Lay_Itemcode Lay_Itemcode { get; set; }
        public List<Lay_Itemcode> ListLay_Itemcode;

        public LIST_TICKET_TH LIST_TICKET_TH { get; set; }
        public List<LIST_TICKET_TH> List_TICKET_TH;
        public LIST_TICKET_ND LIST_TICKET_ND { get; set; }
        public List<LIST_TICKET_ND> List_TICKET_ND;

        public QL_PhanCong_HT QL_PhanCong_HT { get; set; }
        public List<QL_PhanCong_HT> ListQL_PhanCong_HT;

        public THPhanCong_HT THPhanCong_HT { get; set; }
        public List<THPhanCong_HT> ListTHPhanCong_HT;

        public PhanCong_HT PhanCong_HT { get; set; }
        public List<PhanCong_HT> ListPhanCong_HT;

        public TK_TICKET_UpdaDate TK_TICKET_UpdaDate { get; set; }
        public List<TK_TICKET_UpdaDate> ListTK_TICKET_UpdaDate;

        public TH_TICKET_TK TH_TICKET_TK { get; set; }
        public List<TH_TICKET_TK> ListTH_TICKET_TK;
        public CT_TICKET_TK CT_TICKET_TK { get; set; }
        public List<CT_TICKET_TK> ListCT_TICKET_TK;
        

        public MetaData MetaData { get; set; }

    }
}