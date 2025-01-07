using System;
using System.Collections.Generic;
using System.Globalization;
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

        public int ConvertToIntTime(string dateTime)
        {
            try
            {
                // Kiểm tra nếu chuỗi dateTime không phải null hoặc rỗng
                if (string.IsNullOrEmpty(dateTime))
                {
                    throw new ArgumentException("Chuỗi dateTime không được null hoặc rỗng.");
                }

                // Chuyển chuỗi thành DateTime theo định dạng "yyyy-MM-dd HH:mm"
                DateTime parsedDateTime = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                // Chuyển đổi DateTime thành định dạng "HHmm" rồi ép kiểu sang int
                string dateTimeString = parsedDateTime.ToString("HHmm");

                // Chuyển đổi chuỗi thành int
                int result = Int32.Parse(dateTimeString);
                return result;
            }
            catch (FormatException ex)
            {
                // Xử lý lỗi khi định dạng không đúng
                Console.WriteLine($"Lỗi khi chuyển đổi dateTime: {ex.Message}");
                return 0;  // Trả về giá trị mặc định nếu có lỗi
            }
            catch (Exception ex)
            {
                // Xử lý lỗi tổng quát
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                return 0;  // Trả về giá trị mặc định nếu có lỗi
            }
        }

        public int ConvertToIntDate(string dateTime)
        {
            try
            {
                // Kiểm tra nếu chuỗi dateTime không phải null hoặc rỗng
                if (string.IsNullOrEmpty(dateTime))
                {
                    throw new ArgumentException("Input dateTime string is null or empty.");
                }

                // Chuyển chuỗi thành DateTime theo định dạng "yyyy-MM-dd HH:mm"
                DateTime parsedDateTime = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                // Chuyển đổi DateTime thành định dạng "yyyyMMddHHmm" rồi ép kiểu sang int
                string dateTimeString = parsedDateTime.ToString("yyyyMMdd");

                // Kiểm tra nếu độ dài chuỗi quá dài để chuyển thành int (Int32 có thể chứa tối đa 10 chữ số)
                if (dateTimeString.Length > 10)
                {
                    throw new OverflowException("The date-time value is too large to be converted to an integer.");
                }

                // Chuyển đổi chuỗi thành int
                int result = Int32.Parse(dateTimeString);
                return result;
            }
            catch (Exception ex)
            {
                // In lỗi nếu có ngoại lệ
                Console.WriteLine($"Error converting dateTime: {ex.Message}");
                return 0;  // Trả về giá trị mặc định nếu có lỗi
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

        public int ConvertTimeToInt(string timeString)
        {
            DateTime time = DateTime.ParseExact(timeString, "hh:mm:ss tt", CultureInfo.InvariantCulture);
            string hhmm = time.ToString("HHmm");
            int result = int.Parse(hhmm);
            return result;
        }
        public int ConvertTimeToInt_NEW(string timeString)
        {
            DateTime time = DateTime.ParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture);
            string hhmm = time.ToString("HHmm");
            int result = int.Parse(hhmm);
            return result;
        }
    }
}