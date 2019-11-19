using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteFlower.Models;

namespace WebsiteFlower.Controllers
{
    public class NguoiDungController : Controller
    {
        dbQLBanHoaDataContext data = new dbQLBanHoaDataContext();
        // GET: NguoiDung
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            //Them khách hàng vào table            
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDn"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["MatKhauNhapLai"];
            var diachi = collection["DiaChiKH"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Họ tên khách hàng không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["loi4"] = " phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["loi5"] = "Email không được bỏ trống";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["loi6"] = "Địa chỉ không được bỏ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["loi7"] = "Địên thoại không được bỏ trống";
            }
            else
            {
                kh.HOTEN = hoten;
                kh.TAIKHOAN = tendn;
                kh.MATKHAU = matkhau;
                kh.DIACHIKH = diachi;
                kh.EMAIL = email;
                kh.DIENTHOAIKH = dienthoai;
                kh.NGAYSINH = DateTime.Parse(ngaysinh);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
            }
            ViewBag.ThongBao = "Chào mừng bạn đến với cửa hàng hoa BFlower";
            return RedirectToAction("DangNhap");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TenDn"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.TAIKHOAN == tendn && n.MATKHAU == matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("TrangChu", "BFlower");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("TrangChu", "BFlower");
        }
    }
}