using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class KPI_Thu_Gom
    {
        public int STT { get; set; }
        public string PO_CODE { get; set; }
        public string PO_NAME    { get; set; }
        public string ROUTE_CODE { get; set; }
        public string ROUTE_NAME { get; set; }
        public string Id_Postman { get; set; }
        public string PostMan_Name { get; set; }
        public string DV { get; set; }
        public string ARRIVED_WEIGHT { get; set; }
        public string San_Luong { get; set; }
        public string Tra_Lai { get; set; }
        public string SL2kg { get; set; }
        public string KL2kg { get; set; }
        public string SLL2kg { get; set; }
        public string KLL2kg { get; set; }
        public string TongKG { get; set; }
        public string TongKL { get; set; }
    }

    public class ReturnKPI_Thu_Gom
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

        public KPI_Thu_Gom KPI_Thu_Gom { get; set; }
        public List<KPI_Thu_Gom> ListKPI_Thu_Gom;
        }
    }