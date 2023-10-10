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
    public class ItemSendRepository
    {

        // Phần lấy dữ liệu từ bảng e1_bd13_di
        #region ResponseItemSend          
        public ResponseItemSend ITEM_SEND_DETAIL(int page_index, int page_size, int Type, int Startdate, int Enddate, string CustomerCode,string ItemCode)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ResponseItemSend _returnItemSend = new ResponseItemSend();

            List<ItemSend> listItemSendDetail = null;
            ItemSend oItemSendDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("GTGT_GateWay.ListItemSend", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_PageIdx", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("v_Pagesize", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("v_type", OracleDbType.Int32).Value = Type;
                    myCommand.Parameters.Add("v_Startdate", OracleDbType.Int32).Value = Startdate;
                    myCommand.Parameters.Add("v_Enddate", OracleDbType.Int32).Value = Enddate;
                    myCommand.Parameters.Add("v_CustomerCode", OracleDbType.NVarchar2).Value = (CustomerCode == "0") ? null : CustomerCode ;
                    myCommand.Parameters.Add("v_ItemCode", OracleDbType.Varchar2).Value = (ItemCode == "0") ? null : ItemCode;
                    myCommand.Parameters.Add("v_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listItemSendDetail = new List<ItemSend>();
                        while (dr.Read())
                        {
                            oItemSendDetail = new ItemSend();
                            oItemSendDetail.CustomerCode = dr["CUSTOMERCODE"].ToString();
                            oItemSendDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oItemSendDetail.AcceptancePos = dr["ACCEPTANCEPOS"].ToString();
                            oItemSendDetail.Gate = dr["GATE"].ToString();
                            oItemSendDetail.SenderPhone = dr["SENDERPHONE"].ToString();
                            oItemSendDetail.ReceiverPhone = dr["RECEIVERPHONE"].ToString();
                            oItemSendDetail.ReceiverEmail = dr["RECEIVEREMAIL"].ToString();
                            oItemSendDetail.DeliveryState = dr["DELIVERYSTATE"].ToString();
                            oItemSendDetail.SendState = dr["SENDSTATE"].ToString();
                            oItemSendDetail.SendingTime = dr["SENDINGTIME"].ToString();
                            listItemSendDetail.Add(oItemSendDetail);

                        }
                        _returnItemSend.Code = "00";
                        _returnItemSend.Message = "Lấy dữ liệu thành công.";
                        _returnItemSend.Total = Convert.ToInt32(myCommand.Parameters["v_TOTAL"].Value.ToString());
                        _returnItemSend.listItemSendReport = listItemSendDetail;
                    }
                    else
                    {
                        _returnItemSend.Code = "01";
                        _returnItemSend.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnItemSend.Code = "99";
                _returnItemSend.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnItemSend;
        }



        #endregion


        

        
    }



}

