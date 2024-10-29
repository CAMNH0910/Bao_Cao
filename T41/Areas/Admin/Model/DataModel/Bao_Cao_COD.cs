using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class Bao_Cao_COD
    {

        public int STT { get; set; }
        public string Ngay_CN { get; set; }
        public string Ngay_COD { get; set; }
        public string Makh { get; set; }
        public string Ma_Tong { get; set; }
        public string NguoiGui { get; set; }
        public string Chi_Nhanh { get; set; }
        public string SO_THAM_CHIEU { get; set; }
        public string Mae1 { get; set; }
        public string COD { get; set; }
        public string Trang_Thai { get; set; }

    }
    public class ReturnBao_Cao_COD
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

        public Bao_Cao_COD Bao_Cao_COD { get; set; }
        public List<Bao_Cao_COD> ListBao_Cao_COD;
    }
}