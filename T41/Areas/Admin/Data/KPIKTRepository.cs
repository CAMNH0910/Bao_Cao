﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;
using System.Data.Odbc;

namespace T41.Areas.Admin.Data
{
    public class KPIKTRepository
    {
        
    
        #region THSL          
        public ReturnKPIKT THSL(string buucuc, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPIKT _returnPostMan = new ReturnKPIKT();

            
            List<THSLDetail> listTHSLDetail = null;
            THSLDetail oTHSLDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                   OracleCommand myCommand = new OracleCommand("tt_report.TH_SL", Helper.OraDCComOracleConnection);
                   //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;                                         
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();                                   
                   
                    myCommand.Parameters.Add("v_PostCode", OracleDbType.NVarchar2).Value = buucuc;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                  
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTHSLDetail = new List<THSLDetail>();
                        while (dr.Read())
                        {
                            oTHSLDetail = new THSLDetail();
                            oTHSLDetail.STT = a++;
                            oTHSLDetail.WorkCenter = dr["WORKCENTER"].ToString();
                            oTHSLDetail.LeaveProduction = dr["LEAVEPRODUCTION"].ToString();
                            oTHSLDetail.LeaveWeight = dr["LEAVEWEIGHT"].ToString();
                            oTHSLDetail.ErrorPRODUCT = dr["ERRORPRODUCT"].ToString();
                            oTHSLDetail.ErrorWeight = dr["ERRORWEIGHT"].ToString();
                            oTHSLDetail.TLSLTC = dr["TLSLTC"].ToString();
                            oTHSLDetail.TLKLTC =dr["TLKLTC"].ToString();
                            listTHSLDetail.Add(oTHSLDetail);
                            
                        }
                        _returnPostMan.Code = "00";
                        _returnPostMan.Message = "Lấy dữ liệu thành công.";
                        _returnPostMan.ListTHSLReport = listTHSLDetail;
                    }
                    else
                    {
                        _returnPostMan.Code = "01";
                        _returnPostMan.Message = "Không có dữ liệu";
                        
                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PostmanDeliveryRepository.POSTMAN_DELIVERY_DETAIL" + ex.Message);
                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListTHSLReport = null;
            }
            return _returnPostMan;
        }
        public int ConvertToInt(object str)
        {
            try
            {
                return Convert.ToInt32(str.ToString());
            }
            catch
            {
                return 0;
            }
        }


        #endregion

        #region THTC          
        public ReturnKPIKT THTC(string buucuc, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPIKT _returnPostMan = new ReturnKPIKT();


            List<THTCDetail> listTHTCDetail = null;
            THTCDetail oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("tt_report.TH_TC", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_PostCode", OracleDbType.NVarchar2).Value = buucuc;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTHTCDetail = new List<THTCDetail>();
                        while (dr.Read())
                        {
                            oTHTCDetail = new THTCDetail();
                            oTHTCDetail.STT = a++;
                            oTHTCDetail.IdVnpost = dr["IDVNPOST"].ToString();
                            oTHTCDetail.MailrouteName = dr["MAILROUTENAME"].ToString();
                            oTHTCDetail.INSTOCKPRODUCT = dr["INSTOCKPRODUCT"].ToString();
                            oTHTCDetail.InstockWeight = dr["INSTOCKWEIGHT"].ToString();
                            oTHTCDetail.ArrivePRODUCT = dr["ARRIVEPRODUCT"].ToString();
                            oTHTCDetail.ArriveWeight = dr["ARRIVEWEIGHT"].ToString();
                            oTHTCDetail.totalproduct = dr["TOTALPRODUCT"].ToString();
                            oTHTCDetail.totalweight = dr["TOTALWEIGHT"].ToString();
                            oTHTCDetail.PeakProduct = dr["PEAKPRODUCT"].ToString();
                            oTHTCDetail.Peakweight = dr["PEAKWEIGHT"].ToString();
                            oTHTCDetail.LeaveProduction = dr["LEAVEPRODUCTION"].ToString();
                            oTHTCDetail.LeaveWeight = dr["LEAVEWEIGHT"].ToString();
                            oTHTCDetail.ErrorPRODUCT = dr["ERRORPRODUCT"].ToString();
                            oTHTCDetail.ErrorWeight = dr["ERRORWEIGHT"].ToString();
                            listTHTCDetail.Add(oTHTCDetail);

                        }
                        _returnPostMan.Code = "00";
                        _returnPostMan.Message = "Lấy dữ liệu thành công.";
                        _returnPostMan.ListTHTCReport = listTHTCDetail;
                    }
                    else
                    {
                        _returnPostMan.Code = "01";
                        _returnPostMan.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
              
                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListTHTCReport = null;
            }
            return _returnPostMan;
        }
        #endregion



