using QLAdmin.Areas.Admin.Data;
using QLAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Controllers
{
    public class QLSizeController : Controller
    {
        // GET: Admin/QLSize
        private QLTuisach _context = new QLTuisach();
        public ActionResult Index()
        {

            var lstkichthuoc = (from s in _context.ProductSizes
                               orderby s.SizeID ascending //descending
                               select new KichthuocVM()
                               {
                                   SizeID = s.SizeID,
                                   Size = s.Size,
                               }).ToList();
            ViewBag.message = lstkichthuoc.Count();
            return View(lstkichthuoc);
        }
        public ActionResult DetaillSize(int id)
        {
            var item = _context.ProductSizes
           .Where(x => x.SizeID == id)
           .Select(t => new KichthuocVM
           {
               SizeID = t.SizeID,
               Size = t.Size,
           })
           .FirstOrDefault();

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }
        [HttpGet]
        public ActionResult AddSize()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddSize(KichthuocVM formData)
        {
            var item = new ProductSize();
            item.Size = formData.Size;

            _context.ProductSizes.Add(item);

            _context.SaveChanges();// save to DB
            return RedirectToAction("Index", "QLSize");
        }

        [HttpGet]
        public ActionResult EditSize(int id)
        {
            //var cd = _context.ChuDe.Where(x => x.MaChuDe == id).Select(item=>new ChuDeVM()
            //{
            //    MaCD = item.MaChuDe,
            //    TenCD = item.TenChuDe,
            //}).FirstOrDefault();

            var kt = (from item in _context.ProductSizes
                      where item.SizeID == id
                      select new KichthuocVM()
                      {
                          SizeID = item.SizeID,
                          Size = item.Size
                      }).FirstOrDefault();
            if (kt == null)
            {
                return RedirectToAction("Index", "QLSize");
            }
            return View(kt);
        }
        [HttpPost]
        public ActionResult EditSize(KichthuocVM formData)
        {
            var item = _context.ProductSizes.Where(x => x.SizeID == formData.SizeID).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLSize");
            }

            item.Size = formData.Size;

            _context.SaveChanges();

            return RedirectToAction("Index", "QLSize");
        }
        [HttpGet]
        public ActionResult DeleteSize(int id)
        {
            var item = _context.ProductSizes.Where(x => x.SizeID == id).Select(x => new KichthuocVM()
            {
                SizeID = x.SizeID,
                Size = x.Size
            }).FirstOrDefault();
            return View(item);
        }
        [HttpPost, ActionName("DeleteSize")]
        public ActionResult ConfirmDeleteSize(int id)
        {
            var item = _context.ProductSizes.Where(x => x.SizeID == id).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index", "QLSize");
            }
            _context.ProductSizes.Remove(item);

            _context.SaveChanges();

            return RedirectToAction("Index", "QLSize");
        }
    }
}