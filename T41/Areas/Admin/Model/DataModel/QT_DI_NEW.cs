using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class QT_DI_NEW
    {
        public int STT { get; set; }
        public String Donvi { get; set; }
        public String Matinhnhan { get; set; }
        public String Tentinhnhan { get; set; }
        //public int BuuCuc { get; set; }
        public String Mae1 { get; set; }
        public String MaKH { get; set; }
        public String Manuoctra { get; set; }
        public String Ten_Nuoc { get; set; }
        public String NgayPhatHanh { get; set; }

        public String Madvchinh { get; set; }
        //  public String TenDV { get; set; }
        public String PhanLoai { get; set; }
        public String khoiluong { get; set; }
        public String NacKL { get; set; }
        public String KLQD { get; set; }
        public String Tongcuoc { get; set; }
    }
    public class ReturnQT_DI_NEW
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

        public QT_DI_NEW QT_DI_NEW { get; set; }
        public List<QT_DI_NEW> ListDetaiQT_DI_NEW;

        
    }
}