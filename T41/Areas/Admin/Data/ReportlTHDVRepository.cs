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
    public class ReportlTHDVRepository
    {
        #region GETDONVI
        //Lấy mã bưu cục phát dưới DB Procedure Detail_DeliveryPosCode_Ems
        public IEnumerable<GETDONVI> GetDonVi()
        {
            List<GETDONVI> listGetDonvi = null;
            GETDONVI oGetDonvi = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "reportbusinessweb.Get_List_Don_Vi";
                    cm.CommandType = CommandType.StoredProcedure;


                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetDonvi = new List<GETDONVI>();
                        while (dr.Read())
                        {
                            oGetDonvi = new GETDONVI();
                            oGetDonvi.STT = int.Parse(dr["STT"].ToString());
                            oGetDonvi.DONVI = dr["DONVI"].ToString();
                            listGetDonvi.Add(oGetDonvi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetallDV" + ex.Message);
                listGetDonvi = null;
            }

            return listGetDonvi;
        }
        #endregion

        #region GETPHANLOAIDV
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public IEnumerable<GETPHANLOAIDV> GetAllPLDV()
        {
            List<GETPHANLOAIDV> listPHANLOAIDV = null;
            GETPHANLOAIDV oGetPHANLOAIDV = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_Phan_loai_DV";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listPHANLOAIDV = new List<GETPHANLOAIDV>();
                        while (dr.Read())
                        {
                            oGetPHANLOAIDV = new GETPHANLOAIDV();
                            oGetPHANLOAIDV.STT = int.Parse(dr["STT"].ToString());
                            oGetPHANLOAIDV.PHANLOAI = dr["PHANLOAI"].ToString();
                            listPHANLOAIDV.Add(oGetPHANLOAIDV);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetPhanLoaiDV" + ex.Message);
                listPHANLOAIDV = null;
            }

            return listPHANLOAIDV;
        }

        #endregion
        #region GETLISTINH
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetAllTINH()
        {
            List<GETLISTTINH> listTinh = null;
            GETLISTTINH oGetTinh = null;
            string LISTTINH = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_Tinh";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTTINH += "<option value='" + dr["ProvinceCode"].ToString() + "'>" + dr["ProvinceCode"].ToString() + '-' + dr["ProvinceName"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetTinh" + ex.Message);
                LISTTINH = null;
            }

            return LISTTINH;
        }

        #endregion


        #region GETLISHUYEN
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetAllHUYEN()
        {
            List<GETLISTHUYEN> listHuyen = null;
            GETLISTTINH oGetHuyen = null;
            string LISTHUYEN = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_Huyen";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTHUYEN += "<option value='" + dr["DistrictCode"].ToString() + "'>" + dr["DistrictCode"].ToString() + '-' + dr["DistrictName"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetHuyen" + ex.Message);
                LISTHUYEN = null;
            }

            return LISTHUYEN;
        }

        #endregion

        #region GETDV
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetAllDV()
        {
            List<GETLISTDV> listDV = null;
            GETLISTDV oGetDV = null;
            string LISTTINH  = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_Dich_Vu";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTTINH += "<option value='" + dr["MADICHVU"].ToString() + "'>" + dr["MADICHVU"].ToString() + '-' + dr["TENDICHVU"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetDV" + ex.Message);
                listDV = null;
            }

            return LISTTINH;
        }

        #endregion
        #region GETDV
        //Lấy mã tuyến phát dưới DB Procedure Detail_DeliveryRoute_Ems
        public string GetAllDVCT()
        {
            List<GETLISTDVCT> listDVCT = null;
            GETLISTDVCT oGetDV = null;
            string LISTDVCT = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_Dich_Vu_Cong_Them";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        // LISTTINH += "<option value='" + 0 + "'>"+ "Tất cả" + "</option>";
                        while (dr.Read())
                        {

                            LISTDVCT += "<option value='" + dr["MADICHVU"].ToString() + "'>" + dr["MADICHVU"].ToString() + '-' + dr["TENDICHVU"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetDV" + ex.Message);
                listDVCT = null;
            }

            return LISTDVCT;
        }

        #endregion
        public string GetMANC()
        {
            List<GETLISTMANUOC> listmanc = null;
            GETLISTMANUOC oGetmanc = null;
            string LISTMANC = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_MANUOC";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {                      
                        while (dr.Read())
                        {

                            LISTMANC += "<option value='" + dr["MA_NUOC"].ToString() + "'>" + dr["MA_NUOC"].ToString() + '-' + dr["TEN_NUOC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetTinh" + ex.Message);
                LISTMANC = null;
            }

            return LISTMANC;
        }

        public string GetMANCOE()
        {
            List<GETLISTMANUOC> listmanc = null;
            GETLISTMANUOC oGetmanc = null;
            string LISTMANC = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDSOracleConnection;
                    cm.CommandText = Helper.SchemaName + "ems.reportbusinessweb.Get_List_OE_Nhan";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_DataReport", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            LISTMANC += "<option value='" + dr["STT"].ToString() + "'>" + dr["STT"].ToString() + '-' + dr["PHANLOAI"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetTinh" + ex.Message);
                LISTMANC = null;
            }

            return LISTMANC;
        }

        //Phần chi tiết của bảng tổng hợp sản lượng đi phát
        #region QUALITY_DETAIL_DICHVU
        public ReturnReportTHDV QUALITY_THDV_DETAIL(int DONVI, int PHANLOAI, string DICHVU, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetail> listQualityReportKDDetal = null;
            QualityReportKDDetail oQualityReportKDDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_TH_Theo_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_PhanloaiDV", OracleDbType.Int32).Value = PHANLOAI;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetal = new List<QualityReportKDDetail>();
                        while (dr.Read())
                        {
                            oQualityReportKDDetal = new QualityReportKDDetail();
                            oQualityReportKDDetal.STT = a++;
                            oQualityReportKDDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetal.PhanloaiDV = dr["PHANLOAIDV"].ToString();
                            oQualityReportKDDetal.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetal.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetal.SL = dr["SL"].ToString();
                            oQualityReportKDDetal.KL = dr["KL"].ToString();
                            oQualityReportKDDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportKDDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportKDDetal.Add(oQualityReportKDDetal);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListQualityReportKDReport = listQualityReportKDDetal;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListQualityReportKDReport = null;
            }
            return _returnQualityTHDV;
        }
        public ReturnReportTHDV QUALITY_THDV_DETAIL_ITEM(int DONVI, int PHANLOAI, string DICHVU, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetailItem> listQualityReportKDDetalItem = null;
            QualityReportKDDetailItem oQualityReportKDDetalItem = null;
            int a = 1;
            

            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_CT_Report_TH_Theo_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_PhanloaiDV", OracleDbType.Int32).Value = PHANLOAI;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetalItem = new List<QualityReportKDDetailItem>();
                        while (dr.Read())
                        {

                            oQualityReportKDDetalItem = new QualityReportKDDetailItem();
                            oQualityReportKDDetalItem.STT = a++;
                           
                                oQualityReportKDDetalItem.Donvi = dr["DONVI"].ToString();                        
                                oQualityReportKDDetalItem.Matinhnhan = dr["MATINHNHAN"].ToString();                         
                                oQualityReportKDDetalItem.Tentinhnhan = dr["TENTINHNHAN"].ToString();                                                     
                                oQualityReportKDDetalItem.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();                         
                                oQualityReportKDDetalItem.Madvchinh = dr["MADVCHINH"].ToString();                         
                                oQualityReportKDDetalItem.TenDV = dr["TENDV"].ToString();                           
                                oQualityReportKDDetalItem.Mae1 = dr["MAE1"].ToString();                         
                                oQualityReportKDDetalItem.MaKH = dr["MAKH"].ToString();                        
                                oQualityReportKDDetalItem.TenNguoiGui = dr["TENNGUOIGUI"].ToString();                          
                                oQualityReportKDDetalItem.DiaChiGui = dr["DIACHIGUI"].ToString();                          
                                oQualityReportKDDetalItem.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();                           
                                oQualityReportKDDetalItem.DiaChiNhan = dr["DIACHINHAN"].ToString();                          
                                oQualityReportKDDetalItem.MaTinhTra = dr["MATINHTRA"].ToString();                           
                                oQualityReportKDDetalItem.Tentinhtra = dr["TENTINHTRA"].ToString();                         
                                oQualityReportKDDetalItem.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();                          
                                oQualityReportKDDetalItem.khoiluong = dr["KL"].ToString();                           
                                oQualityReportKDDetalItem.KLQD = dr["KLQD"].ToString();                          
                                oQualityReportKDDetalItem.Cuocchinh = dr["CUOCCHINH"].ToString();                           
                                oQualityReportKDDetalItem.Cuocdvct = dr["CUOCDVCT"].ToString();                          
                                oQualityReportKDDetalItem.PPXD = dr["PPXD"].ToString();                       
                                oQualityReportKDDetalItem.PPVX = dr["PPVX"].ToString();                           
                                oQualityReportKDDetalItem.PPKhac = dr["PPKHAC"].ToString();                           
                                oQualityReportKDDetalItem.Tongcuoc = dr["TONGCUOC"].ToString();
                          

                            listQualityReportKDDetalItem.Add(oQualityReportKDDetalItem);
                        }
                        _returnQualityTHDV.Code = "00";                        
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";                                               
                        _returnQualityTHDV.ListDetailedItemQualityTHDVReport = listQualityReportKDDetalItem;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListDetailedItemQualityTHDVReport = null;
            }
            return _returnQualityTHDV;
        }
        #endregion 
        #region QUALITY_DETAIL_DONVI      
        public ReturnReportTHDONVI QUALITY_THDONVI_DETAIL(int DONVI, int TINH, int PHANLOAI, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDONVI _metadataTHDONVI = new MetaDataTHDONVI();
            Convertion common = new Convertion();
            ReturnReportTHDONVI _returnQualityTHDV = new ReturnReportTHDONVI();

            _metadataTHDONVI.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDONVI = _metadataTHDONVI;
            List<QualityReportDONVIDetail> listQualityReportKDDetal = null;
            QualityReportDONVIDetail oQualityReportKDDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_TH_Theo_DonVi", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_PhanloaiDV", OracleDbType.Int32).Value = PHANLOAI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINH;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetal = new List<QualityReportDONVIDetail>();
                        while (dr.Read())
                        {
                            oQualityReportKDDetal = new QualityReportDONVIDetail();
                            oQualityReportKDDetal.STT = a++;
                            oQualityReportKDDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetal.PhanloaiDV = dr["PHANLOAIDV"].ToString();
                            oQualityReportKDDetal.SL = dr["SL"].ToString();
                            oQualityReportKDDetal.KL = dr["KL"].ToString();
                            oQualityReportKDDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportKDDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportKDDetal.Add(oQualityReportKDDetal);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListQualityReportTHDONVIReport = listQualityReportKDDetal;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListQualityReportTHDONVIReport = null;
            }
            return _returnQualityTHDV;
        }

        public ReturnReportTHDV QUALITY_THDONVI_DETAIL_ITEM(int DONVI, int TINHNHAN,int PHANLOAI, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetailItem> listQualityReportKDDetalItem = null;
            QualityReportKDDetailItem oQualityReportKDDetalItem = null;
            int a = 1;


            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_CT_Report_TH_DONVI", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_PhanloaiDV", OracleDbType.Int32).Value = PHANLOAI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetalItem = new List<QualityReportKDDetailItem>();
                        while (dr.Read())
                        {

                            oQualityReportKDDetalItem = new QualityReportKDDetailItem();
                            oQualityReportKDDetalItem.STT = a++;

                            oQualityReportKDDetalItem.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetalItem.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetalItem.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetalItem.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportKDDetalItem.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetalItem.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetalItem.Mae1 = dr["MAE1"].ToString();
                            oQualityReportKDDetalItem.MaKH = dr["MAKH"].ToString();
                            oQualityReportKDDetalItem.TenNguoiGui = dr["TENNGUOIGUI"].ToString();
                            oQualityReportKDDetalItem.DiaChiGui = dr["DIACHIGUI"].ToString();
                            oQualityReportKDDetalItem.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();
                            oQualityReportKDDetalItem.DiaChiNhan = dr["DIACHINHAN"].ToString();
                            oQualityReportKDDetalItem.MaTinhTra = dr["MATINHTRA"].ToString();
                            oQualityReportKDDetalItem.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportKDDetalItem.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            oQualityReportKDDetalItem.khoiluong = dr["KL"].ToString();
                            oQualityReportKDDetalItem.KLQD = dr["KLQD"].ToString();
                            oQualityReportKDDetalItem.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetalItem.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetalItem.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetalItem.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetalItem.PPKhac = dr["PPKHAC"].ToString();
                            oQualityReportKDDetalItem.Tongcuoc = dr["TONGCUOC"].ToString();


                            listQualityReportKDDetalItem.Add(oQualityReportKDDetalItem);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListDetailedItemQualityTHDVReport = listQualityReportKDDetalItem;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListDetailedItemQualityTHDVReport = null;
            }
            return _returnQualityTHDV;
        }
        #endregion
        #region QUALITY_DETAIL_DONVI_DICHVU      

        public ReturnReportTHDV QUALITY_THDONVIDICHVU_DETAIL_ITEM(int DONVI,int TINHNHAN, int PHANLOAI, string DICHVU, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetailItem> listQualityReportKDDetalItem = null;
            QualityReportKDDetailItem oQualityReportKDDetalItem = null;
            int a = 1;


            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_CT_Report_TH_DONVI_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_PhanloaiDV", OracleDbType.Int32).Value = PHANLOAI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetalItem = new List<QualityReportKDDetailItem>();
                        while (dr.Read())
                        {

                            oQualityReportKDDetalItem = new QualityReportKDDetailItem();
                            oQualityReportKDDetalItem.STT = a++;

                            oQualityReportKDDetalItem.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetalItem.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetalItem.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetalItem.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportKDDetalItem.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetalItem.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetalItem.Mae1 = dr["MAE1"].ToString();
                            oQualityReportKDDetalItem.MaKH = dr["MAKH"].ToString();
                            oQualityReportKDDetalItem.TenNguoiGui = dr["TENNGUOIGUI"].ToString();
                            oQualityReportKDDetalItem.DiaChiGui = dr["DIACHIGUI"].ToString();
                            oQualityReportKDDetalItem.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();
                            oQualityReportKDDetalItem.DiaChiNhan = dr["DIACHINHAN"].ToString();
                            oQualityReportKDDetalItem.MaTinhTra = dr["MATINHTRA"].ToString();
                            oQualityReportKDDetalItem.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportKDDetalItem.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            oQualityReportKDDetalItem.khoiluong = dr["KL"].ToString();
                            oQualityReportKDDetalItem.KLQD = dr["KLQD"].ToString();
                            oQualityReportKDDetalItem.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetalItem.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetalItem.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetalItem.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetalItem.PPKhac = dr["PPKHAC"].ToString();
                            oQualityReportKDDetalItem.Tongcuoc = dr["TONGCUOC"].ToString();


                            listQualityReportKDDetalItem.Add(oQualityReportKDDetalItem);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListDetailedItemQualityTHDVReport = listQualityReportKDDetalItem;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListDetailedItemQualityTHDVReport = null;
            }
            return _returnQualityTHDV;
        }
        public ReturnReportTHDONVIDICHVU QUALITY_THDONVIDICHVU_DETAIL(int DONVI, int TINH, int PHANLOAI, string DICHVU, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDONVIDICHVU _metadataTHDONVI = new MetaDataTHDONVIDICHVU();
            Convertion common = new Convertion();
            ReturnReportTHDONVIDICHVU _returnQualityTHDV = new ReturnReportTHDONVIDICHVU();

            _metadataTHDONVI.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDONVIDICHVU = _metadataTHDONVI;
            List<QualityReportDONVIDICHVUDetail> listQualityReportKDDetal = null;
            QualityReportDONVIDICHVUDetail oQualityReportKDDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_TH_DonVi_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_PhanloaiDV", OracleDbType.Int32).Value = PHANLOAI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINH;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetal = new List<QualityReportDONVIDICHVUDetail>();
                        while (dr.Read())
                        {
                            oQualityReportKDDetal = new QualityReportDONVIDICHVUDetail();
                            oQualityReportKDDetal.STT = a++;
                            oQualityReportKDDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetal.PhanloaiDV = dr["PHANLOAIDV"].ToString();
                            oQualityReportKDDetal.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetal.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetal.SL = dr["SL"].ToString();
                            oQualityReportKDDetal.KL = dr["KL"].ToString();
                            oQualityReportKDDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportKDDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportKDDetal.Add(oQualityReportKDDetal);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListQualityReportTHDONVIDICHVUReport = listQualityReportKDDetal;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListQualityReportTHDONVIDICHVUReport = null;
            }
            return _returnQualityTHDV;
        }
        #endregion
        #region QUALITY_DETAIL_TINH_TINH
        public ReturnReportTINHTINH QUALITY_THTINHTINH_DETAIL(int DONVI, int TINHNHAN, int TINHTRA, string DICHVU, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHTINHTINH _metadataTHTINHTINH = new MetaDataTHTINHTINH();
            Convertion common = new Convertion();
            ReturnReportTINHTINH _returnQualityTINHTINH = new ReturnReportTINHTINH();

            _metadataTHTINHTINH.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTINHTINH.MetaDataTINHTINH = _metadataTHTINHTINH;
            List<QualityReportTINHTINHDetail> listQualityReportTINHTINHDetal = null;
            QualityReportTINHTINHDetail oQualityReportTINHTINHDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_Tinh_Tinh_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.Int32).Value = TINHTRA;       
                    //if(DICHVU =="0")
                    //{
                    //    DICHVU = null;
                    //}
                    //else
                    //{
                    //    DICHVU = DICHVU;
                    //}
                              
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportTINHTINHDetal = new List<QualityReportTINHTINHDetail>();
                        while (dr.Read())
                        {
                            oQualityReportTINHTINHDetal = new QualityReportTINHTINHDetail();
                            oQualityReportTINHTINHDetal.STT = a++;
                            oQualityReportTINHTINHDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportTINHTINHDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportTINHTINHDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportTINHTINHDetal.Matinhtra = dr["MATINHTRA"].ToString();
                            oQualityReportTINHTINHDetal.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportTINHTINHDetal.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportTINHTINHDetal.TenDV = dr["TENDV"].ToString();
                            oQualityReportTINHTINHDetal.NacKL = dr["NACKL"].ToString();
                            oQualityReportTINHTINHDetal.TenNacKL = dr["TENNACKL"].ToString();
                            oQualityReportTINHTINHDetal.SL = dr["SL"].ToString();
                            oQualityReportTINHTINHDetal.KL = dr["KL"].ToString();
                            oQualityReportTINHTINHDetal.KLQD = dr["KLQD"].ToString();
                            oQualityReportTINHTINHDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportTINHTINHDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportTINHTINHDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportTINHTINHDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportTINHTINHDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportTINHTINHDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportTINHTINHDetal.Add(oQualityReportTINHTINHDetal);
                        }
                        _returnQualityTINHTINH.Code = "00";
                        _returnQualityTINHTINH.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTINHTINH.ListDetailedQualityTHTINHTINHReport = listQualityReportTINHTINHDetal;
                    }
                    else
                    {
                        _returnQualityTINHTINH.Code = "01";
                        _returnQualityTINHTINH.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTINHTINH.Code = "99";
                _returnQualityTINHTINH.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTINHTINH.ListDetailedQualityTHTINHTINHReport = null;
            }
            return _returnQualityTINHTINH;
        }

        public ReturnReportTHCH QUALITY_THCH_DETAIL(int DONVI, int TINHNHAN, int TINHTRA, string DICHVU, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHCH _metadataTHCH = new MetaDataTHCH();
            Convertion common = new Convertion();
            ReturnReportTHCH _returnQualityTHCH = new ReturnReportTHCH();

            _metadataTHCH.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHCH.MetaDataTHCH = _metadataTHCH;
            List<QualityReportTHCHDetail> listQualityReportTHCHDetail = null;
            QualityReportTHCHDetail oQualityReportTHCHDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_TH_CH", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.Int32).Value = TINHTRA;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportTHCHDetail = new List<QualityReportTHCHDetail>();
                        while (dr.Read())
                        {
                            oQualityReportTHCHDetail = new QualityReportTHCHDetail();
                            oQualityReportTHCHDetail.STT = a++;
                            oQualityReportTHCHDetail.Donvi = dr["DONVI"].ToString();
                            oQualityReportTHCHDetail.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportTHCHDetail.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportTHCHDetail.Matinhtra = dr["MATINHTRA"].ToString();
                            oQualityReportTHCHDetail.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportTHCHDetail.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportTHCHDetail.TenDV = dr["TENDV"].ToString();
                            oQualityReportTHCHDetail.NacKL = dr["NACKL"].ToString();
                            oQualityReportTHCHDetail.TenNacKL = dr["TENNACKL"].ToString();
                            oQualityReportTHCHDetail.SL = dr["SL"].ToString();
                            oQualityReportTHCHDetail.KL = dr["KL"].ToString();
                            oQualityReportTHCHDetail.KLQD = dr["KLQD"].ToString();
                            oQualityReportTHCHDetail.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportTHCHDetail.PPXD = dr["PPXD"].ToString();
                            oQualityReportTHCHDetail.PPVX = dr["PPVX"].ToString();
                            oQualityReportTHCHDetail.PPMD = dr["PPMD"].ToString();
                            oQualityReportTHCHDetail.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportTHCHDetail.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportTHCHDetail.Add(oQualityReportTHCHDetail);
                        }
                        _returnQualityTHCH.Code = "00";
                        _returnQualityTHCH.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHCH.ListDetailedQualityTHCHReport = listQualityReportTHCHDetail;
                    }
                    else
                    {
                        _returnQualityTHCH.Code = "01";
                        _returnQualityTHCH.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHCH.Code = "99";
                _returnQualityTHCH.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHCH.ListDetailedQualityTHCHReport = null;
            }
            return _returnQualityTHCH;
        }
        public ReturnReportTHDV QUALITY_THTINHTINH_DETAIL_ITEM(int DONVI, int TINHNHAN, int TINHTRA, string DICHVU, int startdate, int enddate, int NACKL)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetailItem> listQualityReportKDDetalItem = null;
            QualityReportKDDetailItem oQualityReportKDDetalItem = null;
            int a = 1;


            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_CT_Report_TH_TINH_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.Int32).Value = TINHTRA;                                    
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = DICHVU;
                    myCommand.Parameters.Add("v_Nackl", OracleDbType.Int32).Value = NACKL;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetalItem = new List<QualityReportKDDetailItem>();
                        while (dr.Read())
                        {

                            oQualityReportKDDetalItem = new QualityReportKDDetailItem();
                            oQualityReportKDDetalItem.STT = a++;

                            oQualityReportKDDetalItem.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetalItem.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetalItem.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetalItem.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportKDDetalItem.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetalItem.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetalItem.Mae1 = dr["MAE1"].ToString();
                            oQualityReportKDDetalItem.MaKH = dr["MAKH"].ToString();
                            oQualityReportKDDetalItem.TenNguoiGui = dr["TENNGUOIGUI"].ToString();
                            oQualityReportKDDetalItem.DiaChiGui = dr["DIACHIGUI"].ToString();
                            oQualityReportKDDetalItem.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();
                            oQualityReportKDDetalItem.DiaChiNhan = dr["DIACHINHAN"].ToString();
                            oQualityReportKDDetalItem.MaTinhTra = dr["MATINHTRA"].ToString();
                            oQualityReportKDDetalItem.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportKDDetalItem.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            oQualityReportKDDetalItem.khoiluong = dr["KL"].ToString();
                            oQualityReportKDDetalItem.KLQD = dr["KLQD"].ToString();
                            oQualityReportKDDetalItem.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetalItem.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetalItem.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetalItem.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetalItem.PPKhac = dr["PPKHAC"].ToString();
                            oQualityReportKDDetalItem.Tongcuoc = dr["TONGCUOC"].ToString();


                            listQualityReportKDDetalItem.Add(oQualityReportKDDetalItem);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListDetailedItemQualityTHDVReport = listQualityReportKDDetalItem;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListDetailedItemQualityTHDVReport = null;
            }
            return _returnQualityTHDV;
        }

        #endregion

        #region huyen huyen
        public ReturnReportHUYENHUYEN QUALITY_THHUYENHUYEN_DETAIL(int DONVI, int TINHNHAN, int HUYENNHAN, int TINHTRA, int HUYENTRA, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHHUYENHUYEN _metadataTHHUYENHUYEN = new MetaDataTHHUYENHUYEN();
            Convertion common = new Convertion();
            ReturnReportHUYENHUYEN _returnQualityHUYENHUYEN = new ReturnReportHUYENHUYEN();

            _metadataTHHUYENHUYEN.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityHUYENHUYEN.MetaDataHUYENHUYEN = _metadataTHHUYENHUYEN;
            List<QualityReportHUYENHUYENDetail> listQualityReportHUYENHUYENDetal = null;
            QualityReportHUYENHUYENDetail oQualityReportHUYENHUYENDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_Huyen_Huyen_DV", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_HuyenNhan", OracleDbType.Int32).Value = HUYENNHAN;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.Int32).Value = TINHTRA;
                    myCommand.Parameters.Add("v_HuyenTra", OracleDbType.Int32).Value = HUYENTRA;


                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportHUYENHUYENDetal = new List<QualityReportHUYENHUYENDetail>();
                        while (dr.Read())
                        {
                            oQualityReportHUYENHUYENDetal = new QualityReportHUYENHUYENDetail();
                            oQualityReportHUYENHUYENDetal.STT = a++;
                            oQualityReportHUYENHUYENDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportHUYENHUYENDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportHUYENHUYENDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();

                            oQualityReportHUYENHUYENDetal.Mahuyennhan = dr["MAHUYENNHAN"].ToString();
                            oQualityReportHUYENHUYENDetal.Tenhuyennhan = dr["TENHUYENNHAN"].ToString();

                            oQualityReportHUYENHUYENDetal.Matinhtra = dr["MATINHTRA"].ToString();
                            oQualityReportHUYENHUYENDetal.Tentinhtra = dr["TENTINHTRA"].ToString();

                            oQualityReportHUYENHUYENDetal.Mahuyentra = dr["MAHUYENTRA"].ToString();
                            oQualityReportHUYENHUYENDetal.Tenhuyentra = dr["TENHUYENTRA"].ToString();


                            oQualityReportHUYENHUYENDetal.NacKL = dr["NACKL"].ToString();
                            oQualityReportHUYENHUYENDetal.TenNacKL = dr["TENNACKL"].ToString();
                            oQualityReportHUYENHUYENDetal.SL = dr["SL"].ToString();
                            oQualityReportHUYENHUYENDetal.KL = dr["KL"].ToString();
                            oQualityReportHUYENHUYENDetal.KLQD = dr["KLQD"].ToString();
                            oQualityReportHUYENHUYENDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportHUYENHUYENDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportHUYENHUYENDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportHUYENHUYENDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportHUYENHUYENDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportHUYENHUYENDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportHUYENHUYENDetal.Add(oQualityReportHUYENHUYENDetal);
                        }
                        _returnQualityHUYENHUYEN.Code = "00";
                        _returnQualityHUYENHUYEN.Message = "Lấy dữ liệu thành công.";
                        _returnQualityHUYENHUYEN.ListDetailedQualityTHHUYENHUYENReport = listQualityReportHUYENHUYENDetal;
                    }
                    else
                    {
                        _returnQualityHUYENHUYEN.Code = "01";
                        _returnQualityHUYENHUYEN.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityHUYENHUYEN.Code = "99";
                _returnQualityHUYENHUYEN.Message = "Lỗi xử lý dữ liệu";
                _returnQualityHUYENHUYEN.ListDetailedQualityTHHUYENHUYENReport = null;
            }
            return _returnQualityHUYENHUYEN;
        }


        public ReturnReportTHDV QUALITY_THHUYENHUYEN_DETAIL_ITEM(int DONVI, int TINHNHAN, int HUYENNHAN, int TINHTRA, int HUYENTRA, int startdate, int enddate,int Nackl)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetailItem> listQualityReportKDDetalItem = null;
            QualityReportKDDetailItem oQualityReportKDDetalItem = null;
            int a = 1;


            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_CT_Report_Huyen", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_HuyenNhan", OracleDbType.Int32).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.Int32).Value = TINHTRA;
                    myCommand.Parameters.Add("v_HuyenTra", OracleDbType.Int32).Value = TINHTRA;                   
                    myCommand.Parameters.Add("v_NacKL", OracleDbType.Int32).Value = Nackl;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetalItem = new List<QualityReportKDDetailItem>();
                        while (dr.Read())
                        {

                            oQualityReportKDDetalItem = new QualityReportKDDetailItem();
                            oQualityReportKDDetalItem.STT = a++;

                            oQualityReportKDDetalItem.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetalItem.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetalItem.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetalItem.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportKDDetalItem.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetalItem.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetalItem.Mae1 = dr["MAE1"].ToString();
                            oQualityReportKDDetalItem.MaKH = dr["MAKH"].ToString();
                            oQualityReportKDDetalItem.TenNguoiGui = dr["TENNGUOIGUI"].ToString();
                            oQualityReportKDDetalItem.DiaChiGui = dr["DIACHIGUI"].ToString();
                            oQualityReportKDDetalItem.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();
                            oQualityReportKDDetalItem.DiaChiNhan = dr["DIACHINHAN"].ToString();
                            oQualityReportKDDetalItem.MaTinhTra = dr["MATINHTRA"].ToString();
                            oQualityReportKDDetalItem.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportKDDetalItem.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            oQualityReportKDDetalItem.khoiluong = dr["KL"].ToString();
                            oQualityReportKDDetalItem.KLQD = dr["KLQD"].ToString();
                            oQualityReportKDDetalItem.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetalItem.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetalItem.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetalItem.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetalItem.PPKhac = dr["PPKHAC"].ToString();
                            oQualityReportKDDetalItem.Tongcuoc = dr["TONGCUOC"].ToString();


                            listQualityReportKDDetalItem.Add(oQualityReportKDDetalItem);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListDetailedItemQualityTHDVReport = listQualityReportKDDetalItem;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListDetailedItemQualityTHDVReport = null;
            }
            return _returnQualityTHDV;
        }
        #endregion
        #region QT DI
        public ReturnReportQTDI QUALITY_THQTDI_DETAIL(int MADV, int DONVI, string NCNHAN, int PHANLOAI, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataQTDI _metadataTHQTDI = new MetaDataQTDI();
            Convertion common = new Convertion();
            ReturnReportQTDI _returnQualityQTDI = new ReturnReportQTDI();

            _metadataTHQTDI.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityQTDI.MetaDataQTDI = _metadataTHQTDI;
            List<QualityReportTHQTDIDetail> listQualityReportQTDIDetal = null;
            QualityReportTHQTDIDetail oQualityReportQTDIDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_CT_E1_QT_Di", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_PhanLoaiDV", OracleDbType.Int32).Value = MADV;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_NuocNhan", OracleDbType.NVarchar2).Value = NCNHAN;
                    myCommand.Parameters.Add("v_PhanLoaiHH", OracleDbType.Int32).Value = PHANLOAI;


                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportQTDIDetal = new List<QualityReportTHQTDIDetail>();
                        while (dr.Read())
                        {
                            oQualityReportQTDIDetal = new QualityReportTHQTDIDetail();
                            oQualityReportQTDIDetal.STT = a++;
                            oQualityReportQTDIDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportQTDIDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportQTDIDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportQTDIDetal.Mae1 = dr["MAE1"].ToString();
                            oQualityReportQTDIDetal.Manuoctra = dr["MANUOCTRA"].ToString();
                            oQualityReportQTDIDetal.Ten_Nuoc = dr["TEN_NUOC"].ToString();
                            oQualityReportQTDIDetal.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportQTDIDetal.Madvchinh = dr["MADVCHINH"].ToString();
                           // oQualityReportQTDIDetal.TenDV = dr["TENDV"].ToString();
                            oQualityReportQTDIDetal.PhanLoai = dr["PHANLOAI"].ToString();
                            oQualityReportQTDIDetal.khoiluong = dr["KL"].ToString();
                            oQualityReportQTDIDetal.KLQD = dr["KLQD"].ToString();
                            oQualityReportQTDIDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportQTDIDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportQTDIDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportQTDIDetal.PPHK = dr["PPHK"].ToString();
                            oQualityReportQTDIDetal.PPKC = dr["PPKC"].ToString();
                            oQualityReportQTDIDetal.PPKhac = dr["PPKHAC"].ToString();
                            oQualityReportQTDIDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            oQualityReportQTDIDetal.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            listQualityReportQTDIDetal.Add(oQualityReportQTDIDetal);
                        }
                        _returnQualityQTDI.Code = "00";
                        _returnQualityQTDI.Message = "Lấy dữ liệu thành công.";
                        _returnQualityQTDI.ListDetailedQualityTHQTDIReport = listQualityReportQTDIDetal;
                    }
                    else
                    {
                        _returnQualityQTDI.Code = "01";
                        _returnQualityQTDI.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityQTDI.Code = "99";
                _returnQualityQTDI.Message = "Lỗi xử lý dữ liệu";
                _returnQualityQTDI.ListDetailedQualityTHQTDIReport = null;
            }
            return _returnQualityQTDI;
        }
        #endregion


        #region QT DI
        public ReturnReportQTDEN QUALITY_THQTDEN_DETAIL(int NUOCOE, string NCNHAN, int PHANLOAI, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataQTDEN _metadataTHQTDEN = new MetaDataQTDEN();
            Convertion common = new Convertion();
            ReturnReportQTDEN _returnQualityQTDEN = new ReturnReportQTDEN();

            _metadataTHQTDEN.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityQTDEN.MetaDataQTDEN = _metadataTHQTDEN;
            List<QualityReportTHQTDENDetail> listQualityReportQTDENDetal = null;
            QualityReportTHQTDENDetail oQualityReportQTDENDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_CT_E1_QT_Den", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_NuocNhan", OracleDbType.NVarchar2).Value = NCNHAN;
                    myCommand.Parameters.Add("v_OENhan", OracleDbType.Int32).Value = NUOCOE;
                    myCommand.Parameters.Add("v_PhanLoaiHH", OracleDbType.Int32).Value = PHANLOAI;
                  


                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportQTDENDetal = new List<QualityReportTHQTDENDetail>();
                        while (dr.Read())
                        {
                            oQualityReportQTDENDetal = new QualityReportTHQTDENDetail();
                            oQualityReportQTDENDetal.STT = a++;
                            oQualityReportQTDENDetal.Mae1 = dr["MAE1"].ToString();
                            oQualityReportQTDENDetal.Mancnhan = dr["MANCNHAN"].ToString();
                            oQualityReportQTDENDetal.Ten_Nuoc = dr["TEN_NUOC"].ToString();
                            oQualityReportQTDENDetal.Ngay_Den = dr["NGAY_DEN"].ToString();
                            oQualityReportQTDENDetal.OE_Nhan = dr["OE_NHAN"].ToString();
                            oQualityReportQTDENDetal.PhanLoai = dr["PHANLOAI"].ToString();
                            oQualityReportQTDENDetal.Khoiluong = dr["KL"].ToString();
                            oQualityReportQTDENDetal.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            listQualityReportQTDENDetal.Add(oQualityReportQTDENDetal);
                        }
                        _returnQualityQTDEN.Code = "00";
                        _returnQualityQTDEN.Message = "Lấy dữ liệu thành công.";
                        _returnQualityQTDEN.ListDetailedQualityTHQTDENReport = listQualityReportQTDENDetal;
                    }
                    else
                    {
                        _returnQualityQTDEN.Code = "01";
                        _returnQualityQTDEN.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityQTDEN.Code = "99";
                _returnQualityQTDEN.Message = "Lỗi xử lý dữ liệu";
                _returnQualityQTDEN.ListDetailedQualityTHQTDENReport = null;
            }
            return _returnQualityQTDEN;
        }
        #endregion

        #region E1 TN
        public ReturnReportE1TN QUALITY_THE1TN_DETAIL(int donvi, int tinhnhan, string dichvu,string khachhang, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataE1TN _metadataTHE1TN = new MetaDataE1TN();
            Convertion common = new Convertion();
            ReturnReportE1TN _returnQualityE1TN = new ReturnReportE1TN();

            _metadataTHE1TN.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityE1TN.MetaDataE1TN = _metadataTHE1TN;
            List<QualityReportTHE1TNDetail> listQualityReportE1TNDetal = null;
            QualityReportTHE1TNDetail oQualityReportE1TNDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_CT_E1_TN", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = donvi;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = tinhnhan;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = dichvu;
                    //if(khachhang=="")
                    //{
                    //    khachhang = null;
                    //}
                    //else
                    //{
                    //    khachhang=khachhang;

                    //}
                    myCommand.Parameters.Add("v_MaKH", OracleDbType.Varchar2).Value = khachhang;


                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportE1TNDetal = new List<QualityReportTHE1TNDetail>();
                        while (dr.Read())
                        {
                            oQualityReportE1TNDetal = new QualityReportTHE1TNDetail();
                            oQualityReportE1TNDetal.STT = a++;
                            try
                            {
                                oQualityReportE1TNDetal.Donvi = dr["DONVI"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.Madvchinh = dr["MADVCHINH"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.TenDV = dr["TENDV"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.Mae1 = dr["MAE1"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.MaKH = dr["MAKH"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.TenNguoiGui = dr["TENNGUOIGUI"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }


                            try
                            {
                                oQualityReportE1TNDetal.DiaChiGui = dr["DIACHIGUI"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.DiaChiNhan = dr["DIACHINHAN"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.MaTinhTra = dr["MATINHTRA"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.Tentinhtra = dr["TENTINHTRA"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.khoiluong = dr["KL"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.KLQD = dr["KLQD"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                oQualityReportE1TNDetal.PPXD = dr["PPXD"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.PPVX = dr["PPVX"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                oQualityReportE1TNDetal.PPKhac = dr["PPKHAC"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {


                                oQualityReportE1TNDetal.Tongcuoc = dr["TONGCUOC"].ToString();
                            }
                            catch (Exception ex)
                            {

                            }


                            listQualityReportE1TNDetal.Add(oQualityReportE1TNDetal);

                            _returnQualityE1TN.Code = "00";
                            _returnQualityE1TN.Message = "Lấy dữ liệu thành công.";
                            _returnQualityE1TN.ListDetailedQualityTHE1TNReport = listQualityReportE1TNDetal;
                        }
                    }
                    else
                    {
                        _returnQualityE1TN.Code = "01";
                        _returnQualityE1TN.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityE1TN.Code = "99";
                _returnQualityE1TN.Message = "Lỗi xử lý dữ liệu";
                _returnQualityE1TN.ListDetailedQualityTHE1TNReport = null;
            }
            return _returnQualityE1TN;
        }
        #endregion


        #region TH DVCT
        public ReturnReportTHDVCT QUALITY_THDVCT_DETAIL(int donvi, string dichvu, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDVCT _metadataTHDVCT = new MetaDataTHDVCT();
            Convertion common = new Convertion();
            ReturnReportTHDVCT _returnQualityTHDVCT = new ReturnReportTHDVCT();

            _metadataTHDVCT.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDVCT.MetaDataTHDVCT = _metadataTHDVCT;
            List<QualityReportTHDVCTDetail> listQualityReportTHDVCTDetal = null;
            QualityReportTHDVCTDetail oQualityReportTHDVCTDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_TH_DVCT", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = donvi;                 
                    myCommand.Parameters.Add("v_MaDVCT", OracleDbType.NVarchar2).Value = dichvu;
                  

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportTHDVCTDetal = new List<QualityReportTHDVCTDetail>();
                        while (dr.Read())
                        {
                            oQualityReportTHDVCTDetal = new QualityReportTHDVCTDetail();
                            oQualityReportTHDVCTDetal.STT = a++;
                            oQualityReportTHDVCTDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportTHDVCTDetal.Madvct = dr["MADVCT"].ToString();
                            oQualityReportTHDVCTDetal.TenDV = dr["TENDV"].ToString();
                            oQualityReportTHDVCTDetal.SL = dr["SL"].ToString();
                            oQualityReportTHDVCTDetal.KL = dr["KL"].ToString();
                            oQualityReportTHDVCTDetal.KLQD = dr["KLQD"].ToString();
                            oQualityReportTHDVCTDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportTHDVCTDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportTHDVCTDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportTHDVCTDetal.PPMD = dr["PPMD"].ToString();
                          
                            oQualityReportTHDVCTDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportTHDVCTDetal.Tongcuoc = dr["TONGCUOC"].ToString();


                            listQualityReportTHDVCTDetal.Add(oQualityReportTHDVCTDetal);
                        }
                        _returnQualityTHDVCT.Code = "00";
                        _returnQualityTHDVCT.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDVCT.ListDetailedQualityTHDVCTReport = listQualityReportTHDVCTDetal;
                    }
                    else
                    {
                        _returnQualityTHDVCT.Code = "01";
                        _returnQualityTHDVCT.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDVCT.Code = "99";
                _returnQualityTHDVCT.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDVCT.ListDetailedQualityTHDVCTReport = null;
            }
            return _returnQualityTHDVCT;
        }

        public ReturnReportTHDV QUALITY_THDVCT_DETAIL_ITEM(int donvi, string dichvu, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataTHDV _metadataTHDV = new MetaDataTHDV();
            Convertion common = new Convertion();
            ReturnReportTHDV _returnQualityTHDV = new ReturnReportTHDV();

            _metadataTHDV.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityTHDV.MetaDataTHDV = _metadataTHDV;
            List<QualityReportKDDetailItem> listQualityReportKDDetalItem = null;
            QualityReportKDDetailItem oQualityReportKDDetalItem = null;
            int a = 1;


            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_CT_Report_TH_DVCT", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = donvi;
                    myCommand.Parameters.Add("v_MaDVCT", OracleDbType.NVarchar2).Value = dichvu;

                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportKDDetalItem = new List<QualityReportKDDetailItem>();
                        while (dr.Read())
                        {

                            oQualityReportKDDetalItem = new QualityReportKDDetailItem();
                            oQualityReportKDDetalItem.STT = a++;

                            oQualityReportKDDetalItem.Donvi = dr["DONVI"].ToString();
                            oQualityReportKDDetalItem.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportKDDetalItem.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportKDDetalItem.NgayPhatHanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportKDDetalItem.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportKDDetalItem.TenDV = dr["TENDV"].ToString();
                            oQualityReportKDDetalItem.Mae1 = dr["MAE1"].ToString();
                            oQualityReportKDDetalItem.MaKH = dr["MAKH"].ToString();
                            oQualityReportKDDetalItem.TenNguoiGui = dr["TENNGUOIGUI"].ToString();
                            oQualityReportKDDetalItem.DiaChiGui = dr["DIACHIGUI"].ToString();
                            oQualityReportKDDetalItem.TenNguoiNhan = dr["TENNGUOINHAN"].ToString();
                            oQualityReportKDDetalItem.DiaChiNhan = dr["DIACHINHAN"].ToString();
                            oQualityReportKDDetalItem.MaTinhTra = dr["MATINHTRA"].ToString();
                            oQualityReportKDDetalItem.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportKDDetalItem.TrangThaiPhat = dr["TRANGTHAIPHAT"].ToString();
                            oQualityReportKDDetalItem.khoiluong = dr["KL"].ToString();
                            oQualityReportKDDetalItem.KLQD = dr["KLQD"].ToString();
                            oQualityReportKDDetalItem.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportKDDetalItem.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportKDDetalItem.PPXD = dr["PPXD"].ToString();
                            oQualityReportKDDetalItem.PPVX = dr["PPVX"].ToString();
                            oQualityReportKDDetalItem.PPKhac = dr["PPKHAC"].ToString();
                            oQualityReportKDDetalItem.Tongcuoc = dr["TONGCUOC"].ToString();


                            listQualityReportKDDetalItem.Add(oQualityReportKDDetalItem);
                        }
                        _returnQualityTHDV.Code = "00";
                        _returnQualityTHDV.Message = "Lấy dữ liệu thành công.";
                        _returnQualityTHDV.ListDetailedItemQualityTHDVReport = listQualityReportKDDetalItem;
                    }
                    else
                    {
                        _returnQualityTHDV.Code = "01";
                        _returnQualityTHDV.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityTHDV.Code = "99";
                _returnQualityTHDV.Message = "Lỗi xử lý dữ liệu";
                _returnQualityTHDV.ListDetailedItemQualityTHDVReport = null;
            }
            return _returnQualityTHDV;
        }
        #endregion



        #region CT DVCT
        public ReturnReportCTDVCT QUALITY_CTDVCT_DETAIL(int donvi,int tinhnhan,int tinhtra ,string dichvu,string dichvuct, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataCTDVCT _metadataCTDVCT = new MetaDataCTDVCT();
            Convertion common = new Convertion();
            ReturnReportCTDVCT _returnQualityCTDVCT = new ReturnReportCTDVCT();

            _metadataCTDVCT.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityCTDVCT.MetaDataCTDVCT = _metadataCTDVCT;
            List<QualityReportCTDVCTDetail> listQualityReportCTDVCTDetal = null;
            QualityReportCTDVCTDetail oQualityReportCTDVCTDetal = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_CT_DVCT", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = donvi;
                    myCommand.Parameters.Add("v_MaDVCT", OracleDbType.NVarchar2).Value = dichvuct;
                    myCommand.Parameters.Add("v_MaDV", OracleDbType.NVarchar2).Value = dichvu;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.Int32).Value = tinhnhan;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.Int32).Value = tinhtra;


                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportCTDVCTDetal = new List<QualityReportCTDVCTDetail>();
                        while (dr.Read())
                        {
                            oQualityReportCTDVCTDetal = new QualityReportCTDVCTDetail();
                            oQualityReportCTDVCTDetal.STT = a++;
                            oQualityReportCTDVCTDetal.Donvi = dr["DONVI"].ToString();
                            oQualityReportCTDVCTDetal.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportCTDVCTDetal.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportCTDVCTDetal.Matinhtra = dr["MATINHTRA"].ToString();
                            oQualityReportCTDVCTDetal.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportCTDVCTDetal.Madvct = dr["MADVCT"].ToString();
                            oQualityReportCTDVCTDetal.TenDVCT = dr["TENDVCT"].ToString();
                            oQualityReportCTDVCTDetal.Madvchinh = dr["MADVCHINH"].ToString();
                            oQualityReportCTDVCTDetal.TenDVChinh = dr["TENDVCHINH"].ToString();
                            oQualityReportCTDVCTDetal.SL = dr["SL"].ToString();

                            oQualityReportCTDVCTDetal.KL = dr["KL"].ToString();
                            oQualityReportCTDVCTDetal.KLQD = dr["KLQD"].ToString();
                            oQualityReportCTDVCTDetal.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportCTDVCTDetal.PPXD = dr["PPXD"].ToString();
                            oQualityReportCTDVCTDetal.PPVX = dr["PPVX"].ToString();
                            oQualityReportCTDVCTDetal.PPMD = dr["PPMD"].ToString();
                            oQualityReportCTDVCTDetal.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportCTDVCTDetal.Tongcuoc = dr["TONGCUOC"].ToString();

                            listQualityReportCTDVCTDetal.Add(oQualityReportCTDVCTDetal);
                        }
                        _returnQualityCTDVCT.Code = "00";
                        _returnQualityCTDVCT.Message = "Lấy dữ liệu thành công.";
                        _returnQualityCTDVCT.ListDetailedQualityCTDVCTReport = listQualityReportCTDVCTDetal;
                    }
                    else
                    {
                        _returnQualityCTDVCT.Code = "01";
                        _returnQualityCTDVCT.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityCTDVCT.Code = "99";
                _returnQualityCTDVCT.Message = "Lỗi xử lý dữ liệu";
                _returnQualityCTDVCT.ListDetailedQualityCTDVCTReport = null;
            }
            return _returnQualityCTDVCT;
        }
        #endregion
        #region CT_LO
        public ReturnReportCTLO QUALITY_CTLO_DETAIL(int DONVI, string TINHNHAN, string TINHTRA, int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaDataCTLO _metadataCTLO = new MetaDataCTLO();
            Convertion common = new Convertion();
            ReturnReportCTLO _returnQualityCTLO = new ReturnReportCTLO();

            _metadataCTLO.from_to_date = "Từ ngày " + common.Convert_Date(startdate) + " đến ngày " + common.Convert_Date(enddate);

            _returnQualityCTLO.MetaDataCTLO = _metadataCTLO;
            List<QualityReportCTLODetail> listQualityReportCTLODetail = null;
            QualityReportCTLODetail oQualityReportCTLODetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("ems.reportbusinessweb.Get_List_Report_CT_Theo_DV_LO", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
                    myCommand.Parameters.Add("v_DonVi", OracleDbType.Int32).Value = DONVI;
                    myCommand.Parameters.Add("v_TinhNhan", OracleDbType.NVarchar2).Value = TINHNHAN;
                    myCommand.Parameters.Add("v_TinhTra", OracleDbType.NVarchar2).Value = TINHTRA;


                    myCommand.Parameters.Add(new OracleParameter("v_DataReport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listQualityReportCTLODetail = new List<QualityReportCTLODetail>();
                        while (dr.Read())
                        {
                            oQualityReportCTLODetail = new QualityReportCTLODetail();
                            oQualityReportCTLODetail.STT = a++;
                            oQualityReportCTLODetail.Donvi = dr["DONVI"].ToString();
                            oQualityReportCTLODetail.Mae1 = dr["MAE1"].ToString();
                            oQualityReportCTLODetail.So_lo = dr["SO_LO"].ToString();
                            oQualityReportCTLODetail.Ngayphathanh = dr["NGAYPHATHANH"].ToString();
                            oQualityReportCTLODetail.Tentinhnhan = dr["TENTINHNHAN"].ToString();
                            oQualityReportCTLODetail.Matinhnhan = dr["MATINHNHAN"].ToString();
                            oQualityReportCTLODetail.Matinhtra = dr["MATINHTRA"].ToString();
                            oQualityReportCTLODetail.Tentinhtra = dr["TENTINHTRA"].ToString();
                            oQualityReportCTLODetail.KL = dr["KL"].ToString();
                            oQualityReportCTLODetail.KLQD = dr["KLQD"].ToString();
                            oQualityReportCTLODetail.Cuocchinh = dr["CUOCCHINH"].ToString();
                            oQualityReportCTLODetail.Cuocdvct = dr["CUOCDVCT"].ToString();
                            oQualityReportCTLODetail.PPXD = dr["PPXD"].ToString();
                            oQualityReportCTLODetail.PPVX = dr["PPVX"].ToString();
                            oQualityReportCTLODetail.PPKC = dr["PPKC"].ToString();
                            oQualityReportCTLODetail.Tongcuoc = dr["TONGCUOC"].ToString();
                            listQualityReportCTLODetail.Add(oQualityReportCTLODetail);
                        }
                        _returnQualityCTLO.Code = "00";
                        _returnQualityCTLO.Message = "Lấy dữ liệu thành công.";
                        _returnQualityCTLO.ListQualityCTLOReport = listQualityReportCTLODetail;
                    }
                    else
                    {
                        _returnQualityCTLO.Code = "01";
                        _returnQualityCTLO.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnQualityCTLO.Code = "99";
                _returnQualityCTLO.Message = "Lỗi xử lý dữ liệu";
                _returnQualityCTLO.ListQualityCTLOReport = null;
            }
            return _returnQualityCTLO;
        }

    #endregion

    }

    

}

