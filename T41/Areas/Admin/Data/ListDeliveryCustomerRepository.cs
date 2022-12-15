using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class ListDeliveryCustomerRepository
    {
        Convertion common = new Convertion();

      
      


        // Phần lấy dữ liệu từ bảng business_profile_oa
    
        public ReturnListCustomer List_Delivery(string customerCode, int StartDate, int EndDate)
        {
            int STT = 1;
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnListCustomer _returnCustomerManagement = new ReturnListCustomer();
            List<ListCustomer_Detail> listCustomerDetail = null;
            ListCustomer_Detail oUserCustomerDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    //OracleCommand myCommand = new OracleCommand("CRM_USER_MANAGEMENT.Detail_User", Helper.OraEVComOracleConnection);
                    OracleCommand myCommand = new OracleCommand("SysData_Customer_v2.ListTraceDelivery", Helper.OraDCOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_CustomerCode", OracleDbType.Varchar2).Value = customerCode;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCustomerDetail = new List<ListCustomer_Detail>();
                        while (dr.Read())
                        {
                            oUserCustomerDetail = new ListCustomer_Detail();
                            oUserCustomerDetail.STT = STT++;
                            oUserCustomerDetail.Ma_Tham_Chieu = dr["Ma_Tham_Chieu"].ToString();
                            oUserCustomerDetail.Ma_Van_Don = dr["Ma_Van_Don"].ToString();
                            oUserCustomerDetail.Ma_bc_phat = dr["Ma_bc_phat"].ToString();
                            oUserCustomerDetail.Nguoi_Nhan = dr["Nguoi_Nhan"].ToString();
                            oUserCustomerDetail.Dien_Thoai_Nhan = dr["Dien_Thoai_Nhan"].ToString();
                            oUserCustomerDetail.Dia_Chi_Nhan = dr["Dia_Chi_Nhan"].ToString();
                            oUserCustomerDetail.Tinh_Nhan = dr["Tinh_Nhan"].ToString();
                            oUserCustomerDetail.Trang_Thai_Hien_Tai = dr["Trang_Thai_Hien_Tai"].ToString();
                            oUserCustomerDetail.Trang_Thai_1 = dr["Trang_Thai_1"].ToString();
                            oUserCustomerDetail.Ghi_chu_1 = dr["Ghi_chu_1"].ToString();
                            oUserCustomerDetail.Ngay_giao_1 = dr["Ngay_giao_1"].ToString();
                            oUserCustomerDetail.Gio_Giao_1 = dr["Gio_Giao_1"].ToString();
                            oUserCustomerDetail.Trang_Thai_2 = dr["Trang_Thai_2"].ToString();
                            oUserCustomerDetail.Ghi_chu_2 = dr["Ghi_chu_2"].ToString();
                            oUserCustomerDetail.Ngay_giao_2 = dr["Ngay_giao_2"].ToString();
                            oUserCustomerDetail.Gio_Giao_2 = dr["Gio_Giao_2"].ToString();
                            oUserCustomerDetail.Trang_Thai_3 = dr["Trang_Thai_3"].ToString();
                            oUserCustomerDetail.Ghi_chu_3 = dr["Ghi_chu_3"].ToString();
                            oUserCustomerDetail.Ngay_giao_3 = dr["Ngay_giao_3"].ToString();
                            oUserCustomerDetail.Gio_Giao_3 = dr["Gio_Giao_3"].ToString();
                            oUserCustomerDetail.Trang_Thai_4 = dr["Trang_Thai_4"].ToString();
                            oUserCustomerDetail.Ghi_chu_4 = dr["Ghi_chu_4"].ToString();
                            oUserCustomerDetail.Ngay_giao_4 = dr["Ngay_giao_4"].ToString();
                            oUserCustomerDetail.Gio_Giao_4 = dr["Gio_Giao_4"].ToString();
                            oUserCustomerDetail.Lan_Phat = dr["Lan_Phat"].ToString();

                            listCustomerDetail.Add(oUserCustomerDetail);

                        }
                        _returnCustomerManagement.Code = "00";
                        _returnCustomerManagement.Message = "Lấy dữ liệu thành công.";
                        _returnCustomerManagement.Total = listCustomerDetail.Count();
                        _returnCustomerManagement.ListCustomer_Report_Detail = listCustomerDetail;
                    }
                    else
                    {
                        _returnCustomerManagement.Code = "01";
                        _returnCustomerManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnCustomerManagement.Code = "99";
                _returnCustomerManagement.Message = "Lỗi xử lý dữ liệu";
            }
            return _returnCustomerManagement;
        }
        
       

        
    }



}

