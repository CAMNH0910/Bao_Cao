using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{     
    public class PARAMETERCustomer
    {
          public string Ngay { get; set; }
     
    }

    //Lấy chi tiết của bảng tổng hợp sản lượng đi phát
    public class CustomerDetail
    {
        public int STT { get; set; }
        public String DienThoai { get; set; }
        //public int BuuCuc { get; set; }
        public String Tinh { get; set; }
        public String SanLuong { get; set; }
        public String Ngay { get; set; }

    }
    //Lấy chi tiết của từng bưu gửi theo số lượng phát thành công trong 6H
   
    public class ReturnCustomer
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public CustomerDetail CustomerReport { get; set; }
        public List<CustomerDetail> ListCustomerReport;     
        public MetaDataCustomer MetaDataCustomer { get; set; }


    }
    public class MetaDataCustomer
    {
        public string from_to_date { get; set; }
      
    }


}