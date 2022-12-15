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
        public String ItemCode { get; set; }
        public String WorkDate { get; set; }
        public String FromPosCode { get; set; }
        public String FromPosCodeName { get; set; }
        public String ToPosCode { get; set; }
        public String ToPosCodeName { get; set; }
        public String WorkCenter { get; set; }
        public String IDVnpost { get; set; }
        public String MailrouteName { get; set; }
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