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
    public class KPIDeliveryLOGRepository
    {

        #region GETLISTBUUCUC
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems

        public string GetPoscodeEms()
        {
            List<PoscodeEMS> listPoscodeEMS = null;
            PoscodeEMS oGetPoscodeEMS = null;
            string LISTPOSCODE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "kpi_detail_LOG.Detail_DeliveryPosCode_Ems";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                      //  LISTPOSCODE += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTPOSCODE += "<option value='" + dr["POSCODE"].ToString() + "'>" + dr["POSCODE"].ToString() + '-' + dr["POSNAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetPoscodeEms" + ex.Message);
                LISTPOSCODE = null;
            }

            return LISTPOSCODE;
        }

        #endregion
        //Phần TONG HOP
        #region kpi_delivery_log        



        

        #endregion

        public ReturnKPIDeliveryLOG Detail_PLD_XND(int StartPoscode, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPIDeliveryLOG _returnKPIDeliveryLOG = new ReturnKPIDeliveryLOG();
            
            List<Detail_PLD_XND> listDetail_PLD_XND = null;
            Detail_PLD_XND oDetail_PLD_XND = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_LOG.Detail_PLD_XND", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPoscode;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;                  
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetail_PLD_XND = new List<Detail_PLD_XND>();
                        while (dr.Read())
                        {
                            oDetail_PLD_XND = new Detail_PLD_XND();
                            oDetail_PLD_XND.STT = a++;
                            oDetail_PLD_XND.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_PLD_XND.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_PLD_XND.PostmanId = dr["POSTMANID"].ToString();
                            oDetail_PLD_XND.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_PLD_XND.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            oDetail_PLD_XND.TongSL = dr["TONGSL"].ToString();
                            oDetail_PLD_XND.SanLuongPTC = dr["SANLUONGPTC"].ToString();
                            oDetail_PLD_XND.TyLePTC = dr["TYLEPTC"].ToString();
                            oDetail_PLD_XND.SanLuongDat = dr["SANLUONGDAT"].ToString();
                            oDetail_PLD_XND.TyLeDat = dr["TYLEDAT"].ToString();
                            oDetail_PLD_XND.SanLuongTruot = dr["SANLUONGTRUOT"].ToString();
                            oDetail_PLD_XND.TyLeTruot = dr["TYLETRUOT"].ToString();
                            oDetail_PLD_XND.SanLuongKTT = dr["SANLUONGKTT"].ToString();
                            oDetail_PLD_XND.TyLeKTT = dr["TYLEKTT"].ToString();
                            listDetail_PLD_XND.Add(oDetail_PLD_XND);
                        }
                        _returnKPIDeliveryLOG.Code = "00";
                        _returnKPIDeliveryLOG.Message = "Lấy dữ liệu thành công.";
                        _returnKPIDeliveryLOG.ListDetail_PLD_XNDReport = listDetail_PLD_XND;
                    }
                    else
                    {
                        _returnKPIDeliveryLOG.Code = "01";
                        _returnKPIDeliveryLOG.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnKPIDeliveryLOG.Code = "99";
                _returnKPIDeliveryLOG.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnKPIDeliveryLOG;
        }

        public ReturnKPIDeliveryLOG Detail_PTC_XND(int StartPoscode, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPIDeliveryLOG _returnKPIDeliveryLOG = new ReturnKPIDeliveryLOG();
            
            List<Detail_PTC_XND> listDetail_PTC_XND = null;
            Detail_PTC_XND oDetail_PTC_XND = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_LOG.Detail_PTC_XND", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPoscode;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;                  
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetail_PTC_XND = new List<Detail_PTC_XND>();
                        while (dr.Read())
                        {
                            oDetail_PTC_XND = new Detail_PTC_XND();
                            oDetail_PTC_XND.STT = a++;
                            oDetail_PTC_XND.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_PTC_XND.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_PTC_XND.PostmanId = dr["POSTMANID"].ToString();
                            oDetail_PTC_XND.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_PTC_XND.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            oDetail_PTC_XND.TongSL = dr["TONGSL"].ToString();
                            oDetail_PTC_XND.SanLuongPTC = dr["SANLUONGPTC"].ToString();
                            oDetail_PTC_XND.TyLePTC = dr["TYLEPTC"].ToString();
                            oDetail_PTC_XND.SanLuongDat = dr["SANLUONGDAT"].ToString();
                            oDetail_PTC_XND.TyLeDat = dr["TYLEDAT"].ToString();
                            oDetail_PTC_XND.SanLuongTruot = dr["SANLUONGTRUOT"].ToString();
                            oDetail_PTC_XND.TyLeTruot = dr["TYLETRUOT"].ToString();
                            oDetail_PTC_XND.SanLuongKTT = dr["SANLUONGKTT"].ToString();
                            oDetail_PTC_XND.TyLeKTT = dr["TYLEKTT"].ToString();
                            listDetail_PTC_XND.Add(oDetail_PTC_XND);
                        }
                        _returnKPIDeliveryLOG.Code = "00";
                        _returnKPIDeliveryLOG.Message = "Lấy dữ liệu thành công.";
                        _returnKPIDeliveryLOG.ListDetail_PTC_XNDReport = listDetail_PTC_XND;
                    }
                    else
                    {
                        _returnKPIDeliveryLOG.Code = "01";
                        _returnKPIDeliveryLOG.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnKPIDeliveryLOG.Code = "99";
                _returnKPIDeliveryLOG.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnKPIDeliveryLOG;
        }

        public ReturnKPIDeliveryLOG Detail_PLD_BT(int StartPoscode, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPIDeliveryLOG _returnKPIDeliveryLOG = new ReturnKPIDeliveryLOG();

            List<Detail_PLD_BT> listDetail_PLD_BT = null;
            Detail_PLD_BT oDetail_PLD_BT = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_LOG.Detail_PLD_BT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPoscode;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetail_PLD_BT = new List<Detail_PLD_BT>();
                        while (dr.Read())
                        {
                            oDetail_PLD_BT = new Detail_PLD_BT();
                            oDetail_PLD_BT.STT = a++;
                            oDetail_PLD_BT.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_PLD_BT.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_PLD_BT.PostmanId = dr["POSTMANID"].ToString();
                            oDetail_PLD_BT.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_PLD_BT.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            oDetail_PLD_BT.TongSL = dr["TONGSL"].ToString();
                            oDetail_PLD_BT.SanLuongPTC = dr["SANLUONGPTC"].ToString();
                            oDetail_PLD_BT.TyLePTC = dr["TYLEPTC"].ToString();
                            oDetail_PLD_BT.SanLuongDat = dr["SANLUONGDAT"].ToString();
                            oDetail_PLD_BT.TyLeDat = dr["TYLEDAT"].ToString();
                            oDetail_PLD_BT.SanLuongTruot = dr["SANLUONGTRUOT"].ToString();
                            oDetail_PLD_BT.TyLeTruot = dr["TYLETRUOT"].ToString();
                            oDetail_PLD_BT.SanLuongKTT = dr["SANLUONGKTT"].ToString();
                            oDetail_PLD_BT.TyLeKTT = dr["TYLEKTT"].ToString();
                            listDetail_PLD_BT.Add(oDetail_PLD_BT);
                        }
                        _returnKPIDeliveryLOG.Code = "00";
                        _returnKPIDeliveryLOG.Message = "Lấy dữ liệu thành công.";
                        _returnKPIDeliveryLOG.ListDetail_PLD_BTReport = listDetail_PLD_BT;
                    }
                    else
                    {
                        _returnKPIDeliveryLOG.Code = "01";
                        _returnKPIDeliveryLOG.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnKPIDeliveryLOG.Code = "99";
                _returnKPIDeliveryLOG.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnKPIDeliveryLOG;
        }

        public ReturnKPIDeliveryLOG Detail_PTC_BT(int StartPoscode, int IsService, int StartDate, int EndDate)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnKPIDeliveryLOG _returnKPIDeliveryLOG = new ReturnKPIDeliveryLOG();

            List<Detail_PTC_BT> listDetail_PTC_BT = null;
            Detail_PTC_BT oDetail_PTC_BT = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_LOG.Detail_PTC_BT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPoscode;
                    myCommand.Parameters.Add("v_IsService", OracleDbType.Int32).Value = IsService;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetail_PTC_BT = new List<Detail_PTC_BT>();
                        while (dr.Read())
                        {
                            oDetail_PTC_BT = new Detail_PTC_BT();
                            oDetail_PTC_BT.STT = a++;
                            oDetail_PTC_BT.StartPostCode = dr["STARTPOSTCODE"].ToString();
                            oDetail_PTC_BT.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_PTC_BT.PostmanId = dr["POSTMANID"].ToString();
                            oDetail_PTC_BT.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_PTC_BT.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            oDetail_PTC_BT.TongSL = dr["TONGSL"].ToString();
                            oDetail_PTC_BT.SanLuongPTC = dr["SANLUONGPTC"].ToString();
                            oDetail_PTC_BT.TyLePTC = dr["TYLEPTC"].ToString();
                            oDetail_PTC_BT.SanLuongDat = dr["SANLUONGDAT"].ToString();
                            oDetail_PTC_BT.TyLeDat = dr["TYLEDAT"].ToString();
                            oDetail_PTC_BT.SanLuongTruot = dr["SANLUONGTRUOT"].ToString();
                            oDetail_PTC_BT.TyLeTruot = dr["TYLETRUOT"].ToString();
                            oDetail_PTC_BT.SanLuongKTT = dr["SANLUONGKTT"].ToString();
                            oDetail_PTC_BT.TyLeKTT = dr["TYLEKTT"].ToString();
                            listDetail_PTC_BT.Add(oDetail_PTC_BT);
                        }
                        _returnKPIDeliveryLOG.Code = "00";
                        _returnKPIDeliveryLOG.Message = "Lấy dữ liệu thành công.";
                        _returnKPIDeliveryLOG.ListDetail_PTC_BTReport = listDetail_PTC_BT;
                    }
                    else
                    {
                        _returnKPIDeliveryLOG.Code = "01";
                        _returnKPIDeliveryLOG.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                _returnKPIDeliveryLOG.Code = "99";
                _returnKPIDeliveryLOG.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnKPIDeliveryLOG;
        }
        public ReturnDetail_Item_Ems_TRUOT Detail_Item_Ems_TRUOT(int StartPoscode, int PostmanID, int StartDate, int EndDate, string IsService, int Istype)
        {
            string nameBC = "";
            switch (Istype)
            {
                case 0:
                    nameBC = "Báo cáo sản lượng trượt phát lần đầu giao bưu tá";
                    break;
                case 1:
                    nameBC = "Báo cáo sản lượng trượt phát thành công giao bưu tá ";
                    break;
                case 2:
                    nameBC = "Báo cáo sản lượng trượt phát lần đầu theo thời gian XND tại bưu cục ";
                    break;
                case 3:
                    nameBC = "Báo cáo sản lượng trượt phát thành công theo thời gian XND tại bưu cục ";
                    break;

            }
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDetail_Item_Ems_TRUOT ReturnDetail_Item_Ems_TRUOT = new ReturnDetail_Item_Ems_TRUOT();

            List<Detail_Item_Ems_TRUOT> listDetail_Item_Ems_TRUOT = null;
            Detail_Item_Ems_TRUOT oDetail_Item_Ems_TRUOT = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_LOG.Detail_Item_Ems_TRUOT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPoscode;
                    myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = PostmanID;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Varchar2).Value = IsService;
                    myCommand.Parameters.Add("v_Istype", OracleDbType.Int32).Value = Istype;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetail_Item_Ems_TRUOT = new List<Detail_Item_Ems_TRUOT>();
                        while (dr.Read())
                        {
                            oDetail_Item_Ems_TRUOT = new Detail_Item_Ems_TRUOT();
                            oDetail_Item_Ems_TRUOT.Itemcode = dr["ITEMCODE"].ToString();
                            oDetail_Item_Ems_TRUOT.StartPostcode = dr["STARTPOSTCODE"].ToString();
                            oDetail_Item_Ems_TRUOT.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_Item_Ems_TRUOT.POSTMANID = dr["POSTMANID"].ToString();
                            oDetail_Item_Ems_TRUOT.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_Item_Ems_TRUOT.C17StatusDate = dr["C17STATUSDATE"].ToString();
                            oDetail_Item_Ems_TRUOT.C17StatusTime = dr["C17STATUSTIME"].ToString();
                            oDetail_Item_Ems_TRUOT.StatusDate = dr["STATUSDATE"].ToString();
                            oDetail_Item_Ems_TRUOT.StatusTime = dr["STATUSTIME"].ToString();
                            oDetail_Item_Ems_TRUOT.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            listDetail_Item_Ems_TRUOT.Add(oDetail_Item_Ems_TRUOT);
                        }
                        ReturnDetail_Item_Ems_TRUOT.NameBC = nameBC;
                        ReturnDetail_Item_Ems_TRUOT.Code = "00";
                        ReturnDetail_Item_Ems_TRUOT.Message = "Lấy dữ liệu thành công.";
                        ReturnDetail_Item_Ems_TRUOT.ListDetail_Item_Ems_TRUOT = listDetail_Item_Ems_TRUOT;
                    }
                    else
                    {
                        ReturnDetail_Item_Ems_TRUOT.NameBC = nameBC;
                        ReturnDetail_Item_Ems_TRUOT.Code = "01";
                        ReturnDetail_Item_Ems_TRUOT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                ReturnDetail_Item_Ems_TRUOT.NameBC = nameBC;
                ReturnDetail_Item_Ems_TRUOT.Code = "99";
                ReturnDetail_Item_Ems_TRUOT.Message = "Lỗi xử lý dữ liệu";

            }
            return ReturnDetail_Item_Ems_TRUOT;
        }

        public ReturnDetail_Item_Ems_KTT Detail_Item_Ems_KTT(int StartPoscode, int PostmanID, int StartDate, int EndDate, string IsService, int Istype)
        {
            string nameBC = "";
            switch (Istype)
            {
                case 0:
                    nameBC = "Báo cáo sản lượng KTT phát lần đầu bưu tá giao cho bưu tá";
                    break;
                case 1:
                    nameBC = "Báo cáo sản lượng KTT phát thành công giao cho bưu tá ";
                    break;
                case 2:
                    nameBC = "Báo cáo sản lượng KTT phát lần đầu theo thời gian XND tại bưu cục ";
                    break;
                case 3:
                    nameBC = "Báo cáo sản lượng KTT phát thành công theo thời gian XND tại bưu cục ";
                    break;

            }
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnDetail_Item_Ems_KTT ReturnDetail_Item_Ems_KTT = new ReturnDetail_Item_Ems_KTT();

            List<Detail_Item_Ems_KTT> listDetail_Item_Ems_KTT = null;
            Detail_Item_Ems_KTT oDetail_Item_Ems_KTT = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("kpi_detail_LOG.Detail_Item_Ems_KTT", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartPostCode", OracleDbType.Int32).Value = StartPoscode;
                    myCommand.Parameters.Add("v_PostmanID", OracleDbType.Int32).Value = PostmanID;
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = StartDate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = EndDate;
                    myCommand.Parameters.Add("v_Isservice", OracleDbType.Varchar2).Value = IsService;
                    myCommand.Parameters.Add("v_Istype", OracleDbType.Int32).Value = Istype;

                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listDetail_Item_Ems_KTT = new List<Detail_Item_Ems_KTT>();
                        while (dr.Read())
                        {
                            oDetail_Item_Ems_KTT = new Detail_Item_Ems_KTT();
                            oDetail_Item_Ems_KTT.Itemcode = dr["ITEMCODE"].ToString();
                            oDetail_Item_Ems_KTT.StartPostcode = dr["STARTPOSTCODE"].ToString();
                            oDetail_Item_Ems_KTT.StartPostcodeName = dr["STARTPOSTCODENAME"].ToString();
                            oDetail_Item_Ems_KTT.POSTMANID = dr["POSTMANID"].ToString();
                            oDetail_Item_Ems_KTT.PostmanName = dr["POSTMANNAME"].ToString();
                            oDetail_Item_Ems_KTT.StatusDate = dr["STATUSDATE"].ToString();
                            oDetail_Item_Ems_KTT.StatusTime = dr["STATUSTIME"].ToString();
                            oDetail_Item_Ems_KTT.ServiceTypeNumber = dr["SERVICETYPENUMBER"].ToString();
                            listDetail_Item_Ems_KTT.Add(oDetail_Item_Ems_KTT);
                        }
                        ReturnDetail_Item_Ems_KTT.NameBC = nameBC;
                        ReturnDetail_Item_Ems_KTT.Code = "00";
                        ReturnDetail_Item_Ems_KTT.Message = "Lấy dữ liệu thành công.";
                        ReturnDetail_Item_Ems_KTT.ListDetail_Item_Ems_KTT = listDetail_Item_Ems_KTT;
                    }
                    else
                    {
                        ReturnDetail_Item_Ems_KTT.NameBC = nameBC;
                        ReturnDetail_Item_Ems_KTT.Code = "01";
                        ReturnDetail_Item_Ems_KTT.Message = "Không có dữ liệu";

                    }
                }
            }
            catch (Exception ex)
            {
                ReturnDetail_Item_Ems_KTT.NameBC = nameBC;
                ReturnDetail_Item_Ems_KTT.Code = "99";
                ReturnDetail_Item_Ems_KTT.Message = "Lỗi xử lý dữ liệu";

            }
            return ReturnDetail_Item_Ems_KTT;
        }
    }

}

