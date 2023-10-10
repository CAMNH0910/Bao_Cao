using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Models.DataModel;

using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using T41.Areas.Admin.Model.DataModel;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Wordprocessing;

namespace T41.Areas.Admin.Data
{
    public class KPI_BD10_Hub_SLRepository
    {
        public ReturnKPI_BD10_Hub_SL KPI_BD10_Hub_SL(int startdate, int enddate, string bc37)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPI_BD10_Hub_SL _returnPostMan = new ReturnKPI_BD10_Hub_SL();


            List<KPI_BD10_Hub_SL> listTHTCDetail = null;
            KPI_BD10_Hub_SL oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                SqlConnection sqlConnection = new SqlConnection();

                sqlConnection = new SqlConnection("Server=192.168.68.165; database=DBHANH; uid=hanhbd; pwd=hanhbd123");

                // Gọi vào DB để lấy dữ liệu.
                SqlCommand myCommand = new SqlCommand("List_SL_DT_BD13", sqlConnection);
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                //xử lý tham số truyền vào data table
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@Fromdate", SqlDbType.BigInt).Value = startdate;
                myCommand.Parameters.Add("@Todate", SqlDbType.BigInt).Value = enddate;
                myCommand.Parameters.Add("@bc37code", SqlDbType.NVarChar).Value = bc37;
                myCommand.CommandTimeout = 150;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    listTHTCDetail = new List<KPI_BD10_Hub_SL>();
                    while (dr.Read())
                    {
                        oTHTCDetail = new KPI_BD10_Hub_SL();
                        oTHTCDetail.STT = a++;
                        oTHTCDetail.Duong_Thu = dr["Duong_Thu"].ToString();
                        oTHTCDetail.Ten_Duong_Thu = dr["Ten_Duong_Thu"].ToString();
                        oTHTCDetail.BD10 = dr["BD10"].ToString();
                        oTHTCDetail.BCGui = dr["BCGui"].ToString();
                        oTHTCDetail.BCNhan = dr["BCNhan"].ToString();
                        oTHTCDetail.Ngay = dr["Ngay"].ToString();
                        oTHTCDetail.SL = dr["SL"].ToString();
                        oTHTCDetail.KL = dr["KL"].ToString();
                        listTHTCDetail.Add(oTHTCDetail);

                    }
                    _returnPostMan.Code = "00";
                    _returnPostMan.Message = "Lấy dữ liệu thành công.";
                    _returnPostMan.ListKPI_BD10_Hub_SL = listTHTCDetail;
                }
                else
                {
                    _returnPostMan.Code = "01";
                    _returnPostMan.Message = "Không có dữ liệu";

                }
            }
            catch (Exception ex)
            {

                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListKPI_BD10_Hub_SL = null;
            }
            return _returnPostMan;
        }

        public ReturnKPI_BD10_Hub_SL KPI_BD10_Hub_SLEX(int startdate, int enddate, string bc37)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPI_BD10_Hub_SL _returnPostMan = new ReturnKPI_BD10_Hub_SL();


            List<KPI_BD10_Hub_SLEX> listTHTCDetail = null;
            KPI_BD10_Hub_SLEX oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                SqlConnection sqlConnection = new SqlConnection();

                sqlConnection = new SqlConnection("Server=192.168.68.165; database=DBHANH; uid=hanhbd; pwd=hanhbd123");

                // Gọi vào DB để lấy dữ liệu.
                SqlCommand myCommand = new SqlCommand("[List_SL_DT_BD13_EX]", sqlConnection);
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                //xử lý tham số truyền vào data table
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@Fromdate", SqlDbType.BigInt).Value = startdate;
                myCommand.Parameters.Add("@Todate", SqlDbType.BigInt).Value = enddate;
                myCommand.Parameters.Add("@bc37Code", SqlDbType.VarChar).Value = bc37;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    listTHTCDetail = new List<KPI_BD10_Hub_SLEX>();
                    while (dr.Read())
                    {
                        oTHTCDetail = new KPI_BD10_Hub_SLEX();
                        oTHTCDetail.STT = a++;
                        oTHTCDetail.Duong_Thu = dr["Duong_Thu"].ToString();
                        oTHTCDetail.Ten_Duong_Thu = dr["Ten_Duong_Thu"].ToString();
                        oTHTCDetail.BD10 = dr["BD10"].ToString();
                        oTHTCDetail.BCGui = dr["BCGui"].ToString();
                        oTHTCDetail.BCNhan = dr["BCNhan"].ToString();
                        oTHTCDetail.Ngay = dr["Ngay"].ToString();
                        oTHTCDetail.SL = dr["SL"].ToString();
                        oTHTCDetail.KL = dr["KL"].ToString();
                        listTHTCDetail.Add(oTHTCDetail);

                    }
                    _returnPostMan.Code = "00";
                    _returnPostMan.Message = "Lấy dữ liệu thành công.";
                    _returnPostMan.ListKPI_BD10_Hub_SLEX = listTHTCDetail;
                }
                else
                {
                    _returnPostMan.Code = "01";
                    _returnPostMan.Message = "Không có dữ liệu";

                }
            }
            catch (Exception ex)
            {

                _returnPostMan.Code = "99";
                _returnPostMan.Message = "Lỗi xử lý dữ liệu";
                _returnPostMan.ListKPI_BD10_Hub_SLEX = null;
            }
            return _returnPostMan;
        }
    }
}