﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_ROADWAY
    {
        
        public string date { get; set; }
       
        public int way { get; set; }
        public string mailroutecode { get; set; }
        public int tungay { get; set; }
        public int denngay { get; set; }
        public int vung { get; set; }
        public string cap { get; set; }
        public int loaipt { get; set; }
    }
    //Phần lấy ra mã tuyến phát dưới DB
    public class MAILROUTE
    {
        public string MAILROUTECODE { get; set; }
        public string MAILROUTENAME { get; set; }
    }

    //Dữ liệu lấy ra của Báo cáo giao nhận vận chuyển theo đường thư
    public class RoadwayTransportDetail
    {
        
        public String NGAY { get; set; }
        public String BD10 { get; set; }
        public String SLTUI { get; set; }
        public String KL { get; set; }
        public String BCDONG { get; set; }

        
        public String TENBCDONG { get; set; }
        public String BCNHAN { get; set; }
        public String TENBCNHAN { get; set; }
        public String IDHANHTRINH { get; set; }
        public String HANHTRINH { get; set; }
        public String CAP { get; set; }
        public String LOAIPT { get; set; }

        
        
    }

    
    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnRoadwayTransport
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        
        public RoadwayTransportDetail RoadwayTransportReport { get; set; }
        public List<RoadwayTransportDetail> ListRoadwayTransportReport;

        
        public MetaData MetaData { get; set; }


    }

    
}