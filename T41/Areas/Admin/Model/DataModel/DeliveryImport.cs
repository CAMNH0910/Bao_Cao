using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class GETLISTPOSCODE
    {
        public string POSCODE { get; set; }
        public string POSCODENAME { get; set; }
    }

    public class ReturnResponseInsert
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }


    }
}