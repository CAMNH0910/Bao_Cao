using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace T41.Areas.Admin.Data
{
    public class ExpressRoadRepository
    {
        
        //Phần chi tiết của bảng tổng hợp 
        #region EXPRESS_ROAD_DETAIL          
        public ReturnExpressRoad EXPRESS_ROAD_DETAIL()
        {
            //Tính thời gian gọi xuống server
            //var sw = Stopwatch.StartNew();
            //Do.Something();
            //sw.Stop();
            //Debug.WriteLine("Time elapsed: " + sw.ElapsedMilliseconds);

            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnExpressRoad _returnExpressRoad = new ReturnExpressRoad();

            List<ExpressRoadDetail> listExpressRoad = null;
            List<ListTracking> listTracking = null;
            ExpressRoadDetail oExpressRoadDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("JOURNEY_PKG.DETAIL_JOURNEY_LOG", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    
                    //myCommand.Parameters.Add("P_ID", OracleDbType.Int32).Value = 43;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                    ////Read Data with oracle datareader , CONVERT DATA-TABLE --> LIST
                    //OracleDataAdapter mAdapter = new OracleDataAdapter();
                    //mAdapter = new OracleDataAdapter(myCommand);
                    //mAdapter.Fill(da);
                    //listExpressRoad = ConvertListToDataTable.DataTableToList<ExpressRoadDetail>(da);
                    //if (listExpressRoad != null && listExpressRoad.Count != 0)
                    //{
                    //    _returnExpressRoad.Code = "00";
                    //    _returnExpressRoad.Message = "Lấy dữ liệu thành công.";
                    //    _returnExpressRoad.ListExpressRoadReport = listExpressRoad;
                    //}

                    //else
                    //{
                    //    _returnExpressRoad.Code = "01";
                    //    _returnExpressRoad.Message = "Không có dữ liệu";
                    //    _returnExpressRoad.ListExpressRoadReport = null;
                    //}


                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();

                    if (dr.HasRows)
                    {
                        listExpressRoad = new List<ExpressRoadDetail>();
                        listTracking = new List<ListTracking>();
                        while (dr.Read())
                        {
                            oExpressRoadDetail = new ExpressRoadDetail();
                            oExpressRoadDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            oExpressRoadDetail.AMND_DATE = dr["AMND_DATE"].ToString();
                            oExpressRoadDetail.READ = dr["READ"].ToString();
                            oExpressRoadDetail.USER_NAME = dr["USER_NAME"].ToString();
                            oExpressRoadDetail.CONTENT = dr["CONTENT"].ToString();
                            
                            
                            var returndata = JsonConvert.DeserializeObject<EMSCODE>(oExpressRoadDetail.CONTENT);
                            oExpressRoadDetail.E_CODE = returndata.E_CODE;
                            oExpressRoadDetail.CUSTOMERCODE = returndata.CUSTOMERCODE;
                            oExpressRoadDetail.STATUS = returndata.STATUS;
                            oExpressRoadDetail.NOTE = returndata.NOTE;
                            oExpressRoadDetail.CITY = returndata.CITY == null ? "0" : returndata.CITY;
                            oExpressRoadDetail.WEIGHT = returndata.WEIGHT == null ? "0" : returndata.WEIGHT;
                            oExpressRoadDetail.COLLECT = returndata.COLLECT;
                            oExpressRoadDetail.DELIVERY_DATE = returndata.DELIVERY_DATE;

                            

                            var results = JsonConvert.DeserializeObject<dynamic>(oExpressRoadDetail.CONTENT);
                            oExpressRoadDetail.E_CODE = results.E_CODE;
                            oExpressRoadDetail.CUSTOMERCODE = results.CUSTOMERCODE;
                            oExpressRoadDetail.STATUS = results.STATUS;
                            oExpressRoadDetail.NOTE = results.NOTE;
                            oExpressRoadDetail.CITY = results.CITY == null ? "0" : results.CITY;
                            oExpressRoadDetail.WEIGHT = results.WEIGHT == null ? "0" : results.WEIGHT;
                            oExpressRoadDetail.COLLECT = results.COLLECT;
                            oExpressRoadDetail.DELIVERY_DATE = results.DELIVERY_DATE;
                            //Lấy Dữ liệu theo list 
                            //listTracking = returndata.ListTracking;
                            listExpressRoad.Add(oExpressRoadDetail);
                            




                        }

                        _returnExpressRoad.Code = "00";
                        _returnExpressRoad.Message = "Lấy dữ liệu thành công.";
                        _returnExpressRoad.ListExpressRoadReport = listExpressRoad;
                        
                        //_returnExpressRoad.ListTracking = listTracking;


                    }
                    else
                    {
                        _returnExpressRoad.Code = "01";
                        _returnExpressRoad.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnExpressRoad.Code = "99";
                _returnExpressRoad.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnExpressRoad;
        }



        #endregion

        #region LIST_JOURNEY_CREATE
        public ReturnJourney LIST_JOURNEY_CREATE(List<JourneyDetail> listJourney)
        {
            JourneyDetail journeyDetail = new JourneyDetail();
            ReturnJourney oReturnJourney = new ReturnJourney();

            OracleTransaction transaction = Helper.OraDCOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                foreach (JourneyDetail paraJOURNEY in listJourney)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Helper.OraDCOracleConnection;
                        cmd.CommandText = Helper.SchemaName + "JOURNEY_PKG.JOURNEY_CONTENT_CREATE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = transaction;
                        cmd.CommandTimeout = 20000;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        cmd.Parameters.Add("P_ECODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = paraJOURNEY.E_CODE;
                        cmd.Parameters.Add("P_CUSTOMERCODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = paraJOURNEY.CUSTOMERCODE;
                        cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = paraJOURNEY.STATUS;
                        cmd.Parameters.Add("P_NOTE", OracleDbType.Int32, ParameterDirection.Input).Value = paraJOURNEY.NOTE;
                        cmd.Parameters.Add("P_CITY", OracleDbType.Varchar2, ParameterDirection.Input).Value = paraJOURNEY.CITY;
                        cmd.Parameters.Add("P_WEIGHT", OracleDbType.NVarchar2, ParameterDirection.Input).Value = paraJOURNEY.WEIGHT;
                        cmd.Parameters.Add("P_COLLECT", OracleDbType.Varchar2, ParameterDirection.Input).Value = paraJOURNEY.COLLECT;
                        cmd.Parameters.Add("P_DELIVERY_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = paraJOURNEY.DELIVERY_DATE;
                        cmd.Parameters.Add("P_POST_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = paraJOURNEY.POST_CODE;
                        cmd.Parameters.Add("P_USER_NAME", OracleDbType.Int32, ParameterDirection.Input).Value = paraJOURNEY.USER_NAME;
                        cmd.ExecuteNonQuery();

                        //var id = Convert.ToInt32(cmd.Parameters["P_ID"].Value.ToString());

                        //if (id > 0)
                        //{
                        //    oReponseEntity.Code = "00";
                        //    oReponseEntity.Message = "Cập nhật dữ liệu thành công";
                        //    oReponseEntity.Value = listCShipment.Count.ToString();
                        //}
                        //else
                        //{
                        //    oReponseEntity.Code = "-99";
                        //    oReponseEntity.Message = "Lỗi cập nhật dữ liệu (region LIST_SHIPMENT_CREATE)";
                        //    oReponseEntity.Value = string.Empty;
                        //}
                    }
                }
                transaction.Commit();
                oReturnJourney.Code = "00";
                oReturnJourney.Message = "Cập nhật dữ liệu thành công";
                oReturnJourney.Total = listJourney.Count.ToString();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                LogAPI.LogToFile(LogFileType.EXCEPTION, "region LIST_JOURNEY_CREATE" + ex.Message);
                oReturnJourney.Code = "99";
                oReturnJourney.Message = "Lỗi xử lý dữ liệu";
                oReturnJourney.Total = string.Empty;

            }
            return oReturnJourney;
        }
        #endregion

    }

}

