using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class GETPOSCODEGD
    {
        public int POSCODE { get; set; }

        public string POSNAME { get; set; }

    }
    public class PARAMETERAffair
    {
        public string PosCode { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }

    }

    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class AffairDetail
    {
        public int STT { get; set; }
        public String ItemCode { get; set; }
        //public int BuuCuc { get; set; }
        public String AcceptancePosCode { get; set; }
        public String AffairType { get; set; }
        public String AmountCOD { get; set; }
        public String AmountCODOld { get; set; }
        public String ReceiverFullName { get; set; }
        public String ReceiverAddress { get; set; }
        public String ReceiverTel { get; set; }
        public String AffairDate { get; set; }

    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
   
    public class ReturnAffair
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public AffairDetail AffairReport { get; set; }
        public List<AffairDetail> ListAffairReport;     
        public MetaDataAffair MetaDataAffair { get; set; }


    }
    public class MetaDataAffair
    {
        public string from_to_date { get; set; }

    }


}