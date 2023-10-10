using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{
    //Lấy mã bưu cục phát 
    public class GETDONVI
    {

        public int STT { get; set; }
        public string DONVI { get; set; }

    }
    //Lấy mã tuyết phát
    public class GETPHANLOAIDV
    {
        public int STT { get; set; }

        public string PHANLOAI { get; set; }

    }

    public class GETLISTDV
    {
        public string MADV { get; set; }

        public string TENDV { get; set; }

    }
    public class GETLISTDVCT
    {
        public string IDDICHVU { get; set; }

        public string MADICHVU { get; set; }

        public string TENDICHVU { get; set; }

    }
    public class GETLISTTINH
    {
        public string PROVINCECODE { get; set; }

        public string PROVINCENAME { get; set; }

    }
    public class GETLISTMANUOC
    {
        public string MA_NUOC { get; set; }

        public string TEN_NUOC { get; set; }

    }

    public class GETLISTMANUOCOE
    {
        public string STT { get; set; }

        public string PHANLOAI { get; set; }

    }
    public class GETLISTHUYEN
    {
        public string DISTRICTCODE { get; set; }

        public string DISTRICTNAME { get; set; }

    }
    public class PARAMETERTHDV
    {
        public int DONVI { get; set; }
        public int PHANLOAI { get; set; }
        public String DICHVU { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    public class PARAMETERTHDONVI
    {
        public int DONVI { get; set; }
        public int MATINH { get; set; }
        public int PHANLOAI { get; set; }
        public String DICHVU { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }

    public class PARAMETERTHDONVIDICHVU
    {
        public int DONVI { get; set; }
        public int MATINH { get; set; }
        public int PHANLOAI { get; set; }
        public String DICHVU { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    public class PARAMETERTHTINHTINH
    {
        public int DONVI { get; set; }
        public int TINHNHAN { get; set; }
        public int TINHTRA { get; set; }
        public String DICHVU { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }

    public class PARAMETERTHHUYENHUYEN
    {
        public int DONVI { get; set; }
        public int TINHNHAN { get; set; }
        public int HUYENNHAN { get; set; }
        public int TINHTRA { get; set; }
        public int HUYENTRA { get; set; }
        public String DICHVU { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    public class PARAMETERTHQTDI
    {
        public int MADV { get; set; }
        public int DONVI { get; set; }
        public int MANC { get; set; }
        public int PHANLOAI { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    public class PARAMETERTHE1TN
    {
        public int DONVI { get; set; }
        public int TINHNHAN { get; set; }
        public int MADV { get; set; }
        public int KHACHHANG { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    public class PARAMETERTHQTDEN
    {
        public int NUOCOE { get; set; }

        public int MANC { get; set; }
        public int PHANLOAI { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    public class PARAMETERCTLO
    {
        public int DONVI { get; set; }
        public String TINHNHAN { get; set; }
        public String TINHTRA { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }

    public class PARAMETERTHCH
    {
        public int DONVI { get; set; }
        public int TINHNHAN { get; set; }
        public int TINHTRA { get; set; }
        public String DICHVU { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class QualityReportKDDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        //public int BuuCuc { get; set; }
        public String PhanloaiDV { get; set; }
        public String Madvchinh { get; set; }
        public String TenDV { get; set; }
        public String SL { get; set; }
        public String KL { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }
    public class QualityReportKDDetailItem
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String NgayPhatHanh { get; set; }
        public String Madvchinh { get; set; }
        public String TenDV { get; set; }
        public String Mae1 { get; set; }

        public String MaKH { get; set; }

        public String TenNguoiGui { get; set; }
        public String DiaChiGui { get; set; }
        public String TenNguoiNhan { get; set; }
        public String DiaChiNhan { get; set; }
        public String MaTinhTra { get; set; }
        public String Tentinhtra { get; set; }
        public String TrangThaiPhat { get; set; }
        public String khoiluong { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String Cuocdvct { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPKhac { get; set; }
        public String Tongcuoc { get; set; }




    }
    public class QualityReportDONVIDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String PhanloaiDV { get; set; }
        public String SL { get; set; }
        public String KL { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }

    public class QualityReportTINHTINHDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        public String Matinhtra { get; set; }
        public String Tentinhtra { get; set; }
        public String Madvchinh { get; set; }
        public String TenDV { get; set; }
        public String NacKL { get; set; }
        public String TenNacKL { get; set; }
        // public String NacKLNumber { get; set; }
        public String SL { get; set; }
        //public int BuuCuc { get; set; }
        public String KL { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }

    public class QualityReportTHCHDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        public String Matinhtra { get; set; }
        public String Tentinhtra { get; set; }
        public String Madvchinh { get; set; }
        public String TenDV { get; set; }
        public String NacKL { get; set; }
        public String TenNacKL { get; set; }
        // public String NacKLNumber { get; set; }
        public String SL { get; set; }
        //public int BuuCuc { get; set; }
        public String KL { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }

    public class QualityReportHUYENHUYENDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        public String Mahuyennhan { get; set; }
        public String Tenhuyennhan { get; set; }
        public String Matinhtra { get; set; }
        public String Tentinhtra { get; set; }

        public String Mahuyentra { get; set; }
        public String Tenhuyentra { get; set; }
        public String NacKL { get; set; }
        public String TenNacKL { get; set; }
        public String SL { get; set; }
        //public int BuuCuc { get; set; }
        public String KL { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }
    public class QualityReportDONVIDICHVUDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String PhanloaiDV { get; set; }
        public String Madvchinh { get; set; }
        public String TenDV { get; set; }
        public String SL { get; set; }
        public String KL { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }

    public class QualityReportTHQTDIDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String Mae1 { get; set; }
        public String MaKH { get; set; }
        public String Manuoctra { get; set; }
        public String Ten_Nuoc { get; set; }
        public String NgayPhatHanh { get; set; }

        public String Madvchinh { get; set; }
        //  public String TenDV { get; set; }
        public String PhanLoai { get; set; }
        public String khoiluong { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPMD { get; set; }
        public String PPHK { get; set; }
        public String PPKC { get; set; }
        public String PPKhac { get; set; }
        public String Tongcuoc { get; set; }

        public String TrangThaiPhat { get; set; }


    }
    public class QualityReportTHQTDIDetail_NEW
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String Mae1 { get; set; }
        public String MaKH { get; set; }
        public String Manuoctra { get; set; }
        public String Ten_Nuoc { get; set; }
        public String NgayPhatHanh { get; set; }

        public String Madvchinh { get; set; }
        //  public String TenDV { get; set; }
        public String PhanLoai { get; set; }
        public String khoiluong { get; set; }
        public String KLQD { get; set; }
        
        public String Tongcuoc { get; set; }

        public String TrangThaiPhat { get; set; }


    }
    public class QualityReportTHQTDENDetail
    {
        public int STT { get; set; }
        public String Mae1 { get; set; }
        public String Mancnhan { get; set; }
        public String Ten_Nuoc { get; set; }
        //public int BuuCuc { get; set; }
        public String Ngay_Den { get; set; }
        public String OE_Nhan { get; set; }
        public String PhanLoai { get; set; }
        public String Khoiluong { get; set; }

        public String TrangThaiPhat { get; set; }


    }



    public class QualityReportTHE1TNDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String NgayPhatHanh { get; set; }
        public String Madvchinh { get; set; }
        public String TenDV { get; set; }
        public String Mae1 { get; set; }
        public String MaKH { get; set; }
        public String TenNguoiGui { get; set; }
        public String DiaChiGui { get; set; }
        public String TenNguoiNhan { get; set; }
        public String DiaChiNhan { get; set; }
        public String MaTinhTra { get; set; }
        public String Tentinhtra { get; set; }
        public String TrangThaiPhat { get; set; }
        public String khoiluong { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String Cuocdvct { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPKhac { get; set; }
        public String Tongcuoc { get; set; }



    }
    public class QualityReportTHDVCTDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Madvct { get; set; }
        public String TenDV { get; set; }
        //public int BuuCuc { get; set; }
        public String SL { get; set; }
        public String KL { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }

        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }


    }

    public class QualityReportCTDVCTDetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String Matinhtra { get; set; }
        public String Tentinhtra { get; set; }
        public String Madvct { get; set; }
        public String TenDVCT { get; set; }
        public String Madvchinh { get; set; }
        public String TenDVChinh { get; set; }
        public String SL { get; set; }

        public String KL { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPMD { get; set; }
        public String Cuocdvct { get; set; }
        public String Tongcuoc { get; set; }
    }
    public class QualityReportCTLODetail
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Mae1 { get; set; }
        public String So_lo { get; set; }
        public String Ngayphathanh { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        public String Matinhtra { get; set; }
        public String Tentinhtra { get; set; }
        public String KL { get; set; }
        public String KLQD { get; set; }
        public String Cuocchinh { get; set; }
        public String Cuocdvct { get; set; }
        public String PPXD { get; set; }
        public String PPVX { get; set; }
        public String PPKC { get; set; }
        public String Tongcuoc { get; set; }
    }
    public class ReturnReportTHDV
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportKDDetail QualityReportKD { get; set; }
        public List<QualityReportKDDetail> ListQualityReportKDReport;

        public QualityReportKDDetailItem QualityReportKDItem { get; set; }
        public List<QualityReportKDDetailItem> ListDetailedItemQualityTHDVReport;

        public MetaDataTHDV MetaDataTHDV { get; set; }


    }

    public class ReturnReportTHDONVI
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportDONVIDetail QualityReportTHDONVI { get; set; }
        public List<QualityReportDONVIDetail> ListQualityReportTHDONVIReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataTHDONVI MetaDataTHDONVI { get; set; }


    }
    public class ReturnReportTINHTINH
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportTINHTINHDetail QualityReportTINHTINH { get; set; }
        public List<QualityReportTINHTINHDetail> ListDetailedQualityTHTINHTINHReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataTHTINHTINH MetaDataTINHTINH { get; set; }


    }

    public class ReturnReportTHCH
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportTHCHDetail QualityReportTHCH { get; set; }
        public List<QualityReportTHCHDetail> ListDetailedQualityTHCHReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataTHCH MetaDataTHCH { get; set; }


    }

    public class ReturnReportHUYENHUYEN
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportHUYENHUYENDetail QualityReportHUYENHUYEN { get; set; }
        public List<QualityReportHUYENHUYENDetail> ListDetailedQualityTHHUYENHUYENReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataTHHUYENHUYEN MetaDataHUYENHUYEN { get; set; }


    }

    public class ReturnReportQTDI
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportTHQTDIDetail QualityReportQTDI { get; set; }
        public List<QualityReportTHQTDIDetail> ListDetailedQualityTHQTDIReport;

       

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataQTDI MetaDataQTDI { get; set; }


    }
    public class ReturnReportQTDI_NEW
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
        public QualityReportTHQTDIDetail_NEW QualityReportQTDI_NEW { get; set; }
        public List<QualityReportTHQTDIDetail_NEW> ListDetailedQualityTHQTDIReport_NEW;



        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;



    }
    public class ReturnReportTHDONVIDICHVU
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportDONVIDICHVUDetail QualityReportTHDONVIDICHVU { get; set; }
        public List<QualityReportDONVIDICHVUDetail> ListQualityReportTHDONVIDICHVUReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataTHDONVIDICHVU MetaDataTHDONVIDICHVU { get; set; }


    }

    public class ReturnReportQTDEN
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportTHQTDENDetail QualityReportQTDEN { get; set; }
        public List<QualityReportTHQTDENDetail> ListDetailedQualityTHQTDENReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataQTDEN MetaDataQTDEN { get; set; }


    }

    public class ReturnReportE1TN
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportTHE1TNDetail QualityReportE1TN { get; set; }
        public List<QualityReportTHE1TNDetail> ListDetailedQualityTHE1TNReport;

        // public QualityDeliverySuccess6HDetail QualityDeliverySuccess6HReport { get; set; }
        //public List<QualityDeliverySuccess6HDetail> ListQualityDeliverySuccess6HReport;

        public MetaDataE1TN MetaDataE1TN { get; set; }


    }

    public class ReturnReportTHDVCT
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportTHDVCTDetail QualityReportTHDVCT { get; set; }
        public List<QualityReportTHDVCTDetail> ListDetailedQualityTHDVCTReport;



        public MetaDataTHDVCT MetaDataTHDVCT { get; set; }


    }

    public class ReturnReportCTDVCT
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportCTDVCTDetail QualityReportCTDVCT { get; set; }
        public List<QualityReportCTDVCTDetail> ListDetailedQualityCTDVCTReport;



        public MetaDataCTDVCT MetaDataCTDVCT { get; set; }


    }
    public class ReturnReportCTLO
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public QualityReportCTLODetail QualityReportCTLO { get; set; }
        public List<QualityReportCTLODetail> ListQualityCTLOReport;
        public MetaDataCTLO MetaDataCTLO { get; set; }
    }

    public class MetaDataTHDV
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }

    public class MetaDataTHDONVI
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataTHDONVIDICHVU
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataTHTINHTINH
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }

    public class MetaDataTHCH
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataTHHUYENHUYEN
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataQTDI
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataQTDEN
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataE1TN
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataTHDVCT
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataCTDVCT
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }
    public class MetaDataCTLO
    {
        public string from_to_date { get; set; }
        public string channel { get; set; }
        public string delivery_post_code { get; set; }
        public string delivery_route_code { get; set; }
        public string status { get; set; }
        public string postman { get; set; }
    }

}