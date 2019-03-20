using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Common
{
    public class Convertion
    {
        public int DateToInt(string date)
        {
            // 01/01/2018
            try
            {
                string yyyy = date.Substring(6, 4);
                string mm = date.Substring(3, 2);
                string dd = date.Substring(0, 2);
                return Convert.ToInt32(yyyy + mm + dd);
            }
            catch
            {
                return 0;
            }

        }
        #region Convert_Date
        public string Convert_Date(int str_Date)
        {
            string str = "";
            str = string.Format("{0:dd/MM/yyyy}", str_Date.ToString());
            if ((str == "") || (str == "0"))
                return str;
            else
            {
                string ngay = str.Substring(6, 2);
                string thang = str.Substring(4, 2);
                string nam = str.Substring(0, 4);
                return ngay + "/" + thang + "/" + nam;
            }
        }
        #endregion  

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
    }
}