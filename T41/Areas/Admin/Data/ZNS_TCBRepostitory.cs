using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using System.Security.Policy;
using System.Web.Services.Description;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;

namespace T41.Areas.Admin.Data
{
    public class ZNS_TCB_Repository
    {
        public ReturnZNS_TCB ZNS_TCB_DETAIL(string Customer, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnZNS_TCB _ReturnZNS_TCB = new ReturnZNS_TCB();
            var test = Helper.OraDCOracleConnection;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("GTGT_GateWay.GetListItem_TCB", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table   
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_CustomerCode", OracleDbType.Varchar2).Value = Customer;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        int a = 0;
                        var listZNS_TCB = new List<ZNS_TCB>();
                        while (dr.Read())
                        {
                            var oZNS_TCB = new ZNS_TCB();
                            oZNS_TCB.STT = a++;
                            oZNS_TCB.RECEIVERPHONE = dr["RECEIVERPHONE"].ToString();
                            //oZNS_TCB.BuuCuc = Convert.ToInt32(dr["BUUCUC"].ToString());
                            oZNS_TCB.CUSTOMERCODE = dr["CUSTOMERCODE"].ToString();
                            oZNS_TCB.ITEMCODE = dr["ITEMCODE"].ToString();
                            oZNS_TCB.GBT = dr["GBT"].ToString();
                            oZNS_TCB.KTC1 = dr["KTC1"].ToString();
                            oZNS_TCB.KTC2 = dr["KTC2"].ToString();
                            oZNS_TCB.KTC3 = dr["KTC3"].ToString();
                            oZNS_TCB.PTC = dr["PTC"].ToString();
                            listZNS_TCB.Add(oZNS_TCB);
                        }
                        _ReturnZNS_TCB.Code = "00";
                        _ReturnZNS_TCB.Message = "Lấy dữ liệu thành công.";
                        _ReturnZNS_TCB.ListZNS_TCB = listZNS_TCB;
                    }
                    else
                    {
                        _ReturnZNS_TCB.Code = "01";
                        _ReturnZNS_TCB.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _ReturnZNS_TCB.Code = "99";
                _ReturnZNS_TCB.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnZNS_TCB;
        }
    }
}