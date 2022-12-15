using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;


namespace T41.Areas.Admin.Data
{
    public class CustomeryRepository
    {
    

        //Phần chi tiết 
        #region Customer_DETAIL          
        public ReturnCustomer Customer_DETAIL(int Ngay)
        {
            DataTable da = new DataTable();
            MetaDataCustomer _metadataCustomer = new MetaDataCustomer();
            Convertion common = new Convertion();
            ReturnCustomer _returnCustomer = new ReturnCustomer();

            _metadataCustomer.from_to_date = "Từ ngày " + common.Convert_Date(Ngay);

            _returnCustomer.MetaDataCustomer = _metadataCustomer;
            List<CustomerDetail> listCustomerDetail = null;
            CustomerDetail oCustomerDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("sync_status_Portal.GetListCustomer", Helper.OraDCOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                  
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = Ngay;                   
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCustomerDetail = new List<CustomerDetail>();
                        while (dr.Read())
                        {
                            oCustomerDetail = new CustomerDetail();
                            oCustomerDetail.STT = a++;
                            oCustomerDetail.DienThoai = dr["DIENTHOAI"].ToString();
                            oCustomerDetail.Tinh = dr["TINH"].ToString();
                            oCustomerDetail.SanLuong = dr["SANLUONG"].ToString();
                            oCustomerDetail.Ngay = dr["NGAYDK"].ToString();
                            listCustomerDetail.Add(oCustomerDetail);
                        }
                        _returnCustomer.Code = "00";
                        _returnCustomer.Message = "Lấy dữ liệu thành công.";
                        _returnCustomer.ListCustomerReport = listCustomerDetail;
                    }
                    else
                    {
                        _returnCustomer.Code = "01";
                        _returnCustomer.Message = "Không có dữ liệu";
                        
                    }


                }
            }
            catch (Exception ex)
            {
                _returnCustomer.Code = "99";
                _returnCustomer.Message = "Lỗi xử lý dữ liệu";
               
            }
            return _returnCustomer;
        }



        #endregion

     
    }

}

