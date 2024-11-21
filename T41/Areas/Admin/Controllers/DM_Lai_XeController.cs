using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class DM_Lai_XeController : Controller
    {

        Convertion common = new Convertion();

        public ActionResult DM_Lai_Xe()
        {
            return View();
        }
        #region Danh mục
        [HttpPost]
        public JsonResult ListDM_Lai_Xe( int zone, int endpostcode,string trangthai)
        {
            ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.trangthai = trangthai;

            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnDM_Lai_Xe returnquality = new ReturnDM_Lai_Xe();
            returnquality = lai_XeRepository.DM_Lai_Xe_DETAIL( zone, endpostcode, trangthai);

            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Khoa(int id)
        {
            // Gọi phương thức Khoa trong repository để thực hiện thao tác khóa
            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnResponseUpdate result = lai_XeRepository.Khoa(id);

            // Trả về kết quả dưới dạng JSON
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetLaiXeInfo(int id)
        {
            DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();
            ReturnDM_Lai_Xe returnquality = lai_XeRepository.CapNhat_Id_Sua(id); // Lấy thông tin lái xe từ cơ sở dữ liệu
            var jsonResult = Json(returnquality, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;  // Đảm bảo có thể truyền lượng dữ liệu lớn
            return jsonResult;
        }
        [HttpPost]
        public JsonResult AddLaiXe(DM_Lai_Xe_Sua model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }

                // Tạo repository để thao tác dữ liệu
                DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Add(model);

                if (result)
                {
                    return Json(new { success = true, message = "Thêm mới lái xe thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm mới lái xe thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateLaiXe(DM_Lai_Xe_Sua model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });
                }

                // Tạo repository để thao tác dữ liệu
                DM_Lai_XeRepository lai_XeRepository = new DM_Lai_XeRepository();

                // Gọi phương thức thêm mới
                var result = lai_XeRepository.Edit(model);

                if (result)
                {
                    return Json(new { success = true, message = "Sửa lái xe thành công.", data = model });
                }
                else
                {
                    return Json(new { success = false, message = "Sửa lái xe thất bại." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết và trả về lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        #endregion
    }
}