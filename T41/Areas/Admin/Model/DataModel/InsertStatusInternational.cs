using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class ReponseEntity
    {
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        public int Id { get; set; }
    }

    public class ParameterInsertStatusInternational
    {
        public string EVENT { get; set; }
        public string MAE1 { get; set; }
        public string MANC { get; set; }
        public string NGAY { get; set; }
        public string GIO { get; set; }
        public string LYDO { get; set; }
        public string HUONGXULY { get; set; }
        public string OE { get; set; }
        public string MABC_KT { get; set; }
    }
}