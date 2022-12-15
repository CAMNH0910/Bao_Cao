using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{


    public class CustomerInfor
    {
        public int STT { get; set; }
        public string E1Code { get; set; }
        public string DeliveryDate { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Province { get; set; }
        public string VisaClass { get; set; }
        public string EntryTime { get; set; }
        public string DS160 { get; set; }
        public string PriorityName { get; set; }
        public string DeliveryOption { get; set; }
    }

    public class CustomerInforExcel
    {
        public string PassportNumber { get; set; }
        public string UID { get; set; }
        public string MailingAddress { get; set; }
        public string MailingCity { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string VisaClass { get; set; }
        public string ScheduleEntry { get; set; }
        public string DS160 { get; set; }
        public string PriorityName { get; set; }
    }

    public class ReturnCustomerInfor
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public CustomerInfor CustomerInfor { get; set; }
        public List<CustomerInfor> listCustomerInfor;
    }

    public class ResponseImportExcel
    {
        public int Total { get; set; }
        public int Success { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }


}