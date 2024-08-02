using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLDanhmucController : Controller
    {
        // GET: Admin/QLDanhmuc
        private QLTuisach _context = new QLTuisach();
        public ActionResult Index(string search, int page = 1)
        {
            int pageSize = 10;
            var lstphanloai = (from s in _context.PhanLoais
                                orderby s.PhanLoaiID ascending//descending
                                select new DanhmucVM()
                                {
                                    PhanLoaiID = s.PhanLoaiID,
                                    TenPhanLoai = s.TenPhanLoai,
                                }).ToList();
            ViewBag.message = lstphanloai.Count();
    
            return View(lstphanloai);

        }
        public ActionResult DetailDanhmuc(int id)
        {
            var item = _context.PhanLoais
            .Where(x => x.PhanLoaiID == id)
            .Select(t => new DanhmucVM
            {
                PhanLoaiID = t.PhanLoaiID,
                TenPhanLoai = t.TenPhanLoai,
            })
            .FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpGet]
        public ActionResult AddDanhmuc()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddDanhmuc(DanhmucVM formData)
        {
            var item = new PhanLoai();
            item.TenPhanLoai = formData.TenPhanLoai;

            _context.PhanLoais.Add(item);

            _context.SaveChanges();// save to DB
            return RedirectToAction("Index", "QLDanhmuc");
        }

        [HttpGet]
        public ActionResult EditDanhmuc(int id)
        {
            //var cd = _context.ChuDe.Where(x => x.MaChuDe == id).Select(item=>new ChuDeVM()
            //{
            //    MaCD = item.MaChuDe,
            //    TenCD = item.TenChuDe,
            //}).FirstOrDefault();

            var pl = (from item in _context.PhanLoais
                      where item.PhanLoaiID == id
                      select new DanhmucVM()
                      {
                          PhanLoaiID = item.PhanLoaiID,
                          TenPhanLoai = item.TenPhanLoai
                      }).FirstOrDefault();
            if (pl == null)
            {
                return RedirectToAction("Index", "QLDanhmuc");
            }
            return View(pl);
        }
        [HttpPost]
        public ActionResult EditDanhmuc(DanhmucVM formData)
        {
            var item = _context.PhanLoais.Where(x => x.PhanLoaiID == formData.PhanLoaiID).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLDanhmuc");
            }

            item.TenPhanLoai = formData.TenPhanLoai;

            _context.SaveChanges();

            return RedirectToAction("Index", "QLDanhmuc");
        }
        [HttpGet]
        public ActionResult DeleteDanhmuc(int id)
        {
            var item = _context.PhanLoais.Where(x => x.PhanLoaiID == id).Select(x => new DanhmucVM()
            {
                PhanLoaiID = x.PhanLoaiID,
                TenPhanLoai = x.TenPhanLoai
            }).FirstOrDefault();
            return View(item);
        }
        [HttpPost, ActionName("DeleteDanhmuc")]
        public ActionResult ConfirmDeleteDM(int id)
        {
            var item = _context.PhanLoais.Where(x => x.PhanLoaiID == id).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLDanhmuc");
            }
            _context.PhanLoais.Remove(item);

            _context.SaveChanges();

            return RedirectToAction("Index", "QLDanhmuc");
        }
    }
}