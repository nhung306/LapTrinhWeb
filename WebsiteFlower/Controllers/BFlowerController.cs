using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteFlower.Models;
using PagedList;
using PagedList.Mvc;


namespace WebsiteFlower.Controllers
{
    public class BFlowerController : Controller
    {
        dbQLBanHoaDataContext data = new dbQLBanHoaDataContext();
        public ActionResult TrangChu(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var hoa = LayHoaMoi(10);
            return View(hoa.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult KieuHoa()
        {
            var kieuhoa = from kh in data.KIEUHOAs select kh;
            return PartialView(kieuhoa);
        }
        public ActionResult LoaiHoa()
        {
            var loaihoa = from lh in data.LOAIHOAs select lh;
            return PartialView(loaihoa);
        }
        public ActionResult HoaTuoi()
        {
            var hoa = from h in data.HOATUOIs select h;
            return PartialView(hoa);
        }
        private List<SANPHAM> LayHoaMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NGAYNHAP).Take(count).ToList();
        }

        public ActionResult HoaTheoKieu(int id)
        {
            var hoa = from h in data.SANPHAMs where h.MAKIEU == id select h;
            return View(hoa);
        }
        public ActionResult HoaTheoLoai(int id)
        {
            var hoa = from h in data.SANPHAMs where h.MALOAI == id select h;
            return View(hoa);
        }
        public ActionResult Hoa(int id)
        {
            var hoa = from h in data.SANPHAMs where h.MAHOA == id select h;
            return View(hoa);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult YnghiaHoa()
        {
            var hoa = from h in data.MEANs select h;
            return View(hoa);
        }
        public ActionResult Details(int id)
        {
            var hoa = from s in data.SANPHAMs where s.MASP == id select s;
            return View(hoa.Single());
        }
    }
}