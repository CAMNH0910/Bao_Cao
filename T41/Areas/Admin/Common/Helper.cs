using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Common
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class Helper
    {
        private static string _me24ConnectionString = string.Empty;
        private static OracleConnection _me24OracleConnection = null;

        //Conection Center CPCPN
        private static string _OraDCConnectionString = string.Empty;
        private static OracleConnection _OraDCOracleConnection = null;

        //Conection Doi Soat 
        private static string _OraDSConnectionString = string.Empty;
        private static OracleConnection _OraDSOracleConnection = null;

        //Conection Center Dev CpcpnDev
        private static string _OraDCDevConnectionString = string.Empty;
        private static OracleConnection _OraDCDevOracleConnection = null;

        //Conection MayCom
        private static string _OraDCComConnectionString = string.Empty;
        private static OracleConnection _OraDCComOracleConnection = null;

        //Conection EVCOM PRODUCTION
        private static string _OraEVComConnectionString = string.Empty;
        private static OracleConnection _OraEVComOracleConnection = null;

        //Conection EVCOM Dev
        private static string _OraEVComDevConnectionString = string.Empty;
        private static OracleConnection _OraEVComDevOracleConnection = null;


        //Conection PNS
        private static string _OraPNSConnectionString = string.Empty;
        private static OracleConnection _OraPNSOracleConnection = null;

        private static string _schemaName = string.Empty;

        /// <summary>
        /// SchemaName
        /// </summary>
        public static string SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                    _schemaName = ConfigurationManager.AppSettings["SCHEMA_NAME"];
                return _schemaName;
            }
            set { _schemaName = value; }
        }

        /// <summary>
        /// ME24ConnectionString
        /// </summary>
        public static string ME24ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_me24ConnectionString))
                    _me24ConnectionString = ConfigurationManager.ConnectionStrings["ME24_CONNECTION_STRING"].ConnectionString;
                return _me24ConnectionString;
            }
            set { _me24ConnectionString = value; }
        }

        /// <summary>
        /// ME24OracleConnection
        /// </summary>
        public static OracleConnection ME24OracleConnection
        {
            get
            {
                if (_me24OracleConnection == null)
                    _me24OracleConnection = new OracleConnection(ME24ConnectionString);
                if (_me24OracleConnection.State == System.Data.ConnectionState.Closed)
                    _me24OracleConnection.Open();
                return _me24OracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }
        //Phần gọi vào Database Cpcpn
        public static string OraDCConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraDCConnectionString))
                    _OraDCConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_DC"].ConnectionString;
                return _OraDCConnectionString;
            }
            set { _OraDCConnectionString = value; }
        }


        public static OracleConnection OraDCOracleConnection
        {
            get
            {
                if (_OraDCOracleConnection == null)
                    _OraDCOracleConnection = new OracleConnection(OraDCConnectionString);
                if (_OraDCOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraDCOracleConnection.Open();
                return _OraDCOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }

        //Phần gọi vào Database Cpcpn Dev 
        public static string OraDCDevConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraDCDevConnectionString))
                    _OraDCDevConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_DCDev"].ConnectionString;
                return _OraDCDevConnectionString;
            }
            set { _OraDCDevConnectionString = value; }
        }


        public static OracleConnection OraDCDevOracleConnection
        {
            get
            {
                if (_OraDCDevOracleConnection == null)
                    _OraDCDevOracleConnection = new OracleConnection(OraDCDevConnectionString);
                if (_OraDCDevOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraDCDevOracleConnection.Open();
                return _OraDCDevOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }

        //Phần gọi vào Database Đối Soát
        public static string OraDSConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraDSConnectionString))
                    _OraDSConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_DS"].ConnectionString;
                return _OraDSConnectionString;
            }
            set { _me24ConnectionString = value; }
        }


        public static OracleConnection OraDSOracleConnection
        {
            get
            {
                if (_OraDSOracleConnection == null)
                    _OraDSOracleConnection = new OracleConnection(OraDSConnectionString);
                if (_OraDSOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraDSOracleConnection.Open();
                return _OraDSOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }

        //Phần gọi vào Database Máy Com
        public static string OraDCComConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraDCComConnectionString))
                    _OraDCComConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_COM"].ConnectionString;
                return _OraDCComConnectionString;
            }
            set { _me24ConnectionString = value; }
        }


        public static OracleConnection OraDCComOracleConnection
        {
            get
            {
                if (_OraDCComOracleConnection == null)
                    _OraDCComOracleConnection = new OracleConnection(OraDCComConnectionString);
                if (_OraDCComOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraDCComOracleConnection.Open();
                return _OraDCComOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }

        //Phần gọi vào Database EVCOM Production
        public static string OraEVComConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraEVComConnectionString))
                    _OraEVComConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_EV_COM"].ConnectionString;
                return _OraEVComConnectionString;
            }
            set { _me24ConnectionString = value; }
        }


        public static OracleConnection OraEVComOracleConnection
        {
            get
            {
                if (_OraEVComOracleConnection == null)
                    _OraEVComOracleConnection = new OracleConnection(OraEVComConnectionString);
                if (_OraEVComOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraEVComOracleConnection.Open();
                return _OraEVComOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }

        //Phần gọi vào Database EVCOM Dev
        public static string OraEVComDevConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraEVComDevConnectionString))
                    _OraEVComDevConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_EV_COM_DEV"].ConnectionString;
                return _OraEVComDevConnectionString;
            }
            set { _me24ConnectionString = value; }
        }


        public static OracleConnection OraEVComDevOracleConnection
        {
            get
            {
                if (_OraEVComDevOracleConnection == null)
                    _OraEVComDevOracleConnection = new OracleConnection(OraEVComDevConnectionString);
                if (_OraEVComDevOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraEVComDevOracleConnection.Open();
                return _OraEVComDevOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }


        //Phần gọi vào Database PNS
        public static string OraPNSConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraPNSConnectionString))
                    _OraPNSConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_PNS"].ConnectionString;
                return _OraPNSConnectionString;
            }
            set { _me24ConnectionString = value; }
        }


        public static OracleConnection OraPNSOracleConnection
        {
            get
            {
                if (_OraPNSOracleConnection == null)
                    _OraPNSOracleConnection = new OracleConnection(OraPNSConnectionString);
                if (_OraPNSOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraPNSOracleConnection.Open();
                return _OraPNSOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(OracleCommand dbCommand)
        {
            int iResult = -1;
            try
            {
                OracleConnection connection = ME24OracleConnection;
                dbCommand.Connection = connection;

                iResult = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ExecuteNonQuery: " + ex.Message);
            }
            return iResult;
        }

        /// <summary>
        /// ExecuteDataReader
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteDataReader(OracleCommand dbCommand, OracleConnection oraConnection)
        {
            try
            {
                OracleConnection connection = oraConnection;

                dbCommand.Connection = connection;
                return dbCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ExecuteDataReader: " + exception.Message);
            }
            return null;
        }
    }
}