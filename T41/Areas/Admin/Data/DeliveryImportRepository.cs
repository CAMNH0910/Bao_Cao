using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Models.DataModel;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class DeliveryImportRepository
    {
        #region GetALLDeliveryPostCode

        public IEnumerable<GETLISTPOSCODE> GetAllPOSCODE(int status)
        {
            List<GETLISTPOSCODE> listGetPosCode = null;
            GETLISTPOSCODE oGetPosCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "Ems_Bccp_Delivery.GetListPosCode";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_Status", OracleDbType.Int32)).Value = status;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetPosCode = new List<GETLISTPOSCODE>();
                        while (dr.Read())
                        {
                            oGetPosCode = new GETLISTPOSCODE();
                            oGetPosCode.POSCODE = dr["DELIVERYROUTE"].ToString();
                            oGetPosCode.POSCODENAME = dr["DELIVERYROUTENAME"].ToString();
                            listGetPosCode.Add(oGetPosCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "Ems_Bccp_Delivery.GetListPosCode" + ex.Message);
                listGetPosCode = null;
            }

            return listGetPosCode;
        }
        #endregion

        public ReturnResponseInsert InsertE1NHRepsitory(string Itemcode, int Poscode, int Status, int DeliveryDate, string DeliveryTime, string Consignee, string Note)
        {

            ReturnResponseInsert R_insert = new ReturnResponseInsert();
            try
            {

                OracleCommand myCommand = new OracleCommand("Ems_Bccp_Delivery.Insert_E1NH", Helper.OraDCOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_ItemCode", OracleDbType.NVarchar2).Value = Itemcode;
                myCommand.Parameters.Add("v_PosCode", OracleDbType.Int32).Value = Poscode;
                myCommand.Parameters.Add("v_Status", OracleDbType.Int32).Value = Status;
                myCommand.Parameters.Add("v_Ngaynhan", OracleDbType.Int32).Value = DeliveryDate;
                myCommand.Parameters.Add("v_Gionhan", OracleDbType.NVarchar2).Value = DeliveryTime;
                myCommand.Parameters.Add("v_ngnhan", OracleDbType.NVarchar2).Value = Consignee;
                myCommand.Parameters.Add("v_Ghichu", OracleDbType.NVarchar2).Value = Note;
                myCommand.ExecuteNonQuery();
                R_insert.Code = "00";
                R_insert.Message = "Thêm mới dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_insert.Code = "01";
                R_insert.Message = ex.ToString();

            }

            return R_insert;


        }
    }
}