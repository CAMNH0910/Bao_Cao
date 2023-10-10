using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class ZNS_TCB
    {
        public int STT { get; set; }
        public string RECEIVERPHONE { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string ITEMCODE { get; set; }
        public string GBT { get; set; }
        public string KTC1 { get; set; }
        public string KTC2 { get; set; }
        public string KTC3 { get; set; }
        public string PTC { get; set; }

    }
    public class ReturnZNS_TCB
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

        public ZNS_TCB ZNS_TCB { get; set; }
        public List<ZNS_TCB> ListZNS_TCB;
    }
    }