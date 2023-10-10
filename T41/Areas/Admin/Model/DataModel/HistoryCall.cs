using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class HistoryCall
    {
        public int STT { get; set; }
        public string TINHTP { get; set; }
        public string NGAY { get; set; }
        public string MABC { get; set; }
        public string MATUYEN { get; set; }
        public string TENBUUTA { get; set; }
        public string TONGSOBUUGUIDIPHAT { get; set; }
        public string SUMBGDINGDONGCOE1 { get; set; }
        public string TYLEGOIQUADINGDONG { get; set; }
    }

    public class DetailHistoryCall
    {
        public int STT { get; set; }
        public string TINHTP { get; set; }
        public string MABC { get; set; }
        public string MATUYEN { get; set; }
        public string MABT { get; set; }
        public string TENBT { get; set; }
        public string MAE1 { get; set; }
        public string SODTNGUOINHAN { get; set; }
        public string LUOTGOITRONGNGAY { get; set; }
        public string TGBATDAUGOI { get; set; }
        public string TGKETTHUCGOI { get; set; }
        public string TRANGTHAIGOI { get; set; }
    }
    public class ReturnTHHistoryCall
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<HistoryCall> listTHHistoryCallReport { get; set; }
    }

    public class ReturnCTHistoryCall
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<DetailHistoryCall> listCTHistoryCallReport { get; set; }
    }
}