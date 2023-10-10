using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class ItemSend
    {
        public string CustomerCode { get; set; }
        public string ItemCode { get; set; }
        public string AcceptancePos { get; set; }
        public string Gate { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string DeliveryState { get; set; }
        public string SendState { get; set; }
        public string SendingTime { get; set; }
    }
    public class ResponseItemSend
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public List<ItemSend> listItemSendReport { get; set; }
    }
}