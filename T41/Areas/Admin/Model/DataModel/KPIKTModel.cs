using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Theo Dõi Đơn Hàng
    public class PARAMETER_KPIKTModel
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string Transporttype { get; set; }
        public string buucuc { get; set; }
    }


    public class THSLDetail
    {
        public int STT { get; set; }
        public String WorkCenter { get; set; }
        public String LeaveProduction { get; set; }
        public String LeaveWeight { get; set; }
        public String ErrorPRODUCT { get; set; }
        public String ErrorWeight { get; set; }
        public String TLSLTC { get; set; }
        public String TLKLTC { get; set; }


    }
    public class THTCDetail
    {
        public int STT { get; set; }
        public String IdVnpost { get; set; }
        public String MailrouteName { get; set; }
        public String INSTOCKPRODUCT { get; set; }
        public String InstockWeight { get; set; }
        public String ArrivePRODUCT { get; set; }
        public String ArriveWeight { get; set; }
        public String totalproduct { get; set; }
        public String totalweight { get; set; }
        public String PeakProduct { get; set; }
        public String Peakweight { get; set; }
        public String LeaveProduction { get; set; }
        public String LeaveWeight { get; set; }
        public String ErrorPRODUCT { get; set; }
        public String ErrorWeight { get; set; }
    }


    public class CTTCDetail
    {
        public int STT { get; set; }
        public String idvnpost { get; set; }
        public String Mailroutename { get; set; }
        public String workdate { get; set; }
        public String FailPRODUCT { get; set; }
        public String FailWeight { get; set; }
    }

    public class TMSKPIDetail
    {
        public int STT { get; set; }
        public String CutOffName { get; set; }
        public String ID_Mailroute { get; set; }
        public String mailrouteschedulename { get; set; }
        public String ToTime { get; set; }
        public String WorkCenter { get; set; }
        public String Cellvalue { get; set; }
        public String PosName { get; set; }
        public String IdEmsservice { get; set; }
        public String Transporttype { get; set; }
        public int TransporttypeInt { get; set; }
        public String Notkpi { get; set; }
        public int NotKpiInt { get; set; }



    }

    public class THSLTCDetail
    {
        public int STT { get; set; }
        public String Status { get; set; }
        public String TimeToSorting { get; set; }
        public String ItemCode { get; set; }
        public String Phan_Loai { get; set; }
        public String Ma_bc_den { get; set; }
        public String Ma_Kho { get; set; }
        public String Ngay_Nhap { get; set; }
        public String Gio_Nhap { get; set; }
        public String Ngay_Xuat { get; set; }
        public String Gio_Xuat { get; set; }
        public String Huong_Di { get; set; }
        public String Weight { get; set; }
        public String IDVNPost { get; set; }
        public String MailRouteScheduleName { get; set; }
        public String BC37FromPosCode { get; set; }
        public String BC37ToPosCode { get; set; }
        public String BC37DateChar { get; set; }
        public String BCCPCreateTime { get; set; }
        public String BC37Index { get; set; }
        public String BC37Code { get; set; }
        public String BCCPTime { get; set; }
        public String MailTripFromProvinceName { get; set; }
        public String FromPosCode { get; set; }
        public String MailTripToProvinceName { get; set; }
        public String ToPosCode { get; set; }
        public String MailTripNumber { get; set; }
        public String PostBagIndex { get; set; }
        public String MailTripDateChar { get; set; }
        public String KTE4Time { get; set; }
        public String postbagcode { get; set; }
        public String WorkCenter { get; set; }


    }

    public class CTSLTCDetail
    {
        public int STT { get; set; }
        public String ItemCode { get; set; }
        public String FromPosCode { get; set; }
        public String FromPosCodeName { get; set; }
        public String ToPosCode { get; set; }
        public String ToPosCodeName { get; set; }
        public String PostBagCode { get; set; }
        public String BC37Code { get; set; }
        public String IDVnpost { get; set; }
        public String MailrouteName { get; set; }
        public String AcceptTime { get; set; }
        public String Event { get; set; }

    }

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB

    public class KPI_KTHT{
        public int STT { get; set; }
        public string Id_Hanh_Trinh { get; set; }
        public string TEN_HANH_TRINH { get; set; }
        public string THOI_GIAN_DI { get; set; }
        public string San_Luong { get; set; }
        public string San_Luong_TC { get; set; }


    }

    public class KPI_KTHT_CT
    {
        public int STT { get; set; }
        public string Id_Hanh_Trinh { get; set; }
        public string TEN_HANH_TRINH { get; set; }
        public string THOI_GIAN_DI { get; set; }
        public string Mae1 { get; set; }
        public string Noi_Dung { get; set; }
        public string Nguoi_Nhan { get; set; }
        public string Dia_Chi { get; set; }
        public string Khoi_Luong { get; set; }
        public string Gio_Den { get; set; }
        public string Gio_Di { get; set; }
        public string Ngay_HT_Di { get; set; }
        
    }

    public class KPI_KTHT_TC
    {
        public int STT { get; set; }
        public string Id_Hanh_Trinh { get; set; }
        public string TEN_HANH_TRINH { get; set; }
        public string THOI_GIAN_DI { get; set; }
        public string Mae1 { get; set; }
        public string Noi_Dung { get; set; }
        public string Nguoi_Nhan { get; set; }
        public string Dia_Chi { get; set; }
        public string Khoi_Luong { get; set; }
        public string Gio_Den { get; set; }
        public string Gio_Di { get; set; }
        public string Ngay_HT_Di { get; set; }
        public string IDVNPOST { get; set; }
        

    }
    public class ReturnKPIKT
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
        public KPI_KTHT KPI_KTHT { get; set; }
        public List<KPI_KTHT> LisKPI_KTHT;

        public KPI_KTHT_CT KPI_KTHT_CT { get; set; }
        public List<KPI_KTHT_CT> LisKPI_KTHT_CT;

        public KPI_KTHT_TC KPI_KTHT_TC { get; set; }
        public List<KPI_KTHT_TC> LisKPI_KTHT_TC;


        public THTCDetail THTCDetailReport { get; set; }
        public List<THTCDetail> ListTHTCReport;

        public CTTCDetail CTTCDetailReport { get; set; }
        public List<CTTCDetail> ListCTTCReport;

        public THSLDetail THSLDetailReport { get; set; }
        public List<THSLDetail> ListTHSLReport;

        public TMSKPIDetail TMSKPIDetailReport { get; set; }
        public List<TMSKPIDetail> ListTMSKPIReport;

        public THSLTCDetail THSLTCDetailReport { get; set; }
        public List<THSLTCDetail> ListTHSLTCDetailReport;

        public CTSLTCDetail CTSLTCDetailReport { get; set; }
        public List<CTSLTCDetail> ListCTSLTCDetailReport;

        public MetaData MetaData { get; set; }

    }
    public class ReturnResponseUpdate
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }

    public class ReturnIdMailRoute
    {
        public string IdMailRoute { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}