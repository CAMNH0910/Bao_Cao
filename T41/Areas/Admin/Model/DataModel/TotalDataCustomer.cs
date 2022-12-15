using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_TOTALDATA
    {
        public int fromprovince { get; set; }
        public int toprovince { get; set; }
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int typeComunication { get; set; }
    }

    public class GETPOSCODE_TOTALDATA
    {
        public string POSTCODE { get; set; }

        public string POSNAME { get; set; }

    }


    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class TotalDataCustomerDetail
    {

        public String STARTPROVINCE { get; set; }
        public String ENDPROVINCE { get; set; }
        public String STARTPROVINCENAME { get; set; }
        public String ENDPROVINCENAME { get; set; }
        public String C17TOTAL { get; set; }
        public String PTC { get; set; }
        public String TLPTC { get; set; }
        public String CH { get; set; }
        public String TLCH { get; set; }
        public String TOTALKTT { get; set; }
        public String TLKTT { get; set; }       
    }

    

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnTotalDataCustomer
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

        public TotalDataCustomerDetail TotalDataCustomerReport { get; set; }
        public List<TotalDataCustomerDetail> ListTotalDataCustomerReport;

        

        public MetaData MetaData { get; set; }


    }

    
}