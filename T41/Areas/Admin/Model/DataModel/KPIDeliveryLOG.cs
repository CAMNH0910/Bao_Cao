using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class PoscodeEMS
    {
        public int Poscode { get; set; }

        public string Poscodename { get; set; }

    }

    public class Detail_Item_Ems_TRUOT
    {
        public string Itemcode { get; set; }
        public string StartPostcode { get; set; }
        public string StartPostcodeName { get; set; }
        public string POSTMANID { get; set; }
        public string PostmanName { get; set; }
        public string C17StatusDate { get; set; }
        public string C17StatusTime { get; set; }
        public string StatusDate { get; set; }
        public string StatusTime { get; set; }
        public string ServiceTypeNumber { get; set; }
    }

    public class Detail_Item_Ems_KTT
    {
        public string Itemcode { get; set; }
        public string StartPostcode { get; set; }
        public string StartPostcodeName { get; set; }
        public string POSTMANID { get; set; }
        public string PostmanName { get; set; }
        public string StatusDate { get; set; }
        public string StatusTime { get; set; }
        public string ServiceTypeNumber { get; set; }
    }


    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class Detail_PLD_XND
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostcodeName { get; set; }
        public string PostmanId { get; set; }
        public string PostmanName { get; set; }
        public string ServiceTypeNumber { get; set; }
        public string TongSL { get; set; }
        public string SanLuongPTC { get; set; }
        public string TyLePTC { get; set; }
        public string SanLuongDat { get; set; }

        public string TyLeDat { get; set; }

        public string SanLuongTruot { get; set; }

        public string TyLeTruot { get; set; }
        
        public string SanLuongKTT { get; set; }
        public string TyLeKTT { get; set; }
    }

    public class ReturnDetail_Item_Ems_TRUOT
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string NameBC { get; set; }
        public Detail_Item_Ems_TRUOT Detail_Item_Ems_TRUOTReport { get; set; }
        public List<Detail_Item_Ems_TRUOT> ListDetail_Item_Ems_TRUOT;


    }

    public class ReturnDetail_Item_Ems_KTT
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string NameBC { get; set; }
        public Detail_Item_Ems_KTT Detail_Item_Ems_KTTReport { get; set; }
        public List<Detail_Item_Ems_KTT> ListDetail_Item_Ems_KTT;


    }
    public class Detail_PTC_XND
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostcodeName { get; set; }
        public string PostmanId { get; set; }
        public string PostmanName { get; set; }
        public string ServiceTypeNumber { get; set; }
        public string TongSL { get; set; }
        public string SanLuongPTC { get; set; }
        public string TyLePTC { get; set; }
        public string SanLuongDat { get; set; }

        public string TyLeDat { get; set; }

        public string SanLuongTruot { get; set; }

        public string TyLeTruot { get; set; }

        public string SanLuongKTT { get; set; }
        public string TyLeKTT { get; set; }

    }

    public class Detail_PLD_BT
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostcodeName { get; set; }
        public string PostmanId { get; set; }
        public string PostmanName { get; set; }
        public string ServiceTypeNumber { get; set; }
        public string TongSL { get; set; }
        public string SanLuongPTC { get; set; }
        public string TyLePTC { get; set; }
        public string SanLuongDat { get; set; }

        public string TyLeDat { get; set; }

        public string SanLuongTruot { get; set; }

        public string TyLeTruot { get; set; }

        public string SanLuongKTT { get; set; }
        public string TyLeKTT { get; set; }

    }

    public class Detail_PTC_BT
    {
        public int STT { get; set; }
        public string StartPostCode { get; set; }
        public string StartPostcodeName { get; set; }
        public string PostmanId { get; set; }
        public string PostmanName { get; set; }
        public string ServiceTypeNumber { get; set; }
        public string TongSL { get; set; }
        public string SanLuongPTC { get; set; }
        public string TyLePTC { get; set; }
        public string SanLuongDat { get; set; }

        public string TyLeDat { get; set; }

        public string SanLuongTruot { get; set; }

        public string TyLeTruot { get; set; }

        public string SanLuongKTT { get; set; }
        public string TyLeKTT { get; set; }

    }


    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H

    public class ReturnKPIDeliveryLOG
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public Detail_PLD_XND Detail_PLD_XNDReport { get; set; }
        public List<Detail_PLD_XND> ListDetail_PLD_XNDReport;

        public Detail_PTC_XND Detail_PTC_XNDReport { get; set; }
        public List<Detail_PTC_XND> ListDetail_PTC_XNDReport;

        public Detail_PLD_BT Detail_PLD_BTReport { get; set; }
        public List<Detail_PLD_BT> ListDetail_PLD_BTReport;

        public Detail_PTC_BT Detail_PTC_BTReport { get; set; }
        public List<Detail_PTC_BT> ListDetail_PTC_BTReport;


    }



}