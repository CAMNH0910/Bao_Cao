using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{

    public class PaymentHMTotalByDate
    {
        public string StatementNumber { get; set; }
        public string TotalItem { get; set; }
        public string TransactionType { get; set; }
        public string SettlementAmount { get; set; }
        public string SettlementCurrency { get; set; }
        public string PaymentDate { get; set; }
        public string Posted { get; set; }
        public string Status { get; set; }
    }
    

    public class CustomerInforExcelHM
    {
        public string StatementNumber { get; set; }
        public string SerialNumber { get; set; }
        public string transactionType { get; set; }
        public string pspReference { get; set; }
        public string amountCaptured { get; set; }
        public string captureTransactionCurrency { get; set; }
        public string settlementAmount { get; set; }
        public string settlementCurrency { get; set; }
        public string paymentDate { get; set; }
        public string postingDate { get; set; }
        public string uniqueReferenceNumber { get; set; }
        public string manualMatchingReference { get; set; }
    }

    public class ReturnCustomerInforHM
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public CustomerInforExcelHM CustomerInforHM { get; set; }
        public List<CustomerInforExcelHM> listCustomerInforHM;
    }

    public class ReturnPaymentHMByDate
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public PaymentHMTotalByDate PaymentHMTotalByDate { get; set; }
        public List<PaymentHMTotalByDate> listPaymentHMTotalByDate;
    }

    public class ReturnConfirmCOD
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ResponseImportExcelHM
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