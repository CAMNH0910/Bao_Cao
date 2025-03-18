using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class List_Item_BCCP
    {
        public int STT { get; set; }
        public string Ma_BCKT { get; set; }
        public string IDVNPOST { get; set; }
        public string Ten_HT { get; set; }
        public string BC37CODE { get; set; }
        public string So_Tui { get; set; }
        public string Khoi_Luong { get; set; }
        public string BC37CREATETIME { get; set; }
        public string CONFIRMDATE { get; set; }
    }

    public class ReturnList_Item_BCCP
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public List_Item_BCCP List_Item_BCCP { get; set; }
        public List<List_Item_BCCP> ListList_Item_BCCP;

    }
}