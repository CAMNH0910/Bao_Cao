using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Kien_Di_Ngoai_TTKTV
    {

        public int STT { get; set; }
        public string Don_Vi { get; set; }
        public string SL_TONG { get; set; }
        public string SL_KTEMS { get; set; }
        public string TY_LE_SL { get; set; }
        public string KL_TONG { get; set; }
        public string KL_KTEMS { get; set; }
        public string TY_LE_KL { get; set; }
    }

    public class ReturnKien_Di_Ngoai_TTKTV
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public Kien_Di_Ngoai_TTKTV Kien_Di_Ngoai_TTKTVReport { get; set; }
        public List<Kien_Di_Ngoai_TTKTV> ListKien_Di_Ngoai_TTKTV;
    }
    }