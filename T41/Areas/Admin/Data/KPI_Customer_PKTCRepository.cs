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
    public class KPI_Customer_PKTCRepository
    {
        public ReturnKPI_Customer_PKTC KPI_Customer_PKTC(int StartDate, int EndDate, int IsService, string Customer)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPI_Customer_PKTC _ReturnKPI_Customer = new ReturnKPI_Customer_PKTC();
            List<KPI_Customer_PKTC> listKPI_Customer_PKTC = null;
            KPI_Customer_PKTC oKPI_Customer = null;
            int a = 1;
            var test = Helper.OraDCDevOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("KPI_Customer_delivery_Total.KPI_CUSTOMER_PKTC", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_Customer", OracleDbType.Varchar2).Value = Customer;
                    // myCommand.Parameters.Add("v_IsCod", OracleDbType.Int32).Value = IsCod;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listKPI_Customer_PKTC = new List<KPI_Customer_PKTC>();
                        while (dr.Read())
                        {
                            oKPI_Customer = new KPI_Customer_PKTC();
                            oKPI_Customer.STT = a++;
                            oKPI_Customer.Itemcode = dr["Itemcode"].ToString();
                            oKPI_Customer.CUSTOMERCODE = dr["CUSTOMERCODE"].ToString();
                            oKPI_Customer.StartPostCodeName = dr["StartPostCodeName"].ToString();
                            oKPI_Customer.STARTPROVINCEName = dr["STARTPROVINCEName"].ToString();
                            oKPI_Customer.ISSERVICE = dr["ISSERVICE"].ToString();
                            oKPI_Customer.CAUSECODE = dr["CAUSECODE"].ToString();
                            oKPI_Customer.DELIVERYDATE = dr["DELIVERYDATE"].ToString();
                            listKPI_Customer_PKTC.Add(oKPI_Customer);
                        }
                        _ReturnKPI_Customer.Code = "00";
                        _ReturnKPI_Customer.Message = "Lấy dữ liệu thành công.";
                        _ReturnKPI_Customer.ListKPI_Customer_PKTCReport = listKPI_Customer_PKTC;
                    }
                    else
                    {
                        _ReturnKPI_Customer.Code = "01";
                        _ReturnKPI_Customer.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnKPI_Customer.Code = "99";
                _ReturnKPI_Customer.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnKPI_Customer;
        }
    }
}