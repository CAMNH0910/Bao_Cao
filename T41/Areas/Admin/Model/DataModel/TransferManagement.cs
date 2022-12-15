using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER
    {
        public int fromprovince { get; set; }
        public int toprovince { get; set; }
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int typeComunication { get; set; }
    }
    
    //Parameter truyền vào DB để gọi dữ liệu của Chi tiết tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_CTTS
    {
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public int date { get; set; }
        public int typeComunication { get; set; }
    }

    //Parameter truyền vào DB để gọi dữ liệu của số túi trong chuyến thư
    public class PARAMETER_ST_CTTS
    {
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public string date { get; set; }
        public int mailtrip { get; set; }
        public int typeComunication { get; set; }
    }


    //Parameter truyền vào DB để gọi dữ liệu của Phần In Bản kê e2 theo chuyến thư túi số, mã cổ túi
    public class PARAMETER_E2
    {
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public int date { get; set; }
        public int mailtrip { get; set; }
        public int postbag { get; set; }
        public int typeComunication { get; set; }
    }
    
    //Lấy mã tỉnh đóng , tỉnh nhận
    public class GETPROVINCE
    {
        public string PROVINCECODE { get; set; }

        public string PROVINCENAME { get; set; }

    }

    //Lấy mã bưu cục đóng , bưu cục nhận, GETCRPOSCODE: GETCLOSERECEIVEPOSCODE
    public class GETCRPOSCODE
    {
        public string POSCODE { get; set; }

        public string POSNAME { get; set; }

    }


    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class TransferManagementDetail
    {
        public int Typecomunication { get; set; }

        public int FromPosCode { get; set; }
        public String FromPosName { get; set; }
        public int ToPosCode { get; set; }
        public String ToPosName { get; set; }
        public String PickUpDate { get; set; }
        public int TotalMailTrip { get; set; }
        public int TotalPostBag { get; set; }
        public int TotalItem { get; set; }

    }

    //Dữ liệu lấy ra của Chi tiết tổng hợp dữ liệu truyền nhận EMS Center
    public class TransferManagement_CTTS_Detail
    {
        public int Typecomunication { get; set; }
        public int FromPosCode { get; set; }
        public String FromPosName { get; set; }
        public int ToPosCode { get; set; }
        public String ToPosName { get; set; }
        public String PickUpDate { get; set; }
        public int TotalMailTrip { get; set; }
        public int TotalPostBag { get; set; }
        public int TotalItem { get; set; }

    }

    //Dữ liệu lấy ra của số túi theo tổng số chuyển thư
    public class TransferManagement_SOTUI_Detail
    {

        public int FromPosCode { get; set; }
        public String FromPosName { get; set; }
        public int ToPosCode { get; set; }
        public String ToPosName { get; set; }
        public String PickUpDate { get; set; }
        public int ChThu { get; set; }
        public int TuiSo { get; set; }
        public int CountMaE1 { get; set; }

    }

    //Dữ liệu lấy ra của chi tiết chuyến thư túi số phần in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_CTTS_E2_Detail
    {

        public String FromPos { get; set; }
        public String ToPos { get; set; }
        public int MailTrip { get; set; }
        public int PostBag { get; set; }
        public String PickUpDate { get; set; }
        public String PickUpTime { get; set; }
        public Decimal TotalWeight { get; set; }

    }


    //Dữ liệu lấy ra của bảng chi tiết in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_E2_Detail
    {

        public String MaE1 { get; set; }
        public int MaBCTra { get; set; }
        public String KhoiLuong { get; set; }
        
        public String CuocCS { get; set; }
        public String Cuoc_DV { get; set; }

        public String Tong_Cuoc { get; set; }
        public String TrangThai { get; set; }
        
    }

    //Dữ liệu lấy ra của chi tiết chuyến thư túi số phần in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_CTTS_MCT_E2_Detail
    {

        public String FromPos { get; set; }
        public String ToPos { get; set; }
        public int MailTrip { get; set; }
        public int PostBag { get; set; }
        public String PickUpDate { get; set; }
        public String PickUpTime { get; set; }
        public Decimal TotalWeight { get; set; }

    }


    //Dữ liệu lấy ra của bảng chi tiết in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_MCT_E2_Detail
    {

        public String MaE1 { get; set; }
        public int MaBCTra { get; set; }
        public String KhoiLuong { get; set; }
        
        public String CuocCS { get; set; }
        public String Cuoc_DV { get; set; }

        public String Tong_Cuoc { get; set; }
        public String TrangThai { get; set; }

    }

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnTransferManagement
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

        public TransferManagementDetail TransferManagementReport { get; set; }
        public List<TransferManagementDetail> ListTransferManagementReport;

        public TransferManagement_CTTS_Detail TransferManagement_CTTS_Report { get; set; }
        public List<TransferManagement_CTTS_Detail> ListTransferManagement_CTTS_Report;

        public TransferManagement_SOTUI_Detail TransferManagement_SOTUI_Report { get; set; }
        public List<TransferManagement_SOTUI_Detail> ListTransferManagement_SOTUI_Report;

        public TransferManagement_E2_Detail TransferManagement_E2_Report { get; set; }
        public List<TransferManagement_E2_Detail> ListTransferManagement_E2_Report;

        public TransferManagement_CTTS_E2_Detail TransferManagement_CTTS_E2_Report { get; set; }
        public List<TransferManagement_CTTS_E2_Detail> ListTransferManagement_CTTS_E2_Report;

        public TransferManagement_MCT_E2_Detail TransferManagement_MCT_E2_Report { get; set; }
        public List<TransferManagement_MCT_E2_Detail> ListTransferManagement_MCT_E2_Report;

        public TransferManagement_CTTS_MCT_E2_Detail TransferManagement_CTTS_MCT_E2_Report { get; set; }
        public List<TransferManagement_CTTS_MCT_E2_Detail> ListTransferManagement_CTTS_MCT_E2_Report;

        public MetaData MetaData { get; set; }


    }

    public class ReturnTransmited
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    
}