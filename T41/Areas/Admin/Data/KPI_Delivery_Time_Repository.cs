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
    public class KPI_Delivery_Time_Repository
    {
        public ReturnKPI_Delivery_Time KPI_Delivery_Time(int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPI_Delivery_Time _returnPostMan = new ReturnKPI_Delivery_Time();


            List<KPI_Delivery_Time> listTHTCDetail = null;
            KPI_Delivery_Time oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                SqlConnection sqlConnection = new SqlConnection();

                sqlConnection = new SqlConnection("Server=192.168.68.165; database=DBHANH; uid=hanhbd; pwd=hanhbd123");

                // Gọi vào DB để lấy dữ liệu.
                SqlCommand myCommand = new SqlCommand("List_Du_Kien_TGPhat", sqlConnection);
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                //xử lý tham số truyền vào data table
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@Fromdate", SqlDbType.BigInt).Value = startdate;
                myCommand.Parameters.Add("@Todate", SqlDbType.BigInt).Value = enddate;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    listTHTCDetail = new List<KPI_Delivery_Time>();
                    while (dr.Read())
                    {
                        oTHTCDetail = new KPI_Delivery_Time();
                        oTHTCDetail.STT = a++;
                        oTHTCDetail.Ma_Tinh = dr["Ma_Tinh"].ToString();
                        oTHTCDetail.Ten_Tinh = dr["Ten_Tinh"].ToString();
                        oTHTCDetail.XND = dr["XND"].ToString();
                        oTHTCDetail.XNP = dr["XNP"].ToString();
                        oTHTCDetail.PTC = dr["PTC"].ToString();
                        oTHTCDetail.Ngay = dr["Ngay"].ToString();
                        listTHTCDetail.Add(oTHTCDetail);

                    }
                    _returnPostMan.Code = "00";
                    _returnPostMan.Message = "Lấy dữ liệu thành công.";
                    _returnPostMan.ListKPI_Delivery_Time = listTHTCDetail;
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
                _returnPostMan.ListKPI_Delivery_Time = null;
            }
            return _returnPostMan;
        }

        public ReturnKPI_Delivery_Time KPI_Delivery_Time_EX(int startdate, int enddate)
        {
            DataTable da = new DataTable();
            MetaData1 _metadata1 = new MetaData1();
            Convertion common = new Convertion();
            ReturnKPI_Delivery_Time _returnPostMan = new ReturnKPI_Delivery_Time();


            List<KPI_Delivery_Time_EX> listTHTCDetail = null;
            KPI_Delivery_Time_EX oTHTCDetail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                SqlConnection sqlConnection = new SqlConnection();

                sqlConnection = new SqlConnection("Server=192.168.68.165; database=DBHANH; uid=hanhbd; pwd=hanhbd123");

                // Gọi vào DB để lấy dữ liệu.
                SqlCommand myCommand = new SqlCommand("[List_Du_Kien_TGPhat]", sqlConnection);
                if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
                //xử lý tham số truyền vào data table
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@Fromdate", SqlDbType.BigInt).Value = startdate;
                myCommand.Parameters.Add("@Todate", SqlDbType.BigInt).Value = enddate;
                SqlDataReader dr = myCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    listTHTCDetail = new List<KPI_Delivery_Time_EX>();
                    while (dr.Read())
                    {
                        oTHTCDetail = new KPI_Delivery_Time_EX();
                        oTHTCDetail.STT = a++;
                        oTHTCDetail.Ma_Tinh = dr["Ma_Tinh"].ToString();
                        oTHTCDetail.Ten_Tinh = dr["Ten_Tinh"].ToString();
                        oTHTCDetail.XND = dr["XND"].ToString();
                        oTHTCDetail.XNP = dr["XNP"].ToString();
                        oTHTCDetail.PTC = dr["PTC"].ToString();
                        oTHTCDetail.Ngay = dr["Ngay"].ToString();
                        listTHTCDetail.Add(oTHTCDetail);

                    }
                    _returnPostMan.Code = "00";
                    _returnPostMan.Message = "Lấy dữ liệu thành công.";
                    _returnPostMan.ListKPI_Delivery_Time_EX = listTHTCDetail;
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
                _returnPostMan.ListKPI_Delivery_Time_EX = null;
            }
            return _returnPostMan;
        }
    }
}