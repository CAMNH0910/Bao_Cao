using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class DM_Lai_Xe
    {
        public int STT { get; set; }
        public string Id { get; set; }
        public string Zone { get; set; }
        public string Buu_Cuc { get; set; }
        public string Tuyen_Phat { get; set; }
        public string Ma_NV { get; set; }
        public string Ten_NV { get; set; }
        public string Buu_Ta { get; set; }

        public string Trang_Thai { get; set; }
    }

    public class DM_Lai_Xe_Sua
    {
        public string Id { get; set; }
        public string Ma_NV { get; set; }
        public string Ten_NV { get; set; }
        public string Zone { get; set; }
        public string Buu_Cuc { get; set; }
        public string Tuyen_Phat { get; set; }
        public string Buu_Ta { get; set; }
        
    }

    public class ReturnDM_Lai_Xe
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

        public DM_Lai_Xe DM_Lai_XeReport { get; set; }
        public List<DM_Lai_Xe> ListDM_Lai_Xe;

        public DM_Lai_Xe_Sua DM_Lai_Xe_SuaReport { get; set; }
        public List<DM_Lai_Xe_Sua> ListDM_Lai_Xe_Sua;
    }
}