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
using System.Security.Cryptography;
using System.Text;

namespace T41.Areas.Admin.Controllers
{

    public class GetServiceInternalComuniticationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ServiceInternalComunication()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLinkService(int Id, string Password)
        {
            ReturnLinkInternalCommunityService _response = new ReturnLinkInternalCommunityService();
            var decodePassword = ComputeSha256Hash(Password);
            if (decodePassword == GetPassword())
            {
                 
                _response.Code = "00";
                _response.Message = "Success";
                _response.Service = GetLink().SingleOrDefault(c => c.Id == Id);
            }
            else
            {
                _response.Code = "01";
                _response.Message = "Incorrect password! Please check again.";
                _response.Service = null;
            }
            return Json(_response, JsonRequestBehavior.AllowGet);

        }

        private IEnumerable<ServiceInternalCommunity> GetLink()
        {
            return new List<ServiceInternalCommunity>
            {
                new ServiceInternalCommunity { Id = 1, Service= "Service lấy dữ liêu xác nhận đến về BCP" ,Link="https://www.24h.com.vn/"},
                new ServiceInternalCommunity { Id = 2, Service = "Service truyền BD13 lên Center", Link="https://bongdaso.com.vn/" },
                new ServiceInternalCommunity { Id = 3, Service = "Service truyền báo phát lên Center", Link="https://google.com.vn/" },
            };
        }

        private string GetPassword()
        {
            return "f87828047aea3c0d25ac67583004b37356ac9c154f56542bf080a441f79f2120";
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}