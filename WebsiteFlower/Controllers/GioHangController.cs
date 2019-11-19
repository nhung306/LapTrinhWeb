using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteFlower.Models;

namespace WebsiteFlower.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLBanHoaDataContext data = new dbQLBanHoaDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int iMASP, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sannpham = lstGiohang.Find(n => n.iMASP == iMASP);
            if (sannpham == null)
            {
                sannpham = new Giohang(iMASP);
                lstGiohang.Add(sannpham);
                return Redirect(strURL);
            }
            else
            {
                sannpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;

        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("TrangChu", "BFlower");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int iMASP)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMASP == iMASP);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMASP == iMASP);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("TrangChu", "BFlower");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGioHang(int iMASP, FormCollection f)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMASP == iMASP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("TrangChu", "BFlower");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                ViewBag.ThongBao = "Bạn đăng nhập để có thể mua hoa ở cửa hàng. Xin cảm ơn ";
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("TrangChu", "BFlower");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DONDAT ddh = new DONDAT();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];

            List<Giohang> gh = Laygiohang();
            ddh.NGAYDAT = DateTime.Now;
            var ngaygiao = String.Format("{0:MM//dd/yyyy}", collection["NgayGiao"]);
            ddh.TINHTRANGGIAOHANG = false;
            ddh.THANHTOAN = false;
            data.DONDATs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            // thêm chi tiết ddowwn hàng
            foreach (var item in gh)
            {
                CTDONDAT ctdh = new CTDONDAT();
                ctdh.MADONDAT = ddh.MADONDAT;
                ctdh.MASP = item.iMASP;
                ctdh.SL = item.iSoLuong;
                ctdh.DONGIA = (decimal)item.dGIABAN;
                data.CTDONDATs.InsertOnSubmit(ctdh);
                data.SubmitChanges();
            }
           
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}