        #region ctTC          
        public ReturnKPIKT CTTC(string buucuc, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPIKT _returnPostMan = new ReturnKPIKT();


            List<CTTCDetail> listCTTCDetail = null;
            CTTCDetail oCTTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("tt_report.CT_TC", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_PostCode", OracleDbType.NVarchar2).Value = buucuc;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCTTCDetail = new List<CTTCDetail>();
                        while (dr.Read())
                        {
                            oCTTCDetail = new CTTCDetail();
                            oCTTCDetail.STT = a++;
                            oCTTCDetail.idvnpost = dr["IDVNPOST"].ToString();
                            oCTTCDetail.Mailroutename = dr["MAILROUTENAME"].ToString();
                            oCTTCDetail.workdate = dr["WORKDATE"].ToString();
                            oCTTCDetail.FailPRODUCT = dr["FAILPRODUCT"].ToString();
                            oCTTCDetail.FailWeight = dr["FAILWEIGHT"].ToString();
                           
                            listCTTCDetail.Add(oCTTCDetail);

                        }
                        _returnPostMan.Code = "00";
                        _returnPostMan.Message = "Lấy dữ liệu thành công.";
                        _returnPostMan.ListCTTCReport = listCTTCDetail;
                    }
                    else
                    {
                        _returnPostMan.Code = "01";
                        _returnPostMan.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "PostmanDeliveryRepository.POSTMAN_DELIVERY_DETAIL" + ex.Message);
                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListCTTCReport = null;
            }
            return _returnPostMan;
        }
        #endregion

        #region TMSKPI          
        public ReturnKPIKT TMSKPI(string buucuc, string Transporttype)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPIKT _returnPostMan = new ReturnKPIKT();


            List<TMSKPIDetail> listTMSKPIDetail = null;
            TMSKPIDetail oTMSKPIDetail = null;
            int a = 1;
            try
            {
                
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("tt_report.Report_Tms_Kpi_Cell", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_PostCode", OracleDbType.NVarchar2).Value = buucuc;                
                    myCommand.Parameters.Add("v_TransPortType", OracleDbType.Int32).Value = Transporttype;                

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTMSKPIDetail = new List<TMSKPIDetail>();
                        while (dr.Read())
                        {
                            oTMSKPIDetail = new TMSKPIDetail();
                            oTMSKPIDetail.STT = a++;
                            oTMSKPIDetail.CutOffName = dr["CUTOFFNAME"].ToString();
                            oTMSKPIDetail.ID_Mailroute = dr["ID_MAILROUTE"].ToString();
                            oTMSKPIDetail.mailrouteschedulename = dr["MAILROUTESCHEDULENAME"].ToString();
                            oTMSKPIDetail.ToTime = dr["TOTIME"].ToString();
                            oTMSKPIDetail.WorkCenter = dr["WORKCENTER"].ToString();
                            oTMSKPIDetail.Cellvalue = dr["CELLVALUE"].ToString();
                            oTMSKPIDetail.PosName = dr["POSNAME"].ToString();
                            oTMSKPIDetail.IdEmsservice = dr["IDEMSSERVICE"].ToString();
                            oTMSKPIDetail.Transporttype = dr["TRANSPORTTYPE"].ToString();
                            if(oTMSKPIDetail.Transporttype == "Bo")
                            {
                                oTMSKPIDetail.TransporttypeInt = 0;
                            }else if(oTMSKPIDetail.Transporttype == "Bay")
                            {
                                oTMSKPIDetail.TransporttypeInt = 1;
                            }
                            else
                            {
                                oTMSKPIDetail.TransporttypeInt = 3;
                            }
                            oTMSKPIDetail.Notkpi = dr["NOTKPI"].ToString();
                            if(oTMSKPIDetail.Notkpi == "NOT KPI")
                            {
                                oTMSKPIDetail.NotKpiInt = 1;
                            }
                            else if(oTMSKPIDetail.Notkpi == "KPI")
                            {
                                oTMSKPIDetail.NotKpiInt = 0;
                            }

                            listTMSKPIDetail.Add(oTMSKPIDetail);

                        }
                        _returnPostMan.Code = "00";
                        _returnPostMan.Message = "Lấy dữ liệu thành công.";
                        _returnPostMan.ListTMSKPIReport = listTMSKPIDetail;
                    }
                    else
                    {
                        _returnPostMan.Code = "01";
                        _returnPostMan.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {

                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListTMSKPIReport = null;
            }
            return _returnPostMan;
        }
        #endregion

        #region Update MailRoute
        

        public ReturnResponseUpdate UpdateMailRoute(int IdMailRoute, int WorkCenter, string Cellvalue, int TransportypeInt, string NameCutOff, string Totime,int NotKpi)
        {
            
            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("tt_report.UpdateMailRoute", Helper.OraDCComOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_IDMailroute", OracleDbType.Int32).Value = IdMailRoute;
                myCommand.Parameters.Add("v_Workcenter", OracleDbType.Int32).Value = WorkCenter;
                myCommand.Parameters.Add("v_Cellvalue", OracleDbType.NVarchar2).Value = Cellvalue;
                myCommand.Parameters.Add("v_Transporttype", OracleDbType.Int32).Value = TransportypeInt;
                myCommand.Parameters.Add("v_NameCutOff", OracleDbType.NVarchar2).Value = NameCutOff;
                myCommand.Parameters.Add("v_Totime", OracleDbType.NVarchar2).Value = Totime;
                myCommand.Parameters.Add("v_Kpi", OracleDbType.Int32).Value = NotKpi;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Cập nhật dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }


        public ReturnIdMailRoute FindIdMailRoute(string Id6Number)
        {

            ReturnIdMailRoute R_find = new ReturnIdMailRoute();
            try
            {
                
                OracleCommand myCommand = new OracleCommand("EMS.FINDIDMAILROUTE_EMS", Helper.OraDCComOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("v_IdMailroute", OracleDbType.Int32, 10, ParameterDirection.ReturnValue);
                myCommand.Parameters.Add("v_ID", OracleDbType.NVarchar2).Value = Id6Number;
                myCommand.ExecuteNonQuery();
                var retVal = Convert.ToString(myCommand.Parameters["v_IdMailroute"].Value);
                R_find.IdMailRoute = retVal;
                R_find.Code = "00";
                R_find.Message = "Thành công";
            }
            catch (Exception ex)
            {         
                R_find.Code = "01";
                R_find.Message = ex.ToString();

            }

            return R_find;
        }

        public ReturnResponseUpdate InsertMailRoute(int IdMailRoute, int WorkCenter, int Cellvalue, int TransportypeInt,int NotKpi, string NameCutOff, string Totime)
        {

            ReturnResponseUpdate R_update = new ReturnResponseUpdate();
            try
            {

                OracleCommand myCommand = new OracleCommand("tt_report.InsertMailRoute", Helper.OraDCComOracleConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 20000;
                myCommand.Parameters.Add("v_IDMailroute", OracleDbType.Int32).Value = IdMailRoute;
                myCommand.Parameters.Add("v_Workcenter", OracleDbType.Int32).Value = WorkCenter;
                myCommand.Parameters.Add("v_Cellvalue", OracleDbType.Int32).Value = Cellvalue;
                myCommand.Parameters.Add("v_Transporttype", OracleDbType.Int32).Value = TransportypeInt;
                myCommand.Parameters.Add("v_Kpi", OracleDbType.Int32).Value = NotKpi;
                myCommand.Parameters.Add("v_NameCutOff", OracleDbType.NVarchar2).Value = NameCutOff;
                myCommand.Parameters.Add("v_Totime", OracleDbType.NVarchar2).Value = Totime;
                myCommand.ExecuteNonQuery();
                R_update.Code = "00";
                R_update.Message = "Thêm mới dữ liệu thành công !";
            }
            catch (Exception ex)
            {
                R_update.Code = "01";
                R_update.Message = ex.ToString();

            }

            return R_update;
        }
        #endregion

        #region THSLTC
        public ReturnKPIKT THSLTC(int WorkCenter,int IDVnpost, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPIKT _returnTHSLTC = new ReturnKPIKT();


            List<THSLTCDetail> listTHSLTCDetail = null;
            THSLTCDetail oTHSLTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("tt_report.DetailListFail", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_Workcenter", OracleDbType.Int32).Value = WorkCenter;
                    myCommand.Parameters.Add("v_IdVNPost", OracleDbType.Int32).Value = IDVnpost;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTHSLTCDetail = new List<THSLTCDetail>();
                        while (dr.Read())
                        {
                            oTHSLTCDetail = new THSLTCDetail();
                            oTHSLTCDetail.STT = a++;
                            oTHSLTCDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oTHSLTCDetail.WorkDate = dr["WORKDATE"].ToString();
                            oTHSLTCDetail.FromPosCode = dr["FROMPOSCODE"].ToString();
                            oTHSLTCDetail.FromPosCodeName = dr["FROMPOSCODENAME"].ToString();
                            oTHSLTCDetail.ToPosCode = dr["TOPOSCODE"].ToString();
                            oTHSLTCDetail.ToPosCodeName = dr["TOPOSCODENAME"].ToString();
                            oTHSLTCDetail.WorkCenter = dr["WORKCENTER"].ToString();
                            oTHSLTCDetail.IDVnpost = dr["IDVNPOST"].ToString();
                            oTHSLTCDetail.MailrouteName = dr["MAILROUTENAME"].ToString();
                            listTHSLTCDetail.Add(oTHSLTCDetail);

                        }
                        _returnTHSLTC.Code = "00";
                        _returnTHSLTC.Message = "Lấy dữ liệu thành công.";
                        _returnTHSLTC.ListTHSLTCDetailReport = listTHSLTCDetail;
                    }
                    else
                    {
                        _returnTHSLTC.Code = "01";
                        _returnTHSLTC.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {

                _returnTHSLTC.Code = "99";
                _returnTHSLTC.Message = "Lỗi xử lý dữ liệu";
                _returnTHSLTC.ListTHTCReport = null;
            }
            return _returnTHSLTC;
        }
        #endregion

        #region CTSLTC
        public ReturnKPIKT CTSLTC(string ItemCode, string WorkCenter)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPIKT _returnCTSLTC = new ReturnKPIKT();


            List<CTSLTCDetail> listCTSLTCDetail = null;
            CTSLTCDetail oCTSLTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("tt_report.DetailItemFail", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();

                    myCommand.Parameters.Add("v_ItemCode", OracleDbType.NVarchar2).Value = ItemCode;
                    myCommand.Parameters.Add("v_Workcenter", OracleDbType.Int32).Value = WorkCenter;


                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listCTSLTCDetail = new List<CTSLTCDetail>();
                        while (dr.Read())
                        {
                            oCTSLTCDetail = new CTSLTCDetail();
                            oCTSLTCDetail.STT = a++;
                            oCTSLTCDetail.ItemCode = dr["ITEMCODE"].ToString();
                            oCTSLTCDetail.FromPosCode = dr["FROMPOSCODE"].ToString();
                            oCTSLTCDetail.FromPosCodeName = dr["FROMPOSCODENAME"].ToString();
                            oCTSLTCDetail.ToPosCode = dr["TOPOSCODE"].ToString();
                            oCTSLTCDetail.ToPosCodeName = dr["TOPOSCODENAME"].ToString();
                            oCTSLTCDetail.PostBagCode = dr["POSTBAGCODE"].ToString();
                            oCTSLTCDetail.BC37Code = dr["BC37CODE"].ToString();
                            oCTSLTCDetail.IDVnpost = dr["IDVNPOST"].ToString();
                            oCTSLTCDetail.MailrouteName = dr["MAILROUTENAME"].ToString();
                            oCTSLTCDetail.AcceptTime = dr["ACCEPTTIME"].ToString();
                            oCTSLTCDetail.Event = dr["EVENT"].ToString();

                            listCTSLTCDetail.Add(oCTSLTCDetail);

                        }
                        _returnCTSLTC.Code = "00";
                        _returnCTSLTC.Message = "Lấy dữ liệu thành công.";
                        _returnCTSLTC.ListCTSLTCDetailReport = listCTSLTCDetail;
                    }
                    else
                    {
                        _returnCTSLTC.Code = "01";
                        _returnCTSLTC.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnCTSLTC.Code = "99";
                _returnCTSLTC.Message = "Lỗi xử lý dữ liệu";
                _returnCTSLTC.ListCTTCReport = null;
            }
            return _returnCTSLTC;
        }
        #endregion
        

    }

}

