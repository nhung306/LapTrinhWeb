using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteFlower.Models;

namespace WebsiteFlower
{
    public class Giohang
    {
        dbQLBanHoaDataContext data = new dbQLBanHoaDataContext();
        public int iMASP { get; set; }
        public string sTENSP { get; set; }
        public string sANH { get; set; }
        public Double dGIABAN { get; set; }
        public int iSoLuong { get; set; }
        public Double dThanhTien
        {
            get { return iSoLuong * dGIABAN; }
        }
        public Giohang(int MASP)
        {
            iMASP = MASP;
            SANPHAM hoa = data.SANPHAMs.Single(n => n.MASP == iMASP);
            sTENSP = hoa.TENSP;
            sANH = hoa.ANH;
            dGIABAN = double.Parse(hoa.GIABAN.ToString());
            iSoLuong = 1;

        }
    }
}