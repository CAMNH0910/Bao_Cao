using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{

    public class PROVINCE
    {
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
    }
    public class KPI_Total_HT
    {
        public int STT { get; set; }
        public string StartProvince { get; set; }
        public string StartProvinceName { get; set; }
        public string Endprovince { get; set; }
        public string EndprovinceName { get; set; }
        public string Target { get; set; }
        public string Total { get; set; }
        public string SLSuccess { get; set; }
        public string TLSuccess { get; set; }
        public string IsFailStatus { get; set; }
        public string TLIsFailStatus { get; set; }
        public string AcceptFrom_Cutoff { get; set; }
        public string AcceptTo_Cutoff { get; set; }
        public string Delivery_Cutoff { get; set; }
    }

    public class KPI_Detail_Fail_Hub
    {
        public int STT { get; set; }
        public string Itemcode { get; set; }
        public string A1PosCode { get; set; }
        public string A1PosCodeName { get; set; }
        public string A1StatusDatetime { get; set; }
        public string A2PosCode { get; set; }
        public string A2PosCodeName { get; set; }
        public string A2StatusDateTime { get; set; }
        public string StartProvince { get; set; }
        public string StartProvinceName { get; set; }
        public string Endprovince { get; set; }
        public string EndprovinceName { get; set; }
        public string Target { get; set; }
        public string IsFailstatus { get; set; }
    }

    public class ResponseKPI_Total_HT
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<KPI_Total_HT> listKPI_Total_HTReport { get; set; }
    }

    public class ResponseKPI_Detail_Fail_Hub
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<KPI_Detail_Fail_Hub> listKPI_Detail_Fail_HubReport { get; set; }
    }
}