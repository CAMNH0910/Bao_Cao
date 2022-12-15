using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Models.DataModel
{

    public class ServiceInternalCommunity
    {
        public int Id { get; set; }
        public string Service { get; set; }
        public string Link { get; set; }
    }
   
   
    public class ReturnLinkInternalCommunityService
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ServiceInternalCommunity Service { get; set; }

    }
    


}