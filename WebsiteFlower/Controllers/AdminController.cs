
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteFlower.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;


namespace WebsiteFlower.Controllers
{

    public class AdminController : Controller
    {
        dbQLBanHoaDataContext data = new dbQLBanHoaDataContext();
        // GET: Admin

        public ActionResult Trangchu()
        {
            if (Session["TaikhoanAdmin"] == null || Session["TaikhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        public ActionResult Hoa(int? page)
        {
            if (Session["TaikhoanAdmin"] == null || Session["TaikhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MASP).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["pass"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhâp tài khoản";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhâp mật khẩu";
            }
            else
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.USERADMIN == tendn && n.PASSADMIN == matkhau);
                if (ad != null)
                {
                    Session["TaikhoanAdmin"] = ad;
                    return RedirectToAction("TrangChu", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult ThemMoiHoa()
        {
            ViewBag.MAHOA = new SelectList(data.HOATUOIs.ToList().OrderBy(n => n.MAHOA), "MAHOA", "TENHOA");
            ViewBag.MAKIEU = new SelectList(data.KIEUHOAs.ToList().OrderBy(n => n.KIEU), "MAKIEU", "KIEU");
            ViewBag.MANCC = new SelectList(data.NCCs.ToList().OrderBy(n => n.MANCC), "MANCC", "TENNCC");
            ViewBag.MALOAI = new SelectList(data.LOAIHOAs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiHoa(SANPHAM hoa, HttpPostedFileBase fileupload)
        {
            ViewBag.MAHOA = new SelectList(data.HOATUOIs.ToList().OrderBy(n => n.MAHOA), "MAHOA", "TENHOA");
            ViewBag.MAKIEU = new SelectList(data.KIEUHOAs.ToList().OrderBy(n => n.KIEU), "MAKIEU", "KIEU");
            ViewBag.MANCC = new SelectList(data.NCCs.ToList().OrderBy(n => n.MANCC), "MANCC", "TENNCC");
            ViewBag.MALOAI = new SelectList(data.LOAIHOAs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    hoa.ANH = fileName;
                    data.SANPHAMs.InsertOnSubmit(hoa);
                    data.SubmitChanges();
                }
                return RedirectToAction("Hoa");
            }
        }
        public ActionResult ChiTiet(int id)
        {
            SANPHAM hoa = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            ViewBag.MASP = hoa.MASP;
            if (hoa == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hoa);
        }
        public ActionResult Xoa(int id)
        {
            SANPHAM hoa = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            ViewBag.MASP = hoa.MASP;
            if (hoa == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hoa);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int id)
        {
            SANPHAM hoa = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            ViewBag.MASP = hoa.MASP;
            if (hoa == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SANPHAMs.DeleteOnSubmit(hoa);
            data.SubmitChanges();
            return RedirectToAction("Hoa");
        }
        public ActionResult Sua(int id)
        {
            SANPHAM hoa = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (hoa == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MAHOA = new SelectList(data.HOATUOIs.ToList().OrderBy(n => n.MAHOA), "MAHOA", "TENHOA", hoa.MAHOA);
            ViewBag.MAKIEU = new SelectList(data.KIEUHOAs.ToList().OrderBy(n => n.KIEU), "MAKIEU", "KIEU", hoa.MAKIEU);
            ViewBag.MANCC = new SelectList(data.NCCs.ToList().OrderBy(n => n.MANCC), "MANCC", "TENNCC", hoa.MANCC);
            ViewBag.MALOAI = new SelectList(data.LOAIHOAs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI", hoa.MALOAI);
            return View(hoa);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(SANPHAM hoa, HttpPostedFileBase fileupload)
        {
            ViewBag.MAHOA = new SelectList(data.HOATUOIs.ToList().OrderBy(n => n.MAHOA), "MAHOA", "TENHOA");
            ViewBag.MAKIEU = new SelectList(data.KIEUHOAs.ToList().OrderBy(n => n.KIEU), "MAKIEU", "KIEU");
            ViewBag.MANCC = new SelectList(data.NCCs.ToList().OrderBy(n => n.MANCC), "MANCC", "TENNCC");
            ViewBag.MALOAI = new SelectList(data.LOAIHOAs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");

            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    hoa.ANH = fileName;
                    UpdateModel(hoa);
                    data.SubmitChanges();
                }
                return RedirectToAction("Hoa");
            }
           
        }
        //quản lý nhà sản xuất
        public ActionResult NhaCC(int? page)
        {
            if (Session["TaikhoanAdmin"] == null || Session["TaikhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.NCCs.ToList().OrderBy(n => n.MANCC).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemMoiNCC()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiNCC(NCC ncc)
        {
            if (ModelState.IsValid)
            {

                data.NCCs.InsertOnSubmit(ncc);
                data.SubmitChanges();
            }
            return RedirectToAction("NhaCC");
        }
        public ActionResult DetailsNCC(int id)
        {
            NCC ncc = data.NCCs.SingleOrDefault(n => n.MANCC == id);
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        public ActionResult DeleteNCC(int id)
        {
            NCC ncc = data.NCCs.SingleOrDefault(n => n.MANCC == id);
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        [HttpPost, ActionName("DeleteNCC")]
        public ActionResult Delete(int id)
        {
            NCC ncc = data.NCCs.SingleOrDefault(n => n.MANCC == id);
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.NCCs.DeleteOnSubmit(ncc);
            data.SubmitChanges();
            return RedirectToAction("NhaCC");
        }
        public ActionResult EditNCC(int id)
        {
            NCC ncc = data.NCCs.SingleOrDefault(n => n.MANCC == id);
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNCC(NCC ncc)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(ncc);
                data.SubmitChanges();
            }
            return RedirectToAction("NhaCC");
        }

        //
        //quản lý danh sách khách hành
        public ActionResult KhachHang(int? page)
        {
            if (Session["TaikhoanAdmin"] == null || Session["TaikhoanAdmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.KHACHHANGs.ToList().OrderBy(n => n.MAKH).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemMoiKH()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiKH(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {

                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
            }
            return RedirectToAction("KhachHang");
        }
        public ActionResult DetailsKH(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = kh.MAKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        public ActionResult DeleteKH(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = kh.MAKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("DeleteKH")]
        public ActionResult XNDeleteKH(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = kh.MAKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.KHACHHANGs.DeleteOnSubmit(kh);
            data.SubmitChanges();
            return RedirectToAction("KhachHang");
        }
        public ActionResult EditKH(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = kh.MAKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditKH(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(kh);
                data.SubmitChanges();
            }
            return RedirectToAction("KhachHang");
        }
    }
}