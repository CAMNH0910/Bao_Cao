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
    public class KPI_BD10_Hub_TimeRepository
    {
        public ReturnKPI_BD10_Hub_Time KPI_BD10_Hub_Time(int startdate, int enddate, string mailrouteScheduleName)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPI_BD10_Hub_Time _returnPostMan = new ReturnKPI_BD10_Hub_Time();


            List<KPI_BD10_Hub_Time> listTHTCDetail = null;
            KPI_BD10_Hub_Time oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                SqlConnection sqlConnection = new SqlConnection();

                sqlConnection = new SqlConnection("Server=192.168.68.165; database=DBHANH; uid=hanhbd; pwd=hanhbd123");

                // Gọi vào DB để lấy dữ liệu.
                SqlCommand myCommand = new SqlCommand("List_SL_DT_BD13_New", sqlConnection);
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                //xử lý tham số truyền vào data table
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@Fromdate", SqlDbType.Int).Value = startdate;
                myCommand.Parameters.Add("@Todate", SqlDbType.Int).Value = enddate;
                myCommand.Parameters.Add("@MailrouteScheduleName", SqlDbType.NVarChar).Value = mailrouteScheduleName;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    listTHTCDetail = new List<KPI_BD10_Hub_Time>();
                    while (dr.Read())
                    {
                        oTHTCDetail = new KPI_BD10_Hub_Time();
                        oTHTCDetail.STT = a++;
                        oTHTCDetail.Duong_Thu = dr["Duong_Thu"].ToString();
                        oTHTCDetail.Ma_DT = dr["Ma_DT"].ToString();
                        oTHTCDetail.BD10 = dr["BD10"].ToString();
                        oTHTCDetail.Ma_BG = dr["Ma_BG"].ToString();
                        oTHTCDetail.BC_Den = dr["BC_Den"].ToString();
                        oTHTCDetail.Ngay_Gio_Den = dr["Ngay_Gio_Den"].ToString();
                        listTHTCDetail.Add(oTHTCDetail);

                    }
                    _returnPostMan.Code = "00";
                    _returnPostMan.Message = "Lấy dữ liệu thành công.";
                    _returnPostMan.ListKPI_BD10_Hub_Time = listTHTCDetail;
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
                _returnPostMan.ListKPI_BD10_Hub_Time = null;
            }
            return _returnPostMan;
        }

        public ReturnKPI_BD10_Hub_Time KPI_BD10_Hub_TimeEX(int startdate, int enddate, string mailrouteScheduleName)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPI_BD10_Hub_Time _returnPostMan = new ReturnKPI_BD10_Hub_Time();


            List<KPI_BD10_Hub_TimeEX> listTHTCDetail = null;
            KPI_BD10_Hub_TimeEX oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                SqlConnection sqlConnection = new SqlConnection();

                sqlConnection = new SqlConnection("Server=192.168.68.165; database=DBHANH; uid=hanhbd; pwd=hanhbd123");

                // Gọi vào DB để lấy dữ liệu.
                SqlCommand myCommand = new SqlCommand("[List_SL_DT_BD13_New_EX]", sqlConnection);
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                //xử lý tham số truyền vào data table
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@Fromdate", SqlDbType.Int).Value = startdate;
                myCommand.Parameters.Add("@Todate", SqlDbType.Int).Value = enddate;
                myCommand.Parameters.Add("@MailrouteScheduleName", SqlDbType.NVarChar).Value = mailrouteScheduleName;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    listTHTCDetail = new List<KPI_BD10_Hub_TimeEX>();
                    while (dr.Read())
                    {
                        oTHTCDetail = new KPI_BD10_Hub_TimeEX();
                        oTHTCDetail.STT = a++;
                        oTHTCDetail.Duong_Thu = dr["Duong_Thu"].ToString();
                        oTHTCDetail.Ma_DT = dr["Ma_DT"].ToString();
                        oTHTCDetail.BD10 = dr["BD10"].ToString();
                        oTHTCDetail.Ma_BG = dr["Ma_BG"].ToString();
                        oTHTCDetail.BC_Den = dr["BC_Den"].ToString();
                        oTHTCDetail.Ngay_Gio_Den = dr["Ngay_Gio_Den"].ToString();
                        listTHTCDetail.Add(oTHTCDetail);

                    }
                    _returnPostMan.Code = "00";
                    _returnPostMan.Message = "Lấy dữ liệu thành công.";
                    _returnPostMan.ListKPI_BD10_Hub_TimeEX = listTHTCDetail;
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
                _returnPostMan.ListKPI_BD10_Hub_TimeEX = null;
            }
            return _returnPostMan;
        }
    }
